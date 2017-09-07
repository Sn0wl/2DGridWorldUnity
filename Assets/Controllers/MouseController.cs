using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

    public GameObject myCamera;

    public GameObject tileMarker;

    Tile tileDragStart;

    Camera Cam;

    Vector3 dragStartPosition;
    Vector3 lastFramePosition;
    Vector3 currFramePosition;

    // Use this for initialization
    void Start () {
        Cam = myCamera.GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {

        currFramePosition = Cam.ScreenToWorldPoint(Input.mousePosition);;

        //Handle screen dragging
        if ( Input.GetMouseButton(2) || Input.GetMouseButton(1))
        {
            Vector3 diff = lastFramePosition - currFramePosition;
            Cam.transform.Translate(diff);
        }

        lastFramePosition = Cam.ScreenToWorldPoint(Input.mousePosition);

        //update the tile Marker position
        Tile tileUnderMouse = GetTileAtWorldCoord(currFramePosition);
        if(tileUnderMouse != null)
        {
            tileMarker.SetActive(true);
            Vector3 cursorPosition = new Vector3(tileUnderMouse.X, tileUnderMouse.Y, 0);
            tileMarker.transform.position = cursorPosition;
        }
        else
        {
            tileMarker.SetActive(false);
        }

        //Handle left  mouse clicks
        // start drag
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPosition = currFramePosition;
        }
        //end drag

        if(Input.GetMouseButtonUp(0))
        {
            int start_x = Mathf.FloorToInt(dragStartPosition.x);
            int end_x = Mathf.FloorToInt(currFramePosition.x);
            if (end_x < start_x)
            {
                int tmp = end_x;
                end_x = start_x;
                start_x = tmp;
            }
            int start_y = Mathf.FloorToInt(dragStartPosition.y);
            int end_y = Mathf.FloorToInt(currFramePosition.y);
            if(end_y < start_y)
            {
                int tmp = end_y;
                end_y = start_y;
                start_y = tmp;
            }
            for (int x = start_x; x <= end_x; x++)
            {
                for (int y = start_y; y <= end_y; y++)
                {
                    Tile t = WorldController.Instance.world.GetTileAt(x, y);
                    if(t != null)
                    {
                        t.Type = Tile.TileType.Floor;
                    }
                }

            }

        }
    }

    Tile GetTileAtWorldCoord(Vector3 coord)
    {
        int x = Mathf.FloorToInt(coord.x);
        int y = Mathf.FloorToInt(coord.y);

        return WorldController.Instance.world.GetTileAt(x, y);
    }
}
