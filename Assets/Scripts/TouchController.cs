using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Scripts 
{ 
    public class TouchController : MonoBehaviour
    {

        Vector3 offset;
        Vector3 startingPos;
        Quaternion startingRot;
        private bool currentlySelected;


        private void OnMouseDown()
        {
            Debug.Log("Mouse Down");
            //GameManager.frameCount = 0;
            //Vector3 touchShapePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //touchShapePos.z = 0;
            //offset = touchShapePos - transform.position;
            //offset.z = 0;

            currentlySelected = true;

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
            Debug.Log(GameManager.frameCount);
        }

    }
}