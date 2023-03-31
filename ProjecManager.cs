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
        public ProjecManager(IInputPlayerController playerControls)
        {
            videoFileSelector = new VideoFileSelector(
                (Button)playerControls.GetElement(InputPlayerControllerType.OpenVideo),
                playerControls
            );
            folderImagesSelector = new FolderImagesSelector(
                (Button)playerControls.GetElement(InputPlayerControllerType.OpenImageFolder),
                playerControls
            );
        }
        
    }
}
