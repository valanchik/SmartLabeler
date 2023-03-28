using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturePlayer
{
    public class ProgressDialog : Form
    {
        private ProgressBar progressBar;
        private Label _progressLabel;
        public ProgressDialog()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            _progressLabel = new Label
            {
                Text = "Сохранение кадров: 0",
                AutoSize = true,
                Location = new Point(20, 20),
            };

            this.Controls.Add(_progressLabel);
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(300, 100);
            Text = "Процесс сохранения";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowIcon = false;
            ShowInTaskbar = false;

            InitializeComponent();
        }

        public void UpdateProgress(int frameIndex)
        {
            _progressLabel.Text = $"Сохранение кадров: {frameIndex}";
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void InitializeComponent()
        {
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(4, 12);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(268, 23);
            this.progressBar.TabIndex = 0;
            // 
            // ProgressDialog
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(284, 44);
            this.Controls.Add(this.progressBar);
            this.DoubleBuffered = true;
            this.Name = "ProgressDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);

        }
    }
}
