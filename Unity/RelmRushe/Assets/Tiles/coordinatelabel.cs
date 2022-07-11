using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class coordinatelabel : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.black;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        DisplayCoordinates();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }

        ColorCoordinates();
    }

    void ColorCoordinates()
    {
        if(gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);

        if(node == null) { return; }

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    void DisplayCoordinates()
    {
        if(gridManager == null) { return; }
       coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.WorldGridSize);
       coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.WorldGridSize);

       label.text = coordinates.x + "," + coordinates.y;

    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
