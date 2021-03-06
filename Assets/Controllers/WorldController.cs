﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldController : MonoBehaviour {

    public static WorldController Instance { get; protected set; }

    public Sprite floorSprite;

    Dictionary<Tile, GameObject> tileGameObjectMap;

    public World world { get; protected set; }

	// Use this for initialization
	void Start () {

        if (Instance != null)
        {
            Debug.LogError("There should never be two world controllers.");
        }
        Instance = this;

        tileGameObjectMap = new Dictionary<Tile, GameObject>();

        //Create Empty wolrd with empty tiles
        world = new World();

        // Create a GameObject for each of our tiles, so they show visually.
        for (int x = 0; x < world.Width; x++)
        {
            for (int y = 0; y < world.Height; y++)
            {
                Tile tile_data = world.GetTileAt(x, y);

                GameObject tile_go = new GameObject();

                tileGameObjectMap.Add(tile_data, tile_go);

                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3( tile_data.X, tile_data.Y,0);
                tile_go.transform.SetParent(this.transform, true);

                // add a sprite renderer, but don´t bother setting a sprite
                // because all tiles are empty right now
                tile_go.AddComponent<SpriteRenderer>();

                // Register our Callback so that our GameObject gets updated whenever
                // the tile type changes
                tile_data.RegisterTileTypeChangedCallback(OnTileTypeChange);
            }
        }

        world.RandomizeTiles();
	}


	void Update () {

    }

    void OnTileTypeChange(Tile tile_data)
    {

        if (!tileGameObjectMap.ContainsKey(tile_data))
        {
            Debug.LogError("tileGameObejct doesn´t contain the tile_date");
            return;
        }

        GameObject tile_go = tileGameObjectMap[tile_data];

        if(tile_go == null)
        {
            Debug.LogError("tileGameObjectMap returned GameObject is null");
            return;
        }

        if(tile_data.Type == TileType.Floor)
                {
            tile_go.GetComponent<SpriteRenderer>().sprite = floorSprite;
        }
        else if(tile_data.Type == TileType.Empty)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            Debug.LogError("OnTileTypeChanged - Unrecognized tile type.");
        }
    }

    public Tile GetTileAtWorldCoord(Vector3 coord)
    {
        int x = Mathf.FloorToInt(coord.x);
        int y = Mathf.FloorToInt(coord.y);

        return world.GetTileAt(x, y);
    }
}
