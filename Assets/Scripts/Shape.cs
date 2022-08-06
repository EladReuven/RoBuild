using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    [SerializeField] Transform topPoint;
    [SerializeField] Transform bottomPoint;
    [SerializeField] List<GameObject> sprites;
    private static int robotNum = 1;

    public Transform GetTopPoint()
    {
        return topPoint;
    }
    public Transform GetBottomPoint()
    {
        return bottomPoint;
    }

    //oh god what have we done... Combine AKA Update
    public Shape Combine(Shape top)
    {
        GameObject obj = new GameObject();
        obj.name = "Robot" + robotNum;
        robotNum++;
        obj.layer = 6;
        obj.transform.position = new Vector3(0,0.7f,0);
        Shape topShape = Instantiate(top, obj.transform).GetComponent<Shape>();
        topShape.gameObject.active = false;
        topShape.gameObject.transform.localScale = Vector3.one;
        Destroy(topShape.gameObject.GetComponent<Rigidbody2D>());
        Destroy(topShape.gameObject.GetComponent<TouchController>());
        Shape bottomShape = Instantiate(this.gameObject, obj.transform).GetComponent<Shape>();
        bottomShape.gameObject.SetActive(false);
        bottomShape.gameObject.transform.localScale = Vector3.one;
        Destroy(bottomShape.gameObject.GetComponent<Rigidbody2D>());
        Destroy(bottomShape.gameObject.GetComponent<TouchController>());
        bottomShape.gameObject.transform.position = obj.transform.position - bottomShape.topPoint.localPosition;
        topShape.gameObject.transform.position = obj.transform.position - topShape.bottomPoint.localPosition;
        Shape newShape = obj.AddComponent<Shape>();
        GameObject newTopPoint = new GameObject();
        newTopPoint.transform.SetParent(obj.transform);
        newTopPoint.transform.position = topShape.topPoint.position;
        GameObject newBottomPoint = new GameObject();
        newBottomPoint.transform.SetParent(obj.transform);
        newBottomPoint.transform.position = bottomShape.bottomPoint.position;
        newShape.sprites = new List<GameObject>();
        GameObject sprite1 = Instantiate(topShape.sprites[0]);
        sprite1.layer = 6;
        sprite1.GetComponent<SpriteRenderer>().sortingLayerName = "ROBOT";
        sprite1.transform.position = topShape.gameObject.transform.position;
        GameObject sprite2 = Instantiate(bottomShape.sprites[0]);
        sprite2.layer = 6;
        sprite2.GetComponent<SpriteRenderer>().sortingLayerName = "ROBOT";
        sprite2.transform.position = bottomShape.gameObject.transform.position;
        newShape.sprites.Add(sprite1);
        newShape.sprites.Add(sprite2);
        newShape.sprites[0].transform.SetParent(obj.transform);
        newShape.sprites[1].transform.SetParent(obj.transform);
        newShape.topPoint = newTopPoint.transform;
        newShape.bottomPoint = newBottomPoint.transform;
        GameManager.RemoveFromScreen(this);
        GameManager.RemoveFromScreen(top);
        Destroy(topShape.gameObject);
        Destroy(bottomShape.gameObject);
        //eyes
        GameObject eyes = Instantiate(Accessories.RandomEyes(),newShape.topPoint.transform);
        eyes.name = "Eyes";
        eyes.transform.position = newShape.topPoint.position;
        eyes.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "EYESANDLEGS";


        //legs
        GameObject legs = Instantiate(Accessories.RandomLegs(), newShape.bottomPoint.transform);
        legs.name = "Legs";
        legs.transform.position = newShape.bottomPoint.position;
        legs.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "EYESANDLEGS";


        obj.transform.position += Vector3.back;
        GameManager.completedRobots.Add(obj);
        obj.transform.localScale *= 1.5f;
        GameManager.currentRobot = obj;
        GameManager.startcoru = true;
        //GameManager.NoTouchyBlock.SetActive(true);
        //StartCoroutine(AdmireYourRobot(obj));
        return newShape;
    }

    [ContextMenu("Clone Shape in middle")]
    public void InstShape()
    {
        GameObject newShape = Instantiate(this.gameObject);
        newShape.transform.position = Vector3.zero;
    }
    
}
