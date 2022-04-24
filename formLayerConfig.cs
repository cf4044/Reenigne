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

	public partial class formLayerConfig : Form
	{
		public int	refreshTimer     = 0;
		public bool updatingList    = false;

		public formLayerConfig()
		{
			InitializeComponent();

			//===============================================================================
			// Set up  the handler for communication from other forms
			//-------------------------------------------------------------------------------
			//glob.progress = new Progress<string>
			//(
			//	( string p ) =>
			//	{
			//		Console.WriteLine( "Progress:{0}", p );
			//		switch( p )
			//		{
			//			case "Layer list needs update":
			//				refreshList();
			//				break;
			//		}
			//	}
			//);

			refreshList();	
		}


		// Layer gridview actions
		private void dataGridViewLayer_CellEndEdit			( object sender, DataGridViewCellEventArgs e )
		{  	// This handles editing of the layer name
			Console.WriteLine( "dataGridViewLayer_CellEndEdit" );
			if ( e.ColumnIndex == layerName.Index & e.RowIndex >= 0 )
			{
				layerInfo lyr = mainForm.traceHandler.layers[e.RowIndex];
				lyr.name = (string)dataGridViewLayer.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
				Console.WriteLine
				( 
					"  (1)Changed layer {0} name to {1}", 
					e.RowIndex, 
					dataGridViewLayer.Rows[e.RowIndex].Cells[e.ColumnIndex].Value 
				);	
			}
		}
		private void dataGridViewLayer_CellContentClick		( object sender, DataGridViewCellEventArgs e )
		{	// This handler is to force an immediate response to a change in the checkbox value see logbook entry of 27/02/2022
			// It is due to a bug or oversight by Microsoft and shouldn;t really be necessary
			Console.WriteLine( "dataGridViewLayer cell content click" );
			if( e.ColumnIndex == mirrored.Index & e.RowIndex >= 0 )
			{
				dataGridViewLayer.CommitEdit( DataGridViewDataErrorContexts.Commit );
			}
		}
		private void dataGridViewLayer_CellValueChanged		( object sender, DataGridViewCellEventArgs e )
		{   // This handles change of the 'mirrored' parameter checkbox
			// note that this needs there to be the CommitEdit 	in the CellContentClick handler (above) as otherwise it won't trigger immediately 
			//Console.WriteLine( "dataGridViewLayer_CellValueChanged" );
			if( e.ColumnIndex == mirrored.Index & e.RowIndex >= 0 )
			{
				layerInfo lyr = mainForm.traceHandler.layers[e.RowIndex];
				lyr.mirrored = (bool)dataGridViewLayer.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
				//Console.WriteLine
				//(
				//	"  (5)Changed layer {0} mirrored to {1}",
				//	e.RowIndex,
				//	dataGridViewLayer.Rows[e.RowIndex].Cells[e.ColumnIndex].Value
				//);
			}
		}
		private void dataGridViewLayer_CellMouseUp			( object sender, DataGridViewCellMouseEventArgs e ) // not used yet
		{

		}
		private void dataGridViewLayer_CellMouseClick		( object sender, DataGridViewCellMouseEventArgs e ) // not used yet
		{

		}
		private void dataGridViewLayer_CellMouseDown		( object sender, DataGridViewCellMouseEventArgs e ) // not used yet
		{

		}
		// Layer buttons
		private void buttonAddLayer_Click					( object sender, EventArgs e )
		{
			layerInfo lyr = new layerInfo()
			{
				layer       = mainForm.traceHandler.layers.Count(),
				name        = "Layer" + mainForm.traceHandler.layers.Count(),
				mirrored    = false

			};
			mainForm.traceHandler.layers.Add( lyr );
			refreshList();
		}
		private void buttonDeleteLayer_Click				( object sender, EventArgs e )
		{
			if( mainForm.traceHandler.layers.Count() > 1 )
			{
				mainForm.traceHandler.layers.RemoveAt( mainForm.traceHandler.layers.Count() - 1 );
				refreshList(); 
			}
		}

		// Board image gridview actions
		private void dataGridViewBoardImage_CellContentClick( object sender, DataGridViewCellEventArgs e ) // not used
		{
			Console.WriteLine( "CellContntClick" );
		}
		private void dataGridViewBoardImage_RowEnter		( object sender, DataGridViewCellEventArgs e )
		{   // Update the textboxes presenting the offsets and scale of the selected board image
			if( dataGridViewBoardImage.Rows[e.RowIndex].Cells[imageIndex.Index].Value != null )
			{
				int index               = (int)dataGridViewBoardImage.Rows[e.RowIndex].Cells[imageIndex.Index].Value;
				boardImage bI           = glob.mainForm.boardImages[index];
				textBoxXOffset.Text		= bI.correctionX.ToString();
				textBoxYOffset.Text		= bI.correctionY.ToString();
				textBoxScale.Text		= bI.correctionScale.ToString();
				textBoxImageName.Text	= bI.imageName;
				comboBoxLayer.Items.Clear();
				for( int i = 0; i < mainForm.traceHandler.layers.Count(); i++ )
				{
					comboBoxLayer.Items.Add
					( 
						i.ToString() + ":" +
						( 
							i < mainForm.traceHandler.layers.Count() ? 
							mainForm.traceHandler.layers[i].name :
							"n/a" 
						)
					);
				}
				comboBoxLayer.Text = 
						bI.layer.ToString() + ":" +
						( 
							bI.layer < mainForm.traceHandler.layers.Count() ? 
							mainForm.traceHandler.layers[bI.layer].name : 
							"n/a"
						);
				checkBoxVisible.Checked = bI.visible;
				pictureBoxPreview.ImageLocation = bI.imageLocation;
				Console.WriteLine( "BoardImage: Entered row of Image {0}", index );
			}
			else
			{
				textBoxXOffset.Text		= "";
				textBoxYOffset.Text		= "";
				textBoxScale.Text		= "";
				textBoxImageName.Text	= "";
				comboBoxLayer.Items.Clear();
				comboBoxLayer.Text		= "";
			}
		}
		private void dataGridViewBoardImage_CellEndEdit		( object sender, DataGridViewCellEventArgs e )
		{
			Console.WriteLine( "BoardImage: Cell end edit" );
			int index = e.RowIndex;
			if (index >= 0 )
			{
				boardImage bI = glob.mainForm.boardImages[index];
				if ( e.ColumnIndex == imageName.Index)
				{
					bI.imageName			= (string)dataGridViewBoardImage.Rows[index].Cells[imageName.Index].Value;
					textBoxImageName.Text	= bI.imageName;
					Console.WriteLine( "  Changed name to {0}", dataGridViewBoardImage.Rows[index].Cells[imageName.Index].Value );
				}
			}

		}
		private void dataGridViewBoardImage_CellValueChanged( object sender, DataGridViewCellEventArgs e ) // not used yet
		{
			//Console.WriteLine( "BoardImage: Cell value changed" );
		}
		private void dataGridViewBoardImage_CellValidated	( object sender, DataGridViewCellEventArgs e ) // not used yet
		{
			Console.WriteLine( "BoardImage: Cell validated" );

		}
		// Board image buttons and other controls
		private void buttonAddImage_Click					( object sender, EventArgs e )
		{
			boardImage board = new boardImage (glob.mainForm)
			{
				correctionX		= 0f,
				correctionY		= 0f,
				correctionScale = 1f,
				layer			= 0,
				shown			= true,
				visible			= true
			};
			glob.mainForm.boardImages.Add( board );
			refreshList();

		}
		private void buttonRemoveImage_Click				( object sender, EventArgs e ) // ToDo: not implemented yet
		{

		}
		private void buttonAsignImageFile_Click				( object sender, EventArgs e )
		{
			int index  = -1;
			if( dataGridViewBoardImage.SelectedCells.Count == 1 )
			{
				index = dataGridViewBoardImage.SelectedCells[0].RowIndex;
			}
			if( dataGridViewBoardImage.SelectedRows.Count == 1 )
			{
				index = dataGridViewBoardImage.SelectedRows[0].Index;
			}
			if( index >= 0)
			{
				index										= dataGridViewBoardImage.SelectedCells[0].RowIndex;
				boardImage bI                               = glob.mainForm.boardImages[index];
				string currentImageFilename                 = glob.mainForm.boardImages[index].imageLocation;
				if( currentImageFilename == "" )
				{
					currentImageFilename = glob.projectFolder;
				}
				openFileDialogAssignImage.FileName = currentImageFilename;
				openFileDialogAssignImage.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
				openFileDialogAssignImage.CheckPathExists = true;
				openFileDialogAssignImage.CheckFileExists = true;
				openFileDialogAssignImage.ShowDialog();
			}

		}
		private void buttonPushBack_Click					( object sender, EventArgs e )
		{                                                                                 // solution to select cell found at https://stackoverflow.com/questions/6265228/selecting-a-row-in-datagridview-programmatically
			int index;
			if( dataGridViewBoardImage.SelectedRows.Count == 1 )
			{
				index = dataGridViewBoardImage.SelectedRows[0].Index;
				if ( index > 0 && index < dataGridViewBoardImage.RowCount )
				{   
					boardImage bI = glob.mainForm.boardImages[index];
					glob.mainForm.boardImages[index] = glob.mainForm.boardImages[index - 1];
					glob.mainForm.boardImages[index - 1] = bI;
					refreshList();
					dataGridViewBoardImage.ClearSelection();
					dataGridViewBoardImage.Rows[index - 1].Selected = true;
					dataGridViewBoardImage.CurrentCell = dataGridViewBoardImage.Rows[index - 1].Cells[0];
					glob.progress.Report( "Board image changed" );
				}
			}
		}
		private void buttonBringForward_Click				( object sender, EventArgs e )
		{
			int index;
			if( dataGridViewBoardImage.SelectedRows.Count == 1 )
			{
				index = dataGridViewBoardImage.SelectedRows[0].Index;
				if( index >= 0 && index < dataGridViewBoardImage.RowCount - 1 )
				{
					boardImage bI = glob.mainForm.boardImages[index];
					glob.mainForm.boardImages[index] = glob.mainForm.boardImages[index + 1];
					glob.mainForm.boardImages[index + 1] = bI;
					refreshList();
					dataGridViewBoardImage.ClearSelection();
					dataGridViewBoardImage.Rows[index + 1].Selected = true;
					dataGridViewBoardImage.CurrentCell = dataGridViewBoardImage.Rows[index + 1].Cells[0];
					glob.progress.Report( "Board image changed" );
				}
			}
		}
 		private void comboBoxLayer_SelectedIndexChanged		( object sender, EventArgs e )
		{
			int index  = -1;
			if( dataGridViewBoardImage.SelectedCells.Count == 1 )
			{
				index = dataGridViewBoardImage.SelectedCells[0].RowIndex;
			}
			if( dataGridViewBoardImage.SelectedRows.Count == 1 )
			{
				index = dataGridViewBoardImage.SelectedRows[0].Index;
			}
			if( index >= 0 )
			{
				boardImage bI = glob.mainForm.boardImages[index];
				bI.layer = comboBoxLayer.SelectedIndex;
				dataGridViewBoardImage.Rows[index].Cells[layer.Index].Value = bI.layer;
				dataGridViewBoardImage.Rows[index].Cells[layerNameZ.Index].Value = mainForm.traceHandler.layers[bI.layer].name;

				//refreshTimer = 2;
				//glob.progress.Report( "Layer list needs update" );
			}
		}
		private void checkBoxVisible_CheckedChanged			( object sender, EventArgs e )
		{
			Console.WriteLine( "checkBoxVisible_CheckedChanged with updatingList = {0}", updatingList );
			if( updatingList ) return;
			int index  = -1;
			if( dataGridViewBoardImage.SelectedRows.Count == 1 )
			{
				index = dataGridViewBoardImage.SelectedRows[0].Index;
			}
			if( index >= 0 )
			{
				boardImage bI = glob.mainForm.boardImages[index];
				bI.visible = checkBoxVisible.Checked;
				refreshTimer = 2;
				glob.progress.Report( "Board image changed" );
			}
		}
		private void textBoxXOffset_TextChanged				( object sender, EventArgs e ) // not used yet
		{

		}
		private void textBox_Enter							( object sender, EventArgs e )
		{	// Select all the text in the box so typing a value overwrites the whole content
			Console.WriteLine( "Enter" );
			( (TextBox)sender ).SelectAll();
		}
		private void textBox_Leave							( object sender, EventArgs e )
		{
			Console.WriteLine( "Leave" );
			if( sender.Equals( textBoxScale ) )
			{	// on leaving the scale text box wraparound back to the XOffset input textbox
				textBoxXOffset.Focus();
			}
		}
		private void textBox_Validating						( object sender, CancelEventArgs e )
		{
			Console.WriteLine( "Validating {0}", sender.ToString() );
			Console.Write( "  Input value:{0}", ((TextBox)sender).Text  );
			if ( float.TryParse( ( (TextBox)sender ).Text, out float value )  )
			{
				Console.Write( " - valid" );

			}
			else
			{
				Console.Write( " - INVALID" );
				e.Cancel = true;
			}
			Console.WriteLine();
		}
		private void textBox_Validated						( object sender, EventArgs e )
		{	// Act on the changed value and redraw the board
			Console.Write( "Validated" );
			int index  = -1;
			if( dataGridViewBoardImage.SelectedRows.Count == 1 )
			{
				index = dataGridViewBoardImage.SelectedRows[0].Index;
			}
			if( index >= 0 )
			{
				boardImage bI = glob.mainForm.boardImages[index];
				float value = float.Parse( ((TextBox)sender).Text );
				if( sender == textBoxXOffset )
				{
					Console.Write( " xOffset" );
					bI.correctionX = value;	
				}
				if( sender == textBoxYOffset )
				{
					Console.Write( " yOffset" );
					bI.correctionY = value;
				}
				if( sender == textBoxScale )
				{
					Console.Write( " scale" );
					bI.correctionScale = value;
				}
				Console.WriteLine();
				glob.progress.Report( "Board image changed" );
			}





			
		}

		// Keyboard actions
		protected override bool ProcessCmdKey( ref Message msg, Keys keyData )
		{
			Console.WriteLine( "{0} : {1}", msg, keyData );
			switch ( keyData )
			{
				case Keys.Return:		// This makes the Return key act as confirmation of input and keeps the text box focused so the user can repeatedly try different values without having to click back into the control
					if( textBoxXOffset.Focused )
					{
						textBoxYOffset.Focus(); // Kludge - this is to temporarily remove the focus so it can be refocused - but there must be a better way to do it
						textBoxXOffset.Focus();
						keyData = Keys.None;
					}
					if( textBoxYOffset.Focused )
					{
						textBoxXOffset.Focus(); // Kludge - this is to temporarily remove the focus so it can be refocused - but there must be a better way to do it
						textBoxYOffset.Focus();
						keyData = Keys.None;
					}
					if( textBoxScale.Focused )
					{
						textBoxXOffset.Focus(); // Kludge - this is to temporarily remove the focus so it can be refocused - but there must be a better way to do it
						textBoxScale.Focus();
						keyData = Keys.None;
					}
					break;
			}
			
			if( keyData == Keys.None )
				return true;
			else
				return base.ProcessCmdKey( ref msg, keyData );
		}

		// Other actions
		private void timer1_Tick							( object sender, EventArgs e )
		{
			{
				if( refreshTimer > 0 )
				{
					refreshTimer--;
					if( refreshTimer <= 0 )
					{
						refreshList();
					}
				}
			}
		}
		private void openFileDialogAssignImage_FileOk		( object sender, CancelEventArgs e )
		{
			//if( dataGridViewBoardImage.SelectedRows.Count == 1 )

			int index  = -1;
			if( dataGridViewBoardImage.SelectedCells.Count == 1 )
			{
				index = dataGridViewBoardImage.SelectedCells[0].RowIndex;
			}
			if( dataGridViewBoardImage.SelectedRows.Count == 1 )
			{
				index = dataGridViewBoardImage.SelectedRows[0].Index;
			}
			if( index >= 0 )
			{
				boardImage bI                           = glob.mainForm.boardImages[index];
				string currentImageFilename             = openFileDialogAssignImage.FileName;
				dataGridViewBoardImage.Rows[index].Cells[fileName.Index].Value
														= Path.GetFileName( currentImageFilename );
				bI.imageLocation = currentImageFilename;
				bI.pictureHolder.WaitOnLoad = true;
				bI.pictureHolder.ImageLocation = currentImageFilename;
				pictureBoxPreview.ImageLocation = currentImageFilename;
				//glob.progress.Report( "Board image changed" );   // force a refresh of the main display
			}
		}


		// Other functions
		void refreshList()
		{   // fill in the gridviews for the layers and board images
			if( updatingList ) return;
			updatingList = true;

			// Memorise which row is selected, so the selection can be restored after the refresh
			int index = -1;
			if( dataGridViewBoardImage.SelectedRows.Count == 1 )
			{
				index = dataGridViewBoardImage.SelectedRows[0].Index;
				Console.WriteLine( "Row {0} was selected before refreshList",index );
			}

			dataGridViewLayer.Rows.Clear();
			for ( int i = 0; i < mainForm.traceHandler.layers.Count(); i++ )
			{
				layerInfo lyr = mainForm.traceHandler.layers[i];
				dataGridViewLayer.Rows.Add(); // ( i, lyr.layer, lyr.name, lyr.mirrored );
				DataGridViewRow dgvr = dataGridViewLayer.Rows[i];
				dgvr.Cells[layerIndex.Index].Value	= i.ToString();
				dgvr.Cells[   layerNo.Index].Value	= lyr.layer.ToString();
				dgvr.Cells[ layerName.Index].Value	= lyr.name;
				dgvr.Cells[  mirrored.Index].Value	= lyr.mirrored;
			}

			dataGridViewBoardImage.Rows.Clear();
			for ( int i = 0; i < glob.mainForm.boardImages.Count(); i++ )
			{
				boardImage bI = glob.mainForm.boardImages[i];
 				dataGridViewBoardImage.Rows.Add();// i, bI.visible, bI.layerName, Path.GetFileName( bI.imageLocation ), bI.layer, bI.layerName );
				DataGridViewRow dgvr = dataGridViewBoardImage.Rows[i];
				dgvr.Cells[   imageIndex.Index].Value	= i;
				dgvr.Cells[      visible.Index].Value	= bI.visible;
				dgvr.Cells[    imageName.Index].Value	= bI.imageName;
				dgvr.Cells[     fileName.Index].Value	= Path.GetFileName( bI.imageLocation );
				dgvr.Cells[        layer.Index].Value	= bI.layer.ToString();
				dgvr.Cells[   layerNameZ.Index].Value	= 
					bI.layer < mainForm.traceHandler.layers.Count() ? mainForm.traceHandler.layers[bI.layer].name : "n/a";
			}

			// Select the row that was selected before the refresh
			dataGridViewBoardImage.ClearSelection();
			if( index >= 0 )
			{
				dataGridViewBoardImage.Rows[index].Selected = true;
				dataGridViewBoardImage.CurrentCell = dataGridViewBoardImage.Rows[index].Cells[0];
			}
			updatingList = false;
		}

		private void dataGridViewBoardImage_CellContentClick_1( object sender, DataGridViewCellEventArgs e )
		{

		}


	}
}
