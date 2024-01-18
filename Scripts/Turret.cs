using UnityEngine;

public class Turret : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]
    public float range = 15f;

    [Header("Default Bullets")]
    public GameObject bulletPrefab;
    public float fireRate = 200f;
    private float fireCountdown = 0f;
    private int damage;

    

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turnSpeed = 20f;

    
    public Transform firePoint;


    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }



    void UpdateTarget ()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject neareastEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                neareastEnemy = enemy;
            }

        }

        if (neareastEnemy != null && shortestDistance <= range)
        {
            target = neareastEnemy.transform;
            targetEnemy = neareastEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null; 
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            return;
        }

        Enemy enemy = target.GetComponent<Enemy>();

        if (enemy != null && enemy.isDead)
        {
            // The target is dead, so reset the target to null
            target = null;
            return;
        }

        LockOnTarget();


        
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
        

        
    }

    public void IncreaseFireRate(float amount)
    {
        
        fireRate += amount;
       
    }

    public void IncreaseRange(float amount)
    {

        range += amount;
        
    }

    public void IncreaseDamage(int amount)
    {

        damage += amount;
       
    }

    void LockOnTarget()
    {
        //Target lock
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Seek(target);
            bullet.damage += damage;
        }
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        
    }
}
