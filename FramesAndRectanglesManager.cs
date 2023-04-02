using PicturePlayer;
using RectSelector;
using System;
using System.Collections.Generic;

public class RectangleManagerForPlayer
{
    private readonly Dictionary<int,IResizableRectangle> _resizableRectangles = new Dictionary<int, IResizableRectangle>();
    private readonly IPlayer player;

    public RectangleManagerForPlayer(IPlayer player)
    {
        this.player = player;
    }
}

