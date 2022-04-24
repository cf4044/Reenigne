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
    // Displaying and handling the images of the printed cirrcuit board
    public partial class boardImage
    {
        // These are static as they are common to all boards, not specific to each of them
        public static float offsetX		= 0;
        public static float offsetY		= 0;
        public static float scale		= 1F;

		// Properties that are stored	in the file
		public string		imageName	= "noName";		// ToDo: this should be changed to imageDesc as it is a description of the image, not the layer. There can be multiple images per layer
		public string	imageLocation	= "";
		public float	  correctionX	= 0;
        public float	  correctionY	= 0;
        public float  correctionScale	= 1.0f;
		public int              layer	= 0;

		// various other variables and stuff
		public bool				shown	= true;                   // this is set dynamically to indicate whether the image should be displayed, typically depending on which layer is selected
		public bool			  visible	= true;					// this is set by user to determine whether or not the image is displayed when its layer is selected
		public PictureBox pictureHolder = new PictureBox();  // this holds the image loaded from file

		// Constructor
        public boardImage(mainForm targetForm)
        {

            //for (int i = 0; i < 5; i++)
            //{
            //    buttons.Add(new Button());
            //    buttons[i].BackColor = Color.Gray;
            //    buttons[i].Text = "Add";
            //    buttons[i].Location = new System.Drawing.Point(490+i*30, 25);
            //    buttons[i].Size = new System.Drawing.Size(50, 25);
            //    targetForm.Controls.Add(buttons[i]);
            //}

            // Create picture holder for the board images. These are picture boxes on the target form.
            //pictureHolder.BackColor = Color.Gray;
			//            pictureHolder.Location = new Point(90 + 60 * targetForm.numBoardImages(), -49);   // place the image almost outside the window. It doesn;t need to be visible

			//pictureHolder.Location = new Point( 90 + 60 * targetForm.numBoardImages(), 50 );   // 

			//pictureHolder.Size = new Size(50, 50);
			targetForm.Controls.Add(pictureHolder);
			pictureHolder.Location = new Point( 90 + 60 * targetForm.numBoardImages(), -49 );
			pictureHolder.Size = new Size( 50, 50 );
			pictureHolder.BringToFront();
		}
		~boardImage()
		{
			Console.WriteLine( "Destroying boardImage" );
			
		}

		// Dsiplaying the board images
        public void drawImage(PictureBox destPictureBox, Graphics boardGraphics)
        {
			if( pictureHolder.Image == null )
			{
				Console.WriteLine( "No image in picture holder!" );
				return;
			}
            if (shown && visible )		 // changed 2022-04-10
            {
                // calulate the size of the patch to be copied from the source image and its location
                int sourceWidth  = (int)(destPictureBox.Width  / (scale * correctionScale));
                int sourceHeight = (int)(destPictureBox.Height / (scale * correctionScale));
                int x = (int) ( ( -offsetX / correctionScale - correctionX )  );
                int y = (int) ( ( -offsetY / correctionScale - correctionY )  );

                // Blit the patch from the source image onto the display
                Graphics destImage;
				destImage = boardGraphics;
                //destImage.Clear(Color.AliceBlue);
                Rectangle destRectangle = new Rectangle
                {
                    X = 0,
                    Y = 0,
                    Width = destPictureBox.Width,
                    Height = destPictureBox.Height
                };
                boardGraphics.DrawImage
                (
                    pictureHolder.Image,
                    destRectangle,
                    x,
                    y,
                    sourceWidth, sourceHeight,
                    GraphicsUnit.Pixel
                );
     //           // If board image is smaller than the window fill in the gaps around it
     //           int wX1, wY1, wX2, wY2, dX1, dY1, dX2, dY2;
     //           wX1 = 0;
     //           wY1 = 0;
     //           wX2 = destPictureBox.Width;
     //           wY2 = destPictureBox.Height;
     //           dX1 = (int)((offsetX + correctionX) * (scale * correctionScale));
     //           dY1 = (int)((offsetY + correctionY) * (scale * correctionScale));
     //           dX2 = (int)((offsetX + correctionX + pictureHolder.Image.Width) * (scale * correctionScale));
     //           dY2 = (int)((offsetY + correctionY + pictureHolder.Image.Height) * (scale * correctionScale));
     //           SolidBrush myBrush = new SolidBrush(Color.AliceBlue);
     //           if (dX1 > 0) // gap on the left
					//boardGraphics.FillRectangle(myBrush, wX1, wY1, dX1 - wX1, wY2 - wY1);
     //           if (dY1 > 0) // gap above
					//boardGraphics.FillRectangle(myBrush, wX1, wY1, wX2 - wX1, dY1 - wY1);
     //           if (dX2 < wX2) //gap on the right
					//boardGraphics.FillRectangle(myBrush, dX2, wY1, wX2 - dX2, wY2 - wY1);
     //           if (dY2 < wY2) //gap below
					//boardGraphics.FillRectangle(myBrush, wX1, dY2, wX2 - wX1, wY2 - dY2);
            }
        }

        // pan, zoom and other display adjustments by user
        public static void setScaleIndex(int scaleIndex, int mouseX, int mouseY, int width, int height)
        {   // changes the scale while also adjusting the offsets to make sure the zooming action is centered on the mouse pointer
            float oldScale = scale;
            scale = 1;
            if (scaleIndex != 0) scale = (float)Math.Pow((double)10.0, (double)(scaleIndex / 100.0));
            offsetX -= mouseX / oldScale ;
            offsetY -= mouseY / oldScale;
            offsetX += mouseX / scale;
            offsetY += mouseY / scale;
        }
        public static void move(float deltaX, float deltaY)
        {   // Moves the image by an amount equivalent to the given screen pixel amounts specified
            offsetX += deltaX / scale;
            offsetY += deltaY / scale;

        }

		// File load and save
		public static void writeConfig( StreamWriter writer, List<boardImage> boards )
		{
			int numBoards = boards.Count();
			writer.WriteLine( "startBoardImages" );
			writer.WriteLine( "numBoardImages,{0}", numBoards );
			for( int i = 0; i < numBoards; i++ )
			{
				//writer.Write("layer,{0},", viaList[i].layer);
				writer.Write( "layerName,{0},",				boards[i].imageName			);
				writer.Write( "imageLocation,{0},",			boards[i].imageLocation		);
				writer.Write( "correctionX,{0},",			boards[i].correctionX		);
				writer.Write( "correctionY,{0},",			boards[i].correctionY		);
				writer.Write( "correctionScale,{0},",		boards[i].correctionScale	);
				writer.WriteLine( "layer,{0}",				boards[i].layer				);

			}
			writer.WriteLine( "endBoardImages" );
		}
		public static void readConfig( StreamReader reader, List<boardImage> boards, mainForm targetForm )
		{
			int numBoards = 0;
			string a;
			string[] b;
			int p;
			//try
			{
				// Get number of boards
				a = reader.ReadLine(); b = a.Split( ',' );
				if( !b[0].Equals( "startBoardImages" ) ) traceHandler.causeException( "Read mismatch " + "startBoardImages : " + b[0] );
				a = reader.ReadLine(); b = a.Split( ',' );
				if( !b[0].Equals( "numBoardImages" ) ) traceHandler.causeException( "Read mismatch " + "numBoardImages : " + b[0] );
				numBoards = int.Parse( b[1] );
				Console.WriteLine( "Reading {0} boards", numBoards );
				for( int i = 0; i < numBoards; i++ )
				{
					boardImage board = new boardImage (targetForm);

					// Get the parameters from the file
					a = reader.ReadLine(); b = a.Split( ',' );
					p = 0;
					if( !b[p++].Equals( "layerName" ) ) traceHandler.causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
					board.imageName = b[p++];
					if( !b[p++].Equals( "imageLocation" ) ) traceHandler.causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
					board.imageLocation = b[p++];
					if( !b[p++].Equals( "correctionX" ) ) traceHandler.causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
					board.correctionX = float.Parse( b[p++] );
					if( !b[p++].Equals( "correctionY" ) ) traceHandler.causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
					board.correctionY = float.Parse( b[p++] );
					if( !b[p++].Equals( "correctionScale" ) ) traceHandler.causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
					board.correctionScale = float.Parse( b[p++] );
					if( !b[p++].Equals( "layer" ) ) traceHandler.causeException( "Read mismatch " + i.ToString() + ", " + p.ToString() );
					board.layer = int.Parse( b[p++] );

					board.shown = boards.Count() == 0;                          // Make the first board image the active one
					board.pictureHolder.WaitOnLoad = true;
					board.pictureHolder.ImageLocation = board.imageLocation;    // Load the image into the control

					//boards.Add( new boardImage( targetForm ) );
					//int nBoards = boards.Count();
					//boards[nBoards - 1] = board;
					boards.Add( board );
				}
				a = reader.ReadLine(); b = a.Split( ',' );
				if( !b[0].Equals( "endBoardImages" ) ) traceHandler.causeException( "Read mismatch " + "endBoardImages : " + b[0] );
			}
			//catch
		}

		// Helper functions
		//public PointF screenCoordFromBoardCoordF(PointF boardPoint)  
  //      {
  //          float xS, yS, xB, yB;         
  //          xB = boardPoint.X;
  //          yB = boardPoint.Y;
  //          xS = (xB + offsetX + correctionX) * (scale * correctionScale);
  //          yS = (yB + offsetY + correctionY) * (scale * correctionScale);
  //          return new PointF(xS,yS);
  //      }
  //      public Point screenCoordFromBoardCoord(PointF boardPoint)
  //      {
  //          PointF p = screenCoordFromBoardCoordF(boardPoint);
  //          return new Point((int)p.X, (int)p.Y);
  //      }
  //      public PointF boardCoordFromScreenCoordF(PointF screenPoint) 
  //      {
  //          float xS, yS, xB, yB;         
  //          xS = screenPoint.X;
  //          yS = screenPoint.Y;
  //          xB = (-offsetX - correctionX) + xS / (scale * correctionScale);
  //          yB = (-offsetY - correctionY) + yS / (scale * correctionScale);
  //          return new PointF(xB,yB);

  //          //  xS = (xB + offsetX + correctionX) * (scale * correctionScale);
  //          //  p  = (x  +    r    +     s      ) * (t     *    u      );
  //          // using https://www.mathpapa.com/algebra-calculator.html
  //          //  xB = (-offsetX * scale * correctionScale - correctionX * scale * correctionScale + xS) / (scale * correctionScale);

  //      }
  //      public Point boardCoordFromScreenCoord(PointF screenPoint)
  //      {
  //          PointF p = boardCoordFromScreenCoordF(screenPoint);
  //          return new Point((int)p.X,(int)p.Y);            
  //      }

	   
		static public PointF screenCoordFromBoardCoordF( PointF boardPoint )
		{
			float xS, yS, xB, yB;
			xB = boardPoint.X;
			yB = boardPoint.Y;
			xS = ( xB + offsetX /*+ correctionX*/ ) * ( scale /** correctionScale*/ );
			yS = ( yB + offsetY /*+ correctionY*/ ) * ( scale /** correctionScale*/ );
			return new PointF( xS, yS );
		}
		static public Point screenCoordFromBoardCoord( PointF boardPoint )
		{
			PointF p = screenCoordFromBoardCoordF(boardPoint);
			return new Point( (int)p.X, (int)p.Y );
		}

		static public PointF boardCoordFromScreenCoordF( PointF screenPoint )
		{
			float xS, yS, xB, yB;
			xS = screenPoint.X;
			yS = screenPoint.Y;
			xB = ( -offsetX /* -correctionX*/ ) + xS / ( scale /* * correctionScale*/ );
			yB = ( -offsetY /* -correctionY*/ ) + yS / ( scale /* * correctionScale*/ );
			return new PointF( xB, yB );

			//  xS = (xB + offsetX + correctionX) * (scale * correctionScale);
			//  p  = (x  +    r    +     s      ) * (t     *    u      );
			// using https://www.mathpapa.com/algebra-calculator.html
			//  xB = (-offsetX * scale * correctionScale - correctionX * scale * correctionScale + xS) / (scale * correctionScale);

		}
		static public Point boardCoordFromScreenCoord( PointF screenPoint )
		{
			PointF p = boardCoordFromScreenCoordF(screenPoint);
			return new Point( (int)p.X, (int)p.Y );
		}

	}
}
