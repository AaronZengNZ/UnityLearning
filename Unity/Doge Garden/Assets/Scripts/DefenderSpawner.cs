using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    Defender defender;
    bool isCheemsSelected = false;

    private void OnMouseDown()
    {
        AttemptToPlaceDefenderAt(GetSquareClicked());
    }

    public void SetSelectedDefender(Defender defenderToSelect, bool isCheems)
    {
        defender = defenderToSelect;
        isCheemsSelected = isCheems;
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
        if (isCheemsSelected)
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
            Vector2 gridPos = SnapToGrid(worldPos, true);
            return gridPos;
        }
        else {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos) + new Vector3(2f, 1f, 0f);
            Vector2 gridPos = SnapToGrid(worldPos, false);
            return gridPos;
        }
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos, bool isCheems)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);
        if (isCheems)
        {
            return new Vector2(newX, newY);
        }
        return new Vector2(newX + 0.285f, newY - 0.2f);
    }

    private void SpawnDefender(Vector2 position)
    {
        Defender newDefender = Instantiate(defender, position, Quaternion.identity) as Defender;
    }
}
