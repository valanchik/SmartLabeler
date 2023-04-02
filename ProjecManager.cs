using InputControllers;
using PicturePlayer;
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
        private IInputPlayerController inputs;
        public ProjecManager(IInputPlayerController inputs)
        {
            this.inputs = inputs;
            inputsHandler = new PlayerInputHandler(inputs);
            videoFileSelector = new VideoFileSelector(
                (Button)inputs.GetElement(InputsPlayerControllerType.OpenVideo)
            );
            videoFileSelector.OnSource += OnSource;
            folderImagesSelector = new FolderImagesSelector(
                (Button)inputs.GetElement(InputsPlayerControllerType.OpenImageFolder)
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
                player.SetSource(source);
                player.Init();
                player.PlaybackSpeed = (int)((NumericUpDown)inputs.GetElement(InputsPlayerControllerType.SpeedPlayback)).Value;
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
