using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject spikeTilePrefab;
    public GameObject[] movingTiles;

    public float tileSpawnTimer = 2f;
    private float currentTileSpawnTimer;

    private int tileSpawnCount;

    public float minX = -2f, maxX = 2f;


    // Start is called before the first frame update
    void Start()
    {
        currentTileSpawnTimer = tileSpawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTiles();
    }
    void SpawnTiles()
    {
        currentTileSpawnTimer += Time.deltaTime;
        if (currentTileSpawnTimer >= tileSpawnTimer)
        {
            tileSpawnCount++;
            Vector3 temp = transform.position;
            temp.x = Random.Range(minX, maxX);

            GameObject newTile = null;

            if (tileSpawnCount < 2)
            {
                newTile = Instantiate(tilePrefab, temp, Quaternion.identity);

            }
            else if (tileSpawnCount == 2)
            {
                if (Random.Range(0, 2) > 0)
                {
                    newTile = Instantiate(tilePrefab, temp, Quaternion.identity);
                }
                else
                {
                    newTile = Instantiate(movingTiles[Random.Range(0, movingTiles.Length)], temp, Quaternion.identity);
                }
            }
            else if (tileSpawnCount == 3)
            {
                if (Random.Range(0, 2) > 0)
                {
                    newTile = Instantiate(tilePrefab, temp, Quaternion.identity);
                }
                else
                {
                    newTile = Instantiate(spikeTilePrefab, temp, Quaternion.identity);
                }
                tileSpawnCount = 0;
            }
            if (newTile)
            newTile.transform.parent = transform;

            currentTileSpawnTimer = 0;
        }
    }
}
