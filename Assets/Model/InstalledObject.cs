using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstalledObject {

    // base tile
    Tile tile;

    string objectType;

    // if movmentCost is 0, then this tile is impassible (walls)
    float movmentCost = 1f;

    int width = 1;
    int height = 1;

    // this is used by our object factory
    public InstalledObject(string objectType, float movmentCost = 1f, int width = 1, int height = 1)
    {
        this.objectType = objectType;
        this.movmentCost = movmentCost;
        this.width = 1;
        this.height = 1;
    }

    protected InstalledObject( InstalledObject proto, Tile tile)
    {
        this.objectType = objectType;
        this.movmentCost = movmentCost;
        this.width = 1;
        this.height = 1;

        this.tile = tile;

        //tile.installedObject = this;
    }
}
