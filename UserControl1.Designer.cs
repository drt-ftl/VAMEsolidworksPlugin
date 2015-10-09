namespace SwCSharpAddin1
{
    partial class UserControl1
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.TimeMax = new System.Windows.Forms.TrackBar();
            this.CodeWindow = new System.Windows.Forms.Label();
            this.FirstLine = new System.Windows.Forms.Label();
            this.TimeMaxLabel = new System.Windows.Forms.Label();
            this.Navigator = new MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel();
            this.SwitchType = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.CoordsPanel = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.p2xDisplay = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.p2zDisplay = new System.Windows.Forms.Label();
            this.p2yDisplay = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.p1xDisplay = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.p1zDisplay = new System.Windows.Forms.Label();
            this.p1yDisplay = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ScrollCode = new System.Windows.Forms.VScrollBar();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.VisSlider = new System.Windows.Forms.TrackBar();
            this.VisLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TimeMax)).BeginInit();
            this.Navigator.SuspendLayout();
            this.CoordsPanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VisSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(7, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(316, 22);
            this.button1.TabIndex = 1;
            this.button1.Text = "Load File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TimeMax
            // 
            this.TimeMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeMax.Location = new System.Drawing.Point(10, 472);
            this.TimeMax.Name = "TimeMax";
            this.TimeMax.Size = new System.Drawing.Size(293, 45);
            this.TimeMax.TabIndex = 4;
            this.TimeMax.Value = 10;
            this.TimeMax.Scroll += new System.EventHandler(this.TimeMax_Scroll);
            // 
            // CodeWindow
            // 
            this.CodeWindow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CodeWindow.BackColor = System.Drawing.SystemColors.Desktop;
            this.CodeWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CodeWindow.ForeColor = System.Drawing.Color.Gold;
            this.CodeWindow.Location = new System.Drawing.Point(8, 97);
            this.CodeWindow.Name = "CodeWindow";
            this.CodeWindow.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.CodeWindow.Size = new System.Drawing.Size(295, 360);
            this.CodeWindow.TabIndex = 2;
            // 
            // FirstLine
            // 
            this.FirstLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FirstLine.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.FirstLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FirstLine.ForeColor = System.Drawing.Color.LawnGreen;
            this.FirstLine.Location = new System.Drawing.Point(8, 97);
            this.FirstLine.Name = "FirstLine";
            this.FirstLine.Size = new System.Drawing.Size(295, 13);
            this.FirstLine.TabIndex = 3;
            this.FirstLine.Text = "Load File...";
            // 
            // TimeMaxLabel
            // 
            this.TimeMaxLabel.AutoSize = true;
            this.TimeMaxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeMaxLabel.Location = new System.Drawing.Point(9, 504);
            this.TimeMaxLabel.Name = "TimeMaxLabel";
            this.TimeMaxLabel.Size = new System.Drawing.Size(49, 12);
            this.TimeMaxLabel.TabIndex = 6;
            this.TimeMaxLabel.Text = "TimeMax: ";
            // 
            // Navigator
            // 
            this.Navigator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Navigator.ButtonSize = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonSize.Normal;
            this.Navigator.ButtonStyle = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonStyle.Circle;
            this.Navigator.Controls.Add(this.SwitchType);
            this.Navigator.Controls.Add(this.label1);
            this.Navigator.Controls.Add(this.CoordsPanel);
            this.Navigator.Controls.Add(this.ScrollCode);
            this.Navigator.Controls.Add(this.TimeMaxLabel);
            this.Navigator.Controls.Add(this.FirstLine);
            this.Navigator.Controls.Add(this.CodeWindow);
            this.Navigator.Controls.Add(this.TimeMax);
            this.Navigator.ExpandedHeight = 0;
            this.Navigator.IsExpanded = true;
            this.Navigator.Location = new System.Drawing.Point(7, 117);
            this.Navigator.Name = "Navigator";
            this.Navigator.Size = new System.Drawing.Size(316, 667);
            this.Navigator.TabIndex = 7;
            this.Navigator.Text = "Navigator";
            this.Navigator.UseAnimation = true;
            // 
            // SwitchType
            // 
            this.SwitchType.Location = new System.Drawing.Point(10, 49);
            this.SwitchType.Name = "SwitchType";
            this.SwitchType.Size = new System.Drawing.Size(146, 23);
            this.SwitchType.TabIndex = 9;
            this.SwitchType.Text = "Switch";
            this.SwitchType.UseVisualStyleBackColor = true;
            this.SwitchType.Visible = false;
            this.SwitchType.Click += new System.EventHandler(this.SwitchType_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(11, 523);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Coordinates";
            // 
            // CoordsPanel
            // 
            this.CoordsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CoordsPanel.Controls.Add(this.groupBox2);
            this.CoordsPanel.Controls.Add(this.groupBox1);
            this.CoordsPanel.Location = new System.Drawing.Point(10, 541);
            this.CoordsPanel.Name = "CoordsPanel";
            this.CoordsPanel.Size = new System.Drawing.Size(293, 123);
            this.CoordsPanel.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.p2xDisplay);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.p2zDisplay);
            this.groupBox2.Controls.Add(this.p2yDisplay);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Location = new System.Drawing.Point(151, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(130, 100);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "P2";
            // 
            // p2xDisplay
            // 
            this.p2xDisplay.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.p2xDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p2xDisplay.ForeColor = System.Drawing.Color.Lime;
            this.p2xDisplay.Location = new System.Drawing.Point(36, 27);
            this.p2xDisplay.Name = "p2xDisplay";
            this.p2xDisplay.Size = new System.Drawing.Size(80, 21);
            this.p2xDisplay.TabIndex = 2;
            this.p2xDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 15);
            this.label8.TabIndex = 7;
            this.label8.Text = "z :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 15);
            this.label9.TabIndex = 3;
            this.label9.Text = "x :";
            // 
            // p2zDisplay
            // 
            this.p2zDisplay.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.p2zDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p2zDisplay.ForeColor = System.Drawing.Color.Lime;
            this.p2zDisplay.Location = new System.Drawing.Point(36, 69);
            this.p2zDisplay.Name = "p2zDisplay";
            this.p2zDisplay.Size = new System.Drawing.Size(80, 21);
            this.p2zDisplay.TabIndex = 6;
            this.p2zDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // p2yDisplay
            // 
            this.p2yDisplay.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.p2yDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p2yDisplay.ForeColor = System.Drawing.Color.Lime;
            this.p2yDisplay.Location = new System.Drawing.Point(36, 48);
            this.p2yDisplay.Name = "p2yDisplay";
            this.p2yDisplay.Size = new System.Drawing.Size(80, 21);
            this.p2yDisplay.TabIndex = 4;
            this.p2yDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 50);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(18, 15);
            this.label12.TabIndex = 5;
            this.label12.Text = "y :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.p1xDisplay);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.p1zDisplay);
            this.groupBox1.Controls.Add(this.p1yDisplay);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(130, 100);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "P1";
            // 
            // p1xDisplay
            // 
            this.p1xDisplay.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.p1xDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p1xDisplay.ForeColor = System.Drawing.Color.Lime;
            this.p1xDisplay.Location = new System.Drawing.Point(36, 27);
            this.p1xDisplay.Name = "p1xDisplay";
            this.p1xDisplay.Size = new System.Drawing.Size(80, 21);
            this.p1xDisplay.TabIndex = 2;
            this.p1xDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "z :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "x :";
            // 
            // p1zDisplay
            // 
            this.p1zDisplay.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.p1zDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p1zDisplay.ForeColor = System.Drawing.Color.Lime;
            this.p1zDisplay.Location = new System.Drawing.Point(36, 69);
            this.p1zDisplay.Name = "p1zDisplay";
            this.p1zDisplay.Size = new System.Drawing.Size(80, 21);
            this.p1zDisplay.TabIndex = 6;
            this.p1zDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // p1yDisplay
            // 
            this.p1yDisplay.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.p1yDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p1yDisplay.ForeColor = System.Drawing.Color.Lime;
            this.p1yDisplay.Location = new System.Drawing.Point(36, 48);
            this.p1yDisplay.Name = "p1yDisplay";
            this.p1yDisplay.Size = new System.Drawing.Size(80, 21);
            this.p1yDisplay.TabIndex = 4;
            this.p1yDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "y :";
            // 
            // ScrollCode
            // 
            this.ScrollCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScrollCode.Location = new System.Drawing.Point(286, 97);
            this.ScrollCode.Name = "ScrollCode";
            this.ScrollCode.Size = new System.Drawing.Size(17, 360);
            this.ScrollCode.TabIndex = 7;
            this.ScrollCode.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollCode_Scroll);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // VisSlider
            // 
            this.VisSlider.LargeChange = 10;
            this.VisSlider.Location = new System.Drawing.Point(17, 53);
            this.VisSlider.Maximum = 100;
            this.VisSlider.Name = "VisSlider";
            this.VisSlider.Size = new System.Drawing.Size(302, 45);
            this.VisSlider.SmallChange = 10;
            this.VisSlider.TabIndex = 8;
            this.VisSlider.TickFrequency = 10;
            this.VisSlider.Value = 100;
            this.VisSlider.Scroll += new System.EventHandler(this.VisSlider_Scroll);
            // 
            // VisLabel
            // 
            this.VisLabel.AutoSize = true;
            this.VisLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VisLabel.ForeColor = System.Drawing.Color.SteelBlue;
            this.VisLabel.Location = new System.Drawing.Point(18, 84);
            this.VisLabel.Name = "VisLabel";
            this.VisLabel.Size = new System.Drawing.Size(45, 12);
            this.VisLabel.TabIndex = 9;
            this.VisLabel.Text = "Visibility: ";
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.VisLabel);
            this.Controls.Add(this.VisSlider);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Navigator);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(330, 787);
            ((System.ComponentModel.ISupportInitialize)(this.TimeMax)).EndInit();
            this.Navigator.ResumeLayout(false);
            this.Navigator.PerformLayout();
            this.CoordsPanel.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VisSlider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TrackBar TimeMax;
        private System.Windows.Forms.Label CodeWindow;
        private System.Windows.Forms.Label FirstLine;
        private System.Windows.Forms.Label TimeMaxLabel;
        private MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel Navigator;
        private System.Windows.Forms.VScrollBar ScrollCode;
        private System.Windows.Forms.Panel CoordsPanel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label p1zDisplay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label p1yDisplay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label p1xDisplay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label p2xDisplay;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label p2zDisplay;
        private System.Windows.Forms.Label p2yDisplay;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.TrackBar VisSlider;
        private System.Windows.Forms.Label VisLabel;
        public System.Windows.Forms.Button SwitchType;
    }
}
