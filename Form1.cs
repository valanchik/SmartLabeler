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
            IInputController rectController = new InputController();
            rectController.SetElement(InputsControllerType.RectangleInfo, rectangleInfo);
            rectController.SetElement(InputsControllerType.AddRectToFrameBtn, addRectToFrameBtn);
            /*//_inputRectController = new InputRectController(addRectToFrameBtn);
            _rectangleSelector = new RectangleSelector(pictureBox, rectController);
            _zoomablePictureBox = new ZoomablePictureBox(_rectangleSelector);*/

            IInputController playerControls = new InputController();
            playerControls.SetElement(InputsControllerType.Play, playBtn);
            playerControls.SetElement(InputsControllerType.Pause, pauseBtn);
            playerControls.SetElement(InputsControllerType.Stop, stopBtn);
            playerControls.SetElement(InputsControllerType.NextFrame, nextFrameBtn);
            playerControls.SetElement(InputsControllerType.PrevFrame, prevFrameBtn);
            playerControls.SetElement(InputsControllerType.TimelineBar, timelineBar);
            playerControls.SetElement(InputsControllerType.PictureBox, pictureBox);
            playerControls.SetElement(InputsControllerType.OpenVideo, openVideoButton);
            playerControls.SetElement(InputsControllerType.OpenImageFolder, openFolderButton);
            playerControls.SetElement(InputsControllerType.SpeedPlayback, speedPlayback);

            new ProjecManager(playerControls, rectController);
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
