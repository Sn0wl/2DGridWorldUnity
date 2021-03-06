﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Events;
using UnityEditor.IMGUI;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour {

    public GameObject myCamera;

    public GameObject tileMarkerPrefab;

    bool buildModeIsObjects = false;
    string buildModeObjectType;

    TileType buildModeTile = TileType.Floor;


    Camera Cam;

    List<GameObject> dragPreviewGo;

    private int dragSize = 15;

    Vector3 dragStartPosition;
    Vector3 lastFramePosition;
    Vector3 currFramePosition;

    // Use this for initialization
    void Start () {
        dragPreviewGo = new List<GameObject>();
        Cam = myCamera.GetComponent<Camera>();
        SimplePool.Preload(tileMarkerPrefab, 1000);
    }

    // Update is called once per frame
    void Update() {

        currFramePosition = Cam.ScreenToWorldPoint(Input.mousePosition);

        //UpdateCursor();
        UpdateDraging();
        UpdateCameraMovment();
        
    }
    void UpdateCameraMovment()
    {
        //Handle screen dragging
        if (Input.GetMouseButton(2) || Input.GetMouseButton(1))
        {
            Vector3 diff = lastFramePosition - currFramePosition;
            Cam.transform.Translate(diff);
        }

        lastFramePosition = Cam.ScreenToWorldPoint(Input.mousePosition);

        Cam.orthographicSize -= Cam.orthographicSize * Input.GetAxis("Mouse ScrollWheel");

        Cam.orthographicSize = Mathf.Clamp(Cam.orthographicSize, 3f, 25f);
    }
/*
    void UpdateCursor()
    {
        Tile tileUnderMouse = WorldController.Instance.GetTileAtWorldCoord(currFramePosition);
        if (tileUnderMouse != null)
        {
            tileMarker.SetActive(true);
            Vector3 cursorPosition = new Vector3(tileUnderMouse.X, tileUnderMouse.Y, 0);
            tileMarker.transform.position = cursorPosition;
        }
        else
        {
            tileMarker.SetActive(false);
        }
    }
*/
    void UpdateDraging()
    {
        // if we are over an Ui-element bail out from this
        

        // start drag
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPosition = currFramePosition;
        }
        
        int start_x = Mathf.FloorToInt(dragStartPosition.x);
        int end_x = Mathf.FloorToInt(currFramePosition.x);
        if (end_x < start_x)
        {
            int tmp = end_x;
            end_x = start_x;
            start_x = tmp;

            if ((end_x - start_x) > dragSize - 1)
                start_x = end_x - dragSize + 1;
        }
        else
        {
            if ((end_x - start_x) > dragSize - 1)
                end_x = start_x + dragSize - 1;
        }

        int start_y = Mathf.FloorToInt(dragStartPosition.y);
        int end_y = Mathf.FloorToInt(currFramePosition.y);
        if (end_y < start_y)
        {
            int tmp = end_y;
            end_y = start_y;
            start_y = tmp;

            if ((end_y - start_y) > dragSize - 1)
                start_y = end_y - dragSize + 1;
        }
        else
        {
            if ((end_y - start_y) > dragSize - 1)
                end_y = start_y + dragSize - 1;
        }


        //Clean up old drag previews
        for (int i = 0; i < dragPreviewGo.Count; i++)
        {
            SimplePool.Despawn(dragPreviewGo[i]);
        }
        dragPreviewGo.Clear();
        

        if (Input.GetMouseButton(0))
        {
            for (int x = start_x; x <= end_x; x++)
            {
                for (int y = start_y; y <= end_y; y++)
                {
                    Tile t = WorldController.Instance.world.GetTileAt(x, y);
                    if (t != null)
                    {
                        // Display the building hint in top of this tile position
                        GameObject go = SimplePool.Spawn(tileMarkerPrefab, new Vector3(x, y, 0), Quaternion.identity);
                        go.transform.SetParent(this.transform, true);
                        dragPreviewGo.Add(go);
                    }
                }

            }
        }
        //end drag and place tiles
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonUp(0))
        { 
            for (int x = start_x; x <= end_x; x++)
            {
                for (int y = start_y; y <= end_y; y++)
                {
                    Tile t = WorldController.Instance.world.GetTileAt(x, y);
                    if (buildModeIsObjects && t != null)
                    {
                        // FixME: we´re assume walls
                        //WorldController.Instance.world.PlaceInstalledObject(buildModeObjectType, t);
                    }
                    else
                    {
                        // We are in Tile-changing mode.
                        t.Type = buildModeTile;
                    }
                }

            }

        }
    }

    public void SetMode_Floor()
    {
        buildModeIsObjects = false;
        buildModeTile = TileType.Floor;
    }

    public void SetMode_Bulldoze()
    {
        buildModeIsObjects = false;
        buildModeTile = TileType.Empty;
    }

    public void SetMode_BuildInstalledObject(string objectType)
    {
        buildModeIsObjects = true;
        buildModeObjectType = objectType;
        // wall is not a Tile! Wall is an "InstalledObject"
    }

}
