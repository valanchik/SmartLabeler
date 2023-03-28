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
            };

            progressBar = new ProgressBar
            {
                Location = new Point(4, 40),
                Name = "progressBar",
                Size = new System.Drawing.Size(268, 23),
                TabIndex = 0,
            };

            // Выровнять _progressLabel по центру progressBar
            _progressLabel.Location = new Point((progressBar.Width - _progressLabel.Width) / 2, progressBar.Top - _progressLabel.Height - 5);
            this.Controls.Add(_progressLabel);
            this.Controls.Add(progressBar);

            StartPosition = FormStartPosition.CenterScreen;
            AutoSize = true; // Изменить размер окна в соответствии с размером элементов
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Padding = new Padding(20); // Отступы вокруг элементов формы
            Text = "Процесс сохранения";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowIcon = false;
            ShowInTaskbar = false;
        }

        public void UpdateProgress(int frameIndex, int count)
        {
            _progressLabel.Text = $"Сохранение кадров: {frameIndex}";

            progressBar.Maximum = count;
            progressBar.Value = frameIndex;

            // Обновить положение _progressLabel
            _progressLabel.Location = new Point((progressBar.Width - _progressLabel.Width) / 2, progressBar.Top - _progressLabel.Height - 5);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }

}
