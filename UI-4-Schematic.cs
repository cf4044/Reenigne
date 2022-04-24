using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Reenigne
{
#pragma warning disable IDE1006
#pragma warning disable IDE0017 // Simplify object initialization


	public partial class mainForm : Form
	{
		// Actions related to the schematic diagram picture box - scrolling, zooming etc
		private void circuitPictureBox_MouseDown( object sender, MouseEventArgs e )
		{
			if( e.Button == MouseButtons.Middle )   // pressed the moushweel button to start dragging the image
			{
				mouseWheelPressed = true;
				mouseWheelStartX  = e.X;
				mouseWheelStartY  = e.Y;
				Console.WriteLine( "Button pressed {0} : startX {1} : startY {2}", e.Button, mouseWheelStartX, mouseWheelStartY );
			}
			if( e.Button == MouseButtons.Left   )   // pressed the left button 
			{
				mouseLeftPressed  = true;
				PointF schemPoint = schem.schemCoordFromScreenCoordF( new PointF( e.X, e.Y ) );
				mouseLeftStartX   = schemPoint.X;
				mouseLeftStartY   = schemPoint.Y;

				switch( circuitMouseMode )
				{
					case circuitMouseModes.mainMode:
						
						// Check for a component being selected
						int tmpComponentSelctorIndex = componentSelectorIndex;
						int tmpSectionSelectorIndex  = sectionSelectorIndex;
						componentSelectorIndex = -1;
						sectionSelectorIndex = -1;
						foreach( component comp in traceHandler.componentss )
						{
							foreach( component.section sec in comp.sections )
							{
								if( sec.absBoundBox.Contains( schemPoint ) )
								{   // set up to start dragging the component
									componentSelectorIndex = traceHandler.componentss.IndexOf( comp );
									sectionSelectorIndex   = traceHandler.componentss[componentSelectorIndex].sections.IndexOf( sec );
									Console.WriteLine( "Selected component {0}, section {1}", componentSelectorIndex, sectionSelectorIndex );
									circuitChangeMouseMode( circuitMouseModes.dragComponent );
								}
							}
						}
						if( tmpComponentSelctorIndex != componentSelectorIndex || tmpSectionSelectorIndex != sectionSelectorIndex )
						{
							if( componentSelectorIndex >= 0 )
							{
								//textBoxDesigType.Text = traceHandler.componentss[componentSelectorIndex].desigType;
								//textBoxDesigNum.Text  = traceHandler.componentss[componentSelectorIndex].desigNum.ToString();
								//textBoxDesigPart.Text = sectionSelectorIndex.ToString();
								if( checkBoxCenterOnSelection.Checked )
								{
									boardImage.offsetX =
										0.5f * boardPictureBox.Width / boardImage.scale
										- traceHandler.componentss[componentSelectorIndex].position.X;
									boardImage.offsetY =
										0.5f * boardPictureBox.Height / boardImage.scale
										- traceHandler.componentss[componentSelectorIndex].position.Y;
								}
							}
							else
							{
								//textBoxDesigType.Text = "--";
								//textBoxDesigNum.Text  = "--";
								//textBoxDesigPart.Text = "--";
							}
							redrawBoards();
							schem.drawSchematic( schemGraphics, traceHandler.componentss /* quick: true*/ );
							Refresh();
						}

						// Check for a centroid being selected
						int tmpNetSelector = netSelector;
						centroidSelectorIndex = -1;
						netSelector = 0;
						netSelectorIndex = -1;
						foreach( schematic.netElement netEl in schematic.netElements )
						{
							if( distanceBetween( schem.screenCoordFromSchemCoordF( netEl.centroid ), new PointF( e.X, e.Y ) ) < 4f )                // Kludge - replace the literal 4 with a configurable parameter
							{
								netSelectorIndex = schematic.netElements.IndexOf( netEl );
								netSelector = netEl.netNum;
								Console.WriteLine( "Selected centroid of net {0}", netSelector );
								centroidSelectorIndex = netSelectorIndex;
								circuitChangeMouseMode( circuitMouseModes.dragCentroid );
							}
						}

						// Check for a net connection line being selected
						foreach( schematic.netElement nE in schematic.netElements )
						{
							foreach( schematic.connectionLine cL in nE.connectionLines )
							{
								if( cL.points.Count() >= 2 )
								{
									PointF p1,p2;
									for( int j = 1; j < cL.points.Count(); j++ )
									{
										p1 = cL.points[j - 1];
										p2 = cL.points[j];
										if( glob.distancePointFromLine( schemPoint, p1, p2 ) < 25f )    // kludge - the constant needs to be replaced with a configurable value or dynamically changed according to scale
										{
											netSelectorIndex	= schematic.netElements.IndexOf( nE );
											netSelector			= nE.netNum;
										}
									}
								}
							}
						}

						// Check for a point in connection line being selected
						foreach( schematic.netElement nE in schematic.netElements )
						{
							foreach( schematic.connectionLine cL in nE.connectionLines )
							{
								if( cL.points.Count() >= 2 )
								{
									PointF p1;
									for( int j = 0; j < cL.points.Count(); j++ )	  // changed j=1 to j=0 26 mar 22 because first point was not being selected - ToDo: need to check if this correction has unintended side effects
									{
										p1 = cL.points[j];
										if( glob.distanceBetweenPoints( schemPoint, p1 ) < 10f / schem.scale  )    // kludge - the constant needs to be replaced with a configurable value
										{
											netSelectorIndex = schematic.netElements.IndexOf( nE );
											netSelector = nE.netNum;
											//selectedConnectionPoint = cL.points[j];
											selectedConnectionPointIndex[0] = schematic.netElements.IndexOf( nE );
											selectedConnectionPointIndex[1] = schematic.netElements[selectedConnectionPointIndex[0]].connectionLines.IndexOf( cL  );
											selectedConnectionPointIndex[2] = j;
											circuitChangeMouseMode( circuitMouseModes.editConnectionLine );
										}
									}
								}
							}
						}

						// Redraw if selections have changed
						if( tmpNetSelector != netSelector )
						{
							redrawBoards();
							schem.drawSchematic( schemGraphics, traceHandler.componentss /* quick: true*/ );
							Refresh();
						}

						// if nothing remains selected initiate a drag select
						if
						(
							componentSelectorIndex == -1
							&& sectionSelectorIndex == -1
							&& netSelector == 0
							&& netSelectorIndex == -1
						)
						{
							circuitChangeMouseMode( circuitMouseModes.dragSelecting );
						}
						break;
					
					case circuitMouseModes.drawConnectionLine:
						drawingConnectionAddPoint( sender, e );
						break;

					case circuitMouseModes.draggingSelection:
						foreach( circuitSelections cS in circuitSelections )
						{
							cS.initialX = cS.selectionBox.X;
							cS.initialY = cS.selectionBox.Y;
						}
						break;
				}
			}
			if( e.Button == MouseButtons.Right  ) // Pressed the menu button 
			{   // 
				mouseRightStartX = e.X;
				mouseRightStartY = e.Y;
			}
		}
		private void circuitPictureBox_Move( object sender, MouseEventArgs e )
		{
			
			Pen pen = new Pen( Color.White, 3 );

			// Panning with the mousewheel button
			if( mouseWheelPressed )   // this can be disabled to prevent continuous refresh when dragging
			{
				// -----  Draw the 'permanent' graphics ----------------------------
				// ----- these are drawn onto the 'circuitGraphics' object and will appear on the screen only after a 'Refresh'
				int x = e.X;
				int y = e.Y;
				Console.WriteLine( "Dragging from {0},{1} to {2},{3}", x, y, mouseWheelStartX, mouseWheelStartY );
				schem.move( x - mouseWheelStartX, y - mouseWheelStartY );
				mouseWheelStartX = e.X;
				mouseWheelStartY = e.Y;
				Console.WriteLine( "Dragging" );
				schem.drawSchematic( schemGraphics, traceHandler.componentss /* quick: true*/ );
			}

			// Dragging with the left mouse buton
			if( mouseLeftPressed )
			{
				PointF mousePoint = schem.schemCoordFromScreenCoordF( new PointF( e.X, e.Y ) );

				if( circuitMouseMode == circuitMouseModes.dragComponent  )
				{
					if( componentSelectorIndex >= 0 && sectionSelectorIndex >= 0 )
					{ 
						traceHandler.componentss[componentSelectorIndex].sections[sectionSelectorIndex].position.X += mousePoint.X - mouseLeftStartX;
						traceHandler.componentss[componentSelectorIndex].sections[sectionSelectorIndex].position.Y += mousePoint.Y - mouseLeftStartY;
						mouseLeftStartX = mousePoint.X;
						mouseLeftStartY = mousePoint.Y;
						schem.drawSchematic( schemGraphics, traceHandler.componentss /* quick: true*/ );
					}
					else circuitChangeMouseMode( circuitMouseModes.mainMode );
				}
				if( circuitMouseMode == circuitMouseModes.dragCentroid  )
				{
					if( centroidSelectorIndex >= 0 )
					{
						Console.Write( "Move Centroid from {0} ", schematic.netElements[centroidSelectorIndex].centroid );
						schematic.netElements[centroidSelectorIndex].centroid.X += mousePoint.X - mouseLeftStartX;
						schematic.netElements[centroidSelectorIndex].centroid.Y += mousePoint.Y - mouseLeftStartY;
						schematic.netElements[centroidSelectorIndex].lockCentroid = true;
						mouseLeftStartX = mousePoint.X;
						mouseLeftStartY = mousePoint.Y;
						schem.drawSchematic( schemGraphics, traceHandler.componentss /* quick: true*/ );
						//Refresh();
						Console.WriteLine( " to {0}", schematic.netElements[centroidSelectorIndex].centroid );
					}
					else circuitChangeMouseMode( circuitMouseModes.mainMode );
				}
				if( circuitMouseMode == circuitMouseModes.editConnectionLine )
				{
					//if( centroidSelectorIndex >= 0 )
					{
						schematic.netElements[selectedConnectionPointIndex[0]].connectionLines[selectedConnectionPointIndex[1]].points[selectedConnectionPointIndex[2]] = snapToGrid( mousePoint );
						schem.drawSchematic( schemGraphics, traceHandler.componentss /* quick: true*/ );
						//Refresh();
					}
					//else circuitChangeMouseMode( circuitMouseModes.mainMode );
				}
				if (circuitMouseMode == circuitMouseModes.draggingSelection )
				{
					PointF p1 = new PointF(  mouseLeftStartX, mouseLeftStartY );
					PointF p2 = new PointF(  mousePoint.X, mousePoint.Y );

					schem.circuitDragItemsInSelections( circuitSelections, traceHandler.componentss, snapToGrid( p2.X - p1.X ), snapToGrid( p2.Y - p1.Y ) );

					//mouseLeftStartX = mousePoint.X;
					//mouseLeftStartY = mousePoint.Y;
					schem.drawSchematic( schemGraphics, traceHandler.componentss /* quick: true*/ );
				}
			}

			//============================================================================
			// Draw the temporary graphics, such as crosshairs, rubberbanding lines and so on
			//----------------------------------------------------------------------------
			Graphics destImage = circuitPictureBox.CreateGraphics();	// this is what the non permanent graphics will be drawn on to.
			Refresh();													// clears previously drawn temporary graphics			
			
			// Draw the temporary conneciton lines
			if( circuitMouseMode == circuitMouseModes.drawConnectionLine )	
			{
				if( connectionLine.Count() >= 2 )
				{
					Refresh();
					//Graphics destImage = circuitPictureBox.CreateGraphics();
					PointF p1,p2;
					pen = new Pen( Color.Orange, 4 );
					connectionLine[connectionLine.Count() - 1] = 
						snapToGrid( schem.schemCoordFromScreenCoordF( new PointF( e.X, e.Y ) ) );
					for( int i = 1; i < connectionLine.Count(); i++ )
					{
						p1 = schem.screenCoordFromSchemCoordF( connectionLine[i - 1] );
						p2 = schem.screenCoordFromSchemCoordF( connectionLine[i] );
						destImage.DrawLine( pen, p1, p2 );
					}
				}
				else circuitChangeMouseMode( circuitMouseModes.mainMode );
			}

			// Show the mouse co-ordinates and draw crosshairs
			PointF pS = schem.schemCoordFromScreenCoordF( new PointF { X = e.X, Y = e.Y } );
			snapToGrid( ref pS );
			textBoxCoordinates.Text = pS.X.ToString() + " : " + pS.Y.ToString();
			PointF pD = schem.screenCoordFromSchemCoordF( pS );
			pen = new Pen( Color.White, 1 );
			pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
			destImage.DrawLine( pen, 0f, pD.Y - 1, circuitPictureBox.Width, pD.Y - 1 );
			destImage.DrawLine( pen, 0f, pD.Y + 1, circuitPictureBox.Width, pD.Y + 1 );
			destImage.DrawLine( pen, pD.X-1, 0, pD.X-1, circuitPictureBox.Height );
			destImage.DrawLine( pen, pD.X+1, 0, pD.X+1, circuitPictureBox.Height );
			pen.Color = Color.Black;
			destImage.DrawLine( pen, 0f, pD.Y, circuitPictureBox.Width, pD.Y );
			destImage.DrawLine( pen, pD.X, 0, pD.X, circuitPictureBox.Height );

			// Draw the selection box
			if( circuitMouseMode == circuitMouseModes.dragSelecting )
			{
				PointF p1,p2;
				p1 = schem.screenCoordFromSchemCoordF( new PointF( mouseLeftStartX, mouseLeftStartY ) );
				p2 = new PointF( e.X, e.Y );
				pen.Color = Color.Yellow;
				pen.Width = 3;
				destImage.DrawRectangle( pen, p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y );
				pen.Color = Color.Red;
				pen.Width = 1;
				destImage.DrawRectangle( pen, p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y );
			}
			
			if ( circuitSelections.Count() > 0)
			{
				PointF p1,p2;
				pen.Color = Color.Yellow;
				pen.Width = 3;
				foreach( circuitSelections selection in circuitSelections )
				{
					RectangleF r = selection.selectionBox;
					float x1,y1,x2,y2;
					x1 = r.Left;
					y1 = r.Top;
					x2 = r.Left + r.Width;
					y2 = r.Top + r.Height;

					p1 = schem.screenCoordFromSchemCoordF( new PointF( x1, y1 ) );
					p2 = schem.screenCoordFromSchemCoordF( new PointF( x2, y2 ) );

					pen.Color = Color.Yellow;
					pen.Width = 3;
					destImage.DrawRectangle( pen, p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y );
					pen.Color = Color.Red;
					pen.Width = 1;
					destImage.DrawRectangle( pen, p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y );
				}
			}

		}
		private void circuitPictureBox_Click( object sender, MouseEventArgs e )
		{   // note that this activates when a mouse button is released, not when it is pressed
			if( e.Button == MouseButtons.Middle )
			{
				int x = e.X;
				int y = e.Y;
				mouseWheelPressed = false;
				schem.drawSchematic( schemGraphics, traceHandler.componentss /* quick: false*/ );
			}
			if( e.Button == MouseButtons.Left )
			{
				mouseLeftPressed = false;

				switch( circuitMouseMode )
				{
					case circuitMouseModes.dragCentroid:
						snapToGrid( ref schematic.netElements[centroidSelectorIndex].centroid );
						foreach( schematic.connectionLine connectionLine in schematic.netElements[centroidSelectorIndex].connectionLines  )
						{
							//connectionLine.points[0] = schematic.netElements[centroidSelectorIndex].centroid; // removed on 13 March 2022. see log
						}
						circuitChangeMouseMode( circuitMouseModes.mainMode );
						schem.drawSchematic( schemGraphics, traceHandler.componentss /* quick: true*/ );
						break;
					case circuitMouseModes.dragComponent :
						snapToGrid( ref traceHandler.componentss[componentSelectorIndex].sections[sectionSelectorIndex].position );
						circuitChangeMouseMode( circuitMouseModes.mainMode );
						schem.drawSchematic( schemGraphics, traceHandler.componentss /* quick: true*/ );
						break;
					case circuitMouseModes.dragSelecting:
						PointF p1 = new PointF( mouseLeftStartX, mouseLeftStartY );
						PointF p2 = schem.schemCoordFromScreenCoord( new PointF( e.X, e.Y ) );
						if( distanceBetween( p1, p2 ) > 400 )
						{
							RectangleF selectionRectangle = new RectangleF( p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y );
							schem.circuitSelectItemsInFrame( traceHandler.componentss, circuitSelections, selectionRectangle );
						}
						circuitChangeMouseMode( circuitMouseModes.mainMode );
						break;
					case circuitMouseModes.draggingSelection:
						schem.circuitSelectionReInit( circuitSelections );
						break;
					case circuitMouseModes.editConnectionLine:
						circuitChangeMouseMode( circuitMouseModes.mainMode );
						break;
				}
			}
		}
		private void circuitPictureBox_MouseEnter( object sender, EventArgs e )
		{
			circuitPictureBoxActive = true;
		}
		private void circuitPictureBox_MouseLeave( object sender, EventArgs e )
		{
			circuitPictureBoxActive = false;
			mouseWheelPressed = false;
			mouseLeftPressed = false;
		}
		private void circuitPictureBox_MouseWheel( object sender, MouseEventArgs e )
		{   // zoming in/out the circuit image
			int s = circuitZoomIndex + e.Delta / 20;
			if( s < -100 ) s = -100;
			if( s > 200 ) s = 200;
			circuitZoomIndex = s;
			schem.setScaleIndex( s, e.X, e.Y, circuitPictureBox.Width, circuitPictureBox.Height );
			//this.SuspendLayout();
			schem.drawSchematic( schemGraphics, traceHandler.componentss /* quick: true*/ );
			schem.drawKSymbolOld( sym, schemGraphics, this );  // DEV  REMOVE
			Refresh();
			//this.ResumeLayout(false);
			Console.WriteLine( "Scroll {0} - button {1}", e.Delta, e.Button );
		}
		private void circuitContextMenu_Opening( object sender, CancelEventArgs e )
		{   // create menu items according to the situation
			removeMenuItem( circuitContextMenu.Items, "Draw connection" );
			removeMenuItem( circuitContextMenu.Items, "Delete all connection lines of this net" );
			removeMenuItem( circuitContextMenu.Items, "Edit connection lines of this net" );

			removeMenuItem( circuitContextMenu.Items, "Finish drawing connection" );
			removeMenuItem( circuitContextMenu.Items, "Delete last point" );
			removeMenuItem( circuitContextMenu.Items, "Cancel drawing connection" );

			removeMenuItem( circuitContextMenu.Items, "Finish editing connection lines" );

			removeMenuItem( circuitContextMenu.Items, "Drag selections" );
			removeMenuItem( circuitContextMenu.Items, "Stop dragging selections" );
			removeMenuItem( circuitContextMenu.Items, "Release last selection" );
			removeMenuItem( circuitContextMenu.Items, "Release all selections" );

			removeMenuItem( circuitContextMenu.Items, "Reset symbol" );

			ToolStripMenuItem item;
			switch( circuitMouseMode )
			{
				case circuitMouseModes.mainMode :
					if ( componentSelectorIndex >= 0 )
					{
						if( traceHandler.componentss[componentSelectorIndex].symbol != null )
						{
							item = new ToolStripMenuItem();
							item.Text = "Reset symbol";
							item.Click += resetSymbol;
							circuitContextMenu.Items.Insert( 0, item );
						}
					}
					if( netSelector > 0 && connectionLine.Count() == 0 )
					{
						if( schematic.netElements.Find( r => r.netNum == netSelector ).connectionLines.Count() > 0 )
						{
							item = new ToolStripMenuItem();
							item.Text = "Delete all connection lines of this net";
							item.Click += deleteConnectionLines;
							circuitContextMenu.Items.Insert( 0, item );

							//item = new ToolStripMenuItem();
							//item.Text = "Edit connection lines of this net";
							//item.Click += editConnectionLines;
							//circuitContextMenu.Items.Insert( 0, item );
						}
				
						item = new ToolStripMenuItem();
						item.Text = "Draw connection";
						item.Click += drawingConnectionStart;
						circuitContextMenu.Items.Insert( 0, item );
					}
					if( circuitSelections.Count() > 1 )
					{
						item = new ToolStripMenuItem();
						item.Text = "Release all selections";
						item.Click += draggingSelectionsCancelAll;
						circuitContextMenu.Items.Insert( 0, item );
					}
					if ( circuitSelections.Count() > 0)
					{
						item = new ToolStripMenuItem();
						item.Text = "Release last selection";
						item.Click += draggingSelectionsCancelLast;
						circuitContextMenu.Items.Insert( 0, item );
						item = new ToolStripMenuItem();
						item.Text = "Drag selections";
						item.Click += draggingSelectionsStart;
						circuitContextMenu.Items.Insert( 0, item );
					}
					break;

				case circuitMouseModes.drawConnectionLine:
					if( netSelector > 0 && connectionLine.Count() > 0 )
					{
						item = new ToolStripMenuItem();
						item.Text = "Cancel drawing connection";
						item.Click += drawingConnectionCancel;
						circuitContextMenu.Items.Insert( 0, item );
					}
					if( netSelector > 0 && connectionLine.Count() > 2 )
					{
						item = new ToolStripMenuItem();
						item.Text = "Delete last point";
						item.Click += drawingConnectionDeletePoint;
						circuitContextMenu.Items.Insert( 0, item );
					}
					if( netSelector > 0 && connectionLine.Count() > 0 )
					{
						item = new ToolStripMenuItem();
						item.Text = "Finish drawing connection";
						item.Click += drawingConnectionEnd;
						circuitContextMenu.Items.Insert( 0, item );
					}
					break;
				case circuitMouseModes.editConnectionLine:
					item = new ToolStripMenuItem();
					item.Text = "Finish editing connection lines";
					item.Click += drawingConnectionEnd;
					circuitContextMenu.Items.Insert( 0, item );
					break;
				case circuitMouseModes.draggingSelection:
					item = new ToolStripMenuItem();
					item.Text = "Release all selections";
					item.Click += draggingSelectionsCancelAll;
					circuitContextMenu.Items.Insert( 0, item );
					item = new ToolStripMenuItem();
					item.Text = "Stop dragging selections";
					item.Click += draggingSelectionsEnd;
					circuitContextMenu.Items.Insert( 0, item );
					break;
			}
			circuitContextMenu.Refresh();
		}

		// Symbol assigning and changing
		private void resetSymbol( object sender, EventArgs e )
		{	// Removes the symbol from library and resets to the default
			traceHandler.componentss[componentSelectorIndex].symbol = null;
		}

		// Drawing and editing connections
		private void drawingConnectionStart( object sender, EventArgs e )
		{
			Console.WriteLine( "Start drawing connection" );
			schematic.netElement netEl = schematic.netElements.Find( r => r.netNum == netSelector );
			//connectionLine.Add( new PointF( netEl.centroid.X, netEl.centroid.Y ) );
			//connectionLine.Add( new PointF( netEl.centroid.X, netEl.centroid.Y ) );
			PointF schemPoint = snapToGrid( schem.schemCoordFromScreenCoordF( new PointF( mouseRightStartX, mouseRightStartY ) ) );
			connectionLine.Add( schemPoint );
			connectionLine.Add( schemPoint );
			circuitChangeMouseMode( circuitMouseModes.drawConnectionLine );
		}
		private void drawingConnectionAddPoint( object sender, MouseEventArgs e )
		{
			PointF schemPoint = schem.schemCoordFromScreenCoordF( new PointF( e.X, e.Y ) );
			connectionLine.Add( schemPoint );
		}
		private void drawingConnectionDeletePoint( object sender, EventArgs e )
		{
			if( connectionLine.Count() > 2 )
			{
				connectionLine.RemoveAt( connectionLine.Count() - 1 );
			}
		}
		private void drawingConnectionEnd( object sender, EventArgs e )
		{
			Console.WriteLine( "End drawing connection" );
			schematic.netElement netEl = schematic.netElements.Find( r => r.netNum == netSelector );
			netEl.connectionLines.Add( new schematic.connectionLine { points = connectionLine } );			
			connectionLine = new List<PointF>();		// clear the temporary drawing list of points
			circuitChangeMouseMode( circuitMouseModes.mainMode );
			schem.drawSchematic( schemGraphics, traceHandler.componentss /* quick: true*/ );
			Refresh();
		}
		private void drawingConnectionCancel( object sender, EventArgs e )
		{
			Console.WriteLine( "End drawing connection" );
			connectionLine = new List<PointF>();        // clear the temporary drawing list of points
			circuitChangeMouseMode( circuitMouseModes.mainMode );
			schem.drawSchematic( schemGraphics, traceHandler.componentss /* quick: true*/ );
			Refresh();
		}
		private void deleteConnectionLines( object sender, EventArgs e )
		{
			schematic.netElement netEl = schematic.netElements.Find( r => r.netNum == netSelector );
			netEl.connectionLines = new List<schematic.connectionLine>();
		}
		private void editConnectionLines( object sender, EventArgs e )
		{
			schematic.netElement netEl = schematic.netElements.Find( r => r.netNum == netSelector );
			//netEl.connectionLines = new List<schematic.connectionLine>();
			circuitChangeMouseMode( circuitMouseModes.editConnectionLine );
		}

		// Selection frames
		private void draggingSelectionsStart		( object sender, EventArgs e )
		{
			circuitChangeMouseMode( circuitMouseModes.draggingSelection );
		}
		private void draggingSelectionsEnd			( object sender, EventArgs e )
		{
			circuitChangeMouseMode( circuitMouseModes.mainMode );
		}
		private void draggingSelectionsCancelLast	( object sender, EventArgs e )
		{
			circuitSelections.RemoveAt( circuitSelections.Count() - 1 );
		}
		private void draggingSelectionsCancelAll	( object sender, EventArgs e )
		{
			if( circuitMouseMode == circuitMouseModes.draggingSelection )
			{
				circuitChangeMouseMode( circuitMouseModes.mainMode );
			}
			circuitSelections = new List<circuitSelections>();
		}

		private void circuitChangeMouseMode			( circuitMouseModes mode )
		{
			// for now this simply assigns the new mode, but additional logic will need to be added here, such  as
			// cleaning up and closing off things regarding the mode being exited
			circuitMouseModePrevious = circuitMouseMode;
			circuitMouseMode = mode;
			Console.WriteLine( "{0}: Circuit mouse mode changed from {1} to {2}", glob.debugCount++, circuitMouseModePrevious, circuitMouseMode );
		}

		// Helper functions		
		public float   distanceBetween( PointF point1, PointF point2 )
		{
			return ( (float)Math.Sqrt( ( point1.X - point2.X ) * ( point1.X - point2.X ) + ( point1.Y - point2.Y ) * ( point1.Y - point2.Y ) ) );
		}
		public void    snapToGrid( ref PointF point)
		{
			point.X = (float)Math.Round( ( point.X / schemGridSnap ) ) * schemGridSnap;
			point.Y = (float)Math.Round( ( point.Y / schemGridSnap ) ) * schemGridSnap;
		}
		public PointF  snapToGrid( PointF point )
		{
			point.X = (float)Math.Round( ( point.X / schemGridSnap ) ) * schemGridSnap;
			point.Y = (float)Math.Round( ( point.Y / schemGridSnap ) ) * schemGridSnap;
			return point;
		}
		public void    snapToGrid( ref float a )
		{
			a= (float)Math.Round( ( a / schemGridSnap ) ) * schemGridSnap;
		}
		public float   snapToGrid( float a )
		{
			return (float)Math.Round( ( a / schemGridSnap ) ) * schemGridSnap;
		}
	}



}