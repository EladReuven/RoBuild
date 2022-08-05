using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static int frameCount = 0;
        public static bool selection = false;

        [SerializeField] SpriteRenderer m_MeshRenderer;
        [SerializeField] List<GameObject> shapesPrefabs;
        [SerializeField] List<GameObject> possibleShapes;
        [SerializeField] List<GameObject> onScreen = new List<GameObject>();

        public int shapesPerScreen;

        private void Awake()
        {
            InitShapes();
        }

        private void Start()
        {
            Debug.Log(possibleShapes.Count);
            SpawnShapes();
        }

        private void FixedUpdate()
        {
            frameCount++;
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

        private void AddToScreen(GameObject shape)
        {
            onScreen.Add(shape);
            possibleShapes.Remove(shape);
            shape.transform.position = MeshRendererBounds();
            shape.SetActive(true);
        }

        private void RemoveFromScreen(GameObject shape)
        {
            possibleShapes.Add(shape);
            onScreen.Remove(shape);
            shape.SetActive(false);

        }

        private Vector3 MeshRendererBounds()
        {
            Vector3 spawnLocation = new Vector3(Random.Range(m_MeshRenderer.bounds.min.x, m_MeshRenderer.bounds.max.x), Random.Range(m_MeshRenderer.bounds.min.y, m_MeshRenderer.bounds.max.y), 0);
            return spawnLocation;
        }

        private void InitShapes()
        {
            possibleShapes = new List<GameObject>();
            for (int i = 0; i < shapesPrefabs.Count; i++)
            {
                GameObject newShape = Instantiate(shapesPrefabs[i]);
                newShape.SetActive(false);
                possibleShapes.Add(newShape);
            }
        }
        
    }

}