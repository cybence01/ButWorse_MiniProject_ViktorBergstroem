using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Gun Settings")]
    public float damage = 25f;
    [SerializeField] float range = 100f;
    [SerializeField] float fireRate = 0.5f; // Time between shots
    
    [Header("References")]
    [SerializeField] Camera fpsCam;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject impactEffect;
    
    private float nextFireTime = 0f;

    void Start()
    {
        // If no camera assigned, use main camera
        if (fpsCam == null)
        {
            fpsCam = Camera.main;
        }
    }

    void Update()
    {
       // Fire on left mouseclick. Can be held down
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Debug.Log("Shoot() called!");

        // Play muzzle flash if assigned
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // Raycast from center of screen
        RaycastHit hit;
        Vector3 rayOrigin = fpsCam.transform.position;
        Vector3 rayDirection = fpsCam.transform.forward;
        
        Debug.Log($"Raycasting from {rayOrigin} in direction {rayDirection}");
        
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, range))
        {
            Debug.Log($"Hit: {hit.transform.name} at distance {hit.distance}");

            // Check if we hit an enemy
            EnemyBehaviour enemy = hit.transform.GetComponent<EnemyBehaviour>();
            if (enemy != null)
            {
                Debug.Log($"Found enemy component on {hit.transform.name}, dealing {damage} damage");
                enemy.TakeDamage(damage);
            }
            else
            {
                Debug.Log($"No enemy component found on {hit.transform.name}");
            }

            // Spawn impact effect at hit point
            if (impactEffect != null)
            {
                GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 2f);
            }
        }
        else
        {
            Debug.Log("Raycast hit nothing!");
        }
    }

    // Gizmo for the raycast
    void OnDrawGizmosSelected()
    {
        if (fpsCam != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * range);
        }
    }

    public void DoMoreDamage()
    {
        damage = +10f;
    }

   
}