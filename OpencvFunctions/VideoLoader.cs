using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Drawing;
using System.IO;

public class VideoLoader : IDisposable
{
    private VideoCapture _videoCapture;
    private Mat _frame;
    private int _currentFrameIndex;
    private bool _disposedValue;

    public VideoLoader(string filePath)
    {
        _videoCapture = new VideoCapture(filePath);
        if (!_videoCapture.IsOpened())
        {
            throw new ApplicationException("Cannot open video file.");
        }

        _frame = new Mat();
        _currentFrameIndex = 0;
    }

    public int FrameCount => (int)_videoCapture.Get(VideoCaptureProperties.FrameCount);

    public int CurrentFrameIndex
    {
        get => _currentFrameIndex;
        set
        {
            if (value < 0 || value >= FrameCount)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Frame index is out of range.");
            }
            _currentFrameIndex = value;
            _videoCapture.Set(VideoCaptureProperties.PosFrames, value);
        }
    }

    public Bitmap GetCurrentFrame()
    {
        if (_videoCapture.Read(_frame))
        {
            if (_currentFrameIndex == (int)_videoCapture.Get(VideoCaptureProperties.PosFrames)-1)
            {
                return BitmapConverter.ToBitmap(_frame);
            }
        }

        return null;
    }

    public Bitmap GetNextFrame()
    {
        if (_currentFrameIndex + 1 < FrameCount)
        {
            _currentFrameIndex++;
            //_videoCapture.Set(VideoCaptureProperties.PosFrames, _currentFrameIndex);
            return GetCurrentFrame();
        }

        return null;
    }

    public Bitmap GetPreviousFrame()
    {
        if (_currentFrameIndex - 1 >= 0)
        {
            _currentFrameIndex--;
            _videoCapture.Set(VideoCaptureProperties.PosFrames, _currentFrameIndex);
            return GetCurrentFrame();
        }

        return null;
    }
    public Bitmap GetFrameByIndex(int index)
    {
        if (index < 0 || index >= FrameCount)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Frame index is out of range.");
        }

        _currentFrameIndex = index;
        _videoCapture.Set(VideoCaptureProperties.PosFrames, _currentFrameIndex);
        return GetCurrentFrame();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // Dispose managed resources
            }

            // Dispose unmanaged resources
            _videoCapture.Dispose();
            _frame.Dispose();

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~VideoLoader()
    {
        Dispose(false);
    }
}
