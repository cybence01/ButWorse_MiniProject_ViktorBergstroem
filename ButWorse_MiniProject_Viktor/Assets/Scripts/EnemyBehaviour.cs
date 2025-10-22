using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth = 100;
    [SerializeField] float moveSpeed = 5f;

    Rigidbody rb;
    Transform target;
    Vector3 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        target = GameObject.Find("Player").transform;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (target)
        {
            // Calculate direction (keep Y at same level for ground movement)
            Vector3 direction = (target.position - transform.position);
            direction.y = 0; // Keep enemy on ground level
            direction = direction.normalized;

            moveDirection = direction;

            // Rotate to face target
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
    }

    private void FixedUpdate()
    {
        if (target)
        {
            // Move in 3D space
            rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        Debug.Log($"Damage amount: {damageAmount}");
        currentHealth -= damageAmount;
        Debug.Log($"Health is now: {currentHealth}");

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}