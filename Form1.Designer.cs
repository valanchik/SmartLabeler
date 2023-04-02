
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
            this.addRectToFrameBtn = new System.Windows.Forms.Button();
            this.rectangleInfo = new System.Windows.Forms.Label();
            this.pictureBox = new ProcScan.DoubleBufferedPictureBox();
            this.doubleBufferedPanel1 = new ProcScan.DoubleBufferedPanel();
            this.videoToImagesButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.openFolderButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.speedPlayback = new System.Windows.Forms.NumericUpDown();
            this.prevFrameBtn = new System.Windows.Forms.Button();
            this.nextFrameBtn = new System.Windows.Forms.Button();
            this.pauseBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.playBtn = new System.Windows.Forms.Button();
            this.timelineBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.doubleBufferedPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedPlayback)).BeginInit();
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
            this.openVideoButton.Size = new System.Drawing.Size(104, 23);
            this.openVideoButton.TabIndex = 1;
            this.openVideoButton.Text = "Open video file";
            this.openVideoButton.UseVisualStyleBackColor = true;
            // 
            // addRectToFrameBtn
            // 
            this.addRectToFrameBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.addRectToFrameBtn.Location = new System.Drawing.Point(20, 51);
            this.addRectToFrameBtn.Name = "addRectToFrameBtn";
            this.addRectToFrameBtn.Size = new System.Drawing.Size(125, 23);
            this.addRectToFrameBtn.TabIndex = 3;
            this.addRectToFrameBtn.Text = "Add Rectangle";
            this.addRectToFrameBtn.UseVisualStyleBackColor = true;
            // 
            // rectangleInfo
            // 
            this.rectangleInfo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.rectangleInfo.AutoSize = true;
            this.rectangleInfo.Location = new System.Drawing.Point(1092, 455);
            this.rectangleInfo.Name = "rectangleInfo";
            this.rectangleInfo.Size = new System.Drawing.Size(38, 15);
            this.rectangleInfo.TabIndex = 4;
            this.rectangleInfo.Text = "label1";
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
            this.doubleBufferedPanel1.Size = new System.Drawing.Size(1018, 397);
            this.doubleBufferedPanel1.TabIndex = 6;
            // 
            // videoToImagesButton
            // 
            this.videoToImagesButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.videoToImagesButton.Location = new System.Drawing.Point(20, 22);
            this.videoToImagesButton.Name = "videoToImagesButton";
            this.videoToImagesButton.Size = new System.Drawing.Size(125, 23);
            this.videoToImagesButton.TabIndex = 7;
            this.videoToImagesButton.Text = "Conver to images";
            this.videoToImagesButton.UseVisualStyleBackColor = true;
            this.videoToImagesButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.videoToImagesButton);
            this.groupBox1.Controls.Add(this.addRectToFrameBtn);
            this.groupBox1.Location = new System.Drawing.Point(1036, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(170, 88);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Controls ";
            // 
            // openFolderButton
            // 
            this.openFolderButton.Location = new System.Drawing.Point(122, 12);
            this.openFolderButton.Name = "openFolderButton";
            this.openFolderButton.Size = new System.Drawing.Size(104, 23);
            this.openFolderButton.TabIndex = 9;
            this.openFolderButton.Text = "Open folder";
            this.openFolderButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.speedPlayback);
            this.panel1.Controls.Add(this.prevFrameBtn);
            this.panel1.Controls.Add(this.nextFrameBtn);
            this.panel1.Controls.Add(this.pauseBtn);
            this.panel1.Controls.Add(this.stopBtn);
            this.panel1.Controls.Add(this.playBtn);
            this.panel1.Location = new System.Drawing.Point(12, 470);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1018, 40);
            this.panel1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(927, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "кадр/сек.";
            // 
            // speedPlayback
            // 
            this.speedPlayback.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.speedPlayback.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.speedPlayback.Location = new System.Drawing.Point(874, 7);
            this.speedPlayback.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.speedPlayback.Name = "speedPlayback";
            this.speedPlayback.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.speedPlayback.Size = new System.Drawing.Size(47, 26);
            this.speedPlayback.TabIndex = 5;
            this.speedPlayback.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.speedPlayback.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // prevFrameBtn
            // 
            this.prevFrameBtn.BackgroundImage = global::ProcScan.Properties.Resources.back;
            this.prevFrameBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.prevFrameBtn.CausesValidation = false;
            this.prevFrameBtn.Location = new System.Drawing.Point(49, 3);
            this.prevFrameBtn.Name = "prevFrameBtn";
            this.prevFrameBtn.Size = new System.Drawing.Size(40, 34);
            this.prevFrameBtn.TabIndex = 4;
            this.prevFrameBtn.UseVisualStyleBackColor = true;
            // 
            // nextFrameBtn
            // 
            this.nextFrameBtn.BackgroundImage = global::ProcScan.Properties.Resources.forward;
            this.nextFrameBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.nextFrameBtn.CausesValidation = false;
            this.nextFrameBtn.Location = new System.Drawing.Point(141, 3);
            this.nextFrameBtn.Name = "nextFrameBtn";
            this.nextFrameBtn.Size = new System.Drawing.Size(40, 34);
            this.nextFrameBtn.TabIndex = 3;
            this.nextFrameBtn.UseVisualStyleBackColor = true;
            // 
            // pauseBtn
            // 
            this.pauseBtn.BackgroundImage = global::ProcScan.Properties.Resources.pause;
            this.pauseBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pauseBtn.CausesValidation = false;
            this.pauseBtn.Location = new System.Drawing.Point(187, 3);
            this.pauseBtn.Name = "pauseBtn";
            this.pauseBtn.Size = new System.Drawing.Size(40, 34);
            this.pauseBtn.TabIndex = 2;
            this.pauseBtn.UseVisualStyleBackColor = true;
            // 
            // stopBtn
            // 
            this.stopBtn.BackgroundImage = global::ProcScan.Properties.Resources.stop;
            this.stopBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.stopBtn.CausesValidation = false;
            this.stopBtn.Location = new System.Drawing.Point(95, 3);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(40, 34);
            this.stopBtn.TabIndex = 1;
            this.stopBtn.UseVisualStyleBackColor = true;
            // 
            // playBtn
            // 
            this.playBtn.BackgroundImage = global::ProcScan.Properties.Resources.play;
            this.playBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.playBtn.CausesValidation = false;
            this.playBtn.Location = new System.Drawing.Point(3, 3);
            this.playBtn.Name = "playBtn";
            this.playBtn.Size = new System.Drawing.Size(40, 34);
            this.playBtn.TabIndex = 0;
            this.playBtn.UseVisualStyleBackColor = true;
            // 
            // timelineBar
            // 
            this.timelineBar.Location = new System.Drawing.Point(12, 444);
            this.timelineBar.Name = "timelineBar";
            this.timelineBar.Size = new System.Drawing.Size(1018, 23);
            this.timelineBar.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1210, 585);
            this.Controls.Add(this.timelineBar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.openFolderButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.doubleBufferedPanel1);
            this.Controls.Add(this.rectangleInfo);
            this.Controls.Add(this.openVideoButton);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.doubleBufferedPanel1.ResumeLayout(false);
            this.doubleBufferedPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedPlayback)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button openVideoButton;
        private System.Windows.Forms.Button addRectToFrameBtn;
        private System.Windows.Forms.Label rectangleInfo;
        protected System.Windows.Forms.Panel panel2;
        private DoubleBufferedPictureBox doubleBufferedPictureBox1;
        private DoubleBufferedPanel doubleBufferedPanel1;
        private DoubleBufferedPictureBox pictureBox;
        private System.Windows.Forms.Button videoToImagesButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button openFolderButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button playBtn;
        private System.Windows.Forms.Button prevFrameBtn;
        private System.Windows.Forms.Button nextFrameBtn;
        private System.Windows.Forms.Button pauseBtn;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.ProgressBar timelineBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown speedPlayback;
    }
}

