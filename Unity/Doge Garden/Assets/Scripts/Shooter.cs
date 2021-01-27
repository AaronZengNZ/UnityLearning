using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectile, gun;
    
    private void Start()
    {
        SetLaneSpawner();
    }

    private void Update()
    {
        if (IsAttackerInLane())
        {
            //do something
        }
        else
        {
            //bye bye, me sleep
        }
    }

    private void SetLaneSpawner()
    {
        AttackerSpawner[] spawners = FindObjectsOfType<AttackerSpawner>();

        foreach (AttackerSpawner spawner in spawners)
        {

        }
    }

    public void Fire()
    {
        Instantiate(projectile, gun.transform.position, Quaternion.AngleAxis(180, Vector3.up));
    }


}
