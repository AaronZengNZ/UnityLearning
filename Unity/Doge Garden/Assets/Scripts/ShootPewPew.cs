using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPewPew : MonoBehaviour
{
    [SerializeField] GameObject projectile, gun;

    public void Fire()
    {
        GameObject newProjectile = Instantiate(projectile, gun.transform.position, Quaternion.AngleAxis(180, Vector3.up)) as GameObject;
        GetComponent<Shooter>().ProjectileTransferChild(newProjectile);
    }
}
