using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] float damage = 10f;
    [SerializeField] float impactForce = 100f;

    [Header("Effects")]
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private float impactEffectLifetime = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        

        // Try EnemyBehaviour component
        EnemyBehaviour enemy = collision.gameObject.GetComponent<EnemyBehaviour>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        // Apply impact force
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = collision.contacts[0].normal;
            rb.AddForce(-direction * impactForce, ForceMode.Impulse);
        }

        // Spawn impact effect
        if (impactEffect != null)
        {
            ContactPoint contact = collision.contacts[0];
            GameObject effect = Instantiate(impactEffect, contact.point, Quaternion.LookRotation(contact.normal));
            Destroy(effect, impactEffectLifetime);
        }

        // Destroy bullet
        Destroy(gameObject);
    }

    
}