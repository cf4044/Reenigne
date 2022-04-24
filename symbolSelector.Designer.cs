namespace Reenigne
{
	partial class symbolSelectorForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.buttonOpenLibrary = new System.Windows.Forms.Button();
			this.openLibraryDialog = new System.Windows.Forms.OpenFileDialog();
			this.symbolPictureBox = new System.Windows.Forms.PictureBox();
			this.buttonApply = new System.Windows.Forms.Button();
			this.checkBoxFilterByPins = new System.Windows.Forms.CheckBox();
			this.buttonZoomIn = new System.Windows.Forms.Button();
			this.buttonZoomOut = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.symbolPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.listBox1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 16;
			this.listBox1.Location = new System.Drawing.Point(12, 6);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(276, 404);
			this.listBox1.TabIndex = 0;
			this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
			// 
			// buttonOpenLibrary
			// 
			this.buttonOpenLibrary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonOpenLibrary.Location = new System.Drawing.Point(12, 447);
			this.buttonOpenLibrary.Name = "buttonOpenLibrary";
			this.buttonOpenLibrary.Size = new System.Drawing.Size(113, 29);
			this.buttonOpenLibrary.TabIndex = 1;
			this.buttonOpenLibrary.Text = "Open Library";
			this.buttonOpenLibrary.UseVisualStyleBackColor = true;
			this.buttonOpenLibrary.Click += new System.EventHandler(this.buttonOpenLibrary_Click);
			// 
			// openLibraryDialog
			// 
			this.openLibraryDialog.FileName = "openFileDialog1";
			this.openLibraryDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openLibraryDialog_FileOk);
			// 
			// symbolPictureBox
			// 
			this.symbolPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.symbolPictureBox.Location = new System.Drawing.Point(294, 6);
			this.symbolPictureBox.Name = "symbolPictureBox";
			this.symbolPictureBox.Size = new System.Drawing.Size(721, 516);
			this.symbolPictureBox.TabIndex = 3;
			this.symbolPictureBox.TabStop = false;
			// 
			// buttonApply
			// 
			this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonApply.Enabled = false;
			this.buttonApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonApply.Location = new System.Drawing.Point(12, 483);
			this.buttonApply.Name = "buttonApply";
			this.buttonApply.Size = new System.Drawing.Size(276, 39);
			this.buttonApply.TabIndex = 4;
			this.buttonApply.Text = "Apply";
			this.buttonApply.UseVisualStyleBackColor = true;
			this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
			// 
			// checkBoxFilterByPins
			// 
			this.checkBoxFilterByPins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBoxFilterByPins.AutoSize = true;
			this.checkBoxFilterByPins.Checked = true;
			this.checkBoxFilterByPins.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxFilterByPins.Location = new System.Drawing.Point(12, 418);
			this.checkBoxFilterByPins.Name = "checkBoxFilterByPins";
			this.checkBoxFilterByPins.Size = new System.Drawing.Size(155, 17);
			this.checkBoxFilterByPins.TabIndex = 5;
			this.checkBoxFilterByPins.Text = "Filter by matching pin count";
			this.checkBoxFilterByPins.UseVisualStyleBackColor = true;
			this.checkBoxFilterByPins.CheckedChanged += new System.EventHandler(this.checkBoxFilterByPins_CheckedChanged);
			// 
			// buttonZoomIn
			// 
			this.buttonZoomIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonZoomIn.Location = new System.Drawing.Point(210, 418);
			this.buttonZoomIn.Name = "buttonZoomIn";
			this.buttonZoomIn.Size = new System.Drawing.Size(78, 26);
			this.buttonZoomIn.TabIndex = 6;
			this.buttonZoomIn.Text = "Zoom+";
			this.buttonZoomIn.UseVisualStyleBackColor = true;
			this.buttonZoomIn.Click += new System.EventHandler(this.buttonZoomIn_Click);
			// 
			// buttonZoomOut
			// 
			this.buttonZoomOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonZoomOut.Location = new System.Drawing.Point(210, 450);
			this.buttonZoomOut.Name = "buttonZoomOut";
			this.buttonZoomOut.Size = new System.Drawing.Size(78, 26);
			this.buttonZoomOut.TabIndex = 7;
			this.buttonZoomOut.Text = "Zoom-";
			this.buttonZoomOut.UseVisualStyleBackColor = true;
			this.buttonZoomOut.Click += new System.EventHandler(this.buttonZoomOut_Click);
			// 
			// symbolSelectorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1027, 529);
			this.Controls.Add(this.buttonZoomOut);
			this.Controls.Add(this.buttonZoomIn);
			this.Controls.Add(this.checkBoxFilterByPins);
			this.Controls.Add(this.buttonApply);
			this.Controls.Add(this.symbolPictureBox);
			this.Controls.Add(this.buttonOpenLibrary);
			this.Controls.Add(this.listBox1);
			this.Name = "symbolSelectorForm";
			this.Text = "symbolSelector";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.symbolSelectorForm_FormClosed);
			this.ResizeEnd += new System.EventHandler(this.symbolSelectorForm_ResizeEnd);
			((System.ComponentModel.ISupportInitialize)(this.symbolPictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button buttonOpenLibrary;
		private System.Windows.Forms.OpenFileDialog openLibraryDialog;
		private System.Windows.Forms.PictureBox symbolPictureBox;
		private System.Windows.Forms.Button buttonApply;
		private System.Windows.Forms.CheckBox checkBoxFilterByPins;
		private System.Windows.Forms.Button buttonZoomIn;
		private System.Windows.Forms.Button buttonZoomOut;
	}
}