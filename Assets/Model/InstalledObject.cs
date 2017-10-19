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

    protected InstalledObject()
    {

    }

    // this is used by our object factory
    static public InstalledObject CreatePrototype(string objectType, float movmentCost = 1f, int width = 1, int height = 1)
    {
        InstalledObject obj = new InstalledObject();
        obj.objectType = objectType;
        obj.movmentCost = movmentCost;
        obj.width = 1;
        obj.height = 1;
        return obj;
    }

    static public InstalledObject PlaceInstance( InstalledObject proto, Tile tile)
    {
        InstalledObject obj = new InstalledObject();
        obj.objectType = proto.objectType;
        obj.movmentCost = proto.movmentCost;
        obj.width = 1;
        obj.height = 1;

        obj.tile = tile;

        // Fix me: this assumes we are 1x1!
        if(tile.PlaceObject(obj) == false)
        {
            return null;
        }

        return obj;
    }
}
