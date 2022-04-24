using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reenigne
{
#pragma warning disable IDE1006
#pragma warning disable IDE0017



	partial class schematic
	{

		// Constructor
		public schematic()
		{
			//importKSymbol( "", "", ref testSym );
		}

		public float scale		= 0.01f;    // scale of the drawing in pixels per unit
		public float offsetX	= 0;
		public float offsetY	= 0;

		public static List <netElement> netElements = new List<netElement>();
		public kSymbol testSym = new kSymbol();

		public static void importKSymbol( string fileName, string symbolName, ref kSymbol sym ) //, Graphics canvas, mainForm form )
		{	// Imports symbol from a KiCad library and places it in the provided instance of kSymbol class
			float	kiScale = 2f;

			float   x1,y1;
			PointF p = new PointF( 0, 0 );
			PointF q = new PointF( 0, 0 );

			string a;
			string[] b;

			if( fileName == "" ) return;
			  
			int selectDmg = 1;
			StreamReader reader = new StreamReader( fileName );

			// Find the start of this symbol's definition within the library
			bool found = false;
			do
			{
				a = reader.ReadLine();
				b = a.Split( ' ' );
				if( b.Count() > 2 )
				{
					if( b[0] == "DEF" && b[1] == symbolName )
					{
						found = true;
						sym.name			= b[1];
						sym.prefix			= b[2];
						sym.offset			= float.Parse( b[4] ) * kiScale ;
						sym.pinNumbersShown = b[5] == "Y";
						sym.pinNamesShown	= b[6] == "Y";
						sym.parts			= int.Parse( b[7] );
						sym.locked			= b[8] == "L";
						sym.power			= b[9] == "P";
					}
				}

			} while( !found & !reader.EndOfStream );

			// Read in all the elements of the symbol
			if( found )
			{
				found = false;
				do
				{
					 a = reader.ReadLine();
					// unquote any quoted text and replace spaces in quoted text with underscores
					if( a.Contains( "\"" ) )
					{
						bool quote = false;
						for( int i = 0; i < a.Length-1; i++ )
						{
							if( a.Substring( i, 1 ) == "\"" )
							{
								string pad = "";
								if( a.Substring( i + 1, 1 ) == "\"" ) pad = "-";
								a = a.Substring( 0, i ) + pad + a.Substring( i + 1 );
								quote = !quote;
							}
							if( quote && a.Substring( i + 1, 1 ) == " " ) a = a.Substring( 0, i + 1 ) + "_" + a.Substring( i + 2 );
						}
					}
					b = a.Split( ' ' );		// Split the line into fields and put in an array of strings, one element for each field
					if( b.Count() > 0 )
					{
						string recType;
						if( b[0].Length > 0 ) recType = b[0].Substring( 0, 1 ); else recType = "?";
						switch( recType )
						{
							case "D" when b[0].Equals( "DEF" ):
								// this case is dealt with before the switch() - see further up
								// so should never be encountered here
								Debugger.Break();
								break;
								
							case "D" when b[0].Equals( "DRAW" ):	  // The when condition is to avoid misidentifying DEF as D
								// ToDo: This field contains a general description, not yet implemented so just ignored for now
								break;

							case "F":
								kSymbol.typ_field f = new kSymbol.typ_field();
								f.text						=  b[1].Trim( '"' );
								f.position.X				=  float.Parse( b[2] ) * kiScale;
								f.position.Y				= -float.Parse( b[3] ) * kiScale;
								f.size						=  float.Parse( b[4] ) * kiScale;
								f.labelOrientationVertical	=  b[5] == "V";
								f.visible					=  b[6] == "V";
								f.hAlign					=  char.Parse( b[7].Substring( 0, 1 ) );
								f.vAlign					=  char.Parse( b[8].Substring( 0, 1 ) );
								f.italic					=  glob.nthCharacter( b[8], 1 ) == "I";
								f.bold						=  glob.nthCharacter( b[8], 2 ) == "B";
								sym.fields.Add( f );
								break;

							case "C":
								kSymbol.typ_circle c = new kSymbol.typ_circle();
								c.dmg = int.Parse( b[5] );
								if( c.dmg == 0 || c.dmg == selectDmg )
								{
									c.position.X	=  float.Parse( b[1] ) * kiScale;
									c.position.Y	= -float.Parse( b[2] ) * kiScale;
									c.radius		=  float.Parse( b[3] ) * kiScale;
									c.part			=    int.Parse( b[4] );
									c.pen			=  float.Parse( b[6] ) * kiScale;
									c.fill			=   char.Parse( b[7] );
									if( sym.parts == 1 ) c.part = 0;
									sym.circles.Add( c );
								}
								break;

							case "A" when b[0].Equals( "A" ):
								kSymbol.typ_arc arc = new kSymbol.typ_arc();
								arc.dmg = int.Parse( b[7] );
								if( arc.dmg == 0 || arc.dmg == selectDmg )
								{
									arc.position.X	=  float.Parse( b[1] ) * kiScale;
									arc.position.Y	= -float.Parse( b[2] ) * kiScale ;
									arc.radius		=  float.Parse( b[3] ) * kiScale;
									arc.start		= -float.Parse( b[4] ) / 10f;
									arc.end			= -float.Parse( b[5] ) / 10f;
									arc.part		=    int.Parse( b[6] );
									arc.pen			=  float.Parse( b[8] ) * kiScale;
									arc.fill		=   char.Parse( b[9] );
									if( sym.parts == 1 ) arc.part = 0;
									sym.arcs.Add( arc );
								}	
								break;

							case "X":
								kSymbol.typ_pin pin = new kSymbol.typ_pin();
								pin.dmg = int.Parse( b[10] );
								if( pin.dmg == 0 || pin.dmg == selectDmg )
								{
									pin.name		=  b[1];
									pin.pin			=  b[2];
									pin.position.X	=  float.Parse( b[3]  ) * kiScale;
									pin.position.Y =  -float.Parse( b[4]  ) * kiScale;
									pin.length		=  float.Parse( b[5]  ) * kiScale;
									pin.orientation =   char.Parse( b[6]  );
									pin.sizeNum		=  float.Parse( b[7]  ) * kiScale;
									pin.sizeName	=  float.Parse( b[8]  ) * kiScale;
									pin.part		=    int.Parse( b[9]  );
									pin.type		=   char.Parse( b[11] );
									if( b.Count() > 12 ) pin.shape = b[12]; else pin.shape = "";
									if( sym.parts == 1 ) pin.part = 0;
									sym.pins.Add( pin );
								}
								break;

							case "T":
								kSymbol.typ_text txt = new kSymbol.typ_text();
								txt.dmg = int.Parse( b[7] );
								if( txt.dmg == 0 || txt.dmg == selectDmg )
								{ 
									txt.angle		=  float.Parse( b[1] ) / 10f;
									txt.position.X	=  float.Parse( b[2] ) * kiScale;
									txt.position.Y	= -float.Parse( b[3] ) * kiScale;
									txt.size		=  float.Parse( b[4] ) * kiScale;
									txt.hidden		=  b[5] == "H";
									txt.part		=  int.Parse( b[6] );
									txt.text		=  b[8];
									if (b.Count() > 9)
										txt.italic	=  b[9]  == "I";
									if( b.Count() > 10 )
										txt.bold	=  b[10] == "B";
									if( b.Count() > 11 )
										txt.hAlign	=  char.Parse( b[11] );
									if( b.Count() > 12 ) txt.vAlign		=  char.Parse( b[12] );
									if( sym.parts == 1 ) txt.part = 0;
									sym.texts.Add( txt );
								}
								break;

							case "P":       // Polyline
								kSymbol.typ_polygon poly = new kSymbol.typ_polygon();
								poly.vertex = new List<PointF>();
								poly.dmg = int.Parse( b[3] );
								if( poly.dmg == 0 || poly.dmg == selectDmg )
								{
									poly.count		= int.Parse( b[1] );
									poly.part		= int.Parse( b[2] );
									poly.pen		= float.Parse( b[4] ) * kiScale; 
									for( int i = 0; i < poly.count; i++ )
									{
										x1 =  float.Parse( b[5 + i * 2] ) * kiScale;
										y1 = -float.Parse( b[6 + i * 2] ) * kiScale;
										poly.vertex.Add( new PointF( x1, y1 ) );
									}
									poly.fill = char.Parse( b[ 5 + 2 * poly.count ] );
									if( sym.parts == 1 ) poly.part = 0;
									sym.polygons.Add( poly );
								}
								break;

							case "B":       // bezline
								kSymbol.typ_bezier bez = new kSymbol.typ_bezier();
								bez.vertex = new List<PointF>();
								bez.dmg = int.Parse( b[3] );
								if( bez.dmg == 0 || bez.dmg == selectDmg )
								{
									bez.count	= int.Parse( b[1] );
									bez.part	= int.Parse( b[2] );
									bez.pen = float.Parse( b[4] ) * kiScale;
									for( int i = 0; i < bez.count; i++ )
									{
										x1		=  float.Parse( b[5 + i * 2] ) * kiScale;
										y1		= -float.Parse( b[6 + i * 2] ) * kiScale;
										bez.vertex.Add( new PointF( x1, y1 ) );
									}
									bez.fill	= char.Parse( b[5 + 2 * bez.count] );
									if( sym.parts == 1 ) bez.part = 0;
									sym.beziers.Add( bez );
								}
								break;

							case "S":
								kSymbol.typ_rectangle rect = new kSymbol.typ_rectangle();
								rect.dmg = int.Parse( b[6] );
								if( rect.dmg == 0 || rect.dmg == selectDmg )
								{
									rect.topLeft.X		=  float.Parse( b[1] ) * kiScale;
									rect.topLeft.Y		= -float.Parse( b[2] ) * kiScale;
									rect.bottomRight.X	=  float.Parse( b[3] ) * kiScale;
									rect.bottomRight.Y	= -float.Parse( b[4] ) * kiScale;
									rect.part			=    int.Parse( b[5] ); if( sym.parts == 1 ) rect.part = 0;
									//rect.pen			=  float.Parse( b[7] ) * kiScale;
									rect.pen			=     getFloat( b,7 ) * kiScale;
									//rect.fill			=   char.Parse( b[8] );
									rect.fill			=      getChar( b, 8 );
									if( sym.parts == 1 ) rect.part = 0;
									sym.rectangles.Add( rect );	  
								}
								break;

							case "E" when b[0].Equals( "ENDDRAW" ):
								found = true;
								break;

							case "E" when b[0].Equals( "ENDDDEF" ):
								break;

							default:
								Console.WriteLine( "Unrecognised line: {0}", a );
								break;
						}
					}

				} while( !found & !reader.EndOfStream );
			}
			reader.Close();

			char getChar( string[] bb, int index )
			{
				if( index >= bb.Count() ) return (char)0;
				char result;
				if( char.TryParse( bb[index], out result ) ) return result;
				return (char)0;
			}
			float getFloat( string[] bb, int index )
			{
				if( index >= bb.Count() ) return 1f;
				float result;
				if( float.TryParse( bb[index], out result ) ) return result;
				return 1f;			// Kludge - return defaults to 1 as potentially least harmful result
			}
		}
		public void drawKSymbol( kSymbol sym, int section, PointF location, Graphics canvas )
		{
			drawKSymbol( sym, section, location, canvas, -1 );
		}
		public void drawKSymbol( kSymbol sym, int section, PointF location, Graphics canvas, float drawScale )
		{
			if ( section == 0 && sym.parts != 1 ) return;
			var dScale = scale;
			if( drawScale > 0 ) dScale = drawScale;

			if( sym == null ) { Console.WriteLine( "Attempting to draw NULL symbol" ); return; }
			PointF p,q;
			float tmp;

			int selectDmg   = 1;
			int spacing     = 0;     // KLUDGE DEV only - this is only for testing. In rel use each part will have its own co-ordinates
			SizeF offset =  new SizeF( location.X, location.Y );
			// Draw arcs
			foreach( kSymbol.typ_arc arc in sym.arcs )
			{

				if( ( arc.dmg == 0 || arc.dmg == selectDmg ) && ( arc.part == section || arc.part == 0 ) )
				{
					offset.Width = location.X;
					offset.Height = location.Y + spacing * arc.part;
					PointF topLeft      = PointF.Add( arc.position, new SizeF( -arc.radius, -arc.radius ) );
					PointF bottomRight  = PointF.Add( arc.position, new SizeF(  arc.radius,  arc.radius ) );
					p = translateCoordsF( PointF.Add( topLeft, offset ) );
					q = translateCoordsF( PointF.Add( bottomRight, offset ) );
					//if ( arc.end - arc.start > 180 ) { arc.start += 180; arc.end += 180; }
					canvas.DrawArc( new Pen( Color.Green, 1 ), p.X, p.Y, q.X - p.X, q.Y - p.Y, arc.start, arc.end - arc.start );
				}
			}

			// Draw circles
			foreach( kSymbol.typ_circle circle in sym.circles )
			{
				if( ( circle.dmg == 0 || circle.dmg == selectDmg ) &&  ( circle.part == section || circle.part == 0 ) )
				{
					offset.Width = location.X;
					offset.Height = location.Y + spacing * circle.part;
					PointF topLeft      = PointF.Add( circle.position, new SizeF( -circle.radius, -circle.radius ) );
					PointF bottomRight  = PointF.Add( circle.position, new SizeF(  circle.radius,  circle.radius ) );
					p = translateCoordsF( PointF.Add( topLeft, offset ) );
					q = translateCoordsF( PointF.Add( bottomRight, offset ) );
					if( p.X > q.X ) { tmp = p.X; p.X = q.X; q.X = tmp; }
					if( p.Y > q.Y ) { tmp = p.Y; p.Y = q.Y; q.Y = tmp; }
					canvas.DrawArc( new Pen( Color.Green, 1 ), p.X, p.Y, q.X - p.X, q.Y - p.Y, 0, 360 );
				}
			}

			// Draw polygons
			foreach( kSymbol.typ_polygon poly in sym.polygons )
			{
				if( ( poly.dmg == 0 || poly.dmg == selectDmg ) && ( poly.part == section || poly.part == 0 ) )
				{
					offset.Width = location.X;
					offset.Height = location.Y + spacing * poly.part;
					PointF[] pts = new PointF[ poly.vertex.Count()];
					int i = 0;
					foreach( PointF vertex in poly.vertex )
					{
						pts[i++] = translateCoordsF( PointF.Add( vertex, offset ) );
					}
					canvas.DrawLines( new Pen( Color.Green, 1 ), pts );
					//canvas.DrawBeziers( new Pen( Color.Green, 1 ), pts );
				}
			}

			// Draw bezier curves
			foreach( kSymbol.typ_bezier bez in sym.beziers )
			{
				if( ( bez.dmg == 0 || bez.dmg == selectDmg ) && ( bez.part == section || bez.part == 0 ) )
				{
					offset.Width = location.X;
					offset.Height = location.Y + spacing * bez.part;
					PointF[] pts = new PointF[ bez.vertex.Count() ];
					int i = 0;
					foreach( PointF vertex in bez.vertex )
					{
						pts[i++] = translateCoordsF( PointF.Add( vertex, offset ) );
					}
					canvas.DrawLines( new Pen( Color.Green, 1 ), pts );
					//canvas.DrawBeziers( new Pen( Color.Green, 1 ), pts );
				}
			}

			// Draw rectangles
			foreach( kSymbol.typ_rectangle rect in sym.rectangles )
			{
				if( ( rect.dmg == 0 || rect.dmg == selectDmg ) && ( rect.part == section || rect.part == 0 ) )
				{
					offset.Width = location.X;
					offset.Height = location.Y + spacing * rect.part;
					p = translateCoordsF( PointF.Add( rect.topLeft, offset ) );
					q = translateCoordsF( PointF.Add( rect.bottomRight, offset ) );
					if( p.X > q.X ) { tmp = p.X; p.X = q.X; q.X = tmp; }
					if( p.Y > q.Y ) { tmp = p.Y; p.Y = q.Y; q.Y = tmp; }
					canvas.DrawRectangle( new Pen( Color.Black, 1 ), p.X, p.Y, q.X - p.X, q.Y - p.Y );
				}
			}

			// Draw pins
			foreach( kSymbol.typ_pin pin in sym.pins )
			{
				if( ( pin.dmg == 0 || pin.dmg == selectDmg ) && ( pin.part == section || pin.part == 0 ))
				{
					float x, y;
					float x1, y1, x2, y2;
					float mX = 0, mY = 0;
					float dX, dY;

					x = pin.position.X;
					y = pin.position.Y;

					if( pin.orientation == 'U' ) { mX = 0f; mY = -1f; }
					if( pin.orientation == 'D' ) { mX = 0f; mY = 1f; }
					if( pin.orientation == 'L' ) { mX = -1f; mY = 0f; }
					if( pin.orientation == 'R' ) { mX = 1f; mY = 0f; }
					dX = mX * pin.length;
					dY = mY * pin.length;

					offset.Width = location.X;
					offset.Height = location.Y + spacing * pin.part;

					if( pin.shape.Contains( "I" ) )             // inverting or normal
					{
						// inverting  -  the line is a little shorter and there is a small circlewhere it meets the body of the component
						float cSize = 0.1f;
						float cLength = 0.9f;
						x1 = x;
						y1 = y;
						x2 = x + dX * ( cLength - cSize );
						y2 = y + dY * ( cLength - cSize );
						p = translateCoordsF( PointF.Add( new PointF( x1, y1 ), offset ) );
						q = translateCoordsF( PointF.Add( new PointF( x2, y2 ), offset ) );
						canvas.DrawLine( new Pen( Color.Green, 1 ), p.X, p.Y, q.X, q.Y );
						x1 = x + dX * cLength - cSize * pin.length;
						y1 = y + dY * cLength - cSize * pin.length;
						x2 = x + dX * cLength + cSize * pin.length;
						y2 = y + dY * cLength + cSize * pin.length;
						p = translateCoordsF( PointF.Add( new PointF( x1, y1 ), offset ) );
						q = translateCoordsF( PointF.Add( new PointF( x2, y2 ), offset ) );
						canvas.DrawArc( new Pen( Color.Green, 1 ), p.X, p.Y, q.X - p.X, q.Y - p.Y, 0, 360 );
					}
					else
					{
						// basic  wire -  just a line from the pin co-ordinate to the body of the component
						p = translateCoordsF( PointF.Add( new PointF( x, y ), offset ) );
						q = translateCoordsF( PointF.Add( new PointF( x + dX, y + dY ), offset ) );
						canvas.DrawLine( new Pen( Color.Green, 1 ), p.X, p.Y, q.X, q.Y );
					}

					if( pin.shape.Contains( "C" ) )             // clock input
					{
						x1 = x + dX + mX * 1.41f * sym.offset;
						y1 = y + dY + mY * 1.41f * sym.offset;
						x2 = x + dX + mY * sym.offset;
						y2 = y + dY + mX * sym.offset;

						p = translateCoordsF( PointF.Add( new PointF( x1, y1 ), offset ) );
						q = translateCoordsF( PointF.Add( new PointF( x2, y2 ), offset ) );
						canvas.DrawLine( new Pen( Color.Green, 1 ), p.X, p.Y, q.X, q.Y );
						x2 = x + dX - mY * sym.offset;
						y2 = y + dY - mX * sym.offset;
						q = translateCoordsF( PointF.Add( new PointF( x2, y2 ), offset ) );
						canvas.DrawLine( new Pen( Color.Green, 1 ), p.X, p.Y, q.X, q.Y );
					}

					// Draw the pin numbers and names
					x1 = x + dX / 2f;               // x+dX is where the wire meets the body of the symbol  x+dX/2 is half way along the wire
					y1 = y + dY / 2f;               // same for y co-ordinate
					x1 += Math.Abs( mY ) * pin.sizeNum * 0.4f;  // arbitrary offset to place the number closer to the wire
					y1 += Math.Abs( mX ) * pin.sizeNum * 0.4f;  // 
					p = translateCoordsF( PointF.Add( new PointF( x1, y1 ), offset ) );

					x1 = x + dX + mX * sym.offset;       // x+dX is where the wire meets the body of the symbol  the extra term is for the offset specified in the symbol definition
					y1 = y + dY + mY * sym.offset;       // same for y co-ordinate
					q = translateCoordsF( PointF.Add( new PointF( x1, y1 ), offset ) );

					GraphicsState state = canvas.Save();                                    // Save the graphics state.
					canvas.ResetTransform();
					canvas.RotateTransform( -90 * Math.Abs( mY ) );                         // Rotate
					canvas.TranslateTransform( p.X, p.Y, MatrixOrder.Append );              // Translate to desired position. Be sure to append  the rotation so it occurs after the rotation.	

					if( sym.pinNumbersShown )
					{
						StringFormat numFormat = new StringFormat();
						numFormat.LineAlignment = StringAlignment.Far;
						numFormat.Alignment = StringAlignment.Center;
						canvas.DrawString                                                   // Draw the text at the origin.
						(
							pin.pin,
							new Font( FontFamily.GenericMonospace, pin.sizeNum * dScale ),
							new SolidBrush( Color.FromArgb( 255, Color.Orange ) ),
							0, 0,
							numFormat
						);
					}
					canvas.TranslateTransform( q.X - p.X, q.Y - p.Y, MatrixOrder.Append );  // Translate to desired position. Here is relative to the previous one to save us having to restore and re trasfomrm also for the rotation since the names have the same rotation	
					if( sym.pinNamesShown && !pin.name.Equals( "~" ) )
					{
						StringFormat nameFormat             = new StringFormat();
						nameFormat.LineAlignment = StringAlignment.Center;
						nameFormat.Alignment = StringAlignment.Far;
						if( mX >= 0 ) nameFormat.Alignment = StringAlignment.Near;
						if( mY > 0 ) nameFormat.Alignment = StringAlignment.Far;

						canvas.DrawString                                                       // Draw the text at the origin.
						(
							pin.name,
							new Font( FontFamily.GenericMonospace, pin.sizeName * dScale ),
							new SolidBrush( Color.FromArgb( 255, Color.Orange ) ),
							0, 0,
							nameFormat
						);
					}
					canvas.Restore( state );
				}
			}

			// Draw fields
			foreach( kSymbol.typ_field f in sym.fields )
			{
				if( f.visible )
				{
					offset.Width = location.X;
					offset.Height = location.Y;
					PointF pos = new PointF();
					pos = f.position;
					if( f.hAlign == 'C' ) pos.X -= f.size * f.text.Length * 0.5f;
					if( f.hAlign == 'R' ) pos.X -= f.size * f.text.Length;
					pos.Y -= f.size * 0.70f;      // to make the baseline as the reference point;
					p = translateCoordsF( PointF.Add( pos, offset ) );
					GraphicsState state = canvas.Save();                                    // Save the graphics state.
					canvas.ResetTransform();
					if( f.labelOrientationVertical ) canvas.RotateTransform( -90 );                         // Rotate
					canvas.TranslateTransform( p.X, p.Y, MatrixOrder.Append );              // Translate to desired position. Be sure to append  the rotation so it occurs after the rotation.	
					if( true )
					{
						canvas.DrawString                                                       // Draw the text at the origin.
						(
							f.text,
							new Font( FontFamily.GenericMonospace, f.size * dScale ),
							new SolidBrush( Color.FromArgb( 255, Color.Orange ) ),
							0, 0
						);
					}
					canvas.Restore( state );
				}
			}

			// Draw Texts
			foreach( kSymbol.typ_text t in sym.texts )
			{
				if( ( t.dmg == 0 || t.dmg == selectDmg ) && t.part == section )
				{
					offset.Width = location.X;
					offset.Height = location.Y + spacing * t.part;

					p = translateCoordsF( PointF.Add( t.position, offset ) );
					StringFormat textFormat             = new StringFormat();
					textFormat.LineAlignment = StringAlignment.Center;
					if( t.vAlign == 'T' ) textFormat.LineAlignment = StringAlignment.Near;
					if( t.vAlign == 'B' ) textFormat.LineAlignment = StringAlignment.Far;

					textFormat.Alignment = StringAlignment.Center;
					if( t.hAlign == 'L' ) textFormat.Alignment = StringAlignment.Near;
					if( t.hAlign == 'R' ) textFormat.Alignment = StringAlignment.Far;

					GraphicsState state = canvas.Save();                                    // Save the graphics state.
					canvas.ResetTransform();
					canvas.RotateTransform( t.angle );                         // Rotate
					canvas.TranslateTransform( p.X, p.Y, MatrixOrder.Append );              // Translate to desired position. Be sure to append  the rotation so it occurs after the rotation.	

					canvas.DrawString                                                       // Draw the text at the origin.
					(
						t.text,
						new Font( FontFamily.GenericMonospace, t.size * dScale ),
						new SolidBrush( Color.FromArgb( 255, Color.Orange ) ),
						0, 0,
						textFormat
					);
					canvas.Restore( state );
				}
			}

			PointF translateCoordsF( PointF schemPoint )
			{
				if( drawScale <= 0 ) 
				{
					return screenCoordFromSchemCoordF( schemPoint );
				}
				else 
				{
					float xS, yS, xB, yB;
					xB = schemPoint.X;
					yB = schemPoint.Y;
					xS = ( xB  ) * ( drawScale );
					yS = ( yB  ) * ( drawScale );
					return new PointF( xS, yS );
				}
			}

		}
		public void drawKSymbolOld( kSymbol sym, Graphics canvas, mainForm form )
		{
			//kSymbol sym = new kSymbol();
			//importKSymbol( ref sym );
			//sym.transform( 0, cW : false, mirrorX : true );
			drawKSymbol( sym, 0, new PointF( 1000,1000), canvas);

			//drawKSymbolOld( canvas, form );
		}
		public void drawKSymbolOlder( Graphics canvas, mainForm form )
		{



			// Construct a new Rectangle .
			Rectangle  displayRectangle =
					new Rectangle (new Point(40, 40), new Size (80, 80));

			// Construct 2 new StringFormat objects
			StringFormat format1 = new StringFormat(StringFormatFlags.NoClip);
			StringFormat format2 = new StringFormat(format1);

			// Set the LineAlignment and Alignment properties for
			// both StringFormat objects to different values.
			format1.LineAlignment = StringAlignment.Near;
			format1.Alignment = StringAlignment.Near;

			format2.LineAlignment = StringAlignment.Far;
			format2.Alignment = StringAlignment.Far;

			// Draw the bounding rectangle and a string for each
			// StringFormat object.
			canvas.DrawRectangle( Pens.Black, displayRectangle );
			canvas.DrawString( "Showing Format1", new Font( FontFamily.GenericMonospace, 8 ),
				Brushes.Red, (RectangleF)displayRectangle, format1 );

		
			format2.LineAlignment = StringAlignment.Far;
			format2.Alignment = StringAlignment.Far;
			canvas.DrawString( "Showing Format2", new Font( FontFamily.GenericMonospace, 8 ),
				Brushes.Red, new PointF(40,40) , format2 );

			int     numParts = 1;
			float   offs = 0;
			string  pinNumbersVisible = "Y";
			string  pinNamesVisible = "Y";
			string  text;
			float   x;
			float   y;
			float   size;
			float   radius;
			float   start;
			float   end;
			int     part;
			int     dmg;
			string  fill;
			float   xStart;
			float   yStart;
			float   xEnd;
			float   yEnd;
			float   angle;
			string  pinName;
			string  pinNum;
			float   sizeNum;
			float   sizeName;
			float   length;
			string  orientation;
			string  visibility;
			string  hidden;
			string  hAlign;
			string  vAlign;
			string  italic;
			string  bold;
			string  type;
			string  shape;


			float   mX = 0;
			float   mY = 0;
			float   dX = 0;
			float   dY = 0;
			float   x1,y1,x2,y2;


			PointF p = new PointF( 0, 0 );
			PointF q = new PointF( 0, 0 );
			float kiScale = 2f;
			float spacing = 2000;
			string a;
			string[] b;
			float tmp;

			string fileName = "D:\\My Documents\\KiCAD\\library\\Analog_Switch.lib";
			//string symbolName = "ADG715";
			string symbolName = "ADG728";

			//string fileName = "D:\\My Documents\\KiCAD\\library\\Analog_DAC.lib";
			//string symbolName = "AD9142";

			//string fileName = "D:\\My Documents\\KiCAD\\library\\Analog.lib";
			//string symbolName = "MPY634KU";
			//string symbolName = "LF398_DIP8";

			//string fileName = "D:\\My Documents\\KiCAD\\library\\Amplifier_Audio.lib";
			//string symbolName = "SSM2018";

			//string fileName = "D:\\Dropbox\\o\\Reenigne\\TestProject\\4xxx.lib";
			//string symbolName = "4016";
			//string symbolName = "4504";
			//string symbolName = "4001";
			//string symbolName = "4538";

			//string fileName = "D:\\My Documents\\KiCAD\\library\\Transistor_BJT.lib";
			//string symbolName = "2N3055";

			//string fileName = "D:\\My Documents\\KiCAD\\library\\device.lib";
			//string symbolName = "Resonator";
			//string symbolName = "R_POT";

			//string fileName = "D:\\My Documents\\KiCAD\\library\\Espressif.lib";
			//string symbolName = "ESP32-WROOM-E";

			int selectDmg = 1;
			StreamReader reader = new StreamReader( fileName );
			bool found = false;
			do
			{
				a = reader.ReadLine();
				b = a.Split( ' ' );
				if( b.Count() > 2 )
				{
					if( b[0] == "DEF" && b[1] == symbolName )
					{
						found = true;
						offs = float.Parse( b[4] );
						pinNumbersVisible = b[5];
						pinNamesVisible = b[6];
						numParts = int.Parse( b[7] );
					}
				}

			} while( !found & !reader.EndOfStream );
			if( found )
			{
				//Console.WriteLine( a );
				found = false;
				do
				{
					a = reader.ReadLine();
					//Console.WriteLine( a );																						
					if( a.Contains( "\"" ) )
					{
						//Debugger.Break();
						bool quote = false;
						for( int i = 0; i < a.Length; i++ )
						{

							if( a.Substring( i, 1 ) == "\"" )
							{
								string pad = "";
								if( a.Substring( i + 1, 1 ) == "\"" ) pad = "-";
								a = a.Substring( 0, i ) + pad + a.Substring( i + 1 );
								quote = !quote;
							}
							if( quote && a.Substring( i + 1, 1 ) == " " )
								a = a.Substring( 0, i + 1 ) + "_" + a.Substring( i + 2 );
						}
					}
					b = a.Split( ' ' );
					if( b.Count() > 0 )
					{
						string recType;
						if( b[0].Length > 0 ) recType = b[0].Substring( 0, 1 ); else recType = "?";
						switch( recType )
						{
							case "D" when b[0].Equals( "DEF" ):
								// this case is dealt with before the switch() - see further up
								break;
							case "F":
								text = b[1].Trim( '"' );
								x = float.Parse( b[2] ) * kiScale;
								y = -float.Parse( b[3] ) * kiScale;
								size = float.Parse( b[4] ) * kiScale;
								orientation = b[5];
								visibility = b[6];
								hAlign = b[7];
								vAlign = b[8].Substring( 0, 1 );
								italic = b[8].Substring( 1, 1 );
								bold = b[8].Substring( 2, 1 );

								if( visibility == "I" ) break;
								if( hAlign == "C" ) x -= size * text.Length * 0.5f;
								if( hAlign == "R" ) x -= size * text.Length;
								y -= size * 0.70f;      // to make the baseline as the reference point;

								p = screenCoordFromSchemCoordF( new PointF( x, y ) );
								GraphicsState state = canvas.Save();                                    // Save the graphics state.
								canvas.ResetTransform();
								if( orientation == "V" ) canvas.RotateTransform( -90 );                         // Rotate
								canvas.TranslateTransform( p.X, p.Y, MatrixOrder.Append );              // Translate to desired position. Be sure to append  the rotation so it occurs after the rotation.	
								if( pinNumbersVisible == "Y" )
								{
									canvas.DrawString                                                       // Draw the text at the origin.
									(
										text,
										new Font( FontFamily.GenericMonospace, size * scale ),
										new SolidBrush( Color.FromArgb( 255, Color.Orange ) ),
										0, 0
									);
								}
								canvas.Restore( state );                                                // Restore the graphics state



								break;
							case "D" when b[0].Equals( "DRAW" ):
								break;
							case "C":
								dmg = int.Parse( b[5] );
								if( dmg == 0 || dmg == selectDmg )
								{
									x = float.Parse( b[1] ) * kiScale;
									y = -float.Parse( b[2] ) * kiScale;
									radius = float.Parse( b[3] ) * kiScale;
									part = int.Parse( b[4] ); if( numParts == 1 ) part = 0;
									p = screenCoordFromSchemCoordF( new PointF( x - radius, ( y - radius ) + spacing * part * kiScale ) );
									q = screenCoordFromSchemCoordF( new PointF( x + radius, ( y + radius ) + spacing * part * kiScale ) );
									canvas.DrawArc( new Pen( Color.Green, 1 ), p.X, p.Y, q.X - p.X, q.Y - p.Y, 0, 360 );
								}
								break;
							case "A":
								//if( form.debugBreak() ) Debugger.Break();
								dmg = int.Parse( b[7] );
								if( dmg == 0 || dmg == selectDmg )
								{
									x = float.Parse( b[1] ) * kiScale;
									y = -float.Parse( b[2] ) * kiScale;
									radius = float.Parse( b[3] ) * kiScale;
									start = -float.Parse( b[4] ) / 10f;
									end = -float.Parse( b[5] ) / 10f;
									part = int.Parse( b[6] ); if( numParts == 1 ) part = 0;
									fill = b[9];
									xStart = float.Parse( b[10] ) * kiScale;
									yStart = -float.Parse( b[11] ) * kiScale;
									xEnd = float.Parse( b[12] ) * kiScale;
									yEnd = -float.Parse( b[13] ) * kiScale;
									p = screenCoordFromSchemCoordF( new PointF( x - radius, ( y - radius ) + spacing * part * kiScale ) );
									q = screenCoordFromSchemCoordF( new PointF( x + radius, ( y + radius ) + spacing * part * kiScale ) );
									canvas.DrawArc( new Pen( Color.Green, 1 ), p.X, p.Y, q.X - p.X, q.Y - p.Y, start, end - start );
								}
								//form.Refresh();
								break;
							case "X":
								//Console.WriteLine( a );
								dmg = int.Parse( b[10] );
								if( dmg == 0 || dmg == selectDmg )
								{
									pinName = b[1];
									pinNum = b[2];
									x = float.Parse( b[3] ) * kiScale;
									y = -float.Parse( b[4] ) * kiScale;
									length = float.Parse( b[5] ) * kiScale;
									orientation = b[6];
									sizeNum = float.Parse( b[7] ) * kiScale;
									sizeName = float.Parse( b[8] ) * kiScale;
									part = int.Parse( b[9] ); if( numParts == 1 ) part = 0;
									type = b[11];
									if( b.Count() > 12 ) shape = b[12]; else shape = "";
									if( orientation == "U" ) { mX = 0f; mY = -1f; }
									if( orientation == "D" ) { mX = 0f; mY = 1f; }
									if( orientation == "L" ) { mX = -1f; mY = 0f; }
									if( orientation == "R" ) { mX = 1f; mY = 0f; }
									dX = mX * length;
									dY = mY * length;

									if( shape.Contains( "I" ) )             // inverting or normal
									{
										// inverting  -  the line is a little shorter and there is a small circlewhere it meets the body of the component
										float cSize = 0.1f;
										float cLength = 0.9f;
										x1 = x;
										y1 = y + spacing * part * kiScale;
										x2 = x + dX * ( cLength - cSize );
										y2 = y + dY * ( cLength - cSize ) + spacing * part * kiScale;
										p = screenCoordFromSchemCoordF( new PointF( x1, y1 ) );
										q = screenCoordFromSchemCoordF( new PointF( x2, y2 ) );
										canvas.DrawLine( new Pen( Color.Green, 1 ), p.X, p.Y, q.X, q.Y );
										x1 = x + dX * cLength - cSize * length;
										y1 = y + dY * cLength - cSize * length + spacing * part * kiScale;
										x2 = x + dX * cLength + cSize * length;
										y2 = y + dY * cLength + cSize * length + spacing * part * kiScale;
										p = screenCoordFromSchemCoordF( new PointF( x1, y1 ) );
										q = screenCoordFromSchemCoordF( new PointF( x2, y2 ) );
										canvas.DrawArc( new Pen( Color.Green, 1 ), p.X, p.Y, q.X - p.X, q.Y - p.Y, 0, 360 );
									}
									else
									{
										// basic  wire -  just a line from the pin co-ordinate to the body of the component
										p = screenCoordFromSchemCoordF( new PointF( x, y + spacing * part * kiScale ) );
										q = screenCoordFromSchemCoordF( new PointF( x + dX, y + dY + spacing * part * kiScale ) );
										canvas.DrawLine( new Pen( Color.Green, 1 ), p.X, p.Y, q.X, q.Y );
									}

									if( shape.Contains( "C" ) )             // clock input
									{
										float cSize = 0.1f;
										x1 = x + dX + mX * 1.41f * offs;
										y1 = y + dY + mY * 1.41f * offs + spacing * part * kiScale;
										x2 = x + dX + mY * offs;
										y2 = y + dY + mX * offs + spacing * part * kiScale;
										p = screenCoordFromSchemCoordF( new PointF( x1, y1 ) );
										q = screenCoordFromSchemCoordF( new PointF( x2, y2 ) );
										canvas.DrawLine( new Pen( Color.Green, 1 ), p.X, p.Y, q.X, q.Y );
										x2 = x + dX - mY * offs;
										y2 = y + dY - mX * offs + spacing * part * kiScale;
										q = screenCoordFromSchemCoord( new PointF( x2, y2 ) );
										canvas.DrawLine( new Pen( Color.Green, 1 ), p.X, p.Y, q.X, q.Y );
									}

									// Pin numbers	and names
									float numWidth = sizeNum * pinNum.Length;								// KLUDGE - need a more accurate way to measure the length of the text	
									x1 = x + dX / 2f;
									x1 -= Math.Abs( mX ) * numWidth / 2f;
									x1 -= Math.Abs( mY ) * ( sizeNum + 20 );                                // KLUDGE		 
									y1 = y + dY / 2f + spacing * part * kiScale;
									y1 -= Math.Abs( mX ) * ( sizeNum + 20 );                                // KLUDGE the '+20' is an arbitrary offset. Haven't figured out yet why it is necessary   
									y1 += Math.Abs( mY ) * numWidth / 2f;
									p = screenCoordFromSchemCoordF( new PointF( x1, y1 ) );

									float nameWidth = sizeName * pinName.Length;
									x1 = x + dX + offs * mX;    //offs / sizeName
									x1 -= ( mX < 0 ) ? nameWidth : 0;
									x1 -= Math.Abs( mY ) * ( sizeName * 0.75f );                            // KLUDGE		 
									y1 = y + dY + offs * mY + spacing * part * kiScale;     //offs / sizeName
									y1 -= Math.Abs( mX ) * ( sizeName * 0.75f );                            // KLUDGE the '0.75' is an arbitrary offset. Haven't figured out yet why it shouldn't be 0.5f   
									y1 += ( mY > 0 ) ? nameWidth : 0;
									q = screenCoordFromSchemCoordF( new PointF( x1, y1 ) );

									state = canvas.Save();                                  // Save the graphics state.
									canvas.ResetTransform();
									canvas.RotateTransform( -90 * Math.Abs( mY ) );                         // Rotate
									canvas.TranslateTransform( p.X, p.Y, MatrixOrder.Append );              // Translate to desired position. Be sure to append  the rotation so it occurs after the rotation.	
									if( pinNumbersVisible == "Y" )
									{
										canvas.DrawString                                                       // Draw the text at the origin.
																		(
																			pinNum,
																			new Font( FontFamily.GenericMonospace, sizeNum * scale ),
																			new SolidBrush( Color.FromArgb( 255, Color.Orange ) ),
																			0, 0
																		);
									}
									canvas.TranslateTransform( q.X - p.X, q.Y - p.Y, MatrixOrder.Append );  // Translate to desired position. Be sure to append  the rotation so it occurs after the rotation.	
									if( pinNamesVisible == "Y" && !pinName.Equals( "~" ) )
									{
										canvas.DrawString                                                       // Draw the text at the origin.
										(
											pinName,
											new Font( FontFamily.GenericMonospace, sizeName * scale ),
											new SolidBrush( Color.FromArgb( 255, Color.Orange ) ),
											0, 0
										);
									}
									canvas.Restore( state );                                                // Restore the graphics state	 
								}
								break;
							case "T":
								dmg = int.Parse( b[7] );
								if( dmg == 0 || dmg == selectDmg )
								{
									angle = float.Parse( b[1] );
									x = float.Parse( b[2] ) * kiScale;
									y = -float.Parse( b[3] ) * kiScale;
									size = float.Parse( b[4] ) * kiScale;
									hidden = b[5];
									part = int.Parse( b[6] );
									text = b[8];
									italic = b[9];
									bold = b[10];
									hAlign = b[11];
									vAlign = b[12];

									if( hAlign == "C" ) x -= size * text.Length * 0.5f;
									if( hAlign == "R" ) x -= size * text.Length;
									y -= size * 0.70f;      // to make the baseline as the reference point;

									float nameWidth = size * text.Length;
									p = screenCoordFromSchemCoordF( new PointF( x, y ) );

									state = canvas.Save();                                    // Save the graphics state.
									canvas.ResetTransform();
									canvas.RotateTransform( angle / 10f );                         // Rotate
									canvas.TranslateTransform( p.X, p.Y, MatrixOrder.Append );              // Translate to desired position. Be sure to append  the rotation so it occurs after the rotation.	
									if( pinNumbersVisible == "Y" )
									{
										canvas.DrawString                                                       // Draw the text at the origin.
																		(
																			text,
																			new Font( FontFamily.GenericMonospace, size * scale ),
																			new SolidBrush( Color.FromArgb( 255, Color.Orange ) ),
																			0, 0
																		);
									}
									canvas.Restore( state );                                                // Restore the graphics state
																											//FontFamily.GenericMonospace.GetEmHeight()
								}
								break;
							case "P":       // Polyline
								dmg = int.Parse( b[3] );
								if( dmg == 0 || dmg == selectDmg )
								{
									part = int.Parse( b[2] ); if( numParts == 1 ) part = 0;
									int numPoints   =  int.Parse( b[1] );
									x1 = float.Parse( b[5] ) * kiScale;
									y1 = -float.Parse( b[6] ) * kiScale;
									p = screenCoordFromSchemCoordF( new PointF( x1, y1 + spacing * part * kiScale ) );
									for( int i = 1; i < numPoints; i++ )
									{
										x1 = float.Parse( b[5 + i * 2] ) * kiScale;
										y1 = -float.Parse( b[6 + i * 2] ) * kiScale;
										q = screenCoordFromSchemCoordF( new PointF( x1, y1 + spacing * part * kiScale ) );
										canvas.DrawLine( new Pen( Color.Black, 1 ), p.X, p.Y, q.X, q.Y );
										p = q;
									}
								}
								break;
							case "S":
								dmg = int.Parse( b[6] );
								if( dmg == 0 || dmg == selectDmg )
								{
									part = int.Parse( b[5] ); if( numParts == 1 ) part = 0;
									x1 = float.Parse( b[1] ) * kiScale;
									y1 = -float.Parse( b[2] ) * kiScale;
									x2 = float.Parse( b[3] ) * kiScale;
									y2 = -float.Parse( b[4] ) * kiScale;
									p = screenCoordFromSchemCoordF( new PointF( x1, y1 + spacing * part * kiScale ) );
									q = screenCoordFromSchemCoordF( new PointF( x2, y2 + spacing * part * kiScale ) );
									if( p.X > q.X ) { tmp = p.X; p.X = q.X; q.X = tmp; }
									if( p.Y > q.Y ) { tmp = p.Y; p.Y = q.Y; q.Y = tmp; }
									canvas.DrawRectangle( new Pen( Color.Black, 1 ), p.X, p.Y, q.X - p.X, q.Y - p.Y );
									//form.Refresh();
								}
								break;
							case "E" when b[0].Equals( "ENDDRAW" ):
								found = true;
								break;
							case "E" when b[0].Equals( "ENDDDEF" ):
								break;
							default:
								Console.WriteLine( "Unrecognised line: {0}", a );
								break;
						}
					}

				} while( !found & !reader.EndOfStream );
			}
			reader.Close();

			int grum( int slackom ) { int ddx=1; return ddx; };
		}

		// displaying the schematic and adjustments like pan, zoom and other
		public void move( float deltaX, float deltaY )
		{   // Moves the image by an amount equivalent to the given screen pixel amounts specified
			offsetX +=  deltaX / scale;
			offsetY +=  deltaY / scale;
		}
		public void setScaleIndex( int scaleIndex, int mouseX, int mouseY, int width, int height )
		{   // changes the scale while also adjusting the offsets to make sure the zooming action is centered on the mouse pointer
			float oldScale = scale;
			//scale = 1;
			//if( scaleIndex != 0 )
			scale = (float)Math.Pow( (double)10.0, (double)( scaleIndex / 100.0 ) ) / 100;
			offsetX -= mouseX / oldScale;
			offsetY -= mouseY / oldScale;
			offsetX += mouseX / scale;
			offsetY += mouseY / scale;
			Console.WriteLine( "Scale changed from {0} to {1}", oldScale, scale );
		}
		public void drawSchematic( Graphics canvas, List<component> componentss )
		{
			float x, y, width,height;

			buildNetList( componentss );

			canvas.Clear( SystemColors.Control );

			// Draw the components and their pins
			foreach( component component in componentss )
			{
				int a = componentss.IndexOf( component );
				if( component.symbol == null )                             
				{
					for( int i = 0; i < component.sections.Count(); i++ )
					{
						PointF p,q;
						if ( a == mainForm.componentSelectorIndex )
						{
							p = screenCoordFromSchemCoordF( component.sections[i].absBoundBox.Location );
							q = screenCoordFromSchemCoordF( new PointF( component.sections[i].absBoundBox.Right, component.sections[i].absBoundBox.Bottom ) );
							canvas.FillRectangle
							(
								new SolidBrush( Color.Yellow ), //58.87f, 47.09f, 400.33f, 36.39f
								p.X, p.Y,
								q.X - p.X, q.Y - p.Y
							);
						}
						width = scale * 150;      // KLUDGE
						height = scale * 150;     // KLUDGE

						// Draw the pins
						foreach( component.pin pin in component.pins )
						{
							if( pin.section[0] == i )
							{
								x = component.sections[i].position.X + pin.schemPosition.X;
								y = component.sections[i].position.Y + pin.schemPosition.Y;
								p = screenCoordFromSchemCoordF( new PointF( x, y ) );
								canvas.DrawRectangle
								(
									new Pen( Color.Black, 3 ),
									p.X - width / 2, p.Y - height / 2,
									width, height
								);
								canvas.DrawString( pin.pinNumber, new Font( FontFamily.GenericMonospace, height / 2 ), new SolidBrush( Color.FromArgb( 255, Color.Blue ) ), p.X - width / 2, p.Y - height / 2 );
							}
						}
						// draw the bounding box
						RectangleF r1 = component.sections[i].boundBox;
						//r1.Inflate( width*3, height*3 );
						r1.Offset( component.sections[i].position );
						p = screenCoordFromSchemCoordF( new PointF( r1.Left, r1.Top ) );
						q = screenCoordFromSchemCoordF( new PointF( r1.Right, r1.Bottom ) );
						canvas.DrawRectangle( new Pen( Color.Green, 3 ), p.X, p.Y, q.X - p.X, q.Y - p.Y );						
					}
				}
				else       // of: if( component.symbol == null )  
				{
					for( int i = 0; i < component.sections.Count(); i++ )
					{
						PointF p,q;
						if( a == mainForm.componentSelectorIndex )
						{
							p = screenCoordFromSchemCoordF( component.sections[i].absBoundBox.Location );
							q = screenCoordFromSchemCoordF( new PointF( component.sections[i].absBoundBox.Right, component.sections[i].absBoundBox.Bottom ) );
							canvas.FillRectangle
							(
								new SolidBrush( Color.Yellow ), //58.87f, 47.09f, 400.33f, 36.39f
								p.X, p.Y,
								q.X - p.X, q.Y - p.Y
							);
						}
						drawKSymbol( component.symbol, i, component.sections[i].position, canvas );
					}					
				}
			} // end of drawing components and their pins

			// Draw the manually drawn connections
			foreach ( netElement nE in netElements )
			{
				Pen pen = new Pen( Color.White, 4 );
				if( mainForm.netSelector == nE.netNum ) pen = new Pen( Color.Red, 6 ); 
				for( int i = 0; i < 2; i++ )	// loop is so that first the thick white line is drawn then thinner black line over it
				{
					foreach( connectionLine cL in nE.connectionLines )
					{
						if( cL.points.Count() >= 2 )
						{
							PointF p1,p2;
							for( int j = 1; j < cL.points.Count(); j++ )
							{
								p1 = screenCoordFromSchemCoordF( cL.points[j - 1] );
								p2 = screenCoordFromSchemCoordF( cL.points[j] );
								canvas.DrawLine( pen, p1, p2 );
							}
						}
					}
					pen = new Pen( Color.Black, 2 );
					if( mainForm.netSelector == nE.netNum ) pen = new Pen( Color.Yellow, 2 );
				}
				if( mainForm.netSelector == nE.netNum )
				{
					PointF p;
					foreach( connectionLine cL in nE.connectionLines )
					{
						for( int j = 0; j < cL.points.Count(); j++ )
						{
							p = screenCoordFromSchemCoordF( cL.points[j] );
							canvas.FillEllipse( new SolidBrush( Color.FromArgb( 255, Color.DarkGreen  ) ), p.X - 6, p.Y - 6, 12, 12 );
						}
					}
				}
			}

			// Draw the wires from the components to the net centroids
			foreach( component component in componentss )
			{
				for( int i = 0; i < component.sections.Count(); i++ )
				{
					x = component.sections[i].position.X;
					y = component.sections[i].position.Y;
					PointF p,q;

					foreach( component.pin pin in component.pins )
					{
						if( pin.net != 0 )
						{
							int j = netElements.FindIndex( r => r.netNum == pin.net );
							if( j >= 0 )
							{
								if( pin.section[0] == i )
								{
									x = component.sections[i].position.X + pin.schemPosition.X;
									y = component.sections[i].position.Y + pin.schemPosition.Y;

									p = screenCoordFromSchemCoordF( new PointF( x, y ) );
									q = screenCoordFromSchemCoordF( netElements[j].nearestPoint( new PointF( x, y ) ) );
									Pen pen = new Pen( Color.Green , 1 );
									if( mainForm.netSelector == netElements[j].netNum  ) pen = new Pen( Color.Red , 3 );

									//q = screenCoordFromSchemCoordF( new PointF( netElements[j].leftest, netElements[j].downest )  );
									canvas.DrawLine
									(
										pen,
										p.X, p.Y,
										q.X, q.Y
									); 
								}
							}
						}
					}
				}

			}

			// Draw the net centroids
			foreach( netElement netEl in netElements )
			{
				PointF p;
				p = screenCoordFromSchemCoordF( netEl.centroid );
				canvas.FillEllipse( new SolidBrush( Color.FromArgb( 255, Color.Red ) ), p.X - 3, p.Y - 3, 6, 6 );
			}

			// Draw the 'missing' lines between disjoint manually drawn segments
			foreach( netElement nE in netElements )
			{
				Pen pen = new Pen( Color.Green , 1 );

				// find the connection line and point within it that is closest to the centroid
				// draw a line from it to the centroid and
				// mark the connectionline as isConnected
				if ( nE.connectionLines.Count > 0 )
				{
					int closestLineIndex    = -1;
					int closestPointIndex   = -1;
					float shortestDistance  = float.MaxValue;
					for ( int cLIndex = 0; cLIndex < nE.connectionLines.Count; cLIndex++  )
					{
						nE.connectionLines[cLIndex].isConnected = false; // reset the isConnected of each connection line
						connectionLine cL = nE.connectionLines[cLIndex];
						for ( int pIndex = 0; pIndex < cL.points.Count(); pIndex++ )
						{
							if( glob.distanceBetweenPoints( cL.points[pIndex], nE.centroid ) < shortestDistance )
							{
								shortestDistance	= glob.distanceBetweenPoints( cL.points[pIndex], nE.centroid );
								closestLineIndex	= cLIndex;
								closestPointIndex	= pIndex;
							} 
						}
					}
					nE.connectionLines[closestLineIndex].isConnected = true;   // set the isConnected of the one connectionline that is closest to the centroid
					PointF p1,p2;
					p1 = screenCoordFromSchemCoordF( nE.centroid  );
					p2 = screenCoordFromSchemCoordF( nE.connectionLines[closestLineIndex].points[closestPointIndex] );
					pen = new Pen( Color.Green , 1 );
					if( mainForm.netSelector == nE.netNum ) pen = new Pen( Color.Red, 3 );
					canvas.DrawLine( pen, p1, p2 );
				}

				// Go through all connectionlines that are not connected, each time finding the one
				// and the point within it that is closest to an already connected connection line
				// draw a line between the two closest points
				// and mark the connection line as connected
				// and repeat until no more connectionlines remain unconnected
				bool allConnected = false;
				while ( !allConnected )
				{
					int closestLineIndex1		= -1;		// index for the unconnected line that is trying to connect
					int closestPointIndex1		= -1;		// index for the point within the unconnected line that is trying to connect
					int closestLineIndex2		= -1;		// index of the already connected line to which the unconnected one shall connect
					int closestPointIndex2		= -1;		// index of the point within the already connected line
					float shortestDistance		= float.MaxValue;

					allConnected = true;

					for ( int cLI1 = 0; cLI1 < nE.connectionLines.Count(); cLI1++ ) // iterate throught the connectionlines looking for unconneced ones
					{
						connectionLine cL = nE.connectionLines[cLI1];
						if ( cL.isConnected == false )
						{
							allConnected = false;
							connectionLine cL1 = nE.connectionLines[cLI1];
							// Now search through the connected lines to find the one (and point within it) that is closest to a point on this unconnected line
							for ( int cLI2 = 0; cLI2 < nE.connectionLines.Count(); cLI2++ )
							{
								if ( nE.connectionLines[cLI2].isConnected )
								{
									connectionLine cL2 = nE.connectionLines[cLI2];
									// Go through each point in the unconnected line and find the closest point within the connected line currently being iterated
									for ( int cPI1 = 0; cPI1 < cL1.points.Count(); cPI1++ )
									{
										for ( int cPI2 = 0; cPI2 < cL2.points.Count(); cPI2++ )
										{
											float distance = glob.distanceBetweenPoints( cL1.points[cPI1], cL2.points[cPI2] );
											if ( distance < shortestDistance )
											{
												closestLineIndex1	= cLI1;
												closestPointIndex1	= cPI1;
												closestLineIndex2	= cLI2;
												closestPointIndex2	= cPI2;
												shortestDistance	= distance;
											}
										}
									}
								}
							}  // end of searching for the closest pair of points
							   // Mark the unconnected line found to have the closest point as connected
							nE.connectionLines[closestLineIndex1].isConnected = true;
							PointF p1,p2;
							p1 = screenCoordFromSchemCoordF( nE.connectionLines[closestLineIndex1].points[closestPointIndex1] );
							p2 = screenCoordFromSchemCoordF( nE.connectionLines[closestLineIndex2].points[closestPointIndex2] );
							canvas.DrawLine( pen, p1, p2 );
						}
					}
				}

				//	//if( mainForm.netSelector ==  ) pen = new Pen( Color.Red, 3 );		// ToDo fix this to hilite connection if selected
				//	PointF lastPoint =nE.centroid;
				//foreach( connectionLine cL in nE.connectionLines )
				//{
				//	cL.isConnected = false;				
				//}
				//bool unconnectedLines = false;
				//while ( unconnectedLines )
				//{
				//		if( cL.points.Count() > 0 )
				//		{
				//			PointF p1,p2;
				//			p1 = screenCoordFromSchemCoordF( lastPoint );
				//			p2 = screenCoordFromSchemCoordF( cL.points[0] );
				//			canvas.DrawLine( pen, p1, p2 );
				//			lastPoint = cL.points[cL.points.Count() - 1];
				//		}		
				//}
			}
		}

		// File load and save
		public void writeNetElements( StreamWriter writer )
		{
			int numNetElements = netElements .Count();
			writer.WriteLine( "startNetElements" );
			writer.WriteLine( "numNetElements,{0}", numNetElements );
			for( int i = 0; i < numNetElements; i++ )
			{
				netElement nE = netElements[i];
				writer.Write	( "netNum,{0},",		nE.netNum		 );
				writer.Write	( "netName,{0},",		nE.netName		 );
				writer.Write	( "anchorPoint.X,{0},", nE.anchorPoint.X );
				writer.Write	( "anchorPoint.Y,{0},",	nE.anchorPoint.Y );
				writer.Write    ( "layer,{0},",			nE.layer		 );
				writer.Write    ( "centroid,{0},",		nE.centroid		 );
				writer.WriteLine( "lockCentroid,{0}",	nE.lockCentroid	 );
				int numConnectionLines = nE.connectionLines.Count();
				writer.WriteLine( "numConnectionLines,{0}", numConnectionLines );
				for( int j = 0; j < numConnectionLines; j++ )
				{
					writer.Write( "numPoints,{0},", nE.connectionLines[j].points.Count() );
					writer.Write( "points," );
					foreach( PointF point in nE.connectionLines[j].points ) writer.Write( "{0},{1},",point.X, point.Y );
					writer.WriteLine( "" );
				}
			}
			writer.WriteLine( "endNetElements" );
		}
		public void readNetElements ( StreamReader reader )
		{
			int numNetElements = 0;
			string a;
			string[] b;
			int p;
			netElements.Clear();
			{
				// Get number of netElements
				a = reader.ReadLine();
				a = reader.ReadLine(); b = a.Split( ',' ); p = 0;
				numNetElements = readInt( b[p++], b[p++], "numNetElements" );
				//if( !b[0].Equals( "numNetElements" ) ) causeException();
				//numNetElements = int.Parse( b[1] );
				Console.WriteLine( "Reading {0} netElements", numNetElements );
				for( int i = 0; i < numNetElements; i++ )
				{
					netElement netElement = new netElement( 0 );
					a = reader.ReadLine(); b = a.Split( ',' ); p = 0;
					netElement.netNum		 =    readInt( b[p++], b[p++],			"netNum"		);
					netElement.netName		 = readString( b[p++], b[p++],			"netName"		);
					netElement.anchorPoint.X =  readFloat( b[p++], b[p++],			"anchorPoint.X" );
					netElement.anchorPoint.Y =  readFloat( b[p++], b[p++],			"anchorPoint.Y" );
					netElement.layer		 =    readInt( b[p++], b[p++],			"layer"			);
					netElement.centroid		 = readPointF( b[p++], b[p++], b[p++],	"centroid"		);
					netElement.lockCentroid  =   readBool( b[p++], b[p++],			"lockCentroid"	);

					a = reader.ReadLine(); b = a.Split( ',' ); p = 0;
					int numConnectionLines       =   readInt( b[p++], b[p++], "numConnectionLines"        );
					netElement.connectionLines = new List<connectionLine>();
					for( int j = 0; j < numConnectionLines; j++ )
					{
						a = reader.ReadLine(); b = a.Split( ',' ); p = 0;
						int numPoints = readInt( b[p++], b[p++], "numPoints" );
						connectionLine cL = new connectionLine();
						p++;
						for( int k = 0; k < numPoints; k++ ) cL.points.Add( new PointF( float.Parse( b[p++] ), float.Parse( b[p++] ) ) );
						netElement.connectionLines.Add( cL );
					}

					netElements.Add( netElement );
				}
				a = reader.ReadLine();
				b = a.Split( ',' );
				if( !b[0].Equals( "endNetElements" ) ) causeException( "Read mismatch endNetElements : " + b[0] );
			}

			string readString( string tag, string value, string fieldName )
			{
				if( tag != fieldName ) causeException( "Read mismatch. Wanted " + fieldName + ", got " + tag ); ;
				return value;
			}
			char readChar( string tag, string value, string fieldName )
			{
				if( tag != fieldName ) causeException( "Read mismatch. Wanted " + fieldName + ", got " + tag ); ;
				return char.Parse( value );
			}
			int readInt( string tag, string value, string fieldName )
			{
				if( tag != fieldName ) causeException( "Read mismatch. Wanted " + fieldName + ", got " + tag ); ;
				return int.Parse( value );
			}
			float readFloat( string tag, string value, string fieldName )
			{
				if( tag != fieldName ) causeException( "Read mismatch. Wanted " + fieldName + ", got " + tag ); ;
				return float.Parse( value );
			}
			bool readBool( string tag, string value, string fieldName )
			{
				if( tag != fieldName ) causeException( "Read mismatch. Wanted " + fieldName + ", got " + tag ); ;
				if( value != "True" && value != "False" ) causeException( "Value error. Wanted " + fieldName + ", got " + value ); ;
				return ( value == "True" );
			}
			PointF readPointF( string tag, string valueX, string valueY, string fieldName )
			{
				if( tag != fieldName ) causeException( "Read mismatch. Wanted " + fieldName + ", got " + tag );
				float x,y;
				char[] charsToTrim = { '{', 'X', '=', ' ', 'Y', '}' };
				x = float.Parse( valueX.Trim( charsToTrim ) );
				y = float.Parse( valueY.Trim( charsToTrim ) );
				return new PointF( x, y );
			}
		}

		// other things not yet categorized 
		void buildNetList( List<component> components )
		{
			foreach( netElement netEl in netElements )
			{
				if( !netEl.lockCentroid )
				{
					netEl.centroid = new PointF( 0, 0 );
				}
				netEl.centroidSum	= new PointF( 0, 0 ); ;
				//netEl.netNum		= 0;
				netEl.numConn		= 0;
			}

			foreach( component comp in components)
			{
				for( int i = 0; i < comp.sections.Count(); i++ )
				{	
					foreach( component.pin pin in comp.pins)
					{

						if( pin.section[0] == i )
						{
							PointF tmpPos1 = new PointF( comp.sections[i].position.X, comp.sections[i].position.Y );
							PointF tmpPos2 = new PointF( pin.schemPosition.X, pin.schemPosition.Y );
							if( pin.net != 0 )
							{
								int j = netElements.FindIndex( r => r.netNum == pin.net );
								if( j == -1 )
								{
									Debugger.Break();
									netElement netEl = new netElement( pin.net );
									netElements.Add( netEl );
								}
								else
								{
									netElements[j].addPin( tmpPos1, tmpPos2 );
								}
							}  
						}
					}
				}
			}
				
		}
		public void circuitDragItemsInSelections( List<circuitSelections> selectionList, List<component> componentss, float deltaX, float deltaY )
		{
			// Move the component sections
			foreach( circuitSelections selection in selectionList )
			{
				foreach( circuitSelections.componentSection componentSection in selection.componentSectionList )
				{

					var p1 = componentSection.initialPosition;
					componentSection.finalPosition = new PointF( p1.X + deltaX, p1.Y + deltaY );
				}
			}
			foreach( circuitSelections selection in selectionList )
			{
				foreach( circuitSelections.componentSection componentSection in selection.componentSectionList )
				{
					var p =  componentSection.finalPosition;
					componentss[componentSection.componentNum].sections[componentSection.sectionNum].position = p;
				}
			}


			// move the connectionline points
			foreach( circuitSelections selection in selectionList )
			{
				foreach( circuitSelections.connectionPoint connectionPoint in selection.connectionPointList )
				{
					var p1 = connectionPoint.initialPosition;
					connectionPoint.finalPosition = new PointF( p1.X + deltaX, p1.Y + deltaY );
				}
			}
			foreach( circuitSelections selection in selectionList )
			{
				foreach( circuitSelections.connectionPoint connectionPoint in selection.connectionPointList )
				{
					var p =  connectionPoint.finalPosition;
					netElements[connectionPoint.netNum].connectionLines[connectionPoint.connectionLineNumber].points[connectionPoint.pointNumber] = p;
				}
			}

			// move the ccentroids
			foreach( circuitSelections selection in selectionList )
			{
				foreach( circuitSelections.centroid  centroid in selection.centroidList )
				{
					var p1 = centroid.initialPosition;
					centroid.finalPosition = new PointF( p1.X + deltaX, p1.Y + deltaY );
				}
			}
			foreach( circuitSelections selection in selectionList )
			{
				foreach( circuitSelections.centroid centroid in selection.centroidList )
				{
					var p =  centroid.finalPosition;
					netElements[centroid.netNum].centroid  = p;
				}
			}

			// Update the co-ordinates of the selection boxes
			//foreach( RectangleF r in selections )
			for (int i = 0; i < selectionList.Count(); i++ )
			{

				selectionList[i].selectionBox  = new RectangleF
									(
										selectionList[i].initialX + deltaX,
										selectionList[i].initialY + deltaY,
										selectionList[i].selectionBox.Width,
										selectionList[i].selectionBox.Height 
									);
			}
		}
		public void circuitSelectItemsInFrame( List<component> componentss, List<circuitSelections> selectionList, RectangleF selectionRectangle )
		{
			circuitSelections selection = new circuitSelections();
			selection.selectionBox = selectionRectangle;
			
			//Select component sections
			for( int i = 0; i < componentss.Count(); i++ )
			{
				for( int j = 0; j < componentss[i].sections.Count(); j++ )
				{
					if(  selectionRectangle.Contains( componentss[i].sections[j].absBoundBox ) )
					{
						var sec = new circuitSelections.componentSection();
						sec.componentNum = i;
						sec.sectionNum = j;
						sec.initialPosition = componentss[i].sections[j].position;
						sec.finalPosition = componentss[i].sections[j].position;
						selection.componentSectionList.Add( sec ); 
					}
				}
			}

			// Select points in connection lines
			for( int i = 0; i < netElements.Count(); i++ )
			{
				for( int j = 0; j < netElements[i].connectionLines.Count(); j++ )
				{
					for( int k = 0; k < netElements[i].connectionLines[j].points.Count(); k++ )
					{
						if( selectionRectangle.Contains( netElements[i].connectionLines[j].points[k] ) )
						{
							var cP = new circuitSelections.connectionPoint();
							cP.netNum = i;
							cP.connectionLineNumber = j;
							cP.pointNumber = k;
							cP.initialPosition = netElements[i].connectionLines[j].points[k];
							cP.finalPosition = netElements[i].connectionLines[j].points[k];
							selection.connectionPointList.Add( cP ); 
						}
					}
				}
			}

			// Select connection centroids
			for( int i = 0; i < netElements.Count(); i++ )
			{
				if( selectionRectangle.Contains( netElements[i].centroid ) )
				{
					var cP = new circuitSelections.centroid();
					cP.netNum = i;
					cP.initialPosition = netElements[i].centroid;
					cP.finalPosition = netElements[i].centroid;
					selection.centroidList.Add( cP );
				}
			}

			selectionList.Add( selection );
		}
		public void circuitSelectionReInit( List<circuitSelections> selectionList )
		{	// This is called when dragging ends. It resets the initial position to the new location
			foreach( circuitSelections cS in selectionList  )
			{
				cS.initialX = cS.selectionBox.X;
				cS.initialY = cS.selectionBox.Y;
				foreach( circuitSelections.componentSection comp in cS.componentSectionList )
				{
					comp.initialPosition = comp.finalPosition;
				}
				foreach( circuitSelections.connectionPoint cPoint in cS.connectionPointList )
				{
					cPoint.initialPosition = cPoint.finalPosition;
				}
				foreach( circuitSelections.centroid centroid in cS.centroidList )
				{
					centroid.initialPosition = centroid.finalPosition;
				}
			}
		}

		// Helper functions
		public PointF	screenCoordFromSchemCoordF( PointF schemPoint  )
		{
			float xS, yS, xB, yB;
			xB = schemPoint.X;
			yB = schemPoint.Y;
			xS = ( xB + offsetX ) * ( scale  );
			yS = ( yB + offsetY ) * ( scale  );
			return new PointF( xS, yS );
		}
		public Point	screenCoordFromSchemCoord ( PointF schemPoint  )
		{
			PointF p = screenCoordFromSchemCoordF(schemPoint);
			return new Point( (int)p.X, (int)p.Y );
		}
		public PointF	schemCoordFromScreenCoordF( PointF screenPoint )
		{
			float xS, yS, xB, yB;
			xS = screenPoint.X;
			yS = screenPoint.Y;
			xB = ( -offsetX ) + xS / ( scale );
			yB = ( -offsetY ) + yS / ( scale );
			return new PointF( xB, yB );

			//  xS = (xB + offsetX + correctionX) * (scale * correctionScale);
			//  p  = (x  +    r    +     s      ) * (t     *    u      );
			// using https://www.mathpapa.com/algebra-calculator.html
			//  xB = (-offsetX * scale * correctionScale - correctionX * scale * correctionScale + xS) / (scale * correctionScale);

		}
		public Point	schemCoordFromScreenCoord ( PointF screenPoint )
		{
			PointF p = schemCoordFromScreenCoordF(screenPoint);
			return new Point( (int)p.X, (int)p.Y );
		}

		// generic functions
		public static void causeException() { causeException( "Not specified" ); }
		public static void causeException( string errorMessage ) //KLUDGE development only
		{
			Console.WriteLine( "Exception thrown: {0}", errorMessage );
			object o2 = null;
			int i2 = (int)o2;   // Error
		}
		public static int GetEnumFromDescription( string description, Type enumType )
		{
			foreach( var field in enumType.GetFields() )
			{
				if( field.Name == description )
					return (int)field.GetValue( null );
			}
			return 0;
		}
	}
}
