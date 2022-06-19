using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower tower;
    [SerializeField] GameObject towerPrefab;
    [SerializeField] bool isPlaceable = false;
    bool perspectiveIsThirdPerson = false;

    public bool IsPlaceable { get { return isPlaceable; } }

    // Start is called before the first frame update
   void OnMouseDown()
   {
       if (isPlaceable && perspectiveIsThirdPerson)
       {
          bool isPlaced = tower.CreateTower(towerPrefab, transform.position);
           isPlaceable = !isPlaced;
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
