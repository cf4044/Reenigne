namespace Reenigne
{
	partial class formLayerConfig
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formLayerConfig));
			this.dataGridViewLayer = new System.Windows.Forms.DataGridView();
			this.layerIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.layerNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.layerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.mirrored = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewBoardImage = new System.Windows.Forms.DataGridView();
			this.buttonAddLayer = new System.Windows.Forms.Button();
			this.buttonDeleteLayer = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonAddImage = new System.Windows.Forms.Button();
			this.buttonRemoveImage = new System.Windows.Forms.Button();
			this.buttonAsignImageFile = new System.Windows.Forms.Button();
			this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
			this.textBoxXOffset = new System.Windows.Forms.TextBox();
			this.textBoxYOffset = new System.Windows.Forms.TextBox();
			this.textBoxScale = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxImageName = new System.Windows.Forms.TextBox();
			this.checkBoxVisible = new System.Windows.Forms.CheckBox();
			this.comboBoxLayer = new System.Windows.Forms.ComboBox();
			this.openFileDialogAssignImage = new System.Windows.Forms.OpenFileDialog();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.buttonPushBack = new System.Windows.Forms.Button();
			this.buttonBringForward = new System.Windows.Forms.Button();
			this.imageIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.visible = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.imageName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.fileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.layer = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.layerNameZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewLayer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewBoardImage)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewLayer
			// 
			this.dataGridViewLayer.AllowUserToAddRows = false;
			this.dataGridViewLayer.AllowUserToDeleteRows = false;
			this.dataGridViewLayer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewLayer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.layerIndex,
            this.layerNo,
            this.layerName,
            this.mirrored});
			this.dataGridViewLayer.Location = new System.Drawing.Point(3, 4);
			this.dataGridViewLayer.Name = "dataGridViewLayer";
			this.dataGridViewLayer.Size = new System.Drawing.Size(357, 165);
			this.dataGridViewLayer.TabIndex = 0;
			this.dataGridViewLayer.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewLayer_CellContentClick);
			this.dataGridViewLayer.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewLayer_CellContentClick);
			this.dataGridViewLayer.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewLayer_CellEndEdit);
			this.dataGridViewLayer.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewLayer_CellMouseClick);
			this.dataGridViewLayer.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewLayer_CellMouseDown);
			this.dataGridViewLayer.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewLayer_CellMouseUp);
			this.dataGridViewLayer.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewLayer_CellValueChanged);
			// 
			// layerIndex
			// 
			this.layerIndex.HeaderText = "Index";
			this.layerIndex.MinimumWidth = 35;
			this.layerIndex.Name = "layerIndex";
			this.layerIndex.ReadOnly = true;
			this.layerIndex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.layerIndex.Width = 35;
			// 
			// layerNo
			// 
			this.layerNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.layerNo.HeaderText = "Layer";
			this.layerNo.Name = "layerNo";
			this.layerNo.ReadOnly = true;
			this.layerNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.layerNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.layerNo.Width = 39;
			// 
			// layerName
			// 
			this.layerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.layerName.HeaderText = "Name";
			this.layerName.Name = "layerName";
			this.layerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// mirrored
			// 
			this.mirrored.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.mirrored.HeaderText = "Mirrored";
			this.mirrored.Name = "mirrored";
			this.mirrored.Width = 51;
			// 
			// dataGridViewBoardImage
			// 
			this.dataGridViewBoardImage.AllowUserToAddRows = false;
			this.dataGridViewBoardImage.AllowUserToDeleteRows = false;
			this.dataGridViewBoardImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dataGridViewBoardImage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewBoardImage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.imageIndex,
            this.visible,
            this.imageName,
            this.fileName,
            this.layer,
            this.layerNameZ});
			this.dataGridViewBoardImage.Location = new System.Drawing.Point(3, 188);
			this.dataGridViewBoardImage.Name = "dataGridViewBoardImage";
			this.dataGridViewBoardImage.Size = new System.Drawing.Size(701, 249);
			this.dataGridViewBoardImage.TabIndex = 1;
			this.dataGridViewBoardImage.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBoardImage_CellContentClick_1);
			this.dataGridViewBoardImage.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBoardImage_CellEndEdit);
			this.dataGridViewBoardImage.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBoardImage_CellValidated);
			this.dataGridViewBoardImage.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBoardImage_CellValueChanged);
			this.dataGridViewBoardImage.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBoardImage_RowEnter);
			// 
			// buttonAddLayer
			// 
			this.buttonAddLayer.Location = new System.Drawing.Point(366, 3);
			this.buttonAddLayer.Name = "buttonAddLayer";
			this.buttonAddLayer.Size = new System.Drawing.Size(83, 28);
			this.buttonAddLayer.TabIndex = 2;
			this.buttonAddLayer.Text = "Add layer";
			this.buttonAddLayer.UseVisualStyleBackColor = true;
			this.buttonAddLayer.Click += new System.EventHandler(this.buttonAddLayer_Click);
			// 
			// buttonDeleteLayer
			// 
			this.buttonDeleteLayer.Location = new System.Drawing.Point(364, 122);
			this.buttonDeleteLayer.Name = "buttonDeleteLayer";
			this.buttonDeleteLayer.Size = new System.Drawing.Size(83, 42);
			this.buttonDeleteLayer.TabIndex = 3;
			this.buttonDeleteLayer.Text = "Remove last layer";
			this.buttonDeleteLayer.UseVisualStyleBackColor = true;
			this.buttonDeleteLayer.Click += new System.EventHandler(this.buttonDeleteLayer_Click);
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.dataGridViewLayer);
			this.panel1.Controls.Add(this.buttonDeleteLayer);
			this.panel1.Controls.Add(this.buttonAddLayer);
			this.panel1.Location = new System.Drawing.Point(3, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(454, 171);
			this.panel1.TabIndex = 4;
			// 
			// buttonAddImage
			// 
			this.buttonAddImage.Location = new System.Drawing.Point(710, 188);
			this.buttonAddImage.Name = "buttonAddImage";
			this.buttonAddImage.Size = new System.Drawing.Size(83, 37);
			this.buttonAddImage.TabIndex = 5;
			this.buttonAddImage.Text = "Add image";
			this.buttonAddImage.UseVisualStyleBackColor = true;
			this.buttonAddImage.Click += new System.EventHandler(this.buttonAddImage_Click);
			// 
			// buttonRemoveImage
			// 
			this.buttonRemoveImage.Enabled = false;
			this.buttonRemoveImage.Location = new System.Drawing.Point(799, 188);
			this.buttonRemoveImage.Name = "buttonRemoveImage";
			this.buttonRemoveImage.Size = new System.Drawing.Size(83, 37);
			this.buttonRemoveImage.TabIndex = 6;
			this.buttonRemoveImage.Text = "Remove Image";
			this.buttonRemoveImage.UseVisualStyleBackColor = true;
			this.buttonRemoveImage.Click += new System.EventHandler(this.buttonRemoveImage_Click);
			// 
			// buttonAsignImageFile
			// 
			this.buttonAsignImageFile.Location = new System.Drawing.Point(710, 231);
			this.buttonAsignImageFile.Name = "buttonAsignImageFile";
			this.buttonAsignImageFile.Size = new System.Drawing.Size(83, 37);
			this.buttonAsignImageFile.TabIndex = 7;
			this.buttonAsignImageFile.Text = "Assign image file";
			this.buttonAsignImageFile.UseVisualStyleBackColor = true;
			this.buttonAsignImageFile.Click += new System.EventHandler(this.buttonAsignImageFile_Click);
			// 
			// pictureBoxPreview
			// 
			this.pictureBoxPreview.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxPreview.InitialImage")));
			this.pictureBoxPreview.Location = new System.Drawing.Point(470, 12);
			this.pictureBoxPreview.Name = "pictureBoxPreview";
			this.pictureBoxPreview.Size = new System.Drawing.Size(439, 170);
			this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBoxPreview.TabIndex = 8;
			this.pictureBoxPreview.TabStop = false;
			this.pictureBoxPreview.WaitOnLoad = true;
			// 
			// textBoxXOffset
			// 
			this.textBoxXOffset.Location = new System.Drawing.Point(841, 326);
			this.textBoxXOffset.Name = "textBoxXOffset";
			this.textBoxXOffset.Size = new System.Drawing.Size(60, 20);
			this.textBoxXOffset.TabIndex = 9;
			this.textBoxXOffset.Text = "--------";
			this.textBoxXOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textBoxXOffset.TextChanged += new System.EventHandler(this.textBoxXOffset_TextChanged);
			this.textBoxXOffset.Enter += new System.EventHandler(this.textBox_Enter);
			this.textBoxXOffset.Leave += new System.EventHandler(this.textBox_Leave);
			this.textBoxXOffset.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
			this.textBoxXOffset.Validated += new System.EventHandler(this.textBox_Validated);
			// 
			// textBoxYOffset
			// 
			this.textBoxYOffset.Location = new System.Drawing.Point(841, 348);
			this.textBoxYOffset.Name = "textBoxYOffset";
			this.textBoxYOffset.Size = new System.Drawing.Size(60, 20);
			this.textBoxYOffset.TabIndex = 10;
			this.textBoxYOffset.Text = "--------";
			this.textBoxYOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textBoxYOffset.Enter += new System.EventHandler(this.textBox_Enter);
			this.textBoxYOffset.Leave += new System.EventHandler(this.textBox_Leave);
			this.textBoxYOffset.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
			this.textBoxYOffset.Validated += new System.EventHandler(this.textBox_Validated);
			// 
			// textBoxScale
			// 
			this.textBoxScale.Location = new System.Drawing.Point(841, 370);
			this.textBoxScale.Name = "textBoxScale";
			this.textBoxScale.Size = new System.Drawing.Size(60, 20);
			this.textBoxScale.TabIndex = 11;
			this.textBoxScale.Text = "--------";
			this.textBoxScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textBoxScale.Enter += new System.EventHandler(this.textBox_Enter);
			this.textBoxScale.Leave += new System.EventHandler(this.textBox_Leave);
			this.textBoxScale.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
			this.textBoxScale.Validated += new System.EventHandler(this.textBox_Validated);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(777, 326);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 15);
			this.label1.TabIndex = 12;
			this.label1.Text = "X offset";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(777, 348);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60, 15);
			this.label2.TabIndex = 13;
			this.label2.Text = "Y offset";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(777, 370);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 15);
			this.label3.TabIndex = 14;
			this.label3.Text = "Scale";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(710, 304);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(37, 20);
			this.label4.TabIndex = 17;
			this.label4.Text = "Layer";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(710, 280);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(37, 17);
			this.label5.TabIndex = 18;
			this.label5.Text = "Name";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxImageName
			// 
			this.textBoxImageName.Location = new System.Drawing.Point(753, 280);
			this.textBoxImageName.Name = "textBoxImageName";
			this.textBoxImageName.Size = new System.Drawing.Size(147, 20);
			this.textBoxImageName.TabIndex = 19;
			// 
			// checkBoxVisible
			// 
			this.checkBoxVisible.AutoSize = true;
			this.checkBoxVisible.Location = new System.Drawing.Point(710, 331);
			this.checkBoxVisible.Name = "checkBoxVisible";
			this.checkBoxVisible.Size = new System.Drawing.Size(56, 17);
			this.checkBoxVisible.TabIndex = 22;
			this.checkBoxVisible.Text = "Visible";
			this.checkBoxVisible.UseVisualStyleBackColor = true;
			this.checkBoxVisible.CheckedChanged += new System.EventHandler(this.checkBoxVisible_CheckedChanged);
			// 
			// comboBoxLayer
			// 
			this.comboBoxLayer.FormattingEnabled = true;
			this.comboBoxLayer.Location = new System.Drawing.Point(753, 304);
			this.comboBoxLayer.Name = "comboBoxLayer";
			this.comboBoxLayer.Size = new System.Drawing.Size(148, 21);
			this.comboBoxLayer.TabIndex = 24;
			this.comboBoxLayer.SelectedIndexChanged += new System.EventHandler(this.comboBoxLayer_SelectedIndexChanged);
			// 
			// openFileDialogAssignImage
			// 
			this.openFileDialogAssignImage.FileName = "*.*";
			this.openFileDialogAssignImage.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogAssignImage_FileOk);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 200;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// buttonPushBack
			// 
			this.buttonPushBack.Location = new System.Drawing.Point(710, 360);
			this.buttonPushBack.Margin = new System.Windows.Forms.Padding(1);
			this.buttonPushBack.Name = "buttonPushBack";
			this.buttonPushBack.Size = new System.Drawing.Size(56, 38);
			this.buttonPushBack.TabIndex = 25;
			this.buttonPushBack.Text = "Push back";
			this.buttonPushBack.UseVisualStyleBackColor = true;
			this.buttonPushBack.Click += new System.EventHandler(this.buttonPushBack_Click);
			// 
			// buttonBringForward
			// 
			this.buttonBringForward.Location = new System.Drawing.Point(710, 399);
			this.buttonBringForward.Margin = new System.Windows.Forms.Padding(1);
			this.buttonBringForward.Name = "buttonBringForward";
			this.buttonBringForward.Size = new System.Drawing.Size(56, 38);
			this.buttonBringForward.TabIndex = 26;
			this.buttonBringForward.Text = "Bring forward";
			this.buttonBringForward.UseVisualStyleBackColor = true;
			this.buttonBringForward.Click += new System.EventHandler(this.buttonBringForward_Click);
			// 
			// imageIndex
			// 
			this.imageIndex.HeaderText = "Index";
			this.imageIndex.Name = "imageIndex";
			this.imageIndex.ReadOnly = true;
			this.imageIndex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.imageIndex.Width = 35;
			// 
			// visible
			// 
			this.visible.HeaderText = "Visible";
			this.visible.Name = "visible";
			this.visible.ReadOnly = true;
			this.visible.Width = 39;
			// 
			// imageName
			// 
			this.imageName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.imageName.HeaderText = "Name";
			this.imageName.Name = "imageName";
			this.imageName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// fileName
			// 
			this.fileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.fileName.HeaderText = "File name";
			this.fileName.Name = "fileName";
			this.fileName.ReadOnly = true;
			this.fileName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// layer
			// 
			this.layer.HeaderText = "Layer";
			this.layer.Name = "layer";
			this.layer.ReadOnly = true;
			this.layer.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.layer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.layer.Width = 39;
			// 
			// layerNameZ
			// 
			this.layerNameZ.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.layerNameZ.HeaderText = "Layer name";
			this.layerNameZ.Name = "layerNameZ";
			this.layerNameZ.ReadOnly = true;
			this.layerNameZ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// formLayerConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(909, 449);
			this.Controls.Add(this.buttonBringForward);
			this.Controls.Add(this.buttonPushBack);
			this.Controls.Add(this.comboBoxLayer);
			this.Controls.Add(this.checkBoxVisible);
			this.Controls.Add(this.textBoxImageName);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxScale);
			this.Controls.Add(this.textBoxYOffset);
			this.Controls.Add(this.textBoxXOffset);
			this.Controls.Add(this.pictureBoxPreview);
			this.Controls.Add(this.buttonAsignImageFile);
			this.Controls.Add(this.buttonRemoveImage);
			this.Controls.Add(this.buttonAddImage);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.dataGridViewBoardImage);
			this.Location = new System.Drawing.Point(2338, 1400);
			this.Name = "formLayerConfig";
			this.Text = "formLayerConfig";
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewLayer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewBoardImage)).EndInit();
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewLayer;
		private System.Windows.Forms.DataGridView dataGridViewBoardImage;
		private System.Windows.Forms.Button buttonAddLayer;
		private System.Windows.Forms.Button buttonDeleteLayer;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button buttonAddImage;
		private System.Windows.Forms.Button buttonRemoveImage;
		private System.Windows.Forms.Button buttonAsignImageFile;
		private System.Windows.Forms.PictureBox pictureBoxPreview;
		private System.Windows.Forms.TextBox textBoxXOffset;
		private System.Windows.Forms.TextBox textBoxYOffset;
		private System.Windows.Forms.TextBox textBoxScale;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DataGridViewTextBoxColumn layerIndex;
		private System.Windows.Forms.DataGridViewTextBoxColumn layerNo;
		private System.Windows.Forms.DataGridViewTextBoxColumn layerName;
		private System.Windows.Forms.DataGridViewCheckBoxColumn mirrored;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxImageName;
		private System.Windows.Forms.CheckBox checkBoxVisible;
		private System.Windows.Forms.ComboBox comboBoxLayer;
		private System.Windows.Forms.OpenFileDialog openFileDialogAssignImage;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Button buttonPushBack;
		private System.Windows.Forms.Button buttonBringForward;
		private System.Windows.Forms.DataGridViewTextBoxColumn imageIndex;
		private System.Windows.Forms.DataGridViewCheckBoxColumn visible;
		private System.Windows.Forms.DataGridViewTextBoxColumn imageName;
		private System.Windows.Forms.DataGridViewTextBoxColumn fileName;
		private System.Windows.Forms.DataGridViewTextBoxColumn layer;
		private System.Windows.Forms.DataGridViewTextBoxColumn layerNameZ;
	}
}