using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class carve : KeyboardMover
{
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;
    [SerializeField] TileBase Tile1 = null;
    [SerializeField] TileBase Tile2 = null;
    private float timeBetweenSteps;

    // Start is called before the first frame update
   private TileBase TileOnPosition(Vector3 worldPosition) {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }
    IEnumerator Mover(Vector3 newPosition){
            yield return new WaitForSeconds(1/3F);
            transform.position = newPosition;
    }
    IEnumerator carv(Vector3Int cellPosition, TileBase Tile)
    {
        yield return new WaitForSeconds(1 / 3F);
        tilemap.SetTile(cellPosition, Tile);
    }

    void Update()  {
        Vector3 newPosition = NewPosition();
        TileBase tileOnNewPosition = TileOnPosition(newPosition);
        if (allowedTiles.Contain(tileOnNewPosition)) {
            transform.position = newPosition;
        } else if(tileOnNewPosition == Tile1 && Input.GetKey(KeyCode.X)){
            Vector3Int cellPosition = tilemap.WorldToCell(NewPosition());
            StartCoroutine(carv(cellPosition, Tile2));
            StartCoroutine(Mover(newPosition));
            }
        else {
            Debug.Log("You cannot walk on " + tileOnNewPosition + "!");
        }
    }
}
