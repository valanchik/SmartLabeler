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
        private readonly RectangleSelector _rectangleSelector;
        private readonly ZoomablePictureBox _zoomablePictureBox;
        private readonly InputRectController _inputRectController;
        public Form1()
        {
            InitializeComponent();

            _inputRectController = new InputRectController(addRectToFrameBtn);
            _rectangleSelector = new RectangleSelector(pictureBox, rectangleInfo, _inputRectController);
            _zoomablePictureBox = new ZoomablePictureBox(_rectangleSelector);

            IInputPlayerController playerControls = new InputPlayerController();
            playerControls.SetElement(InputsPlayerControllerType.Play, playBtn);
            playerControls.SetElement(InputsPlayerControllerType.Pause, pauseBtn);
            playerControls.SetElement(InputsPlayerControllerType.Stop, stopBtn);
            playerControls.SetElement(InputsPlayerControllerType.NextFrame, nextFrameBtn);
            playerControls.SetElement(InputsPlayerControllerType.PrevFrame, prevFrameBtn);
            playerControls.SetElement(InputsPlayerControllerType.TimelineBar, timelineBar);
            playerControls.SetElement(InputsPlayerControllerType.PictureBox, pictureBox);
            playerControls.SetElement(InputsPlayerControllerType.OpenVideo, openVideoButton);
            playerControls.SetElement(InputsPlayerControllerType.OpenImageFolder, openFolderButton);
            playerControls.SetElement(InputsPlayerControllerType.SpeedPlayback, speedPlayback);

            new ProjecManager(playerControls);
        }
        

        private async void button1_Click(object sender, EventArgs e)
        {

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
