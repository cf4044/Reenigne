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
            this.boardPictureBox = new System.Windows.Forms.PictureBox();
            this.circuitPictureBox = new System.Windows.Forms.PictureBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.scrollBarBoardZoom = new System.Windows.Forms.HScrollBar();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.buttonSwitchBoardLayerView = new System.Windows.Forms.Button();
            this.panelMiddle = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonShrinkCircuit = new System.Windows.Forms.Button();
            this.buttonShrinkBoard = new System.Windows.Forms.Button();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panelTop = new System.Windows.Forms.Panel();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.boardPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.circuitPictureBox)).BeginInit();
            this.panelLeft.SuspendLayout();
            this.panelMiddle.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // boardPictureBox
            // 
            this.boardPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boardPictureBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.boardPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.boardPictureBox.Location = new System.Drawing.Point(109, 105);
            this.boardPictureBox.Name = "boardPictureBox";
            this.boardPictureBox.Size = new System.Drawing.Size(481, 557);
            this.boardPictureBox.TabIndex = 0;
            this.boardPictureBox.TabStop = false;
            this.boardPictureBox.MouseLeave += new System.EventHandler(this.boardPictureBox_MouseLeave);
            // 
            // circuitPictureBox
            // 
            this.circuitPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.circuitPictureBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.circuitPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.circuitPictureBox.Location = new System.Drawing.Point(702, 107);
            this.circuitPictureBox.Name = "circuitPictureBox";
            this.circuitPictureBox.Size = new System.Drawing.Size(481, 557);
            this.circuitPictureBox.TabIndex = 1;
            this.circuitPictureBox.TabStop = false;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(3, 3);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(107, 30);
            this.buttonRefresh.TabIndex = 2;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // scrollBarBoardZoom
            // 
            this.scrollBarBoardZoom.Location = new System.Drawing.Point(151, 10);
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
            this.panelLeft.Controls.Add(this.textBox4);
            this.panelLeft.Controls.Add(this.buttonSwitchBoardLayerView);
            this.panelLeft.Location = new System.Drawing.Point(3, 107);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(100, 553);
            this.panelLeft.TabIndex = 4;
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox4.Location = new System.Drawing.Point(3, 525);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(90, 20);
            this.textBox4.TabIndex = 5;
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
            this.buttonSwitchBoardLayerView.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelMiddle
            // 
            this.panelMiddle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panelMiddle.BackColor = System.Drawing.SystemColors.Control;
            this.panelMiddle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelMiddle.Controls.Add(this.button1);
            this.panelMiddle.Controls.Add(this.buttonShrinkCircuit);
            this.panelMiddle.Controls.Add(this.buttonShrinkBoard);
            this.panelMiddle.Location = new System.Drawing.Point(596, 107);
            this.panelMiddle.Name = "panelMiddle";
            this.panelMiddle.Size = new System.Drawing.Size(100, 553);
            this.panelMiddle.TabIndex = 5;
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
            this.panelTop.Controls.Add(this.buttonOpen);
            this.panelTop.Controls.Add(this.buttonSave);
            this.panelTop.Controls.Add(this.buttonRefresh);
            this.panelTop.Controls.Add(this.scrollBarBoardZoom);
            this.panelTop.Location = new System.Drawing.Point(3, 1);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1284, 100);
            this.panelTop.TabIndex = 8;
            // 
            // buttonOpen
            // 
            this.buttonOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpen.Location = new System.Drawing.Point(1179, 3);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(98, 32);
            this.buttonOpen.TabIndex = 5;
            this.buttonOpen.Text = "Open Project";
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
            this.Name = "mainForm";
            this.Text = "Reenigne";
            this.Activated += new System.EventHandler(this.mainForm_Activated);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.Shown += new System.EventHandler(this.mainForm_Shown);
            this.ResizeEnd += new System.EventHandler(this.mainForm_ResizeEnd);
            this.Resize += new System.EventHandler(this.mainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.boardPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.circuitPictureBox)).EndInit();
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.panelMiddle.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.panelTop.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox textBox4;
    }
}

