using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject dronePrefab;
    public float spawnInterval = 2f;
    public float spawnDistanceFromEdge = 2f; 

    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        InvokeRepeating(nameof(SpawnDrone), 1f, spawnInterval);
    }

    void SpawnDrone()
    {
        Vector2 spawnPos = GetRandomOffScreenPosition();
        Instantiate(dronePrefab, spawnPos, Quaternion.identity);
    }

    Vector2 GetRandomOffScreenPosition()
    {
       
        float camHeight = 2f * mainCam.orthographicSize;
        float camWidth = camHeight * mainCam.aspect;

        Vector2 center = mainCam.transform.position;

        
        int edge = Random.Range(0, 4);
        Vector2 pos = center;

        switch (edge)
        {
            case 0: 
                pos += new Vector2(Random.Range(-camWidth / 2f, camWidth / 2f), camHeight / 2f + spawnDistanceFromEdge);
                break;
            case 1:
                pos += new Vector2(Random.Range(-camWidth / 2f, camWidth / 2f), -camHeight / 2f - spawnDistanceFromEdge);
                break;
            case 2: 
                pos += new Vector2(-camWidth / 2f - spawnDistanceFromEdge, Random.Range(-camHeight / 2f, camHeight / 2f));
                break;
            case 3:
                pos += new Vector2(camWidth / 2f + spawnDistanceFromEdge, Random.Range(-camHeight / 2f, camHeight / 2f));
                break;
        }

        return pos;
    }
}