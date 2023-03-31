using System;
using System.IO;

namespace PicturePlayer
{
    public class SelectorBase
    {
        protected IPlayer player;
        protected string selectedPath;
        protected string GetRandomeDir()
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string randomFolderName = Path.GetRandomFileName();
            return Path.Combine(appDirectory, randomFolderName);
        }
    }
}