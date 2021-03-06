using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed = 2f;
    [SerializeField] float damage = 50f;
    [SerializeField] GameObject projectileBoomVFX;

    void Update ()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        var health = otherCollider.GetComponent<Health>();
        var attacker = otherCollider.GetComponent<Attacker>();

        if (attacker && health)
        {
            health.DealDamage(damage);
            TriggerBoomVFX();
            Destroy(gameObject);
        }
    }

    private void TriggerBoomVFX()
    {
        if (!projectileBoomVFX) { return; }
        GameObject boomVFXObject = Instantiate(projectileBoomVFX, transform.position, transform.rotation);
        Destroy(boomVFXObject, 1f);
    }
}
