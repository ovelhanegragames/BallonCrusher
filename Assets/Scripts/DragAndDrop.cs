using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour
{

    Ray raio;
    public bool dragging;
    float distance;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            raio = Camera.main.ScreenPointToRay(Input.mousePosition);
            print(Input.mousePosition);
            Vector3 rayPoint = raio.GetPoint(distance);
            transform.position = new Vector3(rayPoint.x, rayPoint.y, transform.position.z);
        }
    }

    public void OnMouseDown()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
    }

    public void OnMouseUp()
    {
        dragging = false;
    }

}
