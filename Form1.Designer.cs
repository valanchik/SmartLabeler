
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.doubleBufferedPanel1.SuspendLayout();
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
            this.addRectToFrame.Location = new System.Drawing.Point(750, 82);
            this.addRectToFrame.Name = "addRectToFrame";
            this.addRectToFrame.Size = new System.Drawing.Size(75, 23);
            this.addRectToFrame.TabIndex = 3;
            this.addRectToFrame.Text = "button1";
            this.addRectToFrame.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(792, 446);
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
            this.pictureBox.TabIndex = 5;
            this.pictureBox.TabStop = false;
            // 
            // doubleBufferedPanel1
            // 
            this.doubleBufferedPanel1.Controls.Add(this.pictureBox);
            this.doubleBufferedPanel1.Location = new System.Drawing.Point(12, 69);
            this.doubleBufferedPanel1.Name = "doubleBufferedPanel1";
            this.doubleBufferedPanel1.Size = new System.Drawing.Size(682, 378);
            this.doubleBufferedPanel1.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1210, 571);
            this.Controls.Add(this.doubleBufferedPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addRectToFrame);
            this.Controls.Add(this.videoFilePath);
            this.Controls.Add(this.openVideoButton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.doubleBufferedPanel1.ResumeLayout(false);
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
    }
}

