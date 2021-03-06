﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World {

    Tile[,] tiles;

    Dictionary<string, InstalledObject> InstalledObjectPrototypes;

    int width;

    public int Width
    {
        get
        {
            return width;
        }
    }

    int height;

    public int Height
    {
        get
        {
            return height;
        }
    }

    public World(int width = 100, int height = 100)
    {
        
        this.width = width;
        this.height = height;

        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new Tile(this, x, y);
            }
        }

        Debug.Log("World created with " + (width * height) + " tiles.");

        
        
    }

    void CreateinstalledOnjectPrototypes()
    {
        InstalledObjectPrototypes = new Dictionary<string, InstalledObject>();

        InstalledObjectPrototypes.Add("Wall", InstalledObject.CreatePrototype(
            "Wall",
            0, // Impassable
            1, //width
            1 //Height
            ));
    }



    public Tile GetTileAt(int x, int y)
    {
        if (x >= width || x < 0 || y >= height || y < 0)
        {
            Debug.LogError("Tile (" + x + "," + y + ") is out of range.");
            return null;
        }
        return tiles[x, y];
    }

    public void RandomizeTiles()
    {
        Debug.Log("rondomizedTiles");
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(Random.Range(0,2) == 0)
                {
                    tiles[x, y].Type = TileType.Empty;
                }
                else
                {
                    tiles[x, y].Type = TileType.Floor;
                }
            }
        }
    }

}
