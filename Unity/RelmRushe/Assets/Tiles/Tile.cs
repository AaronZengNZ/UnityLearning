using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower tower;
    [SerializeField] GameObject towerPrefab;

    [SerializeField] bool isPlaceable = false;
    public bool IsPlaceable { get { return isPlaceable; } }

    GridManager gridManager;
    Pathfinding pathfinder;
    Vector2Int coordinates = new Vector2Int();

    

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinding>();
    }

    void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    // Start is called before the first frame update
   void OnMouseDown()
   {
       if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates) )
       {
          bool isSuccessfull = tower.CreateTower(towerPrefab, transform.position);
          if (isSuccessfull) { 
                gridManager.BlockNode(coordinates);
                pathfinder.NotifyReceivers();
            }
       }
   }   

    void OnCollisionStay()
    {
        if (!Input.GetKeyDown(KeyCode.E)) { return; }
        if (isPlaceable)
        {
            bool isPlaced = tower.CreateTower(towerPrefab, transform.position);
            isPlaceable = !isPlaced;
        }
    }
}
