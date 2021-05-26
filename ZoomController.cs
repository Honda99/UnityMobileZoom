using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour
{
    float minZoom = 1f;
    float maxZoom = 10f;
    float sensitivity = 0.01f;
    Vector3 touchStartPt;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch singleTouch = Input.GetTouch(0);
         
            if (singleTouch.phase == TouchPhase.Began)
            {
                touchStartPt = Camera.main.ScreenToWorldPoint(singleTouch.position);
            }
            if (singleTouch.phase == TouchPhase.Moved)
            {
                Vector3 touchDirection= touchStartPt-Camera.main.ScreenToWorldPoint(singleTouch.position);
                Camera.main.transform.position +=touchDirection;
            }
        }
        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);
            Vector3 firstTouchPos = firstTouch.position;
            Vector3 secondTouchPos = secondTouch.position;
            Vector3 firstDelta = firstTouch.deltaPosition;
            Vector3 secondDelta = secondTouch.deltaPosition;
            float firstMagnitude =((secondTouchPos-secondDelta)-(firstTouchPos-firstDelta)).magnitude ;
            float changedMagnitude = (secondTouchPos - firstTouchPos).magnitude;
            float zoomMagnitude = changedMagnitude - firstMagnitude;
            float zoomDimension = Camera.main.orthographicSize-zoomMagnitude*sensitivity;
             Camera.main.orthographicSize= Mathf.Clamp(zoomDimension, minZoom, maxZoom);
        }
    }
}
