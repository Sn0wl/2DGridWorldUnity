using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

    public GameObject myCamera;

    public GameObject tileMarker;

    Camera Cam;

    Vector3 lastFramePosition;
    Vector3 currFramePosition;

    // Use this for initialization
    void Start () {
        Cam = myCamera.GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {

        currFramePosition = Cam.ScreenToWorldPoint(Input.mousePosition);;
        //update the tile Marker position
        currFramePosition.z = 0;
        tileMarker.transform.position = currFramePosition;

        //Handle screen dragging
        if ( Input.GetMouseButton(2) || Input.GetMouseButton(1))
        {
            Vector3 diff = lastFramePosition - currFramePosition;
            Cam.transform.Translate(diff);
        }

        lastFramePosition = Cam.ScreenToWorldPoint(Input.mousePosition);

    }
}
