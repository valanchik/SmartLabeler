using RectSelector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcScan
{
    public partial class Form1 : Form

    {
        private VideoFileSelector _videoFileSelector;
        private RectangleSelector _rectangleSelector;
        private ZoomablePictureBox _zoomablePictureBox;
        public Form1()
        {
            InitializeComponent();
            _videoFileSelector = new VideoFileSelector(videoFilePath, pictureBox, openVideoButton);
            _rectangleSelector = new RectangleSelector(pictureBox, label1, addRectToFrame);
            _zoomablePictureBox = new ZoomablePictureBox(pictureBox, _rectangleSelector);
        }



    }
    public class DoubleBufferedPictureBox : PictureBox
    {
        public DoubleBufferedPictureBox()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            UpdateStyles();
        }
    }
    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            SetStyle(ControlStyles.DoubleBuffer |
                     ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }
    }
}
