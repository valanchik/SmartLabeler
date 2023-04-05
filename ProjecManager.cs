using InputControllers;
using PicturePlayer;
using RectSelector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcScan
{
    public class ProjecManager
    {
        private VideoFileSelector videoFileSelector;
        private FolderImagesSelector folderImagesSelector;
        private IPlayer player;
        private PlayerInputHandler  inputsHandler;
        private IInputController inputs;
        private RectangleManagerForPlayer rectangleManagerForPlayer;
        private RectangleSelector rectangleSelector;
        public ProjecManager(IInputController playerInputs, IInputController rectInputs)
        {
            rectangleSelector = new RectangleSelector((PictureBox)playerInputs.GetElement(InputsControllerType.PictureBox), rectInputs);
            new ZoomablePictureBox(rectangleSelector);
            rectangleManagerForPlayer = new RectangleManagerForPlayer(rectInputs, rectangleSelector);
            this.inputs = playerInputs;
            inputsHandler = new PlayerInputHandler(playerInputs);
            videoFileSelector = new VideoFileSelector(
                (Button)playerInputs.GetElement(InputsControllerType.OpenVideo)
            );
            videoFileSelector.OnSource += OnSource;
            folderImagesSelector = new FolderImagesSelector(
                (Button)playerInputs.GetElement(InputsControllerType.OpenImageFolder)
            );
            folderImagesSelector.OnSource += OnSource;
        }

        private void OnSource(PlaySource source)
        {

            player?.Pause();
            
            switch (source.Type)
            {
                case PlaySourceType.Video:
                    player = new VideoPlayer(inputsHandler, new FrameSaver(GetRandomeDir()));
                    break;
                case PlaySourceType.FolderImages:
                    player = new FolderPlayer(inputsHandler, new FrameSaver(GetRandomeDir()));
                    break;
            }
            if (player!=null)
            {
                rectangleManagerForPlayer.ResetRectangles();
                rectangleManagerForPlayer.SetPlayer(player);
                player.SetSource(source);
                player.Init();
                player.PlaybackSpeed = (int)((NumericUpDown)inputs.GetElement(InputsControllerType.SpeedPlayback)).Value;
                
            }
        }

        private  string GetRandomeDir()
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string randomFolderName = Path.GetRandomFileName();
            return Path.Combine(appDirectory, randomFolderName);
        }
    }
}
