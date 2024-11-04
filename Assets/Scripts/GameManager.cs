using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int frameCount = 0;
    public static bool selection = false;
    public static Shape firstSelected;
    public static Shape lastSelected;

    [SerializeField] GameObject BLOCKBLOCK;
    public static GameObject NoTouchyBlock;

    [SerializeField] SpriteRenderer targetSpawnBoard;
    [SerializeField] List<Shape> shapesPrefabs;
    [SerializeField] static List<Shape> possibleShapes;
    [SerializeField] static List<Shape> onScreen = new List<Shape>();
    [SerializeField] List<SpriteRenderer> outerSpawnLocations;
    [SerializeField] GameObject checkmarkSymbol;
    public static GameObject checkmark;

    public static List<GameObject> completedRobots = new List<GameObject>();
    public GameObject completedLocation;

    public int shapesPerScreen;
    public float screenBounds = 2f;
    public static bool startcoru = false;
    public static GameObject currentRobot;

    


    private void Awake()
    {
        InitShapes();
        NoTouchyBlock = BLOCKBLOCK;
        NoTouchyBlock.SetActive(false);
        checkmark = checkmarkSymbol;
    }

    private void Start()
    {
        Debug.Log(possibleShapes.Count);
        SpawnShapes(shapesPerScreen);
    }

    private void Update()
    {
        if(startcoru)
        {
            StartCoroutine(AdmireYourRobot(currentRobot));
            startcoru = false;
        }

        if(onScreen.Count <= 1)
        {
            SpawnShapes(shapesPerScreen - onScreen.Count);
        }
    }


    private void SpawnShapes(int amountOfShapes)
    {
        int shapeIndex;
        for (int i = 0; i < amountOfShapes; i++)
        {
            
            while(true)
            {
                shapeIndex = Random.Range(0, possibleShapes.Count);
                if (possibleShapes[shapeIndex] != null)
                {
                    AddToScreen(possibleShapes[shapeIndex]);
                    break;
                }
            }

        }
    }

    private void AddToScreen(Shape shape)
    {
        onScreen.Add(shape);
        possibleShapes.Remove(shape);
        StartCoroutine(LerpShapeIn(shape));
        shape.gameObject.SetActive(true);
    }

    public static void RemoveFromScreen(Shape shape)
    {
        possibleShapes.Add(shape);
        onScreen.Remove(shape);
        shape.gameObject.SetActive(false);

    }

    private Vector3 MeshRendererBounds()
    {
        Vector3 spawnLocation = new Vector3(Random.Range(targetSpawnBoard.bounds.min.x + screenBounds, targetSpawnBoard.bounds.max.x - screenBounds), Random.Range(targetSpawnBoard.bounds.min.y + screenBounds, targetSpawnBoard.bounds.max.y - screenBounds), 0);
        return spawnLocation;
    }

    private Vector3 OuterAreaLocation()
    {
        int randOuterArea = Random.Range(0, outerSpawnLocations.Count);
        Vector3 spawnLocation = new Vector3(Random.Range(outerSpawnLocations[randOuterArea].bounds.min.x, outerSpawnLocations[randOuterArea].bounds.max.x), Random.Range(outerSpawnLocations[randOuterArea].bounds.min.y, outerSpawnLocations[randOuterArea].bounds.max.y), 0);
        return spawnLocation;
    }


    private void InitShapes()
    {
        possibleShapes = new List<Shape>();
        for (int i = 0; i < shapesPrefabs.Count; i++)
        {
            Shape newShape = Instantiate(shapesPrefabs[i]);
            newShape.gameObject.SetActive(false);
            possibleShapes.Add(newShape);
        }
    }
    private IEnumerator AdmireYourRobot(GameObject robot)
    {
        GameManager.NoTouchyBlock.SetActive(true);
        yield return new WaitForSeconds(5f);
        GameManager.NoTouchyBlock.SetActive(false);
        robot.SetActive(false);
    }

    private IEnumerator LerpShapeIn(Shape shape)
    {
        Vector3 targetLocation = MeshRendererBounds();
        BoxCollider2D shapeCollider = shape.gameObject.GetComponentInChildren<BoxCollider2D>();
        float sec = 1f;

        shape.transform.position = OuterAreaLocation();
        shapeCollider.enabled = false;

        while(sec >= 0.1f)
        {
            shape.transform.position = Vector3.Lerp(shape.transform.position, targetLocation, 0.05f);
            yield return new WaitForSeconds(0.01f);
            //yield return new WaitForEndOfFrame();
            Debug.Log(sec);
            sec -= Time.deltaTime;
        }

        shapeCollider.enabled = true;
    }

    //[ContextMenu("instantiate robots on the side")]
    //public void InstCreatedRobots()
    //{
        
    //    float offset = 0;
    //    foreach(var robot in completedRobots)
    //    {
    //        GameObject listRobot = Instantiate(robot, completedLocation.transform.position + (new Vector3(0,offset,0)), robot.transform.rotation, completedLocation.transform);
    //        listRobot.transform.localScale *= 0.2f;
    //        listRobot.SetActive(true);
    //        offset -= 1.2f;
    //    }
    //}

}