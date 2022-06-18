using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    ParticleSystem particlesModule;
    public int gunDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        particlesModule = GetComponent<ParticleSystem>();
        ToggleGun(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ToggleGun(true);
        }
        else
        {
            ToggleGun(false);
        }


    }

    void ToggleGun(bool isUsing)
    {
            var emissionModule = particlesModule.emission;
            emissionModule.enabled = isUsing;
    }

}
