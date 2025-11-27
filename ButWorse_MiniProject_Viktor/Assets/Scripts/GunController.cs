using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Gun Settings")]
    public float damage = 10f;
    [SerializeField] float range = 100f;
    [SerializeField] float fireRate = 0.5f; // Time between shots
    
    [Header("References")]
    [SerializeField] Camera fpsCam;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject impactEffect;
    
    // Track upgrade status
    public bool isUpgraded = false; 

    private float nextFireTime = 0f;

    void Start()
    {
        if (fpsCam == null)
        {
            fpsCam = Camera.main;
        }
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        RaycastHit hit;
        Vector3 rayOrigin = fpsCam.transform.position;
        Vector3 rayDirection = fpsCam.transform.forward;
        
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, range))
        {
            EnemyBehaviour enemy = hit.transform.GetComponentInParent<EnemyBehaviour>();    
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            if (impactEffect != null)
            {
                GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 2f);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (fpsCam != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * range);
        }
    }   
}