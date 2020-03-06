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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelGenerations = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.graphicsPanel1 = new GOLSource.GraphicsPanel();
            this.sliderButton1 = new GOLSource.SliderButton();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.graphicsPanel1.SuspendLayout();
            this.SuspendLayout();
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
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer1.Panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMove);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.graphicsPanel1);
            this.splitContainer1.Panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMove);
            this.splitContainer1.Size = new System.Drawing.Size(573, 323);
            this.splitContainer1.SplitterDistance = 191;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 4;
            this.splitContainer1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMove);
            // 
            // graphicsPanel1
            // 
            this.graphicsPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.graphicsPanel1.CellSize = 0F;
            this.graphicsPanel1.Controls.Add(this.sliderButton1);
            this.graphicsPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphicsPanel1.GridHeight = 5;
            this.graphicsPanel1.GridWidth = 5;
            this.graphicsPanel1.Location = new System.Drawing.Point(0, 0);
            this.graphicsPanel1.Name = "graphicsPanel1";
            this.graphicsPanel1.Size = new System.Drawing.Size(381, 323);
            this.graphicsPanel1.TabIndex = 3;
            this.graphicsPanel1.YOff = 0F;
            this.graphicsPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.GraphicsPanel1_Paint);
            this.graphicsPanel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GraphicsPanel1_MouseClick);
            this.graphicsPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMove);
            // 
            // sliderButton1
            // 
            this.sliderButton1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.sliderButton1.Location = new System.Drawing.Point(0, 131);
            this.sliderButton1.Name = "sliderButton1";
            this.sliderButton1.Size = new System.Drawing.Size(22, 67);
            this.sliderButton1.Sliding = false;
            this.sliderButton1.SplitObj = null;
            this.sliderButton1.TabIndex = 1;
            this.sliderButton1.Text = ">";
            this.sliderButton1.UseVisualStyleBackColor = true;
            this.sliderButton1.XOff = 0;
            this.sliderButton1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SliderButton1_MouseDown);
            this.sliderButton1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMove);
            this.sliderButton1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SliderButton1_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 345);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ClientSizeChanged += new System.EventHandler(this.Form1_ClientSizeChanged);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.graphicsPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelGenerations;
        public System.Windows.Forms.SplitContainer splitContainer1;
        private GraphicsPanel graphicsPanel1;
        private SliderButton sliderButton1;
    }
}

