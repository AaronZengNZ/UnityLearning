using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    Defender defender;
    GameObject defenderParent;
    const string DEFENDER_PARENT_NAME = "Defenders";

    private void Start()
    {
        CreateDefenderParent();
    }

    private void CreateDefenderParent()
    {
        defenderParent = GameObject.Find(DEFENDER_PARENT_NAME);
        if (!defenderParent)
        {
            defenderParent = new GameObject(DEFENDER_PARENT_NAME);
        }
    }

    private void OnMouseDown()
    {
        AttemptToPlaceDefenderAt(GetSquareClicked());
    }

    public void SetSelectedDefender(Defender defenderToSelect)
    {
        defender = defenderToSelect;
    }

    private void AttemptToPlaceDefenderAt(Vector2 gridPos)
    {
        var DcoinDisplay = FindObjectOfType<DcoinDisplay>();
        int defenderCost = defender.GetDcoinCost();
        if (DcoinDisplay.HaveEnoughDcoins(defenderCost))
        {
            SpawnDefender(gridPos);
            DcoinDisplay.SpendDcoins(defenderCost);
        }
        
    }

    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
            Vector2 gridPos = SnapToGrid(worldPos, true);
            return gridPos;
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos, bool isCheems)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);
        return new Vector2(newX, newY);
    }

    private void SpawnDefender(Vector2 position)
    {
        Defender newDefender = Instantiate(defender, position, Quaternion.identity) as Defender;
        newDefender.transform.parent = defenderParent.transform;
    }
}
