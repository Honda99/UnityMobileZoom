using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour
{
    float minZoom = 1f;
    float maxZoom = 10f;
    float sensitivity = 0.01f;
    Vector3 touchStartPt;
    Vector3 pinchAveragePos;
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
            if (firstTouch.phase == TouchPhase.Began && secondTouch.phase==TouchPhase.Began)
            {
                Vector3 camPos = Camera.main.transform.position;
                 pinchAveragePos = Camera.main.ScreenToWorldPoint(0.5f * (firstTouchPos + secondTouchPos));
                Camera.main.transform.position = new Vector3(pinchAveragePos.x, pinchAveragePos.y, camPos.z);
            }
        
            float firstMagnitude =((secondTouchPos-secondDelta)-(firstTouchPos-firstDelta)).magnitude ;
            float changedMagnitude = (secondTouchPos - firstTouchPos).magnitude;
            float zoomMagnitude = changedMagnitude - firstMagnitude;
            float zoomDimension = Camera.main.orthographicSize-zoomMagnitude*sensitivity;
            
             Camera.main.orthographicSize= Mathf.Clamp(zoomDimension, minZoom, maxZoom);
           
           
        }
    }
}
