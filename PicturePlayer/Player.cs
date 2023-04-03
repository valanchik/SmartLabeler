using InputControllers;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturePlayer
{
    public abstract class Player : IPlayer,IPlayable
    {
        
        public event Action OnTick;
        public event Action OnPlay;
        public event Action OnPause;
        public event Action OnStop;
        public event Action OnReady;
        public event Action OnEndPlaying;

        protected PictureBox _pictureBox;
        protected Timer _playbackTimer;
        protected int _playbackSpeed = 1;
        protected PlayerInputHandler inputsHandler;
        protected int frameCount;
        protected int currentFrameIndex = 0;
        protected string currentPath;
        private IInputController inputs;

        public Player(PlayerInputHandler inputsHandler)
        {
            this.inputsHandler = inputsHandler;
            this.inputs = inputsHandler.GetInputsController();
            this.inputsHandler.SetPlayer(this);
            _pictureBox = (PictureBox)inputs.GetElement(InputsControllerType.PictureBox);
            _playbackTimer = new Timer();
            _playbackTimer.Tick += OnPlaybackTimerTick;
            
        }

        public int PlaybackSpeed
        {
            get { return _playbackSpeed; }
            set
            {
                _playbackSpeed = value;
                _playbackTimer.Interval = 1000 / value;
            }
        }

        public void Play()
        {
            if (!IsPlaying())
            {
                _playbackTimer.Start();
                OnPlay?.Invoke();
            }
            
        }

        public void Pause()
        {
            if (IsPlaying())
            {
                _playbackTimer.Stop();
                OnPause?.Invoke();
            }
        }
        public void Stop()
        {
            if (IsPlaying())
            {
                _playbackTimer.Stop();
                ShowFrameByIndex(0);
                OnStop?.Invoke();
            }
        }
        public bool IsPlaying()
        {
            return _playbackTimer.Enabled;
        }
       

        protected bool UpdatePictureBox()
        {
            Bitmap frame = GetCurrentFrame();
            
            if (frame != null)
            {
                    _pictureBox.Image = frame;
                    GC.Collect();
                    OnTick?.Invoke();
                    return true;
            }
            return false;
        }

        public abstract Bitmap GetCurrentFrame();

        protected virtual void OnPlaybackTimerTick(object sender, EventArgs e) {
            if (!ShowNextFrame())
            {
                Pause();
                OnEndPlaying?.Invoke();
            }
        }


        
        public virtual bool ShowNextFrame()
        {
            int nextFrameIndex = GetCurrentFrameIndex() + 1;
            if (nextFrameIndex < frameCount)
            {
                SetCurrentFrameIndex(nextFrameIndex);
                UpdatePictureBox();
                return true;
            }
            return false;
        }
        public virtual bool ShowPreviousFrame()
        {
            int previousFrameIndex = GetCurrentFrameIndex() - 1;
            if (previousFrameIndex >= 0)
            {
                SetCurrentFrameIndex(previousFrameIndex);
                UpdatePictureBox();
                return true;
            }

            return false;
        }
        protected bool IndexIsValid(int index)
        {
            if(index >= 0 && index < frameCount)
            {
                return true;
            }
            return false;

        }
        public virtual bool ShowFrameByIndex(int index)
        {
            if (IndexIsValid(index))
            {
                SetCurrentFrameIndex(index);
                UpdatePictureBox();
                return true;
            }
            return false;
        }

        public abstract void Init();
        public abstract IPlayer SetSource(PlaySource resource);
        public abstract bool IsReady();
        public int GetCurrentFrameIndex()
        {
            return currentFrameIndex;
        }
        public void SetCurrentFrameIndex(int index)
        {
             currentFrameIndex = index;
        }
        
        protected void RaiseOnTick() => OnTick?.Invoke();
        protected void RaiseOnPause() => OnPause?.Invoke();
        protected void RaiseOnReady() => OnReady?.Invoke();
        protected void RaiseOnEndPlaying() => OnEndPlaying?.Invoke();

        public abstract IFrameSaver GetFrameSaver();

        public Form GetCurrentWindow() {
            return FindParentForm();
        }
        protected Form FindParentForm()
        {
            Control parent = _pictureBox;

            while (parent != null)
            {
                if (parent is Form)
                {
                    return (Form)parent;
                }

                parent = parent.Parent;
            }

            return null;
        }
        public abstract Task SaveAllFramesAsync();

        public int GetFramesCount()
        {
            return frameCount;
        }

        public PictureBox GetScreen()
        {
            return _pictureBox;
        }
    }

}
