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
        [SerializeField] List<Shape> shapesPrefabs;
        [SerializeField] List<Shape> possibleShapes;
        [SerializeField] List<Shape> onScreen = new List<Shape>();

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

        private void AddToScreen(Shape shape)
        {
            onScreen.Add(shape);
            possibleShapes.Remove(shape);
            shape.transform.position = MeshRendererBounds();
            shape.gameObject.SetActive(true);
        }

        private void RemoveFromScreen(Shape shape)
        {
            possibleShapes.Add(shape);
            onScreen.Remove(shape);
            shape.gameObject.SetActive(false);

        }

        private Vector3 MeshRendererBounds()
        {
            Vector3 spawnLocation = new Vector3(Random.Range(m_MeshRenderer.bounds.min.x, m_MeshRenderer.bounds.max.x), Random.Range(m_MeshRenderer.bounds.min.y, m_MeshRenderer.bounds.max.y), 0);
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
        
    }

}