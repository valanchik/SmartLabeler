
namespace ProcScan
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openVideoButton = new System.Windows.Forms.Button();
            this.videoFilePath = new System.Windows.Forms.TextBox();
            this.addRectToFrame = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox = new ProcScan.DoubleBufferedPictureBox();
            this.doubleBufferedPanel1 = new ProcScan.DoubleBufferedPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.doubleBufferedPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openVideoButton
            // 
            this.openVideoButton.Location = new System.Drawing.Point(12, 12);
            this.openVideoButton.Name = "openVideoButton";
            this.openVideoButton.Size = new System.Drawing.Size(75, 23);
            this.openVideoButton.TabIndex = 1;
            this.openVideoButton.Text = "Open";
            this.openVideoButton.UseVisualStyleBackColor = true;
            // 
            // videoFilePath
            // 
            this.videoFilePath.Location = new System.Drawing.Point(94, 11);
            this.videoFilePath.Name = "videoFilePath";
            this.videoFilePath.ReadOnly = true;
            this.videoFilePath.Size = new System.Drawing.Size(366, 23);
            this.videoFilePath.TabIndex = 2;
            // 
            // addRectToFrame
            // 
            this.addRectToFrame.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.addRectToFrame.Location = new System.Drawing.Point(39, 65);
            this.addRectToFrame.Name = "addRectToFrame";
            this.addRectToFrame.Size = new System.Drawing.Size(75, 23);
            this.addRectToFrame.TabIndex = 3;
            this.addRectToFrame.Text = "button1";
            this.addRectToFrame.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1092, 448);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(40, 46);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(395, 243);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 5;
            this.pictureBox.TabStop = false;
            // 
            // doubleBufferedPanel1
            // 
            this.doubleBufferedPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.doubleBufferedPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.doubleBufferedPanel1.Controls.Add(this.pictureBox);
            this.doubleBufferedPanel1.Location = new System.Drawing.Point(12, 41);
            this.doubleBufferedPanel1.Name = "doubleBufferedPanel1";
            this.doubleBufferedPanel1.Size = new System.Drawing.Size(1018, 438);
            this.doubleBufferedPanel1.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button1.Location = new System.Drawing.Point(39, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.addRectToFrame);
            this.groupBox1.Location = new System.Drawing.Point(1036, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(170, 107);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1210, 571);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.doubleBufferedPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.videoFilePath);
            this.Controls.Add(this.openVideoButton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.doubleBufferedPanel1.ResumeLayout(false);
            this.doubleBufferedPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button openVideoButton;
        private System.Windows.Forms.TextBox videoFilePath;
        private System.Windows.Forms.Button addRectToFrame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        protected System.Windows.Forms.Panel panel2;
        private DoubleBufferedPictureBox doubleBufferedPictureBox1;
        private DoubleBufferedPanel doubleBufferedPanel1;
        private DoubleBufferedPictureBox pictureBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

