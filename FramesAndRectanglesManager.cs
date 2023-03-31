using PicturePlayer;
using RectSelector;
using System;
using System.Collections.Generic;

public class RectangleManagerForPlayer
{
    private readonly List<IResizableRectangle> _resizableRectangles;
    private readonly IPlayer player;

    public RectangleManagerForPlayer(IPlayer player)
    {
        _resizableRectangles = new List<IResizableRectangle>();
        this.player = player;
    }

    public int Count => _resizableRectangles.Count;

    public void AddNewResizableRectangle(IResizableRectangle resizableRectangle)
    {
        _resizableRectangles.Add(resizableRectangle);
    }

    public void RemoveResizableRectangle(int frameIndex)
    {
        if (frameIndex >= 0 && frameIndex < _resizableRectangles.Count)
        {
            _resizableRectangles.RemoveAt(frameIndex);
        }
    }

    public IResizableRectangle GetResizableRectangle(int frameIndex)
    {
        if (frameIndex >= 0 && frameIndex < _resizableRectangles.Count)
        {
            return _resizableRectangles[frameIndex];
        }

        return null;
    }
    public IResizableRectangle GetOrCreateResizableRectangleForCurrentFrame(Func<IResizableRectangle> createResizableRectangle)
    {
        int currentFrameIndex = player.GetCurrentFrameIndex();

        if (currentFrameIndex >= _resizableRectangles.Count)
        {
            for (int i = _resizableRectangles.Count; i <= currentFrameIndex; i++)
            {
                _resizableRectangles.Add(createResizableRectangle());
            }
        }

        return _resizableRectangles[currentFrameIndex];
    }

}

