﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile {

	public enum TileType { Empty, Floor};

    TileType oldType;

    TileType type = TileType.Empty;

    Action<Tile> cbTileTypeChanged;

    public TileType Type
    {
        get
        {
            return type;
        }

        set
        {
            oldType = type;
            type = value;
            // Call the callback and let things know we´ve changed.
            if (cbTileTypeChanged != null && oldType != type)
                cbTileTypeChanged(this);
        }
    }

    public int X
    {
        get
        {
            return x;
        }
    }

    public int Y
    {
        get
        {
            return y;
        }
    }

    LooseObject looseObject;
    InstalledObject installedObject;

    World world;
    int x;
    int y;

    

    public Tile( World world, int x, int y)
    {
        this.world = world;
        this.x = x;
        this.y = y;
    }

    public void RegisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged += callback;
    }

    public void UnregisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged -= callback;
    }
}
