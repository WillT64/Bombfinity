using System.Collections.Generic;
using UnityEngine;

public class InfiniteGroundManager : MonoBehaviour
{
    public GameObject groundSegmentPrefab; // Prefab du segment de sol
    public float segmentWidth = 1.0f; // La largeur d'un seul segment de sol
    public float segmentHeight = 1.0f; // La hauteur d'un seul segment de sol
    public Transform playerTransform; // La position du joueur
    
    private Queue<GameObject> activeHorizontalSegments = new Queue<GameObject>();
    private Queue<GameObject> activeVerticalSegments = new Queue<GameObject>();
    private Vector3 nextHorizontalSegmentPosition = Vector3.zero;
    private Vector3 nextVerticalSegmentPosition;

    // Start is called before the first frame update
void Start()
{
    // Récupère la hauteur et la largeur de la caméra
    float cameraHeight = Camera.main.orthographicSize * 2.0f;
    float cameraWidth = cameraHeight * Camera.main.aspect;
    
    // Calcule la position de départ du sol
    float groundStartY = -cameraHeight / 2 + (cameraHeight * 0.2f);
    nextHorizontalSegmentPosition.y = groundStartY;
    
    InitializeGround();
}


    private void InitializeGround()
    {
        // Instancier les premiers segments de sol
        for (int i = 0; i < 5; i++)
        {
            AddHorizontalSegment();
        }
        nextVerticalSegmentPosition = nextHorizontalSegmentPosition - new Vector3(0, segmentHeight, 0);
    }

    private void AddHorizontalSegment()
    {
        GameObject segment = Instantiate(groundSegmentPrefab, nextHorizontalSegmentPosition, Quaternion.identity);
        activeHorizontalSegments.Enqueue(segment);
        nextHorizontalSegmentPosition.x += segmentWidth;
    }

    private void AddVerticalSegment()
    {
        GameObject segment = Instantiate(groundSegmentPrefab, nextVerticalSegmentPosition, Quaternion.identity);
        activeVerticalSegments.Enqueue(segment);
        nextVerticalSegmentPosition.y -= segmentHeight;
    }

    private void RemoveOldestHorizontalSegment()
    {
        GameObject oldestSegment = activeHorizontalSegments.Dequeue();
        Destroy(oldestSegment);
    }

    // Update is called once per frame
    void Update()
    {
        // Ajouter des segments horizontaux lorsque le joueur avance
        if (playerTransform.position.x > nextHorizontalSegmentPosition.x - 5)
        {
            AddHorizontalSegment();
            RemoveOldestHorizontalSegment();
        }

        // Ajouter des segments verticaux lorsque le joueur descend
        if (playerTransform.position.y < nextVerticalSegmentPosition.y + 5)
        {
            AddVerticalSegment();
        }
    }
}
