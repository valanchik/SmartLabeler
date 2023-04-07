using Hotkeys;
using InputControllers;
using PicturePlayer;
using RectSelector;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SmartLabeler
{
    public partial class SmartLabeler : Form

    {
        private readonly RectangleSelector _rectangleSelector;
        private readonly ZoomablePictureBox _zoomablePictureBox;
        private readonly InputRectController _inputRectController;
        private HotkeyManager hotkeyManager;
        public SmartLabeler()
        {
            InitializeComponent();
            hotkeyManager = HotkeyManager.Instance;
            IInputController rectController = new InputController();
            rectController.SetElement(InputsControllerType.RectangleInfo, rectangleInfo);
            rectController.SetElement(InputsControllerType.AddRectToFrameBtn, addRectToFrameBtn);

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

            new ProjecManager(playerControls, rectController, hotkeyManager);
        }
        

        private async void button1_Click(object sender, EventArgs e)
        {

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Debug.WriteLine(keyData);
            hotkeyManager.ProcessHotkeys(keyData);
            return base.ProcessCmdKey(ref msg, keyData);
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
