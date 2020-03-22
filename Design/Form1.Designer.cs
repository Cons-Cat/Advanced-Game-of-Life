namespace GOLSource
{
    partial class Form1
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
            this.sliderButton1 = new GOLSource.SliderButton(this.Width);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelGenerations = new System.Windows.Forms.ToolStripStatusLabel();
            this.flowLayoutPanelCore = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonTick = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonSize = new System.Windows.Forms.Button();
            this.buttonShape = new System.Windows.Forms.Button();
            this.buttonRandom = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.graphicsPanel1 = new GOLSource.GraphicsPanel();
            this.flowLayoutPanelSettings = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.buttonCore = new System.Windows.Forms.Button();
            this.buttonNew = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.flowLayoutPanelCore.SuspendLayout();
            this.graphicsPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sliderButton1
            // 
            this.sliderButton1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.sliderButton1.ClickCount = 0;
            this.sliderButton1.Location = new System.Drawing.Point(0, 130);
            this.sliderButton1.MoveDist = 0;
            this.sliderButton1.MovePercent = 0D;
            this.sliderButton1.MoveState = 0;
            this.sliderButton1.Name = "sliderButton1";
            this.sliderButton1.Size = new System.Drawing.Size(22, 67);
            this.sliderButton1.Sliding = false;
            this.sliderButton1.SplitObj = null;
            this.sliderButton1.SubTicks = 0;
            this.sliderButton1.TabIndex = 1;
            this.sliderButton1.Text = ">";
            this.sliderButton1.UseVisualStyleBackColor = true;
            this.sliderButton1.XMoveFrom = 0;
            this.sliderButton1.XOff = 0;
            this.sliderButton1.XStart = 83;
            this.sliderButton1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SliderButton1_MouseDown);
            this.sliderButton1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMove);
            this.sliderButton1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SliderButton1_MouseUp);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelGenerations});
            this.statusStrip1.Location = new System.Drawing.Point(0, 323);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(573, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelGenerations
            // 
            this.toolStripStatusLabelGenerations.Name = "toolStripStatusLabelGenerations";
            this.toolStripStatusLabelGenerations.Size = new System.Drawing.Size(90, 17);
            this.toolStripStatusLabelGenerations.Text = "Generations = 0";
            // 
            // flowLayoutPanelCore
            // 
            this.flowLayoutPanelCore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flowLayoutPanelCore.AutoScroll = true;
            this.flowLayoutPanelCore.BackColor = System.Drawing.SystemColors.ControlLight;
            this.flowLayoutPanelCore.CausesValidation = false;
            this.flowLayoutPanelCore.Controls.Add(this.buttonTick);
            this.flowLayoutPanelCore.Controls.Add(this.buttonClear);
            this.flowLayoutPanelCore.Controls.Add(this.buttonPlay);
            this.flowLayoutPanelCore.Controls.Add(this.buttonSize);
            this.flowLayoutPanelCore.Controls.Add(this.buttonShape);
            this.flowLayoutPanelCore.Controls.Add(this.buttonRandom);
            this.flowLayoutPanelCore.Location = new System.Drawing.Point(0, 104);
            this.flowLayoutPanelCore.Name = "flowLayoutPanelCore";
            this.flowLayoutPanelCore.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.flowLayoutPanelCore.Size = new System.Drawing.Size(191, 122);
            this.flowLayoutPanelCore.TabIndex = 0;
            // 
            // buttonTick
            // 
            this.buttonTick.Location = new System.Drawing.Point(3, 6);
            this.buttonTick.Name = "buttonTick";
            this.buttonTick.Size = new System.Drawing.Size(60, 60);
            this.buttonTick.TabIndex = 0;
            this.buttonTick.Text = "Tick";
            this.buttonTick.UseVisualStyleBackColor = true;
            this.buttonTick.Click += new System.EventHandler(this.ButtonTick_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(69, 6);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(60, 60);
            this.buttonClear.TabIndex = 1;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Location = new System.Drawing.Point(3, 72);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(60, 60);
            this.buttonPlay.TabIndex = 2;
            this.buttonPlay.Text = "Play";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonSize
            // 
            this.buttonSize.Location = new System.Drawing.Point(69, 72);
            this.buttonSize.Name = "buttonSize";
            this.buttonSize.Size = new System.Drawing.Size(60, 60);
            this.buttonSize.TabIndex = 3;
            this.buttonSize.Text = "Size";
            this.buttonSize.UseVisualStyleBackColor = true;
            // 
            // buttonShape
            // 
            this.buttonShape.Location = new System.Drawing.Point(3, 138);
            this.buttonShape.Name = "buttonShape";
            this.buttonShape.Size = new System.Drawing.Size(60, 60);
            this.buttonShape.TabIndex = 4;
            this.buttonShape.Text = "Hexagon";
            this.buttonShape.UseVisualStyleBackColor = true;
            this.buttonShape.Click += new System.EventHandler(this.buttonShape_Click);
            // 
            // buttonRandom
            // 
            this.buttonRandom.Location = new System.Drawing.Point(69, 138);
            this.buttonRandom.Name = "buttonRandom";
            this.buttonRandom.Size = new System.Drawing.Size(60, 60);
            this.buttonRandom.TabIndex = 5;
            this.buttonRandom.Text = "Random";
            this.buttonRandom.UseVisualStyleBackColor = true;
            this.buttonRandom.Click += new System.EventHandler(this.buttonRandom_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(54, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(45, 45);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(105, 3);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(45, 45);
            this.buttonOpen.TabIndex = 7;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // graphicsPanel1
            // 
            this.graphicsPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.graphicsPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.graphicsPanel1.CellSize = 0F;
            this.graphicsPanel1.Controls.Add(this.sliderButton1);
            this.graphicsPanel1.GridHeight = 25;
            this.graphicsPanel1.GridWidth = 25;
            this.graphicsPanel1.HexRadius = 0F;
            this.graphicsPanel1.Location = new System.Drawing.Point(192, 3);
            this.graphicsPanel1.Name = "graphicsPanel1";
            this.graphicsPanel1.Size = new System.Drawing.Size(381, 320);
            this.graphicsPanel1.TabIndex = 3;
            this.graphicsPanel1.XOff = 0;
            this.graphicsPanel1.YOff = 0F;
            this.graphicsPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.GraphicsPanel1_Paint);
            this.graphicsPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GraphicsPanel1_MouseClick);
            // 
            // flowLayoutPanelSettings
            // 
            this.flowLayoutPanelSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flowLayoutPanelSettings.AutoScroll = true;
            this.flowLayoutPanelSettings.BackColor = System.Drawing.SystemColors.ControlLight;
            this.flowLayoutPanelSettings.CausesValidation = false;
            this.flowLayoutPanelSettings.Location = new System.Drawing.Point(37, 56);
            this.flowLayoutPanelSettings.Name = "flowLayoutPanelSettings";
            this.flowLayoutPanelSettings.Size = new System.Drawing.Size(191, 264);
            this.flowLayoutPanelSettings.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkGray;
            this.panel1.Controls.Add(this.buttonSettings);
            this.panel1.Controls.Add(this.buttonCore);
            this.panel1.Controls.Add(this.buttonOpen);
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Controls.Add(this.buttonNew);
            this.panel1.Location = new System.Drawing.Point(0, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(191, 98);
            this.panel1.TabIndex = 4;
            // 
            // buttonSettings
            // 
            this.buttonSettings.Location = new System.Drawing.Point(82, 50);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(45, 45);
            this.buttonSettings.TabIndex = 9;
            this.buttonSettings.Text = "Settings";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // buttonCore
            // 
            this.buttonCore.Location = new System.Drawing.Point(31, 50);
            this.buttonCore.Name = "buttonCore";
            this.buttonCore.Size = new System.Drawing.Size(45, 45);
            this.buttonCore.TabIndex = 8;
            this.buttonCore.Text = "Core";
            this.buttonCore.UseVisualStyleBackColor = true;
            this.buttonCore.Click += new System.EventHandler(this.buttonCore_Click);
            // 
            // buttonNew
            // 
            this.buttonNew.Location = new System.Drawing.Point(3, 3);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(45, 45);
            this.buttonNew.TabIndex = 5;
            this.buttonNew.Text = "New";
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(573, 345);
            this.Controls.Add(this.flowLayoutPanelCore);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.graphicsPanel1);
            this.Controls.Add(this.flowLayoutPanelSettings);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ClientSizeChanged += new System.EventHandler(this.Form1_ClientSizeChanged);
            this.SizeChanged += new System.EventHandler(this.Form1_ClientSizeChanged);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.flowLayoutPanelCore.ResumeLayout(false);
            this.graphicsPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelGenerations;
        private GraphicsPanel graphicsPanel1;
        private SliderButton sliderButton1;
        private System.Windows.Forms.Button buttonTick;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCore;
        private System.Windows.Forms.Button buttonShape;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonSize;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Button buttonRandom;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSettings;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button buttonCore;
    }
}

