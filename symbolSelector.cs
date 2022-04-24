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
#pragma warning disable IDE1006 // Naming Styles

	public partial class symbolSelectorForm : Form
	{
		float drawScale = 0.25f;		// The drawing scale for the preview image
		string libraryFullPath;
		string selectedSymbolName = "";
		//List<kSymbol> symbolList = new List<kSymbol>();
		kSymbol sym = new kSymbol();

		public symbolSelectorForm()
		{
			InitializeComponent();
			//listBox1.Items.Add( "asasfffsd" );
			int w, h;
			w = symbolPictureBox.ClientRectangle.Width;
			h = symbolPictureBox.ClientRectangle.Width;
			var schemBitMap = new Bitmap( w, h );
			symbolPictureBox.Image = schemBitMap;
			//schemGraphics = Graphics.FromImage( circuitPictureBox.Image );



		}

		~symbolSelectorForm()
		{
			Console.WriteLine( "Finalizing symbol selector" );
		}

		//private void bagaboom_Click( object sender, EventArgs e )
		//{
		//	Console.WriteLine( "Component index {0}", mainForm.componentSelectorIndex );
		//}
		private void		symbolSelectorForm_FormClosed( object sender, FormClosedEventArgs e )
		{
			this.Dispose();
		}
		private void			  buttonOpenLibrary_Click( object sender, EventArgs e )
		{
			openLibraryDialog.InitialDirectory	= glob.symbolLibraryFolder;
			openLibraryDialog.FileName			= "";
			openLibraryDialog.Filter			= "Symbol Library (*.lib)|*.lib|All files (*.*)|*.*";
			openLibraryDialog.CheckPathExists	= true;
			openLibraryDialog.CheckFileExists	= true;
			openLibraryDialog.ShowDialog();
		}
		private void			 openLibraryDialog_FileOk( object sender, CancelEventArgs e )
		{
			libraryFullPath				= openLibraryDialog.FileName;
			glob.symbolLibraryFolder	= Path.GetDirectoryName( libraryFullPath );
			string a;
			string[] b;
			StreamReader reader;
			try
			{
				reader = new StreamReader( libraryFullPath );
			}
			catch( Exception )
			{
				return;
				//throw;
			}
			listBox1.Items.Clear();
			selectedSymbolName = "";
			string name = "";
			int pins = 0;	// the number of pins in the library symbol
			int numPins;	// the number of pins in the component selected in the schematic
			if( mainForm.componentSelectorIndex >= 0 )
			{
				component comp =  mainForm.traceHandler.componentss[mainForm.componentSelectorIndex];
				numPins = comp.pins.Count();
			}
			else
			{
				numPins = 0;
			}
			do
			{
				a = reader.ReadLine();
				b = a.Split( ' ' );
				if( b.Count() > 0 )
				{
					if( b[0] == "DEF" )
					{
						name = b[1];
						pins = 0;
					}
					if( b[0] == "X" )
					{
						int dmg = int.Parse( b[10] );
						if ( dmg == 0 || dmg == 1 )		// Kludge
						{
							pins++;
						}
					}
					if ( b[0] == "ENDDEF")
					{
						if
						(
							pins == numPins
							|| mainForm.componentSelectorIndex < 0
							|| checkBoxFilterByPins.Checked == false
						)
						{
							listBox1.Items.Add( string.Format( "{0,4:D}", pins ) + " - " + name );
						}
					}
				}
			} while( !reader.EndOfStream );
			reader.Close();
		}
		private void		listBox1_SelectedIndexChanged( object sender, EventArgs e )
		{

			// Import the symbol from the library
			sym = new kSymbol();
			if( listBox1.SelectedItem == null ) return;
			selectedSymbolName = listBox1.SelectedItem.ToString();
			if( selectedSymbolName.Length < 7 ) return;
			selectedSymbolName = selectedSymbolName.Substring(7);
			Console.WriteLine( "Selected item{0}: ", selectedSymbolName );
			schematic.importKSymbol( libraryFullPath, selectedSymbolName, ref sym );

			// Display a preview of the symbol
			float w,h,x,y;
			w = symbolPictureBox.Width  / ( drawScale  );   // width of the symbol preview window in units of the schematic     
			h = symbolPictureBox.Height / (  drawScale );

			int columns	= (int) ( 0.999 + Math.Sqrt( sym.parts ) );
			int rows	= (int) ( 0.999f + (float)sym.parts / (float)columns );

			int row		= 0;
			int column	= 0;

			var canvas = Graphics.FromImage(symbolPictureBox.Image);
			canvas.Clear( SystemColors.Control );
			for( int i = 1; i <= sym.parts; i++ )
			{
				mainForm.schem.drawKSymbol
				( 
					sym, i, 
					new PointF
					( 
						w * ( column * 2f + 1f ) / ( 2f * columns) ,
						h *	( row	 * 2f + 1f ) / ( 2f * rows   )
					), 
					canvas, 
					drawScale 
				);
				column++;
				if ( column >= columns ) { column = 0; row++; }
			}
			symbolPictureBox.Refresh();

			if( mainForm.componentSelectorIndex >= 0 ) 
			{
				component comp =  mainForm.traceHandler.componentss[mainForm.componentSelectorIndex];
				int numPins = comp.pins.Count();

				// Check that the symbol has the same number of pins as the component it shall represent
				// and that they all match
				bool pinMatch = true;
				if( sym.pins.Count() == numPins )
				{
					for ( int i = 0; i < numPins; i++ )
					{
						if ( sym.pins.FindIndex( r => r.pin == comp.pins[i].pinName ) < 0 ) pinMatch = false;
					}
				}
				else
				{
					pinMatch = false;
				}

				if ( pinMatch )
				{
					
					buttonApply.Enabled = true;
				}
				else
				{
					buttonApply.Enabled = false;
				}
			}
		}
		private void					buttonApply_Click( object sender, EventArgs e )
		{
			//buttonApply.Enabled = false;
			if( mainForm.componentSelectorIndex < 0 ) return;
			component comp =  mainForm.traceHandler.componentss[mainForm.componentSelectorIndex];
			int numPins = comp.pins.Count();

			// Re check that the symbol has the same number of pins as the component it shall represent
			// and that they all match
			bool pinMatch = true;
			if( sym.pins.Count() == numPins )
			{
				for( int i = 0; i < numPins; i++ )
				{
					if( sym.pins.FindIndex( r => r.pin == comp.pins[i].pinName ) < 0 ) pinMatch = false;
				}
			}
			else
			{
				pinMatch = false;
			}

			if( pinMatch )
			{
				while ( comp.sections.Count() > sym.parts + 1)
				{
					comp.sections.RemoveAt( comp.sections.Count()-1 );
				}
																	 
				comp.symbol = sym;
				comp.desigType = sym.prefix;
				//copy the pin co-ordinates from the symbol to the pin in the component
				int maxPart = 0;
				for( int j = 0; j < numPins && j < comp.symbol.pins.Count(); j++ )
				{
					string pinNumber = comp.pins[j].pinNumber;
					int kPinIndex = comp.symbol.pins.FindIndex( r => r.pin == pinNumber );
					comp.pins[j].schemPosition = comp.symbol.pins[kPinIndex].position;
					comp.pins[j].section = new int[] { comp.symbol.pins[kPinIndex].part };
					if( comp.symbol.pins[kPinIndex].part > maxPart ) maxPart = comp.symbol.pins[kPinIndex].part;
				}

				// Create new sections in the component if snecessary
				int tmpOffset = -1500;
				while( maxPart >= comp.sections.Count() )
				{
					component.section sec = new component.section( sectionName: char.ToString( (char)( 65 + comp.sections.Count() ) ) ) ;
					// Provisionally set the positions starting from that of the original generic component and movin down for each section 
					sec.position = PointF.Add( comp.sections[0].position, new SizeF( 0, tmpOffset += 1500 ) );
					comp.sections.Add( sec );
					comp.schemUpdateBoundBox( comp.sections.IndexOf( sec ), hasSymbol: comp.symbol != null );
				}

				glob.progress.Report( "Schematic symbol changed" );	   // Lets the main form know that a refresh is needed due to the changed symbol
			}
			else
			{
				Console.WriteLine( "Pin mismatch" );
			}
		}
		private void  checkBoxFilterByPins_CheckedChanged( object sender, EventArgs e )
		{
			openLibraryDialog_FileOk( null, null );
		}
		private void				   buttonZoomIn_Click( object sender, EventArgs e )
		{
			drawScale *= 1.4142f;
			if ( drawScale > 2 ) drawScale = 2;
			listBox1_SelectedIndexChanged( null, null );
			Console.WriteLine( "Symbol preview scale {0}", drawScale );
			glob.progress.Report( "Zing!!!" );
		}
		private void				  buttonZoomOut_Click( object sender, EventArgs e )
		{
			drawScale /= 1.4142f;
			if ( drawScale < 0.03125 ) drawScale = 0.03125f;
			listBox1_SelectedIndexChanged( null, null );
			Console.WriteLine( "Symbol preview scale {0}", drawScale );
			glob.progress.Report( "Zong!!!" );
		}
		private void		 symbolSelectorForm_ResizeEnd( object sender, EventArgs e )
		{
			int w, h;
			w = symbolPictureBox.ClientRectangle.Width;
			h = symbolPictureBox.ClientRectangle.Width;
			var schemBitMap = new Bitmap( w, h );
			symbolPictureBox.Image = schemBitMap;
			listBox1_SelectedIndexChanged( null, null );
		}
	}
}
