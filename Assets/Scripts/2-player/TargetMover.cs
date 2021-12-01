using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component moves its object towards a given target position.
 */
public class TargetMover: MonoBehaviour {
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;

    [Tooltip("The speed by which the object moves towards the target, in meters (=grid units) per second")]
    [SerializeField] float speed = 2f;

    [Tooltip("Maximum number of iterations before BFS algorithm gives up on finding a path")]
    [SerializeField] int maxIterations = 1000;

    [Tooltip("The target position in world coordinates")]
    [SerializeField] Vector3 targetInWorld;

    [Tooltip("The target position in grid coordinates")]
    [SerializeField] Vector3Int targetInGrid;

    protected bool atTarget;  // This property is set to "true" whenever the object has already found the target.

    public void SetTarget(Vector3 newTarget) {
        if (targetInWorld != newTarget) {
            targetInWorld = newTarget;
            targetInGrid = tilemap.WorldToCell(targetInWorld);
            atTarget = false;
        }
    }

    public Vector3 GetTarget() {
        return targetInWorld;
    }

    private TilemapGraph tilemapGraph = null;
    private float timeBetweenSteps;
    private TileBase tileOnNewPosition;

    protected virtual void Start() {
        tilemapGraph = new TilemapGraph(tilemap, allowedTiles.Get());
        timeBetweenSteps = 1 / speed;
        StartCoroutine(MoveTowardsTheTarget());
    }

    private TileBase TileOnPosition(Vector3 worldPosition) {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }
      public void movespeed(){
        tileOnNewPosition = TileOnPosition(transform.position);
        if(tileOnNewPosition == allowedTiles.GetIl(0)) {
            timeBetweenSteps = 1 / speed * 3f;
             Debug.Log("You cannot walk on " + tileOnNewPosition + "!");
        }
        else if(tileOnNewPosition == allowedTiles.GetIl(2)){
            Debug.Log("You cannot walk on " + tileOnNewPosition + "!");
            timeBetweenSteps = 1 / speed * 3f;
            }
        else timeBetweenSteps = 1 / speed;
     }

    IEnumerator MoveTowardsTheTarget() {
        for(;;) {
            yield return new WaitForSeconds(timeBetweenSteps);
            if (enabled && !atTarget)
                MakeOneStepTowardsTheTarget();
        }
    }

    private void MakeOneStepTowardsTheTarget() {
        Vector3Int startNode = tilemap.WorldToCell(transform.position);
        Vector3Int endNode = targetInGrid;
        List<Vector3Int> shortestPath = BFS.GetPath(tilemapGraph, startNode, endNode, maxIterations);
        Debug.Log("shortestPath = " + string.Join(" , ",shortestPath));
        if (shortestPath.Count >= 2) {
            Vector3Int nextNode = shortestPath[1];
            transform.position = tilemap.GetCellCenterWorld(nextNode);
        } else {
            atTarget = true;
        }
    }
}
