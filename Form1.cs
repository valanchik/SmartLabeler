using InputControllers;
using PicturePlayer;
using RectSelector;
using System;
using System.IO;
using System.Windows.Forms;

namespace ProcScan
{
    public partial class Form1 : Form

    {
        private VideoFileSelector _videoFileSelector;
        private FolderImagesSelector folderImagesSelector;
        private RectangleSelector _rectangleSelector;
        private ZoomablePictureBox _zoomablePictureBox;
        private InputRectController _inputRectController;
        private IPlayer videoPlayer;
        public Form1()
        {
            InitializeComponent();
            _inputRectController = new InputRectController(addRectToFrameBtn);
            _rectangleSelector = new RectangleSelector(pictureBox, rectangleInfo, _inputRectController);
            _zoomablePictureBox = new ZoomablePictureBox(_rectangleSelector);
            IFrameSaver frameSaver = new FrameSaver(GetRandomeDir());

            IInputPlayerController playerControls = new InputPlayerController();
            playerControls.SetElement(InputPlayerControllerType.Play, playBtn);
            playerControls.SetElement(InputPlayerControllerType.Pause, pauseBtn);
            playerControls.SetElement(InputPlayerControllerType.Stop, stopBtn);
            playerControls.SetElement(InputPlayerControllerType.NextFrame, nextFrameBtn);
            playerControls.SetElement(InputPlayerControllerType.PrevFrame, prevFrameBtn);
            playerControls.SetElement(InputPlayerControllerType.TimelineBar, timelineBar);

            videoPlayer = new VideoPlayer(pictureBox, playerControls, frameSaver);
            _videoFileSelector = new VideoFileSelector(openVideoButton, videoPlayer);

            IPlayer folderPlayer = new FolderPlayer(pictureBox, playerControls, frameSaver);
            folderImagesSelector = new FolderImagesSelector(openFolderButton, folderPlayer);
        }
        private string GetRandomeDir()
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string randomFolderName = Path.GetRandomFileName();
            return Path.Combine(appDirectory, randomFolderName);
        }

        async private void button1_Click(object sender, EventArgs e)
        {
            await videoPlayer.SaveAllFramesAsync();

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
