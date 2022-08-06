using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{

    Vector3 offset;
    Vector3 startingPos;
    Quaternion startingRot;


    private void OnMouseDown()
    {
        Debug.Log("Mouse Down");
        Shape clickedShape = gameObject.GetComponent<Shape>();
        GameManager.lastSelected = clickedShape;
        if(GameManager.firstSelected == null)
        {
            GameManager.firstSelected = GameManager.lastSelected;
        }
        else if (GameManager.firstSelected != this.gameObject.GetComponent<Shape>())
        { 
            clickedShape.Combine(GameManager.firstSelected);
            Debug.Log("here");
            GameManager.firstSelected = null;
            Debug.Log(GameManager.firstSelected);
            Debug.Log(GameManager.lastSelected);
        }


    }

    private void OnMouseDrag()
    {
        //Debug.Log("Dragging");
        //Vector3 newShapePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //newShapePos.z = 0;
        //transform.position = newShapePos - offset;
    }

    private void OnMouseUp()
    {
        Debug.Log("Mouse Up");
        this.enabled = false;
        Debug.Log(GameManager.frameCount);
    }

}
