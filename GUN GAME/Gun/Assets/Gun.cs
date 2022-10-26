using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string name = "gun";
    public float shootSpeed = 1f;
    public float damage = 10f;
    public float shots = 1f;
    public float recoil = 1f;
    public float dps = 100f;
    public string rank = "common";

    public float burst = 1f;
    public float time = 0.2f;

    public string type;

    public GameObject holdPoint;

    public ParticleSystem gunShoot;
    public bool triShot = false;
    public ParticleSystem gunShoot2;
    public ParticleSystem gunShoot3;
}
