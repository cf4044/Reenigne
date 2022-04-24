using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reenigne
{
#pragma warning disable IDE1006
#pragma warning disable IDE0017


	public enum pinTypes { surface,through };

	public class component
	{

		// items related to both the board and schematic representations
		public string       desigType;      // eg R for resistor, C for capacitor, U for integrated circuit 
		public int          desigNum;       // eg the '12' in 'R12'
		public List<pin>    pins;           // all details of each pin, both with regard to the schematic and the board representations
		public class pin
		{
			public int      pinIndex;                   // index of this pin within the aray of this instance. This is to allow the instance to 'know' its position within the array
			public string   pinNumber;                  // The pin number as per datasheet designation. FOr example simple numbers for dips or something like G26 for a BGA chip
			public string   pinName;                    // The abbreviated name of the pin, such ask CLKIN or SCL				 		  
			public string   pinFullName;                // Full name of the pin, such as 'I2C Serial Clock'  

			public PointF   templatePosition;           // Relative co-ordinate of this pin within the component in the board representation as per original template (i.e before applying scaling and rotation)
			public PointF   boardPosition;              // Relative co-ordinate of this pin within the component in the board representation after applying scaling and rotation

			public PointF   schemPosition;              // Relative co-ordinate of this pin within the component in the schematic representation. For pins that appear in multiple secions (such as clock) the position is always the same within each section
			public PointF   direction;					// The direction in which the wire leaves the pin. Derived from the orientation and offset fields in the symbol definition
			public int[]    section;                    // the section/s to which this pin belongs (eg which inverter in a hex inverter)

			public int      net;                        // the net to which this pin is connected
			public bool     isNetAnchor;	// ToDeprecate            // Does this pin anchor a net
			public string   netName;		// ToDeprecate            // If so, this is the name of the net
		}

		// Items related to the board representation
		public bool         hidden = false; // if set the component is not displayed during redraw
		public int          layer;          // the layer on which this component is placed
		public PointF       position;       // co-ordinates of component within the board
		float               padSize;        // the size of the pads. This is unaffected by the component scale. Only the display scale is applied to this parameter
		pinTypes            pinType;        // whether its pins are surface mount or through hole type
		mirrorModes              mirror;         // whether or not the component is flipped vertically or horizontally
		PointF              scale;          // horizontal and vertical scale of the component relative to the board	( overall scale and aspect ratio )
		float               rotation;       // in degrees

		public float		PadSize		{ get => padSize;	set { padSize = value;	updateTransforms(); } }
		public pinTypes		PinType		{ get => pinType;	set { pinType = value;						} }
		public mirrorModes	Mirror		{ get => mirror;	set { mirror = value;	updateTransforms(); } }
		public PointF		Scale		{ get => scale;		set { scale = value;	updateTransforms(); } }
		public float		Rotation	{ get => rotation;	set { rotation = value; updateTransforms(); } }

		// Items related to the schematic representation
		public kSymbol symbol;				// The graphical symbol to display
		public List<section> sections;      // the sections of the component in the schematic (sections are for example the individual inverters in a hex inverter
		public class section
		{
			public string    name;          // The name of the setcion, example:  A, B, COM or blank if component has only one section
			public PointF    position;      // Co-ordinates of this section of the component within the schematic representation
			public RectangleF boundBox;     // bounding box of the section - used for selection as well as for drawing a provisional outline
			public RectangleF absBoundBox { get { RectangleF absBoundBox = boundBox; absBoundBox.Offset( position ); return absBoundBox; } }

			// Constructor
			public section( string sectionName = "" )
			{
				name = sectionName;
				position = new PointF( 0, 0 );
			}

			// functions

		};

		// static items
		public static string    creationPinFormat       = "compTypeDIL";
		public static int       creationNumPins         = 2;
		public static string    creationOutlineFormat   = "outlineRectangular";
		public static PointF    creationScale           = new PointF( 29f, 29f );
		public static float     creationRotation        = 0f;
		public static float     creationPadSize         = 20;
		public static pinTypes  creationPinType         = pinTypes.surface;

		// constructor
		public component( PointF boardLocation )
		{   // for now just create an 8 pin dip, just to have something to show

			position = boardLocation;
			scale = creationScale;
			rotation = creationRotation;
			padSize = creationPadSize;
			pinType = creationPinType;
			pins = new List<pin>();
			sections = new List<section>();
			// create the pins
			reformatPins( creationNumPins.ToString() );
			// create the sections
			if( sections.Count() == 0 )
			{
				sections.Add( new section() );
				sections[0].position = new PointF( boardLocation.X * 10, boardLocation.Y * 10 );
			}

		}

		//
		public void reformatPins( string change )
		{   // NOTE: this is to be applied only on component creation. It should not be used to modify a component after it has been placed.
			if( int.TryParse( change, out int newNumPins ) ) { if( newNumPins > 0 || newNumPins < 200 ) creationNumPins = newNumPins; }

			if( change.StartsWith( "compType" ) ) { creationPinFormat = change; }

			// Create the pins ( after deleting any existing ones )
			pins.Clear();
			for( float i = 0; i < creationNumPins; i++ )
			{
				// calculate the co-ordinates for each pin
				float x,y;
				float radius, angleStep;
				int pins4;
				PointF dirn;                // the direction the wire leaves the pin in the schematic
				switch( creationPinFormat )
				{
					case "compTypeDIL":
						x = 0;
						y = i;
						dirn = new PointF( -1, 0 );
						if( i >= creationNumPins / 2 )
						{
							x = 1;
							y = creationNumPins - i - 1;
							dirn = new PointF( 1, 0 );
							if( (int)creationNumPins % 2 == 1 ) y -= 0.5f;
						}
						if( creationNumPins == 3 && creationPinType == pinTypes.surface )  // Special case for SMD transistors
						{
							if( i == 0 ) { x = 0.0f; y = 0.0f; };
							if( i == 1 ) { x = 0.0f; y = 1.0f; };
							if( i == 2 ) { x = 1.0f; y = 0.5f; };
						}
						if( creationNumPins == 3 && creationPinType == pinTypes.through )  // Special case for Through hole transistors
						{
							if( i == 0 ) { x = 0.0f; y = 1.0f; };
							if( i == 1 ) { x = 1.0f; y = 0.5f; };
							if( i == 2 ) { x = 0.0f; y = 0.0f; };
						}
						break;
					case "compTypeSIL":
						x = 0;
						y = i;
						dirn = new PointF( 1, 0 );
						break;
					case "compTypeCircular":
						radius = creationNumPins / 10f;
						angleStep = 2 * (float)Math.PI / creationNumPins;
						x = -radius * (float)Math.Sin( i * angleStep );
						y = radius - radius * (float)Math.Cos( i * angleStep );
						dirn = new PointF( 0, -1 );
						if( i > creationNumPins * 1f / 8f ) dirn = new PointF( -1, 0 );
						if( i > creationNumPins * 3f / 8f ) dirn = new PointF( 0, 1 );
						if( i > creationNumPins * 5f / 8f ) dirn = new PointF( 1, 0 );
						if( i > creationNumPins * 7f / 8f ) dirn = new PointF( 0, -1 );
						break;
					case "compTypeSquare":
						pins4 = creationNumPins / 4;
						{						  x = 0;				 y = i;					dirn = new PointF( -1,  0 ); }
						if( i >= pins4 )		{ x = 2 + i - pins4;	 y = pins4 + 1;			dirn = new PointF(  0,  1 ); }
						if( i >= pins4 * 2 )	{ x = 3 + pins4;		 y = pins4 * 3 - i - 1; dirn = new PointF(  1,  0 ); }
						if( i >= pins4 * 3 )	{ x = 1 + pins4 * 4 - i; y = -2;				dirn = new PointF(  0, -1 ); }
						break;
					default:
						x = 0;
						y = i;
						dirn = new PointF( 1, 0 );
						break;
				}

				x *= 2; y *= 2;

				// Add the pin to the list
				pin newPin = new pin()
				{
					pinIndex        = (int)i,
					templatePosition= new PointF( x, y ),
					boardPosition   = transformPoint( new PointF( x , y  ) ),
					pinNumber       = ( (int)i + 1 ).ToString(),
					pinName         = ( (int)i + 1 ).ToString(),
					isNetAnchor     = false,
					schemPosition   = new PointF( x * 100, y * 100),
					direction		= dirn
				};
				newPin.section = new int[] { 0 };
				pins.Add( newPin );
			}
			// Create the bounding box for the schematic
			float width =  100; float height = 100; //KLUDGE
			foreach( section section in sections )
			{
				float maxX = float.MinValue;
				float maxY = float.MinValue;
				float minX = float.MaxValue;
				float minY = float.MaxValue;
				foreach( component.pin pin in pins )
				{
					float x = pin.schemPosition.X;
					float y = pin.schemPosition.Y;
					maxX = Math.Max( maxX, x );
					maxY = Math.Max( maxY, y );
					minX = Math.Min( minX, x );
					minY = Math.Min( minY, y );
				}
				section.boundBox.X = minX - width;
				section.boundBox.Y = minY - height;
				section.boundBox.Width = 2 * width + maxX - minX;
				section.boundBox.Height = 2 * width + maxY - minY;
			}
		}

		// Drawing the board representation
		public void boardDraw( Graphics canvas, PointF displayPoint, float displayScale, bool backPinsOnly = false, bool selected = false )
		{   // draws the component at the specified location in the supplied canvas and given scale
			int i;

			// draw the pins (pads)
			Pen outlinePen = new Pen(Color.Black, 3);
			Brush normalBrush   = new SolidBrush(Color.FromArgb( 255, Color.White));
			Brush highliteBrush = new SolidBrush(Color.FromArgb( 255, Color.Green));
			//PointF padPoint;
			float padDiameter   = padSize * displayScale;
			float padRadius     = padDiameter * 0.5f;
			if( pinType == pinTypes.through )
			{
				for( i = 0; i < pins.Count(); i++ )
				{
					// pads
					float pX = displayPoint.X + ( pins[i].boardPosition.X ) * displayScale - padRadius;
					float pY = displayPoint.Y + ( pins[i].boardPosition.Y ) * displayScale - padRadius;
					canvas.FillEllipse
					(
						( i == 0 ) ? highliteBrush : normalBrush,
						pX,     // pins[i].boardPosition.X * scale.X * mirrorX * displayScale,
						pY,     // pins[i].boardPosition.Y * scale.Y * mirrorY * displayScale,
						padDiameter,
						padDiameter
					);
					canvas.DrawEllipse
					(
						outlinePen,
						pX,     // pins[i].boardPosition.X * scale.X * mirrorX * displayScale,
						pY,     // pins[i].boardPosition.Y * scale.Y * mirrorY * displayScale,
						padDiameter,
						padDiameter
					);

					// Pin numbers
					canvas.DrawString( pins[i].pinNumber, new Font( FontFamily.GenericMonospace, padRadius  ), new SolidBrush( Color.FromArgb( 255, Color.Orange ) ), pX, pY );
				}
			}
			if( !backPinsOnly )
			{
				if( pinType == pinTypes.surface )
				{
					for( i = 0; i < pins.Count(); i++ )
					{
						float pX = displayPoint.X + ( pins[i].boardPosition.X ) * displayScale - padRadius;
						float pY = displayPoint.Y + ( pins[i].boardPosition.Y ) * displayScale - padRadius;
						canvas.FillRectangle
						(
							( i == 0 ) ? highliteBrush : normalBrush,
							pX,     // pins[i].boardPosition.X * scale.X * mirrorX * displayScale,
							pY,     // pins[i].boardPosition.Y * scale.Y * mirrorY * displayScale,
							padDiameter,
							padDiameter
						);
						canvas.DrawRectangle
						(
							outlinePen,
							pX,     // pins[i].boardPosition.X * scale.X * mirrorX * displayScale,
							pY,     // pins[i].boardPosition.Y * scale.Y * mirrorY * displayScale,
							padDiameter,
							padDiameter
						);
						canvas.DrawString( pins[i].pinNumber, new Font( FontFamily.GenericMonospace , padRadius ), new SolidBrush( Color.FromArgb( 255, Color.Orange ) ), pX, pY );
					}
				}

				// Draw an outline enclosing all the pads
				float xMax = float.MinValue, yMax = float.MinValue;
				float xMin = float.MaxValue, yMin = float.MaxValue;
				float x,y;
				float padWidth  = padSize / scale.X;
				float padHeight = padSize / scale.Y;
				for( i = 0; i < pins.Count(); i++ )
				{
					x = pins[i].templatePosition.X;
					y = pins[i].templatePosition.Y;
					xMax = Math.Max( x + padWidth, xMax );
					yMax = Math.Max( y + padHeight, yMax );
					xMin = Math.Min( x - padWidth, xMin );
					yMin = Math.Min( y - padHeight, yMin );
				}
				PointF[] p = new PointF[5];
				p[0] = new PointF( xMin, yMin );
				p[1] = new PointF( xMax, yMin );
				p[2] = new PointF( xMax, yMax );
				p[3] = new PointF( xMin, yMax );
				p[4] = new PointF( xMin, yMin );
				if( !selected )      // Kludge - need a better way to highlight components
				{
					boardDrawPolyLine( canvas, displayPoint, displayScale, p, color: Color.Black, width: 7 );
					boardDrawPolyLine( canvas, displayPoint, displayScale, p, color: Color.White, width: 4 );
				}
				else
				{
					boardDrawPolyLine( canvas, displayPoint, displayScale, p, color: Color.Yellow, width: 9 );
					boardDrawPolyLine( canvas, displayPoint, displayScale, p, color: Color.Red, width: 4 );
				}
			}
		}
		void boardDrawPolyLine( Graphics canvas, PointF displayPoint, float displayScale, PointF[] p, Color? color = null, float width = 2 )
		{
			int i;
			Pen myPen = new Pen( color ?? Color.Black, width);
			PointF[] q = new PointF[ p.Length ];

			for( i = 0; i < p.Length; i++ )
			{
				q[i] = transformPoint( p[i] );
				q[i].X = displayPoint.X + q[i].X * displayScale;
				q[i].Y = displayPoint.Y + q[i].Y * displayScale;

				if( i > 0 ) canvas.DrawLine( myPen, q[i - 1], q[i] );
			}
		}
		public PointF transformPoint( PointF P )
		{   // transforms local points within a componenent's board representation according to the scale and rotation properties of the component
			float sX = P.X;
			float sY = P.Y;
			float mirrorX = 1f, mirrorY = 1f;
			if( mirror == mirrorModes.horizontal ) mirrorX = -1;
			if( mirror == mirrorModes.vertical ) mirrorY = -1;
			float r = rotation * (float)Math.PI / 180f;
			float x, y;
			x = sX * scale.X * mirrorX;
			y = sY * scale.Y * mirrorY;
			return new PointF
				(
					x * (float)Math.Cos( r ) - y * (float)Math.Sin( r ),
					y * (float)Math.Cos( r ) + x * (float)Math.Sin( r )
				);
		}
		void updateTransforms()
		{
			int i;
			// Update the co-ordinates of the pins
			for( i = 0; i < pins.Count(); i++ ) pins[i].boardPosition = transformPoint( pins[i].templatePosition );
		}

		// editing schematic component 
		public void rotate( int sectionIndex )
		{
			schemTransform( sectionIndex, "R" );
		}
		public void flipHorizontal( int sectionIndex )
		{
			schemTransform( sectionIndex, "X" );
		}
		public void flipVertical( int sectionIndex )
		{
			schemTransform( sectionIndex, "Y" );
		}
		public void schemTransform( int sectionIndex, string transform)		   // R, X, Y for rotate, flip hrizontal, flip vertical
		{   // rotates the symbol 90 degrees clockwise or flips horizontally or vertically
			// caution - doesn't work well with pins shared by different sections

			if( symbol != null )
			{   // transform the symbol graphics 
				symbol.transform( sectionIndex, cW: ( transform == "R" ), mirrorX: ( transform == "X" ), mirrorY: ( transform == "Y" ) );
				// copy over the pin co-ordinates from the symbol to the schematic
				for( int j = 0; j < pins.Count(); j++ )
				{
					string pinNumber = pins[j].pinNumber;
					int kPinIndex = symbol.pins.FindIndex( r => r.pin == pinNumber );
					pins[j].schemPosition = symbol.pins[kPinIndex].position;
					pins[j].direction = pointFFromDirection( symbol.pins[kPinIndex].orientation, -symbol.pins[kPinIndex].length  );
				}
			}
			else
			{	// Transform the pins directly if no symbol exists and we have only a default symbol (the one created from the board outline)
				if( sectionIndex >= 0 && sectionIndex < sections.Count() )
				{
					foreach( pin pin in pins )
					{
						// Act on the pin if it is included in this section  (note this can cause confusion if a pin exists in more than one section)
						bool isIncluded = false;
						foreach( int i in pin.section )
						{
							if( i == sectionIndex ) isIncluded = true;
						}
						if( isIncluded )
						{
							switch( transform )
							{
								case "R":       // Rotate 90 degrees clockwise
									float x1 = pin.schemPosition.X;
									float y1 = pin.schemPosition.Y;
									pin.schemPosition.X = y1;
									pin.schemPosition.Y = -x1;
									break;
								case "X":       // Flip horizontally
									pin.schemPosition.X = -pin.schemPosition.X;
									break;
								case "Y":       // Flip vertically
									pin.schemPosition.Y = -pin.schemPosition.Y;
									break;
							}
						}
					}
				}
			}
			schemUpdateBoundBox( sectionIndex, hasSymbol: symbol != null );
		}

		public void schemUpdateBoundBox( int sectionIndex, bool hasSymbol = false )           // Update the bounding box, to be applied after any changes to the symbol or the pins
		{
			// Update the bounding box for the schematic to match the new orientation
			float width =  100; float height = 100; //KLUDGE
			{
				float maxX = float.MinValue;
				float maxY = float.MinValue;
				float minX = float.MaxValue;
				float minY = float.MaxValue;

				// Find the extent of the pins
				if( !hasSymbol )
				{	// if no symbol use the pin co-ordinates derived from the board layout representation
					foreach( component.pin pin in pins )
					{
						if( pin.section[0] == sectionIndex )
						{
							float x = pin.schemPosition.X + pin.direction.X;
							float y = pin.schemPosition.Y + pin.direction.Y;
							maxX = Math.Max( maxX, x );
							maxY = Math.Max( maxY, y );
							minX = Math.Min( minX, x );
							minY = Math.Min( minY, y );
						}
					}
					maxX += width;
					minX -= width;
					maxY += height;
					minY -= height;
				}

				// extend it to include all the graphics of the symbol, if it exists
				if( symbol != null )
				{
					foreach( kSymbol.typ_pin pin in symbol.pins )
					{
						if( pin.part == sectionIndex )
						{
							PointF pt = pin.position;
							{
								maxX = Math.Max( maxX, pt.X );
								maxY = Math.Max( maxY, pt.Y );
								minX = Math.Min( minX, pt.X );
								minY = Math.Min( minY, pt.Y );
							}
						}
					}
					foreach( kSymbol.typ_rectangle rect in symbol.rectangles )
					{
						if( rect.part == sectionIndex )
						{
							maxX = Math.Max( maxX, rect.topLeft.X );
							maxY = Math.Max( maxY, rect.topLeft.Y );
							minX = Math.Min( minX, rect.topLeft.X );
							minY = Math.Min( minY, rect.topLeft.Y );
							maxX = Math.Max( maxX, rect.bottomRight.X );
							maxY = Math.Max( maxY, rect.bottomRight.Y );
							minX = Math.Min( minX, rect.bottomRight.X );
							minY = Math.Min( minY, rect.bottomRight.Y ); 
						}
					}
					foreach( kSymbol.typ_polygon poly in symbol.polygons )
					{
						if( poly.part == sectionIndex )
						{
							foreach( PointF pt in poly.vertex )
							{
								maxX = Math.Max( maxX, pt.X );
								maxY = Math.Max( maxY, pt.Y );
								minX = Math.Min( minX, pt.X );
								minY = Math.Min( minY, pt.Y );
							} 
						}
					}
					foreach( kSymbol.typ_bezier bez in symbol.beziers )
					{
						if( bez.part == sectionIndex )
						{
							foreach( PointF pt in bez.vertex )
							{
								maxX = Math.Max( maxX, pt.X );
								maxY = Math.Max( maxY, pt.Y );
								minX = Math.Min( minX, pt.X );
								minY = Math.Min( minY, pt.Y );
							}
						}
					}
					foreach( kSymbol.typ_circle circle in symbol.circles )
					{
						if( circle.part == sectionIndex )
						{
							maxX = Math.Max( maxX, circle.position.X + circle.radius );
							maxY = Math.Max( maxY, circle.position.Y - circle.radius );
							minX = Math.Min( minX, circle.position.X + circle.radius );
							minY = Math.Min( minY, circle.position.Y - circle.radius );
						}
					}
				}
				float minSize = 100;
				if( maxX - minX < minSize ) { minX -= minSize; maxX += minSize; }
				if( maxY - minY < minSize ) { minY -= minSize; maxY += minSize; }
				sections[sectionIndex].boundBox.X		= minX;	
				sections[sectionIndex].boundBox.Y		= minY;
				sections[ sectionIndex].boundBox.Width  = maxX - minX;
				sections[ sectionIndex].boundBox.Height = maxY - minY;

				Console.WriteLine( "schemUpdateBoundBox section {0} {1},{2}", sectionIndex, maxX - minX, maxY - minY );

			}
		}
		public PointF pointFFromDirection( char direction, float length )
		{   // generates a vector from the ascii direction and length
			PointF p = new PointF ( 0, 0 );
			switch(direction)
			{
				case 'U': p.Y = -length; break;
				case 'D': p.Y =  length; break;
				case 'L': p.X = -length; break;
				case 'R': p.X =  length; break;
			}
			return p;
		}

		// File load and save
		public static void writeComponents( StreamWriter writer, List<component> componentList )
		{
			int numComponents = componentList.Count();
			int numSections;
			int numPins;
			writer.WriteLine( "startComponents" );
			writer.WriteLine( "numComponents,{0}", numComponents );
			for( int i = 0; i < numComponents; i++ )
			{
				writer.Write( "desigType,{0},", componentList[i].desigType );
				writer.Write( "desigNum,{0},", componentList[i].desigNum );

				writer.Write( "layer,{0},", componentList[i].layer );
				writer.Write( "position.X,{0},", componentList[i].position.X );
				writer.Write( "position.Y,{0},", componentList[i].position.Y );
				writer.Write( "PadSize,{0},", componentList[i].PadSize );
				writer.Write( "PinType,{0},", componentList[i].PinType );
				writer.Write( "Mirror,{0},", componentList[i].Mirror );
				writer.Write( "Scale.X,{0},", componentList[i].Scale.X );
				writer.Write( "Scale.Y,{0},", componentList[i].Scale.Y );
				writer.WriteLine( "Rotation,{0}", componentList[i].Rotation );

				writer.WriteLine( "numSections,{0}", numSections = componentList[i].sections.Count() );
				for( int j = 0; j < numSections; j++ )
				{
					writer.Write( "name,{0},", componentList[i].sections[j].name );
					writer.Write( "position.X,{0},", componentList[i].sections[j].position.X );
					writer.Write( "position.Y,{0},", componentList[i].sections[j].position.Y );
					writer.Write( "boundBox.X,{0},", componentList[i].sections[j].boundBox.X );
					writer.Write( "boundBox.Y,{0},", componentList[i].sections[j].boundBox.Y );
					writer.Write( "boundBox.Width,{0},", componentList[i].sections[j].boundBox.Width );
					writer.Write( "boundBox.Height,{0}", componentList[i].sections[j].boundBox.Height );
					writer.WriteLine( "" );
				}

				writer.WriteLine( "numPins,{0}", numPins = componentList[i].pins.Count() );
				for( int j = 0; j < numPins; j++ )
				{
					writer.Write( "pinIndex,{0},", componentList[i].pins[j].pinIndex );
					writer.Write( "pinNumber,{0},", componentList[i].pins[j].pinNumber );
					writer.Write( "pinName,{0},", componentList[i].pins[j].pinName );
					writer.Write( "pinFullName,{0},", componentList[i].pins[j].pinFullName );
					writer.Write( "templatePosition.X,{0},", componentList[i].pins[j].templatePosition.X );
					writer.Write( "templatePosition.Y,{0},", componentList[i].pins[j].templatePosition.Y );
					writer.Write( "boardPosition.X,{0},", componentList[i].pins[j].boardPosition.X );
					writer.Write( "boardPosition.Y,{0},", componentList[i].pins[j].boardPosition.Y );
					writer.Write( "schemPosition.X,{0},", componentList[i].pins[j].schemPosition.X );
					writer.Write( "schemPosition.Y,{0},", componentList[i].pins[j].schemPosition.Y );
					writer.Write( "direction.X,{0},", componentList[i].pins[j].direction.X );
					writer.Write( "direction.Y,{0},", componentList[i].pins[j].direction.Y );
					writer.Write( "net,{0},", componentList[i].pins[j].net );
					writer.Write( "isNetAnchor,{0},", componentList[i].pins[j].isNetAnchor );
					writer.Write( "netName,{0},", componentList[i].pins[j].netName );
					writer.Write( "inSectionCount,{0}", componentList[i].pins[j].section.Count() );
					for( int k = 0; k < componentList[i].pins[j].section.Count(); k++ )
						writer.Write( ",{0}", componentList[i].pins[j].section[k] );
					writer.WriteLine( "" );
				}

				//  Symbol
				if( componentList[i].symbol == null )
				{
					writer.WriteLine( "NO_SYMBOL" );
				}
				else
				{

					int numFields, numArcs, numCircles, numPolygons, numRectangles, numTexts, numBeziers, numSymPins;
					writer.Write( "name,{0},",				glob.cleanText( componentList[i].symbol.name	) );
					writer.Write( "prefix,{0},",			glob.cleanText( componentList[i].symbol.prefix	) );
					writer.Write( "zzzzzz,{0},", 0 );
					writer.Write( "offset,{0},",			componentList[i].symbol.offset );
					writer.Write( "pinNumbersShown,{0},",	componentList[i].symbol.pinNumbersShown );
					writer.Write( "pinNamesShown,{0},",		componentList[i].symbol.pinNamesShown );
					writer.Write( "parts,{0},",				componentList[i].symbol.parts );
					writer.Write( "locked,{0},",			componentList[i].symbol.locked );
					writer.Write( "power,{0}",				componentList[i].symbol.power );
					writer.WriteLine( "" );

					writer.WriteLine( "numFields,{0}", numFields = componentList[i].symbol.fields.Count() );
					for( int j = 0; j < numFields; j++ )
					{
						writer.Write( "text,{0},",			glob.cleanText( componentList[i].symbol.fields[j].text ));
						writer.Write( "position,{0},",		componentList[i].symbol.fields[j].position );
						writer.Write( "size,{0},",			componentList[i].symbol.fields[j].size );
						writer.Write( "labelOrientationVertical,{0},", componentList[i].symbol.fields[j].labelOrientationVertical );
						writer.Write( "visible,{0},",		componentList[i].symbol.fields[j].visible );
						writer.Write( "hAlign,{0},",		componentList[i].symbol.fields[j].hAlign );
						writer.Write( "vAlign,{0},",		componentList[i].symbol.fields[j].vAlign );
						writer.Write( "italic,{0},",		componentList[i].symbol.fields[j].italic );
						writer.Write( "bold,{0}",			componentList[i].symbol.fields[j].bold );
						writer.WriteLine( "" );
					}

					writer.WriteLine( "numArcs,{0}", numArcs = componentList[i].symbol.arcs.Count() );
					for( int j = 0; j < numArcs; j++ )
					{
						writer.Write( "position,{0},"	, componentList[i].symbol.arcs[j].position );
						writer.Write( "radius,{0},"		, componentList[i].symbol.arcs[j].radius );
						writer.Write( "start,{0},"		, componentList[i].symbol.arcs[j].start );
						writer.Write( "end,{0},"		, componentList[i].symbol.arcs[j].end );
						writer.Write( "part,{0},"		, componentList[i].symbol.arcs[j].part );
						writer.Write( "dmg,{0},"		, componentList[i].symbol.arcs[j].dmg );
						writer.Write( "pen,{0},"		, componentList[i].symbol.arcs[j].pen );
						writer.Write( "fill,{0},"		, componentList[i].symbol.arcs[j].fill );
						writer.Write( "startPos,{0},"	, componentList[i].symbol.arcs[j].startPos );
						writer.Write( "endPos,{0}"		, componentList[i].symbol.arcs[j].endPos );
						writer.WriteLine( "" );
					}

					writer.WriteLine( "numCircles,{0}", numCircles = componentList[i].symbol.circles.Count() );
					for( int j = 0; j < numCircles; j++ )
					{
						writer.Write( "position,{0},",	componentList[i].symbol.circles[j].position );
						writer.Write( "radius,{0},",	componentList[i].symbol.circles[j].radius	);
						writer.Write( "part,{0},",		componentList[i].symbol.circles[j].part		);
						writer.Write( "dmg,{0},",		componentList[i].symbol.circles[j].dmg		);
						writer.Write( "pen,{0},",		componentList[i].symbol.circles[j].pen		);
						writer.Write( "fill,{0}",		componentList[i].symbol.circles[j].fill		);
						writer.WriteLine( "" );
					}

					writer.WriteLine( "numPolygons,{0}", numPolygons = componentList[i].symbol.polygons.Count() );
					for( int j = 0; j < numPolygons; j++ )
					{
						writer.Write( "count,{0},",		componentList[i].symbol.polygons[j].count	);
						writer.Write( "part,{0},",		componentList[i].symbol.polygons[j].part	);
						writer.Write( "dmg,{0},",		componentList[i].symbol.polygons[j].dmg		);
						writer.Write( "pen,{0},",		componentList[i].symbol.polygons[j].pen		);
						writer.Write( "vertex," );
						foreach( PointF vertex in componentList[i].symbol.polygons[j].vertex )
							writer.Write( "{0},{1},",	vertex.X, vertex.Y							);
						writer.Write( "fill,{0}",		componentList[i].symbol.polygons[j].fill	);
						writer.WriteLine( "" );
					}

					writer.WriteLine( "numRectangles,{0}", numRectangles = componentList[i].symbol.rectangles.Count() );
					for( int j = 0; j < numRectangles; j++ )
					{
						writer.Write( "topLeft,{0},", componentList[i].symbol.rectangles[j].topLeft );
						writer.Write( "bottomRight,{0},", componentList[i].symbol.rectangles[j].bottomRight );
						writer.Write( "part,{0},", componentList[i].symbol.rectangles[j].part );
						writer.Write( "dmg,{0},", componentList[i].symbol.rectangles[j].dmg );
						writer.Write( "pen,{0},", componentList[i].symbol.rectangles[j].pen );
						writer.Write( "fill,{0}", componentList[i].symbol.rectangles[j].fill );
						writer.WriteLine( "" );
					}

					writer.WriteLine( "numTexts,{0}", numTexts = componentList[i].symbol.texts.Count() );
					for( int j = 0; j < numTexts; j++ )
					{
						writer.Write( "angle,{0},",			componentList[i].symbol.texts[j].angle		);
						writer.Write( "position,{0},",		componentList[i].symbol.texts[j].position	);
						writer.Write( "size,{0},",			componentList[i].symbol.texts[j].size		);
						writer.Write( "hidden,{0},",		componentList[i].symbol.texts[j].hidden		);
						writer.Write( "part,{0},",			componentList[i].symbol.texts[j].part		);
						writer.Write( "dmg,{0},",			componentList[i].symbol.texts[j].dmg		);
						writer.Write( "text,{0},",			glob.cleanText( componentList[i].symbol.texts[j].text) );
						writer.Write( "italic,{0},",		componentList[i].symbol.texts[j].italic		);
						writer.Write( "bold,{0},",			componentList[i].symbol.texts[j].bold		);
						writer.Write( "hAlign,{0},",		componentList[i].symbol.texts[j].hAlign		);
						writer.Write( "vAlign,{0}",			componentList[i].symbol.texts[j].vAlign		);
						writer.WriteLine( "" );
					}

					writer.WriteLine( "numBeziers,{0}", numBeziers = componentList[i].symbol.beziers.Count() );
					for( int j = 0; j < numBeziers; j++ )
					{
						kSymbol.typ_bezier bez = componentList[i].symbol.beziers[j];
						writer.Write( "count,{0},", bez.count	);
						writer.Write( "part,{0},",	bez.part	);
						writer.Write( "dmg,{0},",	bez.dmg		);
						writer.Write( "pen,{0},",	bez.pen		);
						writer.Write( "vertex," );
						foreach( PointF vertex in bez.vertex ) writer.Write( "{0},{1},", vertex.X, vertex.Y );
						writer.Write( "fill,{0}", bez.fill );
						writer.WriteLine( "" );
					}
					writer.WriteLine( "numPins,{0}", numPins = componentList[i].symbol.pins.Count() );
					for( int j = 0; j < numPins; j++ )
					{
						kSymbol.typ_pin pin = componentList[i].symbol.pins[j];
						writer.Write( "name,{0},",			glob.cleanText( pin.name )	);
						writer.Write( "pin,{0},",			pin.pin						);
						writer.Write( "position,{0},",		pin.position				);
						writer.Write( "length,{0},",		pin.length					);
						writer.Write( "orientation,{0},",	pin.orientation				);
						writer.Write( "sizeNum,{0},",		pin.sizeNum					);
						writer.Write( "sizeName,{0},",		pin.sizeName				);
						writer.Write( "part,{0},",			pin.part					);
						writer.Write( "dmg,{0},",			pin.dmg						);
						writer.Write( "type,{0},",			pin.type					);
						writer.Write( "shape,{0}",			pin.shape					);
						writer.WriteLine( "" );
					}

				}
			}
			writer.WriteLine( "endComponents" );
		}
		public static void readComponents( StreamReader reader, List<component> componentList )
		{
			int numComponents = 0;
			int numPins;
			int numSections;
			string a;
			string[] b;
			int p;
			{
				// Get number of components
				a = reader.ReadLine();
				a = reader.ReadLine();
				b = a.Split( ',' );
				if( !b[0].Equals( "numComponents" ) ) causeException();
				numComponents = int.Parse( b[1] );
				Console.WriteLine( "Reading {0} components", numComponents );

				//seg.pointList = new List<PointF>();
				for( int i = 0; i < numComponents; i++ )
				{
					component comp = new component( new PointF(0,0) );

					// Get layer main component attributes
					a = reader.ReadLine();
					b = a.Split( ',' );
					p = 0;

					comp.desigType  = glob.readString( b[p++], b[p++], "desigType"    );
					comp.desigNum	= 	 glob.readInt( b[p++], b[p++], "desigNum"     );
					comp.layer		=    glob.readInt( b[p++], b[p++], "layer"        );
					comp.position.X =  glob.readFloat( b[p++], b[p++], "position.X"	 );
					comp.position.Y =  glob.readFloat( b[p++], b[p++], "position.Y"	 );
					comp.padSize	=  glob.readFloat( b[p++], b[p++], "PadSize"		 );
					string pinType  = glob.readString( b[p++], b[p++], "PinType"      ); comp.PinType = (pinTypes)GetEnumFromDescription( pinType, typeof( pinTypes ) );
					string mirror   = glob.readString( b[p++], b[p++], "Mirror"       ); comp.Mirror  =   (mirrorModes)GetEnumFromDescription( mirror,  typeof( mirrorModes   ) );
					comp.scale = new PointF( glob.readFloat( b[p++], b[p++], "Scale.X"), glob.readFloat( b[p++], b[p++], "Scale.Y" ) );
					comp.Rotation	= glob.readFloat( b[p++], b[p++], "Rotation" );

					a = reader.ReadLine();
					b = a.Split( ',' );
					p = 0;
					numSections = glob.readInt( b[p++], b[p++], "numSections" );
					//Read the list of sections	  
					comp.sections.Clear();
					for( int j = 0; j < numSections; j++ )
					{
						section newSection = new section();
						a = reader.ReadLine();
						b = a.Split( ',' );
						p = 0;
						newSection.name				= glob.readString( b[p++], b[p++], "name"			);
						newSection.position.X		=  glob.readFloat( b[p++], b[p++], "position.X"		);
						newSection.position.Y		=  glob.readFloat( b[p++], b[p++], "position.Y"		);
						newSection.boundBox.X		=  glob.readFloat( b[p++], b[p++], "boundBox.X"		);
						newSection.boundBox.Y		=  glob.readFloat( b[p++], b[p++], "boundBox.Y"		);
						newSection.boundBox.Width	=  glob.readFloat( b[p++], b[p++], "boundBox.Width"	);
						newSection.boundBox.Height	=  glob.readFloat( b[p++], b[p++], "boundBox.Height"	);
						comp.sections.Add( newSection );
					}
					if ( numSections == 0 ) comp.sections.Add( new section("" ) );

					a = reader.ReadLine();
					b = a.Split( ',' );
					p = 0;
					if( !b[p++].Equals( "numPins" ) ) causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
					numPins = int.Parse( b[p++] );

					//Read the list of pins	  
					comp.pins.Clear();
					for( int j = 0; j < numPins; j++ )
					{
						pin newPin = new pin();
						a = reader.ReadLine();
						b = a.Split( ',' );
						p = 0;
						if( !b[p++].Equals( "pinIndex" ) )			causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
						newPin.pinIndex				= int.Parse( b[p++] );

						if( !b[p++].Equals( "pinNumber" ) )			causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
						newPin.pinNumber			= ( b[p++] );

						if( !b[p++].Equals( "pinName" ) )			causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
						newPin.pinName				= ( b[p++] );

						if( !b[p++].Equals( "pinFullName" ) )		causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
						newPin.pinFullName			= ( b[p++] );

						if( !b[p++].Equals( "templatePosition.X" ) ) causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
						newPin.templatePosition.X	= float.Parse( b[p++] );
						if( !b[p++].Equals( "templatePosition.Y" ) ) causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
						newPin.templatePosition.Y	= float.Parse( b[p++] );

						if( !b[p++].Equals( "boardPosition.X" ) )	causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
						newPin.boardPosition.X		= float.Parse( b[p++] );
						if( !b[p++].Equals( "boardPosition.Y" ) )	causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
						newPin.boardPosition.Y		= float.Parse( b[p++] );

						if( !b[p++].Equals( "schemPosition.X" ) )	causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
						newPin.schemPosition.X		= float.Parse( b[p++] );
						if( !b[p++].Equals( "schemPosition.Y" ) )	causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
						newPin.schemPosition.Y		= float.Parse( b[p++] );

						if( !b[p++].Equals( "direction.X" ) ) causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
						newPin.direction.X = float.Parse( b[p++] );
						if( !b[p++].Equals( "direction.Y" ) ) causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
						newPin.direction.Y = float.Parse( b[p++] );

						if( !b[p++].Equals( "net" ) ) causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
						newPin.net = int.Parse( b[p++] );

						if( !b[p++].Equals( "isNetAnchor" ) )		causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
						newPin.isNetAnchor			= bool.Parse( b[p++] );

						if( !b[p++].Equals( "netName" ) )			causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
						newPin.netName				= b[p++];

						if( !b[p++].Equals( "inSectionCount" ) )	causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
						int inSectionCount = int.Parse( b[p++] );
						newPin.section = new int[inSectionCount];
						for( int k = 0; k < inSectionCount; k++ ) newPin.section[k] = int.Parse( b[p++] );

						comp.pins.Add( newPin );
					}

					a = reader.ReadLine(); b = a.Split( ',' ); p = 0;
					if ( a != "NO_SYMBOL")
					{
						kSymbol sym = new kSymbol();
						sym.name			= glob.readString( b[p++], b[p++], "name"			);
						sym.prefix			= glob.readString( b[p++], b[p++], "prefix"			);
						string tmp			= glob.readString( b[p++], b[p++], "zzzzzz"			);
						sym.offset			=  glob.readFloat( b[p++], b[p++], "offset"			);
						sym.pinNumbersShown =   glob.readBool( b[p++], b[p++], "pinNumbersShown" );
						sym.pinNamesShown   =   glob.readBool( b[p++], b[p++], "pinNamesShown"	);
						sym.parts			=    glob.readInt( b[p++], b[p++], "parts"			);
						sym.locked			=   glob.readBool( b[p++], b[p++], "locked"			);
						sym.power			=   glob.readBool( b[p++], b[p++], "power"			);

						a = reader.ReadLine(); 	b = a.Split( ',' );	p = 0;
						int numFields		=   glob.readInt( b[p++], b[p++], "numFields"		);
						for ( int j = 0; j < numFields; j++ )
						{
							a = reader.ReadLine(); b = a.Split( ',' ); p = 0;
							kSymbol.typ_field f = new kSymbol.typ_field();
							f.text						= glob.readString( b[p++], b[p++],			"text"		);
							f.position					= glob.readPointF( b[p++], b[p++], b[p++],	"position"	);
							f.size						=  glob.readFloat( b[p++], b[p++],			"size"		);
							f.labelOrientationVertical	=   glob.readBool( b[p++], b[p++],			"labelOrientationVertical" );
							f.visible					=   glob.readBool( b[p++], b[p++],			"visible"	);
							f.hAlign					=   glob.readChar( b[p++], b[p++],			"hAlign"	);
							f.vAlign					=   glob.readChar( b[p++], b[p++],			"vAlign"	);
							f.italic					=   glob.readBool( b[p++], b[p++],			"italic"	);
							f.bold						=   glob.readBool( b[p++], b[p++],			"bold"		);
							sym.fields.Add( f );
						}

						a = reader.ReadLine(); b = a.Split( ',' ); p = 0;
						int numArcs       =   glob.readInt( b[p++], b[p++], "numArcs"        );
						for( int j = 0; j < numArcs; j++ )
						{
							a = reader.ReadLine(); b = a.Split( ',' ); p = 0;
							kSymbol.typ_arc arc = new kSymbol.typ_arc();
							arc.position				= glob.readPointF( b[p++], b[p++], b[p++],	"position"	);
							arc.radius					=  glob.readFloat( b[p++], b[p++],			"radius"	);
							arc.start					=  glob.readFloat( b[p++], b[p++],			"start"		);
							arc.end						=  glob.readFloat( b[p++], b[p++],			"end"		);
							arc.part					=   glob.readInt ( b[p++], b[p++],			"part"		);
							arc.dmg						=    glob.readInt( b[p++], b[p++],			"dmg"		);
							arc.pen						=  glob.readFloat( b[p++], b[p++],			"pen"		);
							arc.fill					=   glob.readChar( b[p++], b[p++],			"fill"		);
							arc.startPos 				= glob.readPointF( b[p++], b[p++], b[p++],	"radius"	);
							arc.endPos					= glob.readPointF( b[p++], b[p++], b[p++],	"start"		);
							sym.arcs.Add( arc );
						}

						a = reader.ReadLine(); b = a.Split( ',' ); p = 0;
						int numCircles       =   glob.readInt( b[p++], b[p++], "numCircles"        );
						for( int j = 0; j < numCircles; j++ )
						{
							a = reader.ReadLine(); b = a.Split( ',' ); p = 0;
							kSymbol.typ_circle circle = new kSymbol.typ_circle();
							circle.position				= glob.readPointF( b[p++], b[p++], b[p++],	"position"	);
							circle.radius				=  glob.readFloat( b[p++], b[p++],			"radius"	);
							circle.part					=   glob.readInt ( b[p++], b[p++],			"part"		);
							circle.dmg					=    glob.readInt( b[p++], b[p++],			"dmg"		);
							circle.pen					=  glob.readFloat( b[p++], b[p++],			"pen"		);
							circle.fill					=   glob.readChar( b[p++], b[p++],			"fill"		);
							sym.circles.Add( circle );
						}

						a = reader.ReadLine(); b = a.Split( ',' ); p = 0;
						int numPolygons       =   glob.readInt( b[p++], b[p++], "numPolygons"        );
						for( int j = 0; j < numPolygons; j++ )
						{
							a = reader.ReadLine(); b = a.Split( ',' ); p = 0;
							kSymbol.typ_polygon polygon = new kSymbol.typ_polygon(); 
							polygon.count				=    glob.readInt( b[p++], b[p++],			"count"		);
							polygon.part				=    glob.readInt( b[p++], b[p++],			"part"		);
							polygon.dmg					=    glob.readInt( b[p++], b[p++],			"dmg"		);
							polygon.pen					=  glob.readFloat( b[p++], b[p++],			"pen"		);
							tmp							= glob.readString( b[p++], "",				"vertex"    );
							polygon.vertex = new List<PointF>();
							for ( int k = 0; k < polygon.count; k++ ) 	polygon.vertex.Add( new PointF( float.Parse( b[p++] ), float.Parse( b[p++] ) ) ); 
							polygon.fill				=   glob.readChar( b[p++], b[p++],			"fill"		);
							sym.polygons.Add( polygon );
						}

						a = reader.ReadLine(); b = a.Split( ',' ); p = 0;
						int numRectangles       =   glob.readInt( b[p++], b[p++], "numRectangles"        );
						for( int j = 0; j < numRectangles; j++ )
						{
							a = reader.ReadLine(); b = a.Split( ',' ); p = 0;
							kSymbol.typ_rectangle rect = new kSymbol.typ_rectangle();
							rect.topLeft				= glob.readPointF( b[p++], b[p++], b[p++],	"topLeft"		);
							rect.bottomRight			= glob.readPointF( b[p++], b[p++], b[p++],	"bottomRight"	);
							rect.part					=    glob.readInt( b[p++], b[p++],			"part"			);
							rect.dmg					=    glob.readInt( b[p++], b[p++],			"dmg"			);
							rect.pen					=  glob.readFloat( b[p++], b[p++],			"pen"			);
							rect.fill					=   glob.readChar( b[p++], b[p++],			"fill"			);
							sym.rectangles.Add( rect );
						}


						a = reader.ReadLine(); 	b = a.Split( ',' );	p = 0;
						int numTexts		=   glob.readInt( b[p++], b[p++], "numTexts"		);
						for ( int j = 0; j < numTexts; j++ )
						{
							a = reader.ReadLine(); b = a.Split( ',' ); p = 0;
							kSymbol.typ_text t = new kSymbol.typ_text();
							t.angle						=  glob.readFloat( b[p++], b[p++],			"angle"		);
							t.position					= glob.readPointF( b[p++], b[p++], b[p++],	"position"	);
							t.size						=  glob.readFloat( b[p++], b[p++],			"size"		);
							t.hidden					=   glob.readBool( b[p++], b[p++],			"hidden"	);
							t.part						=    glob.readInt( b[p++], b[p++],			"part"			);
							t.dmg						=    glob.readInt( b[p++], b[p++],			"dmg"			);
							t.text						= glob.readString( b[p++], b[p++],			"text"		);
							t.italic					=   glob.readBool( b[p++], b[p++],			"italic"	);
							t.bold						=   glob.readBool( b[p++], b[p++],			"bold"		);
							t.hAlign					=   glob.readChar( b[p++], b[p++],			"hAlign"	);
							t.vAlign					=   glob.readChar( b[p++], b[p++],			"vAlign"	);
							sym.texts.Add( t );
						}

						a = reader.ReadLine(); b = a.Split( ',' ); p = 0;
						int numBeziers       =   glob.readInt( b[p++], b[p++], "numBeziers"        );
						for( int j = 0; j < numBeziers; j++ )
						{
							a = reader.ReadLine(); b = a.Split( ',' ); p = 0;
							kSymbol.typ_bezier beziers	= new kSymbol.typ_bezier(); 
							beziers.count				=   glob.readInt( b[p++], b[p++],			"count"		);
							beziers.part				=   glob.readInt( b[p++], b[p++],			"part"		);
							beziers.dmg					=   glob.readInt( b[p++], b[p++],			"dmg"		);
							beziers.pen					=  glob.readFloat( b[p++], b[p++],			"pen"		);
							tmp							= glob.readString( b[p++], "",				"vertex"    );
							beziers.vertex = new List<PointF>();
							for ( int k = 0; k < beziers.count; k++ ) 	beziers.vertex.Add( new PointF( float.Parse( b[p++] ), float.Parse( b[p++] ) ) ); 
							beziers.fill				=   glob.readChar( b[p++], b[p++],			"fill"		);
							sym.beziers.Add( beziers );
						}

						a = reader.ReadLine(); 	b = a.Split( ',' );	p = 0;
						int numKPins		=   glob.readInt( b[p++], b[p++], "numPins"		);
						for ( int j = 0; j < numKPins; j++ )
						{
							a = reader.ReadLine(); b = a.Split( ',' ); p = 0;
							kSymbol.typ_pin pin = new kSymbol.typ_pin();
							pin.name					= glob.readString( b[p++], b[p++],			"name"			);
							pin.pin						= glob.readString( b[p++], b[p++],			"pin"			);
							pin.position				= glob.readPointF( b[p++], b[p++], b[p++],	"position"		);
							pin.length					=  glob.readFloat( b[p++], b[p++],			"length"		);
							pin.orientation				=   glob.readChar( b[p++], b[p++],			"orientation"	);
							pin.sizeNum					=  glob.readFloat( b[p++], b[p++],			"sizeNum"		);
							pin.sizeName				=  glob.readFloat( b[p++], b[p++],			"sizeName"		);
 							pin.part					=   glob.readInt ( b[p++], b[p++],			"part"			);
							pin.dmg						=   glob.readInt ( b[p++], b[p++],			"dmg"			);
							pin.type					=   glob.readChar( b[p++], b[p++],			"type"			);
							pin.shape					= glob.readString( b[p++], b[p++],			"shape"			);
							sym.pins.Add( pin );
						}



						comp.symbol = sym;
					}
					else
					{
						// kludge dev only
						string symbolName="";
						string symbolFileName="";
						//if( i == 81 ) { symbolName = "ATmega164PV-10AU"; symbolFileName = "D:\\My Documents\\KiCAD\\library\\MCU_Microchip_ATmega.lib"; }
						//if( i >= 65 && i <= 68 ) { symbolName = "ZZZ_ELS-432SURWA_S530-A3"; symbolFileName = "D:\\My Documents\\KiCAD\\library\\Display_Character.lib"; }
						//if( i == 0 || i == 1 ) { symbolName = "4016"; symbolFileName = "D:\\Dropbox\\o\\Reenigne\\TestProject\\4xxx.lib"; }                        // kludge dev only
						//if ( i >= 2 && i <= 4 ) { symbolName = "R_Pack04"; symbolFileName = "D:\\My Documents\\KiCAD\\library\\Device.lib"; }
						if( symbolName != "" )
						{
							// import the symbol and insert it in the component instance
							comp.symbol = new kSymbol();
							schematic.importKSymbol( symbolFileName, symbolName, ref comp.symbol );

							//copy the pin co-ordinates from the symbol to the pin in the component
							int maxPart = 0;
							for( int j = 0; j < numPins; j++ )
							{
								string pinNumber = comp.pins[j].pinNumber;
								int kPinIndex = comp.symbol.pins.FindIndex( r => r.pin == pinNumber );
								comp.pins[j].schemPosition = comp.symbol.pins[kPinIndex].position;
								comp.pins[j].section = new int[] { comp.symbol.pins[kPinIndex].part };
								if( comp.symbol.pins[kPinIndex].part > maxPart ) maxPart = comp.symbol.pins[kPinIndex].part;
							}

							// Create new sections in the component if snecessary
							while( maxPart >= comp.sections.Count() ) comp.sections.Add( new section( sectionName: char.ToString( (char)( 65 + comp.sections.Count() ) ) ) );
							int tmpOffset = 0;
							foreach( section sec in comp.sections )
							{
								// Provisionally set the positions starting from that of the original generic component and movin down for each section 
								sec.position = PointF.Add( comp.sections[0].position, new SizeF( 0, tmpOffset += 1500 ) );
								comp.schemUpdateBoundBox( comp.sections.IndexOf( sec ), hasSymbol: comp.symbol  != null );
							} 
						}
					}
					
					componentList.Add( comp );
				}
				//while (reader.Peek() != -1);
				a = reader.ReadLine();
				
			}

			//string glob.readString( string tag, string value, string fieldName )
			//{
			//	if( tag != fieldName ) causeException( "Read mismatch. Wanted " + fieldName + ", got " + tag ); ;
			//	return value;
			//}
			//char glob.readChar( string tag, string value, string fieldName )
			//{
			//	if( tag != fieldName ) causeException( "Read mismatch. Wanted " + fieldName + ", got " + tag ); ;
			//	return char.Parse(value);
			//}
			//int glob.readInt( string tag, string value, string fieldName )
			//{
			//	if( tag != fieldName ) causeException( "Read mismatch. Wanted " + fieldName + ", got " + tag ); ;
			//	return int.Parse( value );
			//}
			//float glob.readFloat( string tag, string value, string fieldName )
			//{
			//	if( tag != fieldName ) causeException( "Read mismatch. Wanted " + fieldName + ", got " + tag ); ;
			//	return float.Parse( value );
			//}
			//bool glob.readBool( string tag, string value, string fieldName )
			//{
			//	if( tag != fieldName ) causeException( "Read mismatch. Wanted " + fieldName + ", got " + tag ); ;
			//	if( value != "True" && value != "False" ) causeException( "Value error. Wanted " + fieldName + ", got " + value ); ;
			//	return ( value == "True" );
			//}
			//PointF glob.readPointF( string tag, string valueX, string valueY, string fieldName )
			//{
			//	if( tag != fieldName ) causeException( "Read mismatch. Wanted " + fieldName + ", got " + tag );
			//	float x,y;
			//	char[] charsToTrim = { '{', 'X', '=', ' ', 'Y', '}' };
			//	x = float.Parse( valueX.Trim( charsToTrim ) );
			//	y = float.Parse( valueY.Trim( charsToTrim ) );
			//	return new PointF( x, y );
			//}
		}
		
		// Other stuff
		public static void causeException() { causeException( "Not specified" ); }
		public static void causeException( string errorMessage ) //KLUDGE development only
		{
			Console.WriteLine( "Exception thrown: {0}", errorMessage );
			object o2 = null;
			//int i2 = (int)o2;   // Error
		}
        public static int GetEnumFromDescription(string description, Type enumType)
        {   
            foreach (var field in enumType.GetFields())
            {
				if( field.Name == description )
					return (int)field.GetValue( null );
			}
			return 0;
        }

		
	}
}
