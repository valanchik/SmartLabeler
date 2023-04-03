using InputControllers;
using PicturePlayer;
using RectSelector;
using System;
using System.Collections.Generic;

public class RectangleManagerForPlayer
{
    private readonly Dictionary<int, List<ResizableRectangle>> _rectsList = new Dictionary<int, List<ResizableRectangle>>();
    private readonly IInputController rectInputController;
    private IPlayer player;
    private RectangleSelector rectangleSelector;

    public RectangleManagerForPlayer( IInputController rectInputController)
    {
        this.rectInputController = rectInputController;
    }
    public void SetPlayer(IPlayer player)
    {
        this.player?.Pause();
        this.player = player;
        HandlingPlayer();
        rectangleSelector =  new RectangleSelector(player.GetScreen(), rectInputController);
    }

    private void HandlingPlayer()
    {
        player.OnTick -= Player_OnTick;
        player.OnTick += Player_OnTick;
    }

    private void Player_OnTick()
    {
        var currentIndex = player.GetCurrentFrameIndex();

        if (_rectsList.TryGetValue(currentIndex, out var rects))
        {
            rectangleSelector.SetRectangles(rects);
            rectangleSelector.UpdateAllRectangles();
        } else
        {
            var newRects = new List<ResizableRectangle>();
            _rectsList.Add(currentIndex, newRects);
            rectangleSelector.SetRectangles(newRects);
            rectangleSelector.UpdateAllRectangles();
        }
    }
}

