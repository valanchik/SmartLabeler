using InputControllers;
using PicturePlayer;
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
        private InputRectController _inputRectController;
        public Form1()
        {
            InitializeComponent();
            _videoFileSelector = new VideoFileSelector(videoFilePath, pictureBox, openVideoButton);
            _inputRectController = new InputRectController(addRectToFrameBtn);
            _rectangleSelector = new RectangleSelector(pictureBox, label1, _inputRectController);
            _zoomablePictureBox = new ZoomablePictureBox(_rectangleSelector);
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
