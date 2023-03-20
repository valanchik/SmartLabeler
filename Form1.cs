using RectSelector;
using System.Drawing;
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

        private void button1_Click(object sender, System.EventArgs e)
        {
            _rectangleSelector.UpdateAllRectangles();
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
