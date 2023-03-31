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
        private readonly VideoFileSelector _videoFileSelector;
        private readonly FolderImagesSelector folderImagesSelector;
        private readonly RectangleSelector _rectangleSelector;
        private readonly ZoomablePictureBox _zoomablePictureBox;
        private readonly InputRectController _inputRectController;
        private readonly IPlayer videoPlayer;
        public Form1()
        {
            InitializeComponent();

            _inputRectController = new InputRectController(addRectToFrameBtn);
            _rectangleSelector = new RectangleSelector(pictureBox, rectangleInfo, _inputRectController);
            _zoomablePictureBox = new ZoomablePictureBox(_rectangleSelector);

            IInputPlayerController playerControls = new InputPlayerController();
            playerControls.SetElement(InputPlayerControllerType.Play, playBtn);
            playerControls.SetElement(InputPlayerControllerType.Pause, pauseBtn);
            playerControls.SetElement(InputPlayerControllerType.Stop, stopBtn);
            playerControls.SetElement(InputPlayerControllerType.NextFrame, nextFrameBtn);
            playerControls.SetElement(InputPlayerControllerType.PrevFrame, prevFrameBtn);
            playerControls.SetElement(InputPlayerControllerType.TimelineBar, timelineBar);
            playerControls.SetElement(InputPlayerControllerType.PictureBox, pictureBox);
            playerControls.SetElement(InputPlayerControllerType.OpenVideo, openVideoButton);
            playerControls.SetElement(InputPlayerControllerType.OpenImageFolder, openFolderButton);

            new ProjecManager(playerControls);
        }
        

        private async void button1_Click(object sender, EventArgs e)
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
