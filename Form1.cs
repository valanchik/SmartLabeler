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
        private Player picturePlayer;
        public Form1()
        {
            InitializeComponent();
            
            _inputRectController = new InputRectController(addRectToFrameBtn);
            _rectangleSelector = new RectangleSelector(pictureBox, label1, _inputRectController);
            _zoomablePictureBox = new ZoomablePictureBox(_rectangleSelector);

            _videoFileSelector = new VideoFileSelector(videoFilePath, pictureBox, openVideoButton);
            picturePlayer = new Player(_videoFileSelector, pictureBox);
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
