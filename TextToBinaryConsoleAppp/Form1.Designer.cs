namespace TextToBinaryConsoleAppp
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
            this.Open = new System.Windows.Forms.Button();
            this.DSRatioTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.GridSizeTextbox = new System.Windows.Forms.TextBox();
            this.DownSample = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label3 = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.Instruction = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Open
            // 
            this.Open.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Open.Location = new System.Drawing.Point(97, 434);
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(138, 23);
            this.Open.TabIndex = 0;
            this.Open.Text = "Open";
            this.Open.UseVisualStyleBackColor = true;
            this.Open.Click += new System.EventHandler(this.button1_Click);
            // 
            // DSRatioTextbox
            // 
            this.DSRatioTextbox.Location = new System.Drawing.Point(135, 34);
            this.DSRatioTextbox.Name = "DSRatioTextbox";
            this.DSRatioTextbox.Size = new System.Drawing.Size(100, 20);
            this.DSRatioTextbox.TabIndex = 1;
            this.DSRatioTextbox.TextChanged += new System.EventHandler(this.DSRatioTextbox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Down Sample Ratio";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Grid Size";
            // 
            // GridSizeTextbox
            // 
            this.GridSizeTextbox.Location = new System.Drawing.Point(135, 62);
            this.GridSizeTextbox.Name = "GridSizeTextbox";
            this.GridSizeTextbox.Size = new System.Drawing.Size(100, 20);
            this.GridSizeTextbox.TabIndex = 4;
            this.GridSizeTextbox.TextChanged += new System.EventHandler(this.GridSizeTextbox_TextChanged);
            // 
            // DownSample
            // 
            this.DownSample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DownSample.Location = new System.Drawing.Point(555, 434);
            this.DownSample.Name = "DownSample";
            this.DownSample.Size = new System.Drawing.Size(138, 23);
            this.DownSample.TabIndex = 5;
            this.DownSample.Text = "Down Sample";
            this.DownSample.UseVisualStyleBackColor = true;
            this.DownSample.Click += new System.EventHandler(this.DownSample_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(94, 418);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 6;
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.SaveButton.Location = new System.Drawing.Point(326, 434);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(138, 23);
            this.SaveButton.TabIndex = 8;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // Instruction
            // 
            this.Instruction.AllowDrop = true;
            this.Instruction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Instruction.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Instruction.CausesValidation = false;
            this.Instruction.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Instruction.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Instruction.Location = new System.Drawing.Point(97, 214);
            this.Instruction.Name = "Instruction";
            this.Instruction.Size = new System.Drawing.Size(596, 17);
            this.Instruction.TabIndex = 7;
            this.Instruction.Text = "Please Select the point cloud";
            this.Instruction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 469);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.Instruction);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DownSample);
            this.Controls.Add(this.GridSizeTextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DSRatioTextbox);
            this.Controls.Add(this.Open);
            this.Name = "Form1";
            this.Text = "DownSampleProg";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Open;
        private System.Windows.Forms.TextBox DSRatioTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox GridSizeTextbox;
        private System.Windows.Forms.Button DownSample;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label Instruction;
    }
}