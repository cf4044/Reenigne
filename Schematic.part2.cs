using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Reenigne
{
#pragma warning disable IDE1006

	public partial class schematic
	{
		public static void di() { }


		public class netElement
		{
			public int      netNum;
			public string   netName;

			// related to the board traces
			public PointF   anchorPoint;            // the location on the board to which this net is anchored
			public int      layer;                  // the layer on which this anchor is placed


			// related to drawing on the schematic
			public PointF   centroidSum;
			public int      numConn;
			public PointF   centroid;
			public float    leftest			= float.MaxValue;
			public float    rightest		= float.MinValue;
			public float    upest			= float.MaxValue;
			public float    downest			= float.MinValue;
			public bool     lockCentroid	= false;            // when true prevents recalculation of the centroid and allows it to be moved manually
			public List<connectionLine>     connectionLines = new List<connectionLine>();


			// Constructor
			public netElement( int netNumber )
			{
				netNum = netNumber;
				centroidSum = new PointF( 0, 0 );
				numConn = 0;
				centroid = centroidSum;
			}
			public netElement( int netNumber, PointF componentLocation, PointF pinLocation )
			{
				netNum = netNumber;
				centroidSum = new PointF( componentLocation.X + pinLocation.X, componentLocation.Y + pinLocation.Y );
				numConn = 1;
				centroid = centroidSum;
			}

			public void addPin( PointF sectionLocation, PointF pinLocation )
			{
				addPin( sectionLocation, pinLocation, new PointF( 0, 0 ) );
			}
			public void addPin( PointF sectionLocation, PointF pinLocation, PointF pinDirection )
			{
				float x = sectionLocation.X + pinLocation.X;
				float y = sectionLocation.Y + pinLocation.Y;
				centroidSum.X += x;
				centroidSum.Y += y;
				numConn++;
				if( !lockCentroid )
				{
					centroid.X = centroidSum.X / numConn;
					centroid.Y = centroidSum.Y / numConn;
				}
				leftest = Math.Min( leftest, x + pinDirection.X );
				rightest = Math.Max( rightest, x + pinDirection.X );
				upest = Math.Min( upest, y + pinDirection.Y );
				downest = Math.Max( downest, y + pinDirection.Y );
			}
			public PointF nearestPoint( PointF sourcePoint )
			{
				PointF nearestPoint = new PointF( centroid.X, centroid.Y );
				float minDistance = glob.distanceBetweenPoints(sourcePoint, centroid );
				float distance = 0;
				foreach( connectionLine cL in connectionLines )
				{
					foreach( PointF p in cL.points )
					{
						distance = glob.distanceBetweenPoints( sourcePoint, p );
						if ( distance < minDistance )
						{
							minDistance = distance;
							nearestPoint = p;
						}
					}

				}
				return nearestPoint ;
			}
		}

		public class connectionLine
		{   // Draws the connecting wires on the schematic. One instance for each pin, usually starting at the centroid and ending at or near the pin opf a component
			public bool				isConnected = false;			// Whether this segment is connected trough to the centroid in the schematic rendering
			public List<PointF>		points =  new List<PointF>();
		}

	}

	public class kSymbol
	{
		// Constructor
		public kSymbol()
		{
			fields = new List<typ_field>();
			arcs = new List<typ_arc>();
			circles = new List<typ_circle>();
			polygons = new List<typ_polygon>();
			rectangles = new List<typ_rectangle>();
			texts = new List<typ_text>();
			beziers = new List<typ_bezier>();
			pins = new List<typ_pin>();
		}

		// methods (functions)
		public static void transformPoint( ref PointF point, bool cW = true, bool cCW = false, bool mirrorX = false, bool mirrorY = false )
		{	// does rotation in 90 degree steps and mirroring in x and y directions.  Acts on a single PointF.
			float x,y;
			if( cW )
			{
				x = point.X;
				y = point.Y;
				point.X = -y;
				point.Y = x;
			}
			if( cCW )
			{
				x = point.X;
				y = point.Y;
				point.X = y;
				point.Y = -x;
			}
			if( mirrorX ) point.X = -point.X;
			if( mirrorY ) point.Y = -point.Y;
		}
		public        void transform( int part, bool cW = false, bool cCW = false, bool mirrorX = false, bool mirrorY = false )
		{	// applies rotation and mirroring to the whole component
			foreach( typ_field     field     in fields     )	                                  field.transform( cW, cCW, mirrorX, mirrorY );
			foreach( typ_arc       arc       in arcs       )	 if(       arc.part == part )       arc.transform( cW, cCW, mirrorX, mirrorY );
			foreach( typ_circle    circle    in circles    )	 if(    circle.part == part )    circle.transform( cW, cCW, mirrorX, mirrorY );
			foreach( typ_polygon   polygon   in polygons   )	 if(   polygon.part == part )   polygon.transform( cW, cCW, mirrorX, mirrorY );
			foreach( typ_rectangle rectangle in rectangles )	 if( rectangle.part == part ) rectangle.transform( cW, cCW, mirrorX, mirrorY );
			foreach( typ_text      text      in texts      )	 if(      text.part == part )      text.transform( cW, cCW, mirrorX, mirrorY );
			foreach( typ_bezier    bezier    in beziers    )	 if(    bezier.part == part )    bezier.transform( cW, cCW, mirrorX, mirrorY );
			foreach( typ_pin       pin       in pins       )	 if(       pin.part == part )       pin.transform( cW, cCW, mirrorX, mirrorY );
		}

		// fields
		public string               name;               // 1. the type number of the component such as for example 74LS04
		public string               prefix;             // 2. designator prefix, such as for example the C, R or U in 	C8, R22, U9.  It is set to ~ if there is no prefix
														// 3. zero - this field is always zero
		public float                offset;             // 4. the pin name offset, in mil from the end-point of a pin; when set to zero, the pin name is set outside the shape.
		public bool                 pinNumbersShown;    // 5. either “Y” or “N” to indicate whether pin numbers are shown
		public bool                 pinNamesShown;      // 6. either “Y” or “N” to indicate whether pin names are shown
		public int                  parts;              // 7. the number of “parts” in the symbol, this corresponds to what I am calling 'sections'
		public bool                 locked;             // 8. “L” if the parts are locked or an “F” otherwise. 
		public bool                 power;              // 9. “P” for a power symbol or “N” otherwise. 

		public List<typ_field>      fields;
		public List<typ_arc>        arcs;
		public List<typ_circle>     circles;
		public List<typ_polygon>    polygons;
		public List<typ_rectangle>  rectangles;
		public List<typ_text>       texts;
		public List<typ_bezier>     beziers;
		public List<typ_pin>        pins;

		public class typ_field
		{
			public string   text;                       //	1. symbolic prefix text. Same as prefix defined in DEF
			public PointF   position;                   //	2,3 position  of the text field, in mil.
			public float    size;                       //  4. Size of the text
			public bool     labelOrientationVertical;   //	5. H/false for horizontal orientation, or V/true for vertical orientation of the text field
			public bool     visible;                    //	6. I/false for invisible text, or “V”/true otherwise
			public char     hAlign, vAlign;             //	7,8. the horizontal and vertical alignment: C(entred), L(eft), R(ight), T(op) or B(ottom)
			public bool     italic;                     //	9. “I”/true for italic text, or “N”/false otherwise
			public bool     bold;                       // 10. “B”/true for bold text or “N”/false otherwise

			public void transform( bool cW = false, bool cCW = false, bool mirrorX = false, bool mirrorY = false )
			{
				transformPoint( ref position, cW, cCW, mirrorX, mirrorY );
				//labelOrientationVertical = !labelOrientationVertical;
			}
		}
		public class typ_arc
		{
			public PointF   position;           //	1,2. position  of center.
			public float    radius;             //  3. radius of the arc
			public float    start,end;          //  4,5.start and end angles in tenths of a degree, so 900 represents 90 degrees
			public int      part;               //  6. which part (section) this arc belongs to
			public int      dmg;                //  7. part for alternate DeMorgan symbol
			public float    pen;                //  8. line thickness
			public char     fill;               //  9. F=fill in pen colour, f=fill in background colour, N=no fill
			public PointF   startPos;           // 10,11. The co-ordinates where the arc starts
			public PointF   endPos;             // 12,13. The co-ordinates where the arc ends

			public void transform( bool cW = true, bool cCW = false, bool mirrorX = false, bool mirrorY = false )
			{
				transformPoint( ref position, cW, cCW, mirrorX, mirrorY );
				transformPoint( ref startPos, cW, cCW, mirrorX, mirrorY );
				transformPoint( ref endPos, cW, cCW, mirrorX, mirrorY );
				start += ( cW ? 90 : 0 ) + ( cCW ? -90 : 0 );
				end += ( cW ? 90 : 0 ) + ( cCW ? -90 : 0 );
				// failed attempt to fix problem - 4 mar 2022
				//if( mirrorX || mirrorY )
				//{
				//	start += 180;
				//	end += 180;
				//	if( start > 360 ) start -= 360;
				//	if( end   > 360 ) end   -= 360;
				//}
			}
		}
		public class typ_circle
		{
			public PointF   position;           //	1,2. position  of the text field, in mil.
			public float    radius;             //  3. radius of the circle
			public int      part;               //  4. which part (section) this circle belongs to
			public int      dmg;                //  5. part for alternate DeMorgan symbol
			public float    pen;                //  6. line thickness
			public char     fill;               //  7. F=fill in pen colour, f=fill in background colour, N=no fill

			public void transform( bool cW = true, bool cCW = false, bool mirrorX = false, bool mirrorY = false )
			{
				transformPoint( ref position, cW, cCW, mirrorX, mirrorY );
			}
		}
		public class typ_polygon
		{
			public int          count;          // 1. number of vertices in the polygon/polyline
			public int          part;           // 2. which part (section) this polygon belongs to
			public int          dmg;            // 3. part for alternate DeMorgan symbol
			public float        pen;            // 4. line thickness
			public List<PointF> vertex;         // ... list of vertices				
			public char         fill;           // ... F=fill in pen colour, f=fill in background colour, N=no fill

			public void transform( bool cW = true, bool cCW = false, bool mirrorX = false, bool mirrorY = false )
			{
				int i;
				for( i = 0; i < vertex.Count(); i++ )
				{
					PointF p = vertex[i];
					transformPoint( ref p, cW, cCW, mirrorX, mirrorY );
					vertex[i] = p;
				}
			}
		}
		public class typ_rectangle
		{
			public PointF   topLeft;            //  1,2. position  of the top left corner
			public PointF   bottomRight;        //  3,4. position  of the bottom right corner
			public int      part;               //  5. which part (section) this rectangle belongs to
			public int      dmg;                //  6. part for alternate DeMorgan symbol
			public float    pen;                //  7. line thickness
			public char     fill;               //  8. F=fill in pen colour, f=fill in background colour, N=no fill

			public void transform( bool cW = true, bool cCW = false, bool mirrorX = false, bool mirrorY = false )
			{
				transformPoint( ref topLeft, cW, cCW, mirrorX, mirrorY );
				transformPoint( ref bottomRight, cW, cCW, mirrorX, mirrorY );
			}

		}
		public class typ_text
		{
			public float    angle;              //  1. the orientation of the text in degrees (tenths of degree in source file)
			public PointF   position;           //  2,3. Location of the text
			public float    size;               //  4. size of the text font
			public bool     hidden;             //  5. 0/true=hidden   1/false=visible
			public int      part;               //  6. which part (section) this text belongs to
			public int      dmg;                //  7. part for alternate DeMorgan symbol
			public string   text;               //	8. the text to show
			public bool     italic;             //	9. “I”/true for italic text, or “N”/false otherwise
			public bool     bold;               // 10. “B”/true for bold text or “N”/false otherwise
			public char     hAlign, vAlign;     // 11,12. the horizontal and vertical alignment: C(entred), L(eft), R(ight), T(op) or B(ottom)

			public void transform( bool cW = true, bool cCW = false, bool mirrorX = false, bool mirrorY = false )
			{
				transformPoint( ref position, cW, cCW, mirrorX, mirrorY );
				angle += ( cW ? 90 : 0 ) + ( cCW ? -90 : 0 );
			}
		}
		public class typ_bezier
		{
			public int          count;          // 1. number of vertices in the bezier curve
			public int          part;           // 2. which part (section) this polygon belongs to
			public int          dmg;            // 3. part for alternate DeMorgan symbol
			public float        pen;            // 4. line thickness
			public List<PointF> vertex;         // ... list of vertices				
			public char         fill;           // ... F=fill in pen colour, f=fill in background colour, N=no fill

			public void transform( bool cW = true, bool cCW = false, bool mirrorX = false, bool mirrorY = false )
			{
				int i;
				for( i = 0; i < vertex.Count(); i++ )
				{
					PointF p = vertex[i];
					transformPoint( ref p, cW, cCW, mirrorX, mirrorY );
					vertex[i] = p;
				}
			}
		}
		public class typ_pin
		{
			public string   name;               //  1. name of the pin
			public string   pin;                //  2. pin number.  May be alphanumeric.
			public PointF   position;           //  3,4. Location of the pin
			public float    length;             //	5. Length from the pin to the body of the symbol
			public char     orientation;        // 	6. U(p), D(own), L(eft) or R(ight)
			public float    sizeNum;            //  7. text size of pin name
			public float    sizeName;           //  8. text size of pin number
			public int      part;               //  9. which part (section) this text belongs to
			public int      dmg;                //  10. part for alternate DeMorgan symbol
			public char     type;               //	11.	I(nput), O(utout), B(idirectional), T(ristate), P(assive), (open) C(ollector), (open) E(mitter), N(on-connected), U(nspecified), or W for power input or w of power output
			public string   shape;              //	12. I(nverted), C(lock), L for input-low, V for output-low (there are more shapes...). If the shape is prefixed with an “N”, the pin is invisible

			public void transform( bool cW = false, bool cCW = false, bool mirrorX = false, bool mirrorY = false )
			{
				transformPoint( ref position, cW, cCW, mirrorX, mirrorY );
				char o = orientation;
				if( cW )
				{
					if( o == 'U' ) orientation = 'R';
					if( o == 'R' ) orientation = 'D';
					if( o == 'D' ) orientation = 'L';
					if( o == 'L' ) orientation = 'U';
				}
				o = orientation;
				if( cCW )
				{
					if( o == 'U' ) orientation = 'L';
					if( o == 'L' ) orientation = 'D';
					if( o == 'D' ) orientation = 'R';
					if( o == 'R' ) orientation = 'U';
				}
				o = orientation;
				if( mirrorX )
				{
					if( o == 'R' ) orientation = 'L';
					if( o == 'L' ) orientation = 'R';
				}
				o = orientation;
				if( mirrorY )
				{
					if( o == 'U' ) orientation = 'D';
					if( o == 'D' ) orientation = 'U';
				}
			}
		}
	}

	
}
