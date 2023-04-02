using System;
using System.IO;

namespace PicturePlayer
{
    public class SelectorBase
    {
        public string selectedPath;
        public delegate void SourceDelegate(PlaySource source);
        public event SourceDelegate OnSource;
        
        protected void RaiseOnSource(PlaySource source)
        {
            OnSource?.Invoke(source);
        }
    }
}