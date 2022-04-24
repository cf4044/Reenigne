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

  
	public partial class mainForm : Form
	{
		
		// Actions related to the board picture box - scrolling, zooming etc
		private void boardPictureBox_MouseDown( object sender, MouseEventArgs e )
		{
			if( e.Button == MouseButtons.Middle )   // pressed the moushweel button to start dragging the image
			{
				mouseWheelPressed = true;
				mouseWheelStartX = e.X;
				mouseWheelStartY = e.Y;
				Console.WriteLine( "Button pressed {0} : startX {1} : startY {2}", e.Button, mouseWheelStartX, mouseWheelStartY );
			}
			if( e.Button == MouseButtons.Left )   // pressed the left button 
			{
				// drawing on the board image
				switch( boardMouseMode )
				{
					case boardMouseModes.selectItem:
						selectItemClick( sender, e );
						break;
					case boardMouseModes.drawTrace:
						drawTraceClick( sender, e );
						break;
					case boardMouseModes.insertVia:
						insertViaClick( sender, e );
						break;
					case boardMouseModes.addComponent:
						addComponentClick( sender, e );
						break;
					default:
						break;
				}

			}
		}
		private void boardPictureBox_Move( object sender, MouseEventArgs e )
		{
			
			// Panning with the mousewheel button
			if( mouseWheelPressed )   // this can be disabled to prevent continuous refresh when dragging
			{
				// -----  Draw the 'permanent' graphics ----------------------------
				// ----- these are drawn onto the 'boardGraphics' object and will appear on the screen only after a 'Refresh'
				int x = e.X;
				int y = e.Y;
				//for( int i = 0; i < boardImages.Count; i++ )
				//	boardImages[i].move( x - mouseWheelStartX, y - mouseWheelStartY );
				boardImage.move( x - mouseWheelStartX, y - mouseWheelStartY );
				mouseWheelStartX = e.X;
				mouseWheelStartY = e.Y;
				Console.WriteLine( "Dragging" );
				redrawBoards( quick: true );
			}
			
			//==========================================================================================================
			// Draw the temporary graphics like crosshari, rubberbanding lines and so on
			// these will only be drawn as an opverlay on the screen rather than embedded in the 'boardGraphics' object
			//----------------------------------------------------------------------------------------------------------
			Graphics destImage = boardPictureBox.CreateGraphics();
			Pen pen;
			Refresh();

			// Drawing traces on the board image
			if( boardMouseMode == boardMouseModes.drawTrace )
			{
				drawTraceMove( sender, e );
			}
			if( boardMouseMode == boardMouseModes.addComponent )
			{				
				drawComponentMove( e.X, e.Y );
			}

			// Show the mouse co-ordinates and draw the crosshair
			PointF tmp = boardImage.boardCoordFromScreenCoord( new PointF { X = e.X, Y = e.Y } );
			textBoxCoordinates.Text = tmp.X.ToString() + " : " + tmp.Y.ToString();
			pen = new Pen( Color.White, 3 );
			destImage.DrawLine( pen, 0f, e.Y, boardPictureBox.Width, e.Y );
			destImage.DrawLine( pen, e.X, 0, e.X, boardPictureBox.Height );
			pen = new Pen( Color.Black, 1 );
			destImage.DrawLine( pen, 0f, e.Y, boardPictureBox.Width, e.Y );
			destImage.DrawLine( pen, e.X, 0, e.X, boardPictureBox.Height );

		}
		private void boardPictureBox_Click( object sender, MouseEventArgs e )
		{   // note that this activates when a mouse button is released, not when it is pressed
			if( e.Button == MouseButtons.Middle )
			{
				int x = e.X;
				int y = e.Y;
				mouseWheelPressed = false;
				//for( int i = 0; i < boardImages.Count; i++ )
				//	boardImages[i].move( x - mouseWheelStartX, y - mouseWheelStartY );
				boardImage.move( x - mouseWheelStartX, y - mouseWheelStartY );
				redrawBoards();
			}
			//Console.WriteLine("Button Released {0}", e.Button);
		}
		private void boardPictureBox_MouseEnter( object sender, EventArgs e )
		{
			boardPictureBoxActive = true;
		}
		private void boardPictureBox_MouseLeave( object sender, EventArgs e )
		{
			boardPictureBoxActive = false;
			mouseWheelPressed = false;
		}
		private void boardPictureBox_MouseWheel( object sender, MouseEventArgs e )
		{   // zoming in/out the board image
			int s = scrollBarBoardZoom.Value + e.Delta / 20;
			if( s < scrollBarBoardZoom.Minimum ) s = scrollBarBoardZoom.Minimum;
			if( s > scrollBarBoardZoom.Maximum ) s = scrollBarBoardZoom.Maximum;
			scrollBarBoardZoom.Value = s;
			//for( int i = 0; i < boardImages.Count; i++ )
			//	boardImages[i].setScaleIndex( s, e.X, e.Y, boardPictureBox.Width, boardPictureBox.Height );
			boardImage.setScaleIndex( s, e.X, e.Y, boardPictureBox.Width, boardPictureBox.Height );
			//this.SuspendLayout();
			redrawBoards();
			Refresh();
			//this.ResumeLayout(false);
			Console.WriteLine( "Scroll {0} - button {1}", e.Delta, e.Button );
		}
		private void boardContextMenu_Opening( object sender, CancelEventArgs e ) 
		{   // KLUDGE ToDo Dev  --  this is currently just copied from the schematic part.  Need to adapt
			// it for the board view
			// create menu items according to the situation
			removeMenuItem( boardContextMenu.Items, "Draw connection" );
			removeMenuItem( boardContextMenu.Items, "Delete all connection lines of this net" );
			removeMenuItem( boardContextMenu.Items, "Edit connection lines of this net" );

			removeMenuItem( boardContextMenu.Items, "Finish drawing connection" );
			removeMenuItem( boardContextMenu.Items, "Delete last point" );
			removeMenuItem( boardContextMenu.Items, "Cancel drawing connection" );

			removeMenuItem( boardContextMenu.Items, "Finish editing connection lines" );

			removeMenuItem( boardContextMenu.Items, "Drag selections" );
			removeMenuItem( boardContextMenu.Items, "Stop dragging selections" );
			removeMenuItem( boardContextMenu.Items, "Release last selection" );
			removeMenuItem( boardContextMenu.Items, "Release all selections" );

			ToolStripMenuItem item;
			switch( boardMouseMode )
			{
				//case boardMouseModes.mainMode:
				//	if( netSelector > 0 && connectionLine.Count() == 0 )
				//	{
				//		if( schematic.netElements.Find( r => r.netNum == netSelector ).connectionLines.Count() > 0 )
				//		{
				//			item = new ToolStripMenuItem();
				//			item.Text = "Delete all connection lines of this net";
				//			item.Click += deleteConnectionLines;
				//			boardContextMenu.Items.Insert( 0, item );

				//			item = new ToolStripMenuItem();
				//			item.Text = "Edit connection lines of this net";
				//			item.Click += editConnectionLines;
				//			boardContextMenu.Items.Insert( 0, item );
				//		}

				//		item = new ToolStripMenuItem();
				//		item.Text = "Draw connection";
				//		item.Click += drawingConnectionStart;
				//		boardContextMenu.Items.Insert( 0, item );
				//	}
				//	if( boardSelections.Count() > 1 )
				//	{
				//		item = new ToolStripMenuItem();
				//		item.Text = "Release all selections";
				//		item.Click += draggingSelectionsCancelAll;
				//		boardContextMenu.Items.Insert( 0, item );
				//	}
				//	if( boardSelections.Count() > 0 )
				//	{
				//		item = new ToolStripMenuItem();
				//		item.Text = "Release last selection";
				//		item.Click += draggingSelectionsCancelLast;
				//		boardContextMenu.Items.Insert( 0, item );
				//		item = new ToolStripMenuItem();
				//		item.Text = "Drag selections";
				//		item.Click += draggingSelectionsStart;
				//		boardContextMenu.Items.Insert( 0, item );
				//	}
				//	break;

				//case boardMouseModes.drawConnectionLine:
				//	if( netSelector > 0 && connectionLine.Count() > 0 )
				//	{
				//		item = new ToolStripMenuItem();
				//		item.Text = "Cancel drawing connection";
				//		item.Click += drawingConnectionCancel;
				//		boardContextMenu.Items.Insert( 0, item );
				//	}
				//	if( netSelector > 0 && connectionLine.Count() > 2 )
				//	{
				//		item = new ToolStripMenuItem();
				//		item.Text = "Delete last point";
				//		item.Click += drawingConnectionDeletePoint;
				//		boardContextMenu.Items.Insert( 0, item );
				//	}
				//	if( netSelector > 0 && connectionLine.Count() > 0 )
				//	{
				//		item = new ToolStripMenuItem();
				//		item.Text = "Finish drawing connection";
				//		item.Click += drawingConnectionEnd;
				//		boardContextMenu.Items.Insert( 0, item );
				//	}
				//	break;
				//case boardMouseModes.editConnectionLine:
				//	item = new ToolStripMenuItem();
				//	item.Text = "Finish editing connection lines";
				//	item.Click += drawingConnectionEnd;
				//	boardContextMenu.Items.Insert( 0, item );
				//	break;
				//case boardMouseModes.draggingSelection:
				//	item = new ToolStripMenuItem();
				//	item.Text = "Stop dragging selections";
				//	item.Click += draggingSelectionsEnd;
				//	boardContextMenu.Items.Insert( 0, item );
				//	break;
			}
			boardContextMenu.Refresh();
		}

		// Display
		private void redrawBoards( bool quick = false )
		{   // redraws the board views - called after zooming, scrolling, resizing the main window and so on
			if ( boardImages.Count() == 0)
			{   // if there are no boards to draw the screen will not be cleared so need to do explicitly
				boardGraphics.Clear( Color.AliceBlue );
			}
			if( this.Visible )
			{
				boardGraphics.Clear( Color.AliceBlue );
				for( int i = 0; i < boardImages.Count; i++ )
				{
					boardImages[i].drawImage( boardPictureBox, boardGraphics );
				}
			}

			// Redraw the traces, components and such, unless we are in fast redrwaing mode (which draws only the board images)
			if( !quick || boardImages.Count() == 0 )
			{
				traceRedrawAll();
				viasRedraw();
				componentsRedraw();
				netAnchorsRedraw();
				if( netSelector != 0 ) traceRedrawSelectedNet();
				if( segmentSelectorIndex != -1 ) traceRedrawSelectedSegment();
			}

			// Redraw the trace that is currenly being added, if any
			drawTraceRedrawCurrent();

			Refresh();

			//tracePointFlag1 = false;				 // this is probably not needed anymore after replacing the 'DrawReversible' method with the new one.
		}

		// Item selection 
		private void selectItemClick( object sender, MouseEventArgs e )
		{
			bool needRedraw = false;
			// the co-ordinates of the point we need to identify
			PointF searchPoint = boardImage.boardCoordFromScreenCoord(new PointF(e.X, e.Y));

			// Select a trace
			if( traceToolStripMenuItem.Checked )
			{
				int prevTraceSelector = segmentSelectorIndex;
				segmentSelectorIndex = traceHandler.findSegmentIndexAt( searchPoint, layer );
				Console.WriteLine( "Selected trace (segment) {0}", segmentSelectorIndex );
				if( segmentSelectorIndex != prevTraceSelector )
					needRedraw = true;
			}
			// Select a net
			if( netToolStripMenuItem.Checked )
			{
				int prevNetSelector = netSelector;
				netSelector = traceHandler.findNetNumberAt( searchPoint, layer );
				netSelectorIndex = traceHandler.netList.IndexOf( netSelector );
				Console.WriteLine( "Selected net {0}", netSelector );
				if( netSelector != prevNetSelector ) needRedraw = true;
			}
			// Select a via
			if( viaThruHoleToolStripMenuItem.Checked )
			{
				int prevViaSelectorIndex = viaSelectorIndex;
				viaSelectorIndex = traceHandler.findViaIndexAt( searchPoint );
				Console.WriteLine( "Selected Via {0}", viaSelectorIndex );
				if( viaSelectorIndex != prevViaSelectorIndex )
					needRedraw = true;
			}
			// Select a component
			if( componentToolStripMenuItem.Checked )
			{
				int prevComponentSelectorIndex = componentSelectorIndex;
				componentSelectorIndex = traceHandler.findComponentIndexAt( searchPoint, layer );
				Console.WriteLine( "Selected Component {0}", componentSelectorIndex );
				if( componentSelectorIndex != prevComponentSelectorIndex )
					needRedraw = true;
				if( componentSelectorIndex >= 0 )
				{
					//textBoxDesigType.Text = traceHandler.componentss[componentSelectorIndex].desigType;
					//textBoxDesigNum.Text  = traceHandler.componentss[componentSelectorIndex].desigNum.ToString();

					if( checkBoxCenterOnSelection.Checked )
					{
						Console.WriteLine( "Centering on component {0}", componentSelectorIndex );
						float x,y,minX,minY,maxX,maxY;
						minX = float.MaxValue;
						minY = float.MaxValue;
						maxX = float.MinValue;
						maxY = float.MinValue;
						int i,numSections;
						numSections = traceHandler.componentss[componentSelectorIndex].sections.Count();
						x = traceHandler.componentss[componentSelectorIndex].sections[0].position.X;
						y = traceHandler.componentss[componentSelectorIndex].sections[0].position.Y;
						if( numSections > 0 )
						{
							for( i = 1; i < numSections; i++ )
							{
								x = traceHandler.componentss[componentSelectorIndex].sections[i].position.X;
								y = traceHandler.componentss[componentSelectorIndex].sections[i].position.Y;
								if( x < minX ) minX = x;
								if( y < minY ) minY = y;
								if( x > maxX ) maxX = x;
								if( y > maxY ) maxY = y;
								Console.WriteLine( "  Section {0}: ({1},{2})", i, x, y );
								x = ( minX + maxX ) / 2;
								y = ( minY + maxY ) / 2;
							}
						}
						schem.offsetX =
							0.5f * circuitPictureBox.Width / schem.scale - x;
						schem.offsetY =
							0.5f * circuitPictureBox.Height / schem.scale - y;
						
					}


				}
				else
				{
					//textBoxDesigType.Text = "--";
					//textBoxDesigNum.Text = "--";

				}
			}

			if( needRedraw )
			{
				redrawBoards();
				schem.drawSchematic( schemGraphics, traceHandler.componentss /* quick: true*/ );
				Refresh();
			}
		}

		// Trace drawing and editing
		private void drawTraceClick( object sender, MouseEventArgs e )
		{
			if( tracePoints.Count >= 2 )     // >=2 means a trace already exists and we add points to it. If not >=2 then we are staring a new trace
			{
				if( tracePoints[tracePoints.Count() - 2] == boardImage.boardCoordFromScreenCoord( new PointF( e.X, e.Y ) ) )      // if user clicks on same point twice it means the trace is completed
				{
					Console.WriteLine( "Completing and storing trace" );
					traceHandler.addTrace( tracePoints, layer, 0 );
					tracePoints.Clear();
					return;
				}
			}
			else
			{
				Console.WriteLine( "Starting new trace" );
				tracePoints.Add( boardImage.boardCoordFromScreenCoord( new PointF( e.X, e.Y ) ) );

			}
			Console.WriteLine( "Add point to trace and refresh display" );
			tracePoints.Add( boardImage.boardCoordFromScreenCoord( new PointF( e.X, e.Y ) ) );
			drawTraceRedrawCurrent();
		}
		private void drawTraceMove( object sender, MouseEventArgs e )
		{
			// Draw the temporary trace, i.e the one the user is currently drawing
			if( tracePoints.Count >= 2 )     // >=2 means a trace already exists and we add points to it. If not >=2 then there is no trace so nothing to be done
			{
				Control control = (Control)sender;
				Point startPoint, endPoint;
				Point offset;
				Graphics destImage = boardPictureBox.CreateGraphics();
				Pen myPen = new Pen( System.Drawing.Color.Green, 1 );

				tracePoints[tracePoints.Count() - 1] = boardImage.boardCoordFromScreenCoord( new Point( e.X, e.Y ) );
				{	
					int i = tracePoints.Count() - 2;
					startPoint = boardImage.screenCoordFromBoardCoord( tracePoints[i] );
					endPoint = boardImage.screenCoordFromBoardCoord( tracePoints[i + 1] );
					constrainLineWithin( ref startPoint, ref endPoint, boardPictureBox.ClientRectangle.Size );

					destImage.DrawLine( new Pen( System.Drawing.Color.White, 3 ), startPoint, endPoint );

					offset = perpendicularOffset( startPoint, endPoint, 2 );
					startPoint.Offset( offset );
					endPoint.Offset( offset );
					destImage.DrawLine( new Pen( System.Drawing.Color.Black, 2 ), startPoint, endPoint );

					offset = perpendicularOffset( startPoint, endPoint, -4 );
					startPoint.Offset( offset );
					endPoint.Offset( offset );
					destImage.DrawLine( new Pen( System.Drawing.Color.Black, 2 ), startPoint, endPoint );
				}
				
			}
		}
		private void drawTraceAbort()
		{
			tracePoints.Clear();
			redrawBoards();
			//boardMouseMode = boardMouseModes.none;
		}
		private void drawTraceDeleteLast()
		{
			if( tracePoints.Count() >= 3 )
			{
				Console.WriteLine( "Deleted last point from trace" );
				tracePoints.RemoveAt( tracePoints.Count() - 1 );
			}
			else
			{
				tracePoints.Clear();
				Console.WriteLine( "Cleared trace" );
			}
			redrawBoards();
		}
		private void drawTraceRedrawCurrent()
		{
			if( tracePoints.Count() > 2 )
			{
				Graphics graphicsObj;
				graphicsObj = boardGraphics; // boardPictureBox.CreateGraphics();
				Pen myPen = new Pen(traceColour[layer], 5);
				for( int i = 0; i < tracePoints.Count() - 2; i++ )
				{
					graphicsObj.DrawLine
					(
						myPen,
						boardImage.screenCoordFromBoardCoord( tracePoints[i] ),
						boardImage.screenCoordFromBoardCoord( tracePoints[i + 1] )
					);
				}
			}
		}
		private void traceRedrawAll() { traceRedrawAll( Color.AliceBlue ); }
		private void traceRedrawAll( Color colour, int thickness = 0, int selectedNet = 0, int selectedSegmentIndex = -1 )
		{
			//Stopwatch stopWatch = new Stopwatch();
			//stopWatch.Start();
			if( boardGraphics == null ) { Console.WriteLine( "NULL boardgraphics" ); return; }
			//if (selectedNet != 0) Console.WriteLine("Redrawing with Selected net: {0}", selectedNet);
			if( traceHandler.segmentList.Count() != 0 )
			{
				Graphics graphicsObj;
				graphicsObj = boardGraphics; // boardPictureBox.CreateGraphics();
				if( colour.Equals( Color.AliceBlue ) ) colour = traceColour[layer];
				Pen myPen = new Pen( colour, thickness * boardImage.scale  );
				for( int i = 0; i < traceHandler.segmentList.Count; i++ )
				{
					bool                                                    show = true;

					if( selectedNet != 0 | selectedSegmentIndex != -1 ) show = false;
					if( traceHandler.segmentList[i].net == selectedNet ) show = true;
					if( i == selectedSegmentIndex ) show = true;
					if( traceHandler.segmentList[i].layer != layer ) show = false;
					//if (traceHandler.segmentList[i].layer == layer & ( selectedNet == 0 | traceHandler.segmentList[i].net == selectedNet))
					if( show )
					{
						if( thickness == 0 ) myPen.Width = traceHandler.segmentList[i].thickness * boardImage.scale;
						for( int j = 0; j < traceHandler.segmentList[i].pointList.Count() - 1; j++ )
						{
							graphicsObj.DrawLine
							(
								myPen,
								boardImage.screenCoordFromBoardCoord( traceHandler.segmentList[i].pointList[j] ),
								boardImage.screenCoordFromBoardCoord( traceHandler.segmentList[i].pointList[j + 1] )
							);
						}
					}
				}
			}
			Refresh();
			//stopWatch.Stop();
			//TimeSpan ts = stopWatch.Elapsed;
			//string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
			//    ts.Hours, ts.Minutes, ts.Seconds,
			//    ts.Milliseconds / 10);
			//Console.WriteLine("RunTime {0}", elapsedTime );
		}
		private void traceRedrawSelectedSegment()
		{
			traceRedrawAll( colour: Color.Orange, thickness: 5, selectedSegmentIndex: segmentSelectorIndex );
		}
		private void traceRedrawSelectedNet()
		{
			traceRedrawAll( colour: Color.Red, thickness: 5, selectedNet: netSelector );
		}
		private void traceDeleteSelectedLine() { }
		private void traceDeleteSelectedTrace() { }

		// Vias, throughHoles and net anchors
		private void insertViaClick( object sender, MouseEventArgs e )
		{
			traceHandler.AddVia( boardImage.boardCoordFromScreenCoordF( new PointF( e.X, e.Y ) ), 15 );
			viasRedraw( last: true );
		}
		private void viasRedraw( bool last = false )
		{
			if( boardGraphics == null ) { Console.WriteLine( "NULL boardGraphics redrawing Vias" ); return; }
			int i, start, end;
			float scale = boardImage.scale;
			end = traceHandler.viaList.Count() - 1;
			if( last ) start = end; else start = 0;
			Graphics graphicsObj;
			graphicsObj = boardGraphics; // boardPictureBox.CreateGraphics();
			for( i = start; i <= end; i++ )
			{
				float halfThickness = traceHandler.viaList[i].size * 0.1F;
				float outerRadius   = traceHandler.viaList[i].size * 0.5f - halfThickness ;
				float innerRadius   = traceHandler.viaList[i].size * 0.1f + halfThickness;

				Pen myPen = new Pen(Color.Black, halfThickness * 2F * scale);
				if( i == viaSelectorIndex ) myPen.Color = Color.Red;
				graphicsObj.DrawEllipse
				(
					myPen,
					boardImage.screenCoordFromBoardCoord( traceHandler.viaList[i].location ).X - outerRadius * scale,
					boardImage.screenCoordFromBoardCoord( traceHandler.viaList[i].location ).Y - outerRadius * scale,
					outerRadius * scale * 2, outerRadius * scale * 2
				);
				myPen.Color = Color.White;
				if( i == viaSelectorIndex ) myPen.Color = Color.Green;
				graphicsObj.DrawEllipse
				(
					myPen,
					boardImage.screenCoordFromBoardCoord( traceHandler.viaList[i].location ).X - innerRadius * scale,
					boardImage.screenCoordFromBoardCoord( traceHandler.viaList[i].location ).Y - innerRadius * scale,
					innerRadius * scale * 2, innerRadius * scale * 2
				);
			}
		}
		private void netAnchorsRedraw()
		{
			if( boardGraphics == null ) { Console.WriteLine( "NULL boardGraphics redrawing Vias" ); return; }
			int i, start, end;
			float scale = boardImage.scale;
			float outerRadius = 10;
			float innerRadius = 6;
			Graphics graphicsObj;
			graphicsObj = boardGraphics; // boardPictureBox.CreateGraphics();
			foreach( schematic.netElement netEl in schematic.netElements )
			{
				if( netEl.layer == layer )
				{
					Pen myPen = new Pen(Color.Orange, 3);
					graphicsObj.DrawEllipse
					(
						myPen,
						boardImage.screenCoordFromBoardCoord( netEl.anchorPoint ).X - outerRadius * scale,
						boardImage.screenCoordFromBoardCoord( netEl.anchorPoint ).Y - outerRadius * scale,
						outerRadius * scale * 2, outerRadius * scale * 2
					);
					myPen = new Pen( Color.Blue, 3 );
					graphicsObj.DrawEllipse
					(
						myPen,
						boardImage.screenCoordFromBoardCoord( netEl.anchorPoint ).X - innerRadius * scale,
						boardImage.screenCoordFromBoardCoord( netEl.anchorPoint ).Y - innerRadius * scale,
						innerRadius * scale * 2, innerRadius * scale * 2
					);
				}
			}
		}

		// Components
		private void componentsRedraw( bool last = false )
		{
			PointF boardPoint, displayPoint;

			float scale = boardImage.scale; if( boardGraphics == null ) { Console.WriteLine( "NULL boardGraphics redrawing Components" ); return; }
			int i, start, end;

			end = traceHandler.componentss.Count() - 1;
			start = 0; //if( last ) start = end; else start = 0;
			Graphics graphicsObj;
			graphicsObj = boardGraphics; // boardPictureBox.CreateGraphics();
			for( i = start; i <= end; i++ )
			{
				if( traceHandler.componentss[i].layer == layer && !traceHandler.componentss[i].hidden )
				{
					boardPoint = traceHandler.componentss[i].position;
					//Pen myPen = new Pen(Color.Black, 5 * scale);
					//if( i == componentSelectorIndex ) myPen.Color = Color.Red;
					displayPoint = boardImage.screenCoordFromBoardCoord( boardPoint );                 // the position within the display
					traceHandler.componentss[i].boardDraw( graphicsObj, displayPoint, scale, selected: ( componentSelectorIndex == i ) );

				}
				if( traceHandler.componentss[i].layer != layer && !traceHandler.componentss[i].hidden && traceHandler.componentss[i].PinType == pinTypes.through )
				{
					boardPoint = traceHandler.componentss[i].position;
					//Pen myPen = new Pen(Color.Black, 5 * scale);
					//if( i == componentSelectorIndex ) myPen.Color = Color.Red;
					displayPoint = boardImage.screenCoordFromBoardCoord( boardPoint );                 // the position within the display
					traceHandler.componentss[i].boardDraw( graphicsObj, displayPoint, scale, backPinsOnly: true );

				}
			}

		}
		private void drawComponentMove( int x = int.MinValue, int y = int.MinValue )
		{
			int i=traceHandler.componentss.Count()-1;
			if( i >= 0 )
			{
				Point boardPoint, displayPoint;
				Graphics destImage = boardPictureBox.CreateGraphics();
				Pen myPen = new Pen( System.Drawing.Color.Green, 3 );
				float scale = boardImage.scale;
				if( x != int.MinValue && y != int.MinValue )
				{
					boardPoint = boardImage.boardCoordFromScreenCoord( new Point( x, y ) );        // the position within the board
					traceHandler.componentss[i].position = boardPoint;
				}
				else
					boardPoint = new Point( (int)traceHandler.componentss[i].position.X, (int)traceHandler.componentss[i].position.Y );
				displayPoint = boardImage.screenCoordFromBoardCoord( boardPoint );                 // the position within the displaytraceHandler.componentss[i].position = boardPoint;
				traceHandler.componentss[i].layer = layer;

				// tentative replacement of kludge
				if( traceHandler.layers.Find( r => r.layer == layer ).mirrored )
					traceHandler.componentss[i].Mirror = mirrorModes.horizontal;
				else
					traceHandler.componentss[i].Mirror = mirrorModes.none;

				//if( layer == 0 ) traceHandler.componentss[i].Mirror = mirrorModes.none;  // KLUDGE
				//else traceHandler.componentss[i].Mirror = mirrorModes.horizontal;        // KLUDGE





				traceHandler.componentss[i].boardDraw( destImage, displayPoint, scale );
			}
		}
		private void addComponentStart()     // This is called when component insertion mode is selected
		{
			traceHandler.componentss.Add( new component( new PointF( 0, 0 ) ) );
			traceHandler.componentss[traceHandler.componentss.Count() - 1].hidden = true;
		}
		private void addComponentClick( object sender, MouseEventArgs e )
		{
			int i=traceHandler.componentss.Count() - 1;
			// Confirm and complete the addition of the component that was being created
			if( i >= 0 )
			{
				traceHandler.componentss[i].position = boardImage.boardCoordFromScreenCoordF( new PointF( e.X, e.Y ) );
				traceHandler.componentss[i].layer = layer;
				if( layer == 0 ) traceHandler.componentss[i].Mirror = mirrorModes.none;  // KLUDGE
				else traceHandler.componentss[i].Mirror = mirrorModes.horizontal;        // KLUDGE
				traceHandler.componentss[i].hidden = false;
				traceHandler.componentss[i].sections[0].position = new PointF( traceHandler.componentss[i].position.X * 10, traceHandler.componentss[i].position.Y * 10 );   // KLUDGE
				traceHandler.componentss[i].reformatPins( "" );
			}
			// Create a new floating component
			traceHandler.componentss.Add( new component( boardImage.boardCoordFromScreenCoordF( new PointF( e.X, e.Y ) ) ) );
			i = traceHandler.componentss.Count() - 1;
			traceHandler.componentss[i].layer = layer;
			if( layer == 0 ) traceHandler.componentss[i].Mirror = mirrorModes.none;       // KLUDGE
			else traceHandler.componentss[i].Mirror = mirrorModes.horizontal;             // KLUDGE
			traceHandler.componentss[i].hidden = true;
			componentsRedraw( last: true );
		}
		private void addComponentAbort()
		{
			int i=traceHandler.componentss.Count() - 1;
			if( i >= 0 ) traceHandler.componentss.RemoveAt( i );
		}


		//private void InitializeComponent()
		//{
		//	this.SuspendLayout();
		//	// 
		//	// mainForm
		//	// 
		//	this.ClientSize = new System.Drawing.Size(284, 261);
		//	this.Name = "mainForm";
		//	this.Load += new System.EventHandler(this.mainForm_Load);
		//	this.ResumeLayout(false);



	}


}

