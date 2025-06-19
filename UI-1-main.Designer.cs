namespace Reenigne
{
    partial class mainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
			this.boardPictureBox = new System.Windows.Forms.PictureBox();
			this.boardContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.insertViaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.insertThroughHoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.drawTraceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
			this.circuitPictureBox = new System.Windows.Forms.PictureBox();
			this.circuitContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.buttonRefresh = new System.Windows.Forms.Button();
			this.scrollBarBoardZoom = new System.Windows.Forms.HScrollBar();
			this.panelLeft = new System.Windows.Forms.Panel();
			this.radioButtonModePlaceComponent = new System.Windows.Forms.RadioButton();
			this.contextMenuComponentType = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.numPinsHeader = new System.Windows.Forms.ToolStripMenuItem();
			this.numPinsTextBox = new System.Windows.Forms.ToolStripTextBox();
			this.padSizeHeader = new System.Windows.Forms.ToolStripMenuItem();
			this.padSizeTextBox = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.compTypeHeader = new System.Windows.Forms.ToolStripMenuItem();
			this.compTypeSIL = new System.Windows.Forms.ToolStripMenuItem();
			this.compTypeDIL = new System.Windows.Forms.ToolStripMenuItem();
			this.compTypeCircular = new System.Windows.Forms.ToolStripMenuItem();
			this.compTypeSquare = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.outlineHeader = new System.Windows.Forms.ToolStripMenuItem();
			this.outlineRectangular = new System.Windows.Forms.ToolStripMenuItem();
			this.outlineCircular = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.pinTypeHeader = new System.Windows.Forms.ToolStripMenuItem();
			this.pinTypeSmd = new System.Windows.Forms.ToolStripMenuItem();
			this.pinTypeThruHole = new System.Windows.Forms.ToolStripMenuItem();
			this.buttonRetraceNets = new System.Windows.Forms.Button();
			this.radioButtonModeSelect = new System.Windows.Forms.RadioButton();
			this.contextMenuSelectionType = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.pointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.traceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.netToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viaThruHoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.componentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.labelBoardMode = new System.Windows.Forms.Label();
			this.radioButtonModeDropThroughHole = new System.Windows.Forms.RadioButton();
			this.radioButtonModeDropVia = new System.Windows.Forms.RadioButton();
			this.radioButtonModeDrawTrace = new System.Windows.Forms.RadioButton();
			this.textBoxLayerName = new System.Windows.Forms.TextBox();
			this.buttonSwitchBoardLayerView = new System.Windows.Forms.Button();
			this.panelMiddle = new System.Windows.Forms.Panel();
			this.checkBoxCenterOnSelection = new System.Windows.Forms.CheckBox();
			this.checkBoxBreak = new System.Windows.Forms.CheckBox();
			this.textBoxCoordinates = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.buttonShrinkCircuit = new System.Windows.Forms.Button();
			this.buttonShrinkBoard = new System.Windows.Forms.Button();
			this.panelComponentInfo = new System.Windows.Forms.Panel();
			this.textBoxComponentName = new System.Windows.Forms.TextBox();
			this.buttonCompDataSet = new System.Windows.Forms.Button();
			this.textBoxDesigPart = new System.Windows.Forms.TextBox();
			this.textBoxDesigNum = new System.Windows.Forms.TextBox();
			this.textBoxDesigType = new System.Windows.Forms.TextBox();
			this.panelRight = new System.Windows.Forms.Panel();
			this.panelBottom = new System.Windows.Forms.Panel();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.panelTop = new System.Windows.Forms.Panel();
			this.buttonCheckPoint = new System.Windows.Forms.Button();
			this.buttonOpen = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.symbolAssignmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.layerConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.openProjectDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveProjectDialog = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.boardPictureBox)).BeginInit();
			this.boardContextMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.circuitPictureBox)).BeginInit();
			this.circuitContextMenu.SuspendLayout();
			this.panelLeft.SuspendLayout();
			this.contextMenuComponentType.SuspendLayout();
			this.contextMenuSelectionType.SuspendLayout();
			this.panelMiddle.SuspendLayout();
			this.panelComponentInfo.SuspendLayout();
			this.panelBottom.SuspendLayout();
			this.panelTop.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// boardPictureBox
			// 
			this.boardPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.boardPictureBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.boardPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.boardPictureBox.ContextMenuStrip = this.boardContextMenu;
			this.boardPictureBox.Location = new System.Drawing.Point(109, 105);
			this.boardPictureBox.Name = "boardPictureBox";
			this.boardPictureBox.Size = new System.Drawing.Size(481, 557);
			this.boardPictureBox.TabIndex = 0;
			this.boardPictureBox.TabStop = false;
			this.boardPictureBox.Click += new System.EventHandler(this.boardPictureBox_Click);
			// 
			// boardContextMenu
			// 
			this.boardContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertViaToolStripMenuItem,
            this.insertThroughHoleToolStripMenuItem,
            this.drawTraceToolStripMenuItem,
            this.toolStripTextBox1});
			this.boardContextMenu.Name = "boardContextMenu";
			this.boardContextMenu.Size = new System.Drawing.Size(176, 95);
			this.boardContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.boardContextMenu_Opening);
			// 
			// insertViaToolStripMenuItem
			// 
			this.insertViaToolStripMenuItem.Name = "insertViaToolStripMenuItem";
			this.insertViaToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
			this.insertViaToolStripMenuItem.Text = "Insert via";
			// 
			// insertThroughHoleToolStripMenuItem
			// 
			this.insertThroughHoleToolStripMenuItem.Name = "insertThroughHoleToolStripMenuItem";
			this.insertThroughHoleToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
			this.insertThroughHoleToolStripMenuItem.Text = "Insert through hole";
			// 
			// drawTraceToolStripMenuItem
			// 
			this.drawTraceToolStripMenuItem.Name = "drawTraceToolStripMenuItem";
			this.drawTraceToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
			this.drawTraceToolStripMenuItem.Text = "Draw trace";
			// 
			// toolStripTextBox1
			// 
			this.toolStripTextBox1.Name = "toolStripTextBox1";
			this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
			this.toolStripTextBox1.Text = "some text";
			// 
			// circuitPictureBox
			// 
			this.circuitPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.circuitPictureBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.circuitPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.circuitPictureBox.ContextMenuStrip = this.circuitContextMenu;
			this.circuitPictureBox.Location = new System.Drawing.Point(702, 107);
			this.circuitPictureBox.Name = "circuitPictureBox";
			this.circuitPictureBox.Size = new System.Drawing.Size(481, 557);
			this.circuitPictureBox.TabIndex = 1;
			this.circuitPictureBox.TabStop = false;
			// 
			// circuitContextMenu
			// 
			this.circuitContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
			this.circuitContextMenu.Name = "schemContextMenu";
			this.circuitContextMenu.Size = new System.Drawing.Size(155, 26);
			this.circuitContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.circuitContextMenu_Opening);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(154, 22);
			this.toolStripMenuItem1.Text = "----------------";
			// 
			// buttonRefresh
			// 
			this.buttonRefresh.Location = new System.Drawing.Point(3, 27);
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.Size = new System.Drawing.Size(107, 30);
			this.buttonRefresh.TabIndex = 2;
			this.buttonRefresh.Text = "Refresh";
			this.buttonRefresh.UseVisualStyleBackColor = true;
			this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
			// 
			// scrollBarBoardZoom
			// 
			this.scrollBarBoardZoom.Location = new System.Drawing.Point(113, 27);
			this.scrollBarBoardZoom.Minimum = -100;
			this.scrollBarBoardZoom.Name = "scrollBarBoardZoom";
			this.scrollBarBoardZoom.Size = new System.Drawing.Size(80, 17);
			this.scrollBarBoardZoom.TabIndex = 3;
			// 
			// panelLeft
			// 
			this.panelLeft.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.panelLeft.BackColor = System.Drawing.SystemColors.Control;
			this.panelLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelLeft.Controls.Add(this.radioButtonModePlaceComponent);
			this.panelLeft.Controls.Add(this.buttonRetraceNets);
			this.panelLeft.Controls.Add(this.radioButtonModeSelect);
			this.panelLeft.Controls.Add(this.labelBoardMode);
			this.panelLeft.Controls.Add(this.radioButtonModeDropThroughHole);
			this.panelLeft.Controls.Add(this.radioButtonModeDropVia);
			this.panelLeft.Controls.Add(this.radioButtonModeDrawTrace);
			this.panelLeft.Controls.Add(this.textBoxLayerName);
			this.panelLeft.Controls.Add(this.buttonSwitchBoardLayerView);
			this.panelLeft.Location = new System.Drawing.Point(3, 107);
			this.panelLeft.Name = "panelLeft";
			this.panelLeft.Size = new System.Drawing.Size(100, 553);
			this.panelLeft.TabIndex = 4;
			// 
			// radioButtonModePlaceComponent
			// 
			this.radioButtonModePlaceComponent.AutoSize = true;
			this.radioButtonModePlaceComponent.ContextMenuStrip = this.contextMenuComponentType;
			this.radioButtonModePlaceComponent.Location = new System.Drawing.Point(7, 111);
			this.radioButtonModePlaceComponent.Name = "radioButtonModePlaceComponent";
			this.radioButtonModePlaceComponent.Size = new System.Drawing.Size(79, 17);
			this.radioButtonModePlaceComponent.TabIndex = 12;
			this.radioButtonModePlaceComponent.Text = "Component";
			this.toolTip1.SetToolTip(this.radioButtonModePlaceComponent, "Place, move, modify or remove a component");
			this.radioButtonModePlaceComponent.UseVisualStyleBackColor = true;
			this.radioButtonModePlaceComponent.CheckedChanged += new System.EventHandler(this.radioButtonModePlaceComponent_CheckedChanged);
			// 
			// contextMenuComponentType
			// 
			this.contextMenuComponentType.BackColor = System.Drawing.SystemColors.Control;
			this.contextMenuComponentType.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.numPinsHeader,
            this.numPinsTextBox,
            this.padSizeHeader,
            this.padSizeTextBox,
            this.toolStripSeparator2,
            this.compTypeHeader,
            this.compTypeSIL,
            this.compTypeDIL,
            this.compTypeCircular,
            this.compTypeSquare,
            this.toolStripSeparator3,
            this.outlineHeader,
            this.outlineRectangular,
            this.outlineCircular,
            this.toolStripSeparator4,
            this.pinTypeHeader,
            this.pinTypeSmd,
            this.pinTypeThruHole});
			this.contextMenuComponentType.Name = "contextMenuComponentType";
			this.contextMenuComponentType.Size = new System.Drawing.Size(165, 358);
			this.contextMenuComponentType.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuComponentType_ItemClicked);
			// 
			// numPinsHeader
			// 
			this.numPinsHeader.Name = "numPinsHeader";
			this.numPinsHeader.Size = new System.Drawing.Size(164, 22);
			this.numPinsHeader.Text = "Pins:";
			// 
			// numPinsTextBox
			// 
			this.numPinsTextBox.Name = "numPinsTextBox";
			this.numPinsTextBox.Size = new System.Drawing.Size(100, 23);
			this.numPinsTextBox.Text = "8";
			this.numPinsTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numPinsTextBox.TextChanged += new System.EventHandler(this.numPinsTextBox_Validated);
			// 
			// padSizeHeader
			// 
			this.padSizeHeader.Name = "padSizeHeader";
			this.padSizeHeader.Size = new System.Drawing.Size(164, 22);
			this.padSizeHeader.Text = "Pad size";
			// 
			// padSizeTextBox
			// 
			this.padSizeTextBox.Name = "padSizeTextBox";
			this.padSizeTextBox.Size = new System.Drawing.Size(100, 23);
			this.padSizeTextBox.Text = "4";
			this.padSizeTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.padSizeTextBox.TextChanged += new System.EventHandler(this.padSizeTextBox_TextChanged);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(161, 6);
			// 
			// compTypeHeader
			// 
			this.compTypeHeader.Name = "compTypeHeader";
			this.compTypeHeader.Size = new System.Drawing.Size(164, 22);
			this.compTypeHeader.Text = "Pin Layout";
			// 
			// compTypeSIL
			// 
			this.compTypeSIL.Name = "compTypeSIL";
			this.compTypeSIL.Size = new System.Drawing.Size(164, 22);
			this.compTypeSIL.Text = "    Single in line";
			// 
			// compTypeDIL
			// 
			this.compTypeDIL.Name = "compTypeDIL";
			this.compTypeDIL.Size = new System.Drawing.Size(164, 22);
			this.compTypeDIL.Text = "    Dual in line";
			// 
			// compTypeCircular
			// 
			this.compTypeCircular.Name = "compTypeCircular";
			this.compTypeCircular.Size = new System.Drawing.Size(164, 22);
			this.compTypeCircular.Text = "    Circular";
			// 
			// compTypeSquare
			// 
			this.compTypeSquare.Name = "compTypeSquare";
			this.compTypeSquare.Size = new System.Drawing.Size(164, 22);
			this.compTypeSquare.Text = "    Square";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(161, 6);
			// 
			// outlineHeader
			// 
			this.outlineHeader.Name = "outlineHeader";
			this.outlineHeader.Size = new System.Drawing.Size(164, 22);
			this.outlineHeader.Text = "Outline";
			// 
			// outlineRectangular
			// 
			this.outlineRectangular.Name = "outlineRectangular";
			this.outlineRectangular.Size = new System.Drawing.Size(164, 22);
			this.outlineRectangular.Text = "    Rectangular";
			// 
			// outlineCircular
			// 
			this.outlineCircular.Name = "outlineCircular";
			this.outlineCircular.Size = new System.Drawing.Size(164, 22);
			this.outlineCircular.Text = "    Circular";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(161, 6);
			// 
			// pinTypeHeader
			// 
			this.pinTypeHeader.Name = "pinTypeHeader";
			this.pinTypeHeader.Size = new System.Drawing.Size(164, 22);
			this.pinTypeHeader.Text = "Pin type";
			// 
			// pinTypeSmd
			// 
			this.pinTypeSmd.Name = "pinTypeSmd";
			this.pinTypeSmd.Size = new System.Drawing.Size(164, 22);
			this.pinTypeSmd.Text = "    Surface mount";
			// 
			// pinTypeThruHole
			// 
			this.pinTypeThruHole.Name = "pinTypeThruHole";
			this.pinTypeThruHole.Size = new System.Drawing.Size(164, 22);
			this.pinTypeThruHole.Text = "    Through hole";
			// 
			// buttonRetraceNets
			// 
			this.buttonRetraceNets.Location = new System.Drawing.Point(4, 132);
			this.buttonRetraceNets.Name = "buttonRetraceNets";
			this.buttonRetraceNets.Size = new System.Drawing.Size(89, 24);
			this.buttonRetraceNets.TabIndex = 11;
			this.buttonRetraceNets.Text = "Retrace nets";
			this.buttonRetraceNets.UseVisualStyleBackColor = true;
			this.buttonRetraceNets.Click += new System.EventHandler(this.buttonRetraceNets_Click);
			// 
			// radioButtonModeSelect
			// 
			this.radioButtonModeSelect.AutoSize = true;
			this.radioButtonModeSelect.Checked = true;
			this.radioButtonModeSelect.ContextMenuStrip = this.contextMenuSelectionType;
			this.radioButtonModeSelect.Location = new System.Drawing.Point(7, 19);
			this.radioButtonModeSelect.Name = "radioButtonModeSelect";
			this.radioButtonModeSelect.Size = new System.Drawing.Size(93, 17);
			this.radioButtonModeSelect.TabIndex = 10;
			this.radioButtonModeSelect.TabStop = true;
			this.radioButtonModeSelect.Text = "Select/identify";
			this.radioButtonModeSelect.UseVisualStyleBackColor = true;
			this.radioButtonModeSelect.CheckedChanged += new System.EventHandler(this.radioButtonModeSelect_CheckedChanged);
			// 
			// contextMenuSelectionType
			// 
			this.contextMenuSelectionType.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.noneToolStripMenuItem,
            this.toolStripSeparator1,
            this.pointToolStripMenuItem,
            this.lineToolStripMenuItem,
            this.traceToolStripMenuItem,
            this.netToolStripMenuItem,
            this.viaThruHoleToolStripMenuItem,
            this.componentToolStripMenuItem});
			this.contextMenuSelectionType.Name = "contextMenuSelectionType";
			this.contextMenuSelectionType.Size = new System.Drawing.Size(150, 186);
			this.contextMenuSelectionType.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuSelectionType_ItemClicked);
			// 
			// allToolStripMenuItem
			// 
			this.allToolStripMenuItem.Name = "allToolStripMenuItem";
			this.allToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.allToolStripMenuItem.Text = "All";
			// 
			// noneToolStripMenuItem
			// 
			this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
			this.noneToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.noneToolStripMenuItem.Text = "None";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(146, 6);
			// 
			// pointToolStripMenuItem
			// 
			this.pointToolStripMenuItem.CheckOnClick = true;
			this.pointToolStripMenuItem.Name = "pointToolStripMenuItem";
			this.pointToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.pointToolStripMenuItem.Tag = "boardSelectionModes.point";
			this.pointToolStripMenuItem.Text = "Point";
			// 
			// lineToolStripMenuItem
			// 
			this.lineToolStripMenuItem.CheckOnClick = true;
			this.lineToolStripMenuItem.Name = "lineToolStripMenuItem";
			this.lineToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.lineToolStripMenuItem.Tag = "boardSelectionModes.Line";
			this.lineToolStripMenuItem.Text = "Line";
			// 
			// traceToolStripMenuItem
			// 
			this.traceToolStripMenuItem.CheckOnClick = true;
			this.traceToolStripMenuItem.Name = "traceToolStripMenuItem";
			this.traceToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.traceToolStripMenuItem.Tag = "boardSelectionModes.trace";
			this.traceToolStripMenuItem.Text = "Segment";
			// 
			// netToolStripMenuItem
			// 
			this.netToolStripMenuItem.Checked = true;
			this.netToolStripMenuItem.CheckOnClick = true;
			this.netToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.netToolStripMenuItem.Name = "netToolStripMenuItem";
			this.netToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.netToolStripMenuItem.Tag = "boardSelectionModes.net";
			this.netToolStripMenuItem.Text = "Net";
			// 
			// viaThruHoleToolStripMenuItem
			// 
			this.viaThruHoleToolStripMenuItem.CheckOnClick = true;
			this.viaThruHoleToolStripMenuItem.Name = "viaThruHoleToolStripMenuItem";
			this.viaThruHoleToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.viaThruHoleToolStripMenuItem.Tag = "boardSelectionModes.via";
			this.viaThruHoleToolStripMenuItem.Text = "Via / thru hole";
			// 
			// componentToolStripMenuItem
			// 
			this.componentToolStripMenuItem.CheckOnClick = true;
			this.componentToolStripMenuItem.Name = "componentToolStripMenuItem";
			this.componentToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.componentToolStripMenuItem.Tag = "boardSelectionModes.component";
			this.componentToolStripMenuItem.Text = "Component";
			// 
			// labelBoardMode
			// 
			this.labelBoardMode.AutoSize = true;
			this.labelBoardMode.Location = new System.Drawing.Point(5, 3);
			this.labelBoardMode.Name = "labelBoardMode";
			this.labelBoardMode.Size = new System.Drawing.Size(34, 13);
			this.labelBoardMode.TabIndex = 9;
			this.labelBoardMode.Text = "Mode";
			// 
			// radioButtonModeDropThroughHole
			// 
			this.radioButtonModeDropThroughHole.AutoSize = true;
			this.radioButtonModeDropThroughHole.Location = new System.Drawing.Point(7, 88);
			this.radioButtonModeDropThroughHole.Name = "radioButtonModeDropThroughHole";
			this.radioButtonModeDropThroughHole.Size = new System.Drawing.Size(81, 17);
			this.radioButtonModeDropThroughHole.TabIndex = 8;
			this.radioButtonModeDropThroughHole.Text = "Drop T-hole";
			this.radioButtonModeDropThroughHole.UseVisualStyleBackColor = true;
			this.radioButtonModeDropThroughHole.CheckedChanged += new System.EventHandler(this.radioButtonModeDropThroughHole_CheckedChanged);
			// 
			// radioButtonModeDropVia
			// 
			this.radioButtonModeDropVia.AutoSize = true;
			this.radioButtonModeDropVia.Location = new System.Drawing.Point(7, 65);
			this.radioButtonModeDropVia.Name = "radioButtonModeDropVia";
			this.radioButtonModeDropVia.Size = new System.Drawing.Size(66, 17);
			this.radioButtonModeDropVia.TabIndex = 7;
			this.radioButtonModeDropVia.Text = "Drop Via";
			this.radioButtonModeDropVia.UseVisualStyleBackColor = true;
			this.radioButtonModeDropVia.CheckedChanged += new System.EventHandler(this.radioButtonModeDropVia_CheckedChanged);
			// 
			// radioButtonModeDrawTrace
			// 
			this.radioButtonModeDrawTrace.AutoSize = true;
			this.radioButtonModeDrawTrace.Location = new System.Drawing.Point(7, 42);
			this.radioButtonModeDrawTrace.Name = "radioButtonModeDrawTrace";
			this.radioButtonModeDrawTrace.Size = new System.Drawing.Size(81, 17);
			this.radioButtonModeDrawTrace.TabIndex = 6;
			this.radioButtonModeDrawTrace.Text = "Draw Trace";
			this.radioButtonModeDrawTrace.UseVisualStyleBackColor = true;
			this.radioButtonModeDrawTrace.CheckedChanged += new System.EventHandler(this.radioButtonModeDrawTrace_CheckedChanged);
			// 
			// textBoxLayerName
			// 
			this.textBoxLayerName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textBoxLayerName.Location = new System.Drawing.Point(3, 525);
			this.textBoxLayerName.Name = "textBoxLayerName";
			this.textBoxLayerName.Size = new System.Drawing.Size(90, 20);
			this.textBoxLayerName.TabIndex = 5;
			// 
			// buttonSwitchBoardLayerView
			// 
			this.buttonSwitchBoardLayerView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSwitchBoardLayerView.Location = new System.Drawing.Point(3, 496);
			this.buttonSwitchBoardLayerView.Name = "buttonSwitchBoardLayerView";
			this.buttonSwitchBoardLayerView.Size = new System.Drawing.Size(90, 23);
			this.buttonSwitchBoardLayerView.TabIndex = 4;
			this.buttonSwitchBoardLayerView.Text = "Board layer";
			this.toolTip1.SetToolTip(this.buttonSwitchBoardLayerView, "Switch between layers of the board image");
			this.buttonSwitchBoardLayerView.UseVisualStyleBackColor = true;
			this.buttonSwitchBoardLayerView.Click += new System.EventHandler(this.buttonSwitchBoardLayerView_Click);
			// 
			// panelMiddle
			// 
			this.panelMiddle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.panelMiddle.BackColor = System.Drawing.SystemColors.Control;
			this.panelMiddle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelMiddle.Controls.Add(this.checkBoxCenterOnSelection);
			this.panelMiddle.Controls.Add(this.checkBoxBreak);
			this.panelMiddle.Controls.Add(this.textBoxCoordinates);
			this.panelMiddle.Controls.Add(this.button1);
			this.panelMiddle.Controls.Add(this.buttonShrinkCircuit);
			this.panelMiddle.Controls.Add(this.buttonShrinkBoard);
			this.panelMiddle.Location = new System.Drawing.Point(596, 107);
			this.panelMiddle.Name = "panelMiddle";
			this.panelMiddle.Size = new System.Drawing.Size(100, 553);
			this.panelMiddle.TabIndex = 5;
			// 
			// checkBoxCenterOnSelection
			// 
			this.checkBoxCenterOnSelection.Location = new System.Drawing.Point(8, 29);
			this.checkBoxCenterOnSelection.Name = "checkBoxCenterOnSelection";
			this.checkBoxCenterOnSelection.Size = new System.Drawing.Size(78, 30);
			this.checkBoxCenterOnSelection.TabIndex = 6;
			this.checkBoxCenterOnSelection.Text = "Center on selection";
			this.checkBoxCenterOnSelection.UseVisualStyleBackColor = true;
			// 
			// checkBoxBreak
			// 
			this.checkBoxBreak.AutoSize = true;
			this.checkBoxBreak.Location = new System.Drawing.Point(30, 443);
			this.checkBoxBreak.Name = "checkBoxBreak";
			this.checkBoxBreak.Size = new System.Drawing.Size(54, 17);
			this.checkBoxBreak.TabIndex = 4;
			this.checkBoxBreak.Text = "Break";
			this.checkBoxBreak.UseVisualStyleBackColor = true;
			// 
			// textBoxCoordinates
			// 
			this.textBoxCoordinates.Location = new System.Drawing.Point(3, 3);
			this.textBoxCoordinates.Name = "textBoxCoordinates";
			this.textBoxCoordinates.Size = new System.Drawing.Size(90, 20);
			this.textBoxCoordinates.TabIndex = 3;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.Location = new System.Drawing.Point(32, 479);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(32, 72);
			this.button1.TabIndex = 2;
			this.button1.Text = "!";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// buttonShrinkCircuit
			// 
			this.buttonShrinkCircuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonShrinkCircuit.Location = new System.Drawing.Point(70, 523);
			this.buttonShrinkCircuit.Name = "buttonShrinkCircuit";
			this.buttonShrinkCircuit.Size = new System.Drawing.Size(23, 23);
			this.buttonShrinkCircuit.TabIndex = 1;
			this.buttonShrinkCircuit.Text = ">";
			this.toolTip1.SetToolTip(this.buttonShrinkCircuit, "Shrink the circuit view and enlarge the board view");
			this.buttonShrinkCircuit.UseVisualStyleBackColor = true;
			this.buttonShrinkCircuit.Click += new System.EventHandler(this.buttonShrinkCircuit_Click);
			// 
			// buttonShrinkBoard
			// 
			this.buttonShrinkBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonShrinkBoard.Location = new System.Drawing.Point(3, 523);
			this.buttonShrinkBoard.Name = "buttonShrinkBoard";
			this.buttonShrinkBoard.Size = new System.Drawing.Size(23, 23);
			this.buttonShrinkBoard.TabIndex = 0;
			this.buttonShrinkBoard.Text = "<";
			this.toolTip1.SetToolTip(this.buttonShrinkBoard, "Shrink the board view and enlarge the circuit view");
			this.buttonShrinkBoard.UseVisualStyleBackColor = true;
			this.buttonShrinkBoard.Click += new System.EventHandler(this.buttonShrinkBoard_Click);
			// 
			// panelComponentInfo
			// 
			this.panelComponentInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelComponentInfo.Controls.Add(this.textBoxComponentName);
			this.panelComponentInfo.Controls.Add(this.buttonCompDataSet);
			this.panelComponentInfo.Controls.Add(this.textBoxDesigPart);
			this.panelComponentInfo.Controls.Add(this.textBoxDesigNum);
			this.panelComponentInfo.Controls.Add(this.textBoxDesigType);
			this.panelComponentInfo.Location = new System.Drawing.Point(559, 0);
			this.panelComponentInfo.Name = "panelComponentInfo";
			this.panelComponentInfo.Size = new System.Drawing.Size(167, 56);
			this.panelComponentInfo.TabIndex = 5;
			// 
			// textBoxComponentName
			// 
			this.textBoxComponentName.Location = new System.Drawing.Point(3, 29);
			this.textBoxComponentName.Name = "textBoxComponentName";
			this.textBoxComponentName.Size = new System.Drawing.Size(157, 20);
			this.textBoxComponentName.TabIndex = 5;
			// 
			// buttonCompDataSet
			// 
			this.buttonCompDataSet.Location = new System.Drawing.Point(124, 4);
			this.buttonCompDataSet.Name = "buttonCompDataSet";
			this.buttonCompDataSet.Size = new System.Drawing.Size(36, 20);
			this.buttonCompDataSet.TabIndex = 3;
			this.buttonCompDataSet.Text = "Set";
			this.buttonCompDataSet.UseVisualStyleBackColor = true;
			this.buttonCompDataSet.Click += new System.EventHandler(this.buttonCompDataSet_Click);
			// 
			// textBoxDesigPart
			// 
			this.textBoxDesigPart.Location = new System.Drawing.Point(62, 4);
			this.textBoxDesigPart.Name = "textBoxDesigPart";
			this.textBoxDesigPart.Size = new System.Drawing.Size(22, 20);
			this.textBoxDesigPart.TabIndex = 2;
			// 
			// textBoxDesigNum
			// 
			this.textBoxDesigNum.Location = new System.Drawing.Point(26, 4);
			this.textBoxDesigNum.Name = "textBoxDesigNum";
			this.textBoxDesigNum.Size = new System.Drawing.Size(39, 20);
			this.textBoxDesigNum.TabIndex = 1;
			// 
			// textBoxDesigType
			// 
			this.textBoxDesigType.Location = new System.Drawing.Point(3, 4);
			this.textBoxDesigType.Name = "textBoxDesigType";
			this.textBoxDesigType.Size = new System.Drawing.Size(26, 20);
			this.textBoxDesigType.TabIndex = 0;
			// 
			// panelRight
			// 
			this.panelRight.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.panelRight.BackColor = System.Drawing.SystemColors.Control;
			this.panelRight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelRight.Location = new System.Drawing.Point(1187, 107);
			this.panelRight.Name = "panelRight";
			this.panelRight.Size = new System.Drawing.Size(100, 553);
			this.panelRight.TabIndex = 6;
			// 
			// panelBottom
			// 
			this.panelBottom.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.panelBottom.BackColor = System.Drawing.SystemColors.Control;
			this.panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelBottom.Controls.Add(this.panelComponentInfo);
			this.panelBottom.Controls.Add(this.textBox3);
			this.panelBottom.Controls.Add(this.textBox2);
			this.panelBottom.Controls.Add(this.textBox1);
			this.panelBottom.Location = new System.Drawing.Point(3, 666);
			this.panelBottom.Name = "panelBottom";
			this.panelBottom.Size = new System.Drawing.Size(1286, 100);
			this.panelBottom.TabIndex = 7;
			// 
			// textBox3
			// 
			this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox3.Location = new System.Drawing.Point(1184, 49);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(95, 20);
			this.textBox3.TabIndex = 2;
			this.textBox3.Text = "1.00";
			// 
			// textBox2
			// 
			this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox2.Location = new System.Drawing.Point(1083, 49);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(95, 20);
			this.textBox2.TabIndex = 1;
			this.textBox2.Text = "0.00";
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(982, 49);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(95, 20);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "0.00";
			// 
			// panelTop
			// 
			this.panelTop.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.panelTop.BackColor = System.Drawing.SystemColors.Control;
			this.panelTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelTop.Controls.Add(this.buttonCheckPoint);
			this.panelTop.Controls.Add(this.buttonOpen);
			this.panelTop.Controls.Add(this.buttonSave);
			this.panelTop.Controls.Add(this.buttonRefresh);
			this.panelTop.Controls.Add(this.scrollBarBoardZoom);
			this.panelTop.Controls.Add(this.menuStrip1);
			this.panelTop.Location = new System.Drawing.Point(3, 1);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(1284, 100);
			this.panelTop.TabIndex = 8;
			// 
			// buttonCheckPoint
			// 
			this.buttonCheckPoint.Location = new System.Drawing.Point(113, 63);
			this.buttonCheckPoint.Margin = new System.Windows.Forms.Padding(1);
			this.buttonCheckPoint.Name = "buttonCheckPoint";
			this.buttonCheckPoint.Size = new System.Drawing.Size(106, 32);
			this.buttonCheckPoint.TabIndex = 7;
			this.buttonCheckPoint.Text = "Save Checkpoint";
			this.buttonCheckPoint.UseVisualStyleBackColor = true;
			this.buttonCheckPoint.Click += new System.EventHandler(this.buttonCheckPoint_Click);
			// 
			// buttonOpen
			// 
			this.buttonOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOpen.Location = new System.Drawing.Point(1216, 27);
			this.buttonOpen.Name = "buttonOpen";
			this.buttonOpen.Size = new System.Drawing.Size(61, 41);
			this.buttonOpen.TabIndex = 5;
			this.buttonOpen.Text = "Open last project";
			this.buttonOpen.UseVisualStyleBackColor = true;
			this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(1, 63);
			this.buttonSave.Margin = new System.Windows.Forms.Padding(1);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(106, 32);
			this.buttonSave.TabIndex = 4;
			this.buttonSave.Text = "Save Project";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1280, 24);
			this.menuStrip1.TabIndex = 6;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator5,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.toolStripSeparator6,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
			this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.newToolStripMenuItem.Text = "&New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
			this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.openToolStripMenuItem.Text = "&Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(143, 6);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
			this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.saveToolStripMenuItem.Text = "&Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.saveAsToolStripMenuItem.Text = "Save &As";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(143, 6);
			// 
			// printToolStripMenuItem
			// 
			this.printToolStripMenuItem.Enabled = false;
			this.printToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripMenuItem.Image")));
			this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.printToolStripMenuItem.Name = "printToolStripMenuItem";
			this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.printToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.printToolStripMenuItem.Text = "&Print";
			// 
			// printPreviewToolStripMenuItem
			// 
			this.printPreviewToolStripMenuItem.Enabled = false;
			this.printPreviewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripMenuItem.Image")));
			this.printPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
			this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.printPreviewToolStripMenuItem.Text = "Print Pre&view";
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(143, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator7,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator8,
            this.selectAllToolStripMenuItem});
			this.editToolStripMenuItem.Enabled = false;
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// undoToolStripMenuItem
			// 
			this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
			this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.undoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.undoToolStripMenuItem.Text = "&Undo";
			// 
			// redoToolStripMenuItem
			// 
			this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
			this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.redoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.redoToolStripMenuItem.Text = "&Redo";
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(141, 6);
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
			this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.cutToolStripMenuItem.Text = "Cu&t";
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
			this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.copyToolStripMenuItem.Text = "&Copy";
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
			this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.pasteToolStripMenuItem.Text = "&Paste";
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(141, 6);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.selectAllToolStripMenuItem.Text = "Select &All";
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.symbolAssignmentToolStripMenuItem,
            this.layerConfigToolStripMenuItem,
            this.optionsToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
			this.toolsToolStripMenuItem.Text = "&Tools";
			// 
			// symbolAssignmentToolStripMenuItem
			// 
			this.symbolAssignmentToolStripMenuItem.Name = "symbolAssignmentToolStripMenuItem";
			this.symbolAssignmentToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.symbolAssignmentToolStripMenuItem.Text = "Symbol Assignment";
			this.symbolAssignmentToolStripMenuItem.Click += new System.EventHandler(this.symbolAssignmentToolStripMenuItem_Click);
			// 
			// layerConfigToolStripMenuItem
			// 
			this.layerConfigToolStripMenuItem.Name = "layerConfigToolStripMenuItem";
			this.layerConfigToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.layerConfigToolStripMenuItem.Text = "Layers and images";
			this.layerConfigToolStripMenuItem.Click += new System.EventHandler(this.layerConfigToolStripMenuItem_Click);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Enabled = false;
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.optionsToolStripMenuItem.Text = "&Options";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator9,
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Enabled = false;
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// contentsToolStripMenuItem
			// 
			this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
			this.contentsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.contentsToolStripMenuItem.Text = "&Contents";
			// 
			// indexToolStripMenuItem
			// 
			this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
			this.indexToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.indexToolStripMenuItem.Text = "&Index";
			// 
			// searchToolStripMenuItem
			// 
			this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
			this.searchToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.searchToolStripMenuItem.Text = "&Search";
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(119, 6);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.aboutToolStripMenuItem.Text = "&About...";
			// 
			// openProjectDialog
			// 
			this.openProjectDialog.FileName = "*.rgn";
			this.openProjectDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openProjectDialog_FileOk);
			// 
			// saveProjectDialog
			// 
			this.saveProjectDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveProjectDialog_FileOk);
			// 
			// mainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1295, 749);
			this.Controls.Add(this.panelTop);
			this.Controls.Add(this.panelBottom);
			this.Controls.Add(this.panelRight);
			this.Controls.Add(this.panelMiddle);
			this.Controls.Add(this.panelLeft);
			this.Controls.Add(this.circuitPictureBox);
			this.Controls.Add(this.boardPictureBox);
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "mainForm";
			this.Text = "Reenigne";
			this.Activated += new System.EventHandler(this.mainForm_Activated);
			this.Load += new System.EventHandler(this.mainForm_Load);
			this.Shown += new System.EventHandler(this.mainForm_Shown);
			this.ResizeEnd += new System.EventHandler(this.mainForm_ResizeEnd);
			this.Resize += new System.EventHandler(this.mainForm_Resize);
			((System.ComponentModel.ISupportInitialize)(this.boardPictureBox)).EndInit();
			this.boardContextMenu.ResumeLayout(false);
			this.boardContextMenu.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.circuitPictureBox)).EndInit();
			this.circuitContextMenu.ResumeLayout(false);
			this.panelLeft.ResumeLayout(false);
			this.panelLeft.PerformLayout();
			this.contextMenuComponentType.ResumeLayout(false);
			this.contextMenuComponentType.PerformLayout();
			this.contextMenuSelectionType.ResumeLayout(false);
			this.panelMiddle.ResumeLayout(false);
			this.panelMiddle.PerformLayout();
			this.panelComponentInfo.ResumeLayout(false);
			this.panelComponentInfo.PerformLayout();
			this.panelBottom.ResumeLayout(false);
			this.panelBottom.PerformLayout();
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox boardPictureBox;
        private System.Windows.Forms.PictureBox circuitPictureBox;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.HScrollBar scrollBarBoardZoom;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelMiddle;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button buttonSwitchBoardLayerView;
        private System.Windows.Forms.Button buttonShrinkCircuit;
        private System.Windows.Forms.Button buttonShrinkBoard;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.TextBox textBoxLayerName;
        private System.Windows.Forms.TextBox textBoxCoordinates;
        private System.Windows.Forms.ContextMenuStrip boardContextMenu;
        private System.Windows.Forms.ToolStripMenuItem insertViaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertThroughHoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawTraceToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.RadioButton radioButtonModeDrawTrace;
        private System.Windows.Forms.RadioButton radioButtonModeSelect;
        private System.Windows.Forms.Label labelBoardMode;
        private System.Windows.Forms.RadioButton radioButtonModeDropThroughHole;
        private System.Windows.Forms.RadioButton radioButtonModeDropVia;
        private System.Windows.Forms.ContextMenuStrip contextMenuSelectionType;
        private System.Windows.Forms.ToolStripMenuItem pointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem traceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem netToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viaThruHoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem componentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button buttonRetraceNets;
		private System.Windows.Forms.RadioButton radioButtonModePlaceComponent;
		private System.Windows.Forms.ContextMenuStrip contextMenuComponentType;
		private System.Windows.Forms.ToolStripMenuItem compTypeHeader;
		private System.Windows.Forms.ToolStripMenuItem compTypeSIL;
		private System.Windows.Forms.ToolStripMenuItem compTypeDIL;
		private System.Windows.Forms.ToolStripMenuItem compTypeCircular;
		private System.Windows.Forms.ToolStripMenuItem compTypeSquare;
		private System.Windows.Forms.ToolStripTextBox numPinsTextBox;
		private System.Windows.Forms.ToolStripMenuItem numPinsHeader;
		private System.Windows.Forms.ToolStripMenuItem outlineHeader;
		private System.Windows.Forms.ToolStripMenuItem outlineRectangular;
		private System.Windows.Forms.ToolStripMenuItem outlineCircular;
		private System.Windows.Forms.ToolStripMenuItem padSizeHeader;
		private System.Windows.Forms.ToolStripTextBox padSizeTextBox;
		private System.Windows.Forms.ToolStripMenuItem pinTypeHeader;
		private System.Windows.Forms.ToolStripMenuItem pinTypeSmd;
		private System.Windows.Forms.ToolStripMenuItem pinTypeThruHole;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.CheckBox checkBoxBreak;
		private System.Windows.Forms.ContextMenuStrip circuitContextMenu;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem layerConfigToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openProjectDialog;
		private System.Windows.Forms.SaveFileDialog saveProjectDialog;
		private System.Windows.Forms.ToolStripMenuItem symbolAssignmentToolStripMenuItem;
		private System.Windows.Forms.Button buttonCheckPoint;
		private System.Windows.Forms.Panel panelComponentInfo;
		private System.Windows.Forms.TextBox textBoxDesigType;
		private System.Windows.Forms.Button buttonCompDataSet;
		private System.Windows.Forms.TextBox textBoxDesigPart;
		private System.Windows.Forms.TextBox textBoxDesigNum;
		private System.Windows.Forms.CheckBox checkBoxCenterOnSelection;
		private System.Windows.Forms.TextBox textBoxComponentName;
	}
}

