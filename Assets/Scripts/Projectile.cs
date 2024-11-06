using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float maxRange = 70f;
    private float speed = 10f;
    private float damage = 1f;
    private Vector2 startPosition;
    private Vector2 direction;
    private bool targetSet;
    
    private void Update()
    {
        if (!targetSet) return;
        
        transform.position += (Vector3)direction * (speed * Time.deltaTime);
        
        if (Vector2.Distance(startPosition, transform.position) >= maxRange)
        {
            Destroy(gameObject);
        }
    }
    
    public void SetTarget(Vector2 targetPosition)
    {
        direction = (targetPosition - (Vector2)transform.position).normalized;
        targetSet = true;

        RotateTowardsDirection();
    }
    
    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    private void RotateTowardsDirection()
    {
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.TryGetComponent<Unit>(out var unit)) return;

        unit.TakeDamage(damage);
        Destroy(gameObject);
    }
}