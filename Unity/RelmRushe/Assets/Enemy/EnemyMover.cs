using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    
    [SerializeField] [Range(0f, 15f)] float Speed = 0.4f;

    Enemy enemy;
    GridManager gridManager;
    Pathfinding pathfinder;

    List<Node> path = new List<Node>();
    // Start is called before the first frame update
    void OnEnable()
    {
        RecalculatePath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    void Awake()
    {
        enemy = this.GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinding>();
    }
        
    void ReturnToStart()
    {

            transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);

    }

    void RecalculatePath()
    {
        path.Clear();
        path = pathfinder.GetNewPath();
        Debug.Log("recalculated path");
    }

    void FinishPath()
    {

        enemy.StealGold();

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    IEnumerator FollowPath()
    {
        for(int i = 0; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = new Vector3(gridManager.GetPositionFromCoordinates(path[i].coordinates).x, gridManager.GetPositionFromCoordinates(path[i].coordinates).z + 5, gridManager.GetPositionFromCoordinates(path[i].coordinates).y);

            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f) {
                travelPercent += Time.deltaTime * Speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
           }
        }
        FinishPath();
    }

    //void OnMouseDown()
   // {
    //    Destroy(this.gameObject);
    //}
}
