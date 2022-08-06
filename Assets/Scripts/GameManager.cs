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

    [SerializeField] SpriteRenderer m_MeshRenderer;
    [SerializeField] List<Shape> shapesPrefabs;
    [SerializeField] static List<Shape> possibleShapes;
    [SerializeField] static List<Shape> onScreen = new List<Shape>();

    public static List<GameObject> completedRobots = new List<GameObject>();
    public GameObject completedLocation;

    public int shapesPerScreen;
    public int screenBounds = 2;
    public static bool startcoru = false;
    public static GameObject currentRobot;


    private void Awake()
    {
        InitShapes();
        NoTouchyBlock = BLOCKBLOCK;
        NoTouchyBlock.SetActive(false);
    }

    private void Start()
    {
        Debug.Log(possibleShapes.Count);
        SpawnShapes();
    }

    private void Update()
    {
        if(startcoru)
        {
            StartCoroutine(AdmireYourRobot(currentRobot));
            startcoru = false;
        }
    }


    private void SpawnShapes()
    {
        int rand;
        for (int i = 0; i < shapesPerScreen; i++)
        {
            rand = Random.Range(0, possibleShapes.Count);
            AddToScreen(possibleShapes[rand]);
        }
    }

    private void AddToScreen(Shape shape)
    {
        onScreen.Add(shape);
        possibleShapes.Remove(shape);
        shape.transform.position = MeshRendererBounds();
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
        Vector3 spawnLocation = new Vector3(Random.Range(m_MeshRenderer.bounds.min.x + screenBounds, m_MeshRenderer.bounds.max.x - screenBounds), Random.Range(m_MeshRenderer.bounds.min.y + screenBounds, m_MeshRenderer.bounds.max.y - screenBounds), 0);
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