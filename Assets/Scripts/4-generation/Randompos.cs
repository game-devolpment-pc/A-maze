using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//using System;
//using Random = UnityEngine.Random;

public class Randompos : MonoBehaviour
{
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;

    private Random random= new Random();

    float x;
    float y;
    private void RandomePose()
    {
        this.x = (float)Random.Range(5, 95);
        this.y = (float)Random.Range(5, 95);


    }
    private TileBase TileOnPosition(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }
    IEnumerator Mover()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
            RandomePose();
            //Debug.Log(x);
            //Debug.Log(y);
            Vector3 pos = new Vector3(x, y, 0);
            TileBase Tile = TileOnPosition(pos);
            if (allowedTiles.Contain(Tile))
            {
                Vector3Int cellPosition = tilemap.WorldToCell(pos);
                Debug.Log(cellPosition);
                transform.position = cellPosition;
                break;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Mover());
    }

    // Update is called once per frame
    void Update()
    {

    }
}