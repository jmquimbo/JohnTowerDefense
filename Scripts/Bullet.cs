using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 200f;
    public float explosionRadius = 0f;
    public GameObject impactEffect;
    public int damage = 50;

    [Header("Slow")]
    public bool useSlow = false;
    public float dotDamage = 5f; // DoT damage per second
    public float dotDuration = 5f; // Duration of the DoT effect
    public float slowPct = .3f;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        transform.LookAt(target);
    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);

        if (explosionRadius > 0)
        {

             Explode();
            
        }
        else
        {
            Damage(target);
        }

        
        
        Destroy(gameObject);
        //WaveSpawner.EnemiesAlive--;
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                if (useSlow)
                {
                    slowDamage(collider.transform);
                }
                else
                {
                    Damage(collider.transform);
                }
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            if (e.isDead == false)
            {
                e.TakeDamage(damage);
            }
        }

        //WaveSpawner.EnemiesAlive--;
    }

    void slowDamage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            if (e.isDead == false)
            {
                e.TakeDamage(damage);

                e.DamageOverTime(dotDamage, dotDuration, 1f, slowPct);
            }
        }

        //WaveSpawner.EnemiesAlive--;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
