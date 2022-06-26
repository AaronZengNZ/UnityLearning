using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class duolengo : MonoBehaviour
{
    [SerializeField] private GameObject movePositionGameobject;

    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        navMeshAgent.destination = movePositionGameobject.transform.position;
    }
}
