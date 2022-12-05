using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update

    public LineRenderer lineRenderer;

    void Start()
    {
        wayPoints = new List<Transform>();
    }

    public string dragState = "idle"; // in drag
    List<Transform> wayPoints;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && dragState == "idle")
        {
            wayPoints.Clear();
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                dragState = "indrag";
                Transform objectHit = hit.transform;
                wayPoints.Add(objectHit);
                // Do something with the object that was hit by the raycast.
            }
        }
        if (dragState == "indrag")
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                if (wayPoints[wayPoints.Count - 1] != objectHit)
                {
                    wayPoints.Add(objectHit);
                }
                // Do something with the object that was hit by the raycast.
            }
        }
        if (Input.GetMouseButtonUp(0) && dragState == "indrag")
        {
            dragState = "idle";
        }
        lineRenderer.positionCount = wayPoints.Count;
        lineRenderer.SetPositions(wayPoints.Select(t=>t.position + Vector3.up * 1.2f).ToArray());        
    }
}
