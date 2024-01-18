using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed;
    public float speed;

    private Transform target;
    private int wavepointIndex = 0;
    private Animator anim;
    private CharacterController controller;

    public float startHealth;
    public float health;  

    private int worth = 20;
    private float destroyDelay = 5f;
    public bool isDead = false;


    private float dotDamage = 0f; // Damage over time value
    private float dotDuration = 0f; // Duration of the DoT effect
    private float dotTickRate = 1f; // Rate at which DoT damage is applied (e.g., per second)
    private float dotTimer = 0f; // Timer to track DoT duration

    [Header("Unity Stuff")]
    public Image healthBar;
    public Image BackgroundhealthBar;


    public int pathChoice;
    public Transform[] currentPath;

    private bool speedIncreased = false;
    public bool isBoss = false;

    void Start()
    {
        //target = Waypoints.points[0];

        pathChoice = Random.Range(1, 3);

        if (pathChoice == 1)
            currentPath = Waypoints.path1;
        else
            currentPath = Waypoints.path2;

        target = currentPath[0];

        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        speed = startSpeed;
        startHealth = WaveSpawner.enemyHP;
        health = startHealth;

        if (WaveSpawner.waveIndex == 6)
        {
            
            isBoss =true;
        }
    }

    public void TakeDamage(float amount)
    {
        
        health -= amount;
        healthBar.fillAmount = health / startHealth;



        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.Money += worth;

        if (isBoss)
        {
            WaveSpawner.BossAlive--;
        }
        else
        {
            WaveSpawner.EnemiesAlive--;
        }
        
        AnimationClip deathAnimation = null;
        foreach (AnimationClip clip in anim.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "death1") 
            {
                anim.SetInteger("moving", 14);
                deathAnimation = clip;
                break;
            }

            if (clip.name == "die1") 
            {
                anim.SetInteger("moving", 12);
                deathAnimation = clip;
                break;
            }

            if (clip.name == "death_1")
            {
                anim.SetInteger("moving", 14);
                deathAnimation = clip;
                break;
            }
        }

        if (deathAnimation != null)
        {
            StartCoroutine(DestroyAfterDelay(deathAnimation.length));
        }
        else
        {
            StartCoroutine(DestroyAfterDelay(destroyDelay));
        }

        
        isDead = true;

    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(destroyDelay);

        // Destroy the object
        Destroy(gameObject);
    }

    void Update()
    {
        
        if (!isDead)
        {
            Vector3 dir = target.position - transform.position;
           

            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            anim.SetInteger("battle", 1);
            anim.SetInteger("moving", 2);//run

            if (Vector3.Distance(transform.position, target.position) <= 0.2f)
            {
                GetNextWaypoint();
            }


           
            if (dotDamage > 0)
            {
                
                dotTimer += Time.deltaTime;
                
                if (dotTimer >= dotTickRate)
                {
                    health -= dotDamage;
                    dotTimer = 0f;
                }

                if (dotDuration > 0)
                {
                    dotDuration -= Time.deltaTime; 
                    if (dotDuration <= 0)
                    {
                        
                        dotDamage = 0f;
                        speed = startSpeed;
                    }
                }

                if (health <= 0)
                {
                    Die();
                    
                }
            }
        }
        else
        {
            
            anim.SetInteger("battle", 1);
            anim.SetInteger("moving", 2);//run
        }
    }

    public void DamageOverTime(float damage, float duration, float tickRate, float slowPct)
    {
        dotDamage = damage;
        dotDuration = duration;
        dotTickRate = tickRate;
        dotTimer = 0f;
        speed = startSpeed * (1f - slowPct);
    }

    public void IncreaseSpeed(float multiplier)
    {
        if (!speedIncreased)
        {
            speed = startSpeed;
            speed += multiplier;
            speedIncreased = true;

        }
    }

    public void ResetSpeed()
    {
        if (speedIncreased)
        {
            speed = startSpeed;
            speedIncreased = false;

            // Apply the original speed to the enemy's movement logic
            // For example, if you're using Unity's NavMeshAgent, you would update the speed like this:
            // navMeshAgent.speed = baseSpeed;
        }
    }


    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.path1.Length - 1)
        {
            EndPath();
            return;
        }


        if (pathChoice == 1) // For the first path
        {
            wavepointIndex++;
            target = Waypoints.path1[wavepointIndex];

            switch (wavepointIndex)
            {
                case 1:
                    transform.Rotate(0, 90, 0);
                    break;
                case 2:
                    transform.Rotate(0, -90, 0);
                    break;
                case 3:
                    transform.Rotate(0, -90, 0);
                    break;
                case 4:
                    transform.Rotate(0, 90, 0);
                    break;
                case 5:
                    transform.Rotate(0, -90, 0);
                    break;
                default:
                    transform.Rotate(0, 0, 0);
                    break;
            }

        }
        else if (pathChoice == 2) // For the second path
        {
            wavepointIndex++;
            target = Waypoints.path2[wavepointIndex];
            
                switch (wavepointIndex)
                {
                    case 1:
                        transform.Rotate(0, -90, 0);
                        break;
                    case 2:
                        transform.Rotate(0, 90, 0);
                        break;
                    case 3:
                        transform.Rotate(0, 90, 0);
                        break;
                    case 4:
                        transform.Rotate(0, -90, 0);
                        break;
                    case 5:
                        transform.Rotate(0, 90, 0);
                        break;
                    default:
                        transform.Rotate(0, 0, 0);
                        break;
            }
        }
    }

    void EndPath()
    {
        Destroy(gameObject);
        PlayerStats.Lives--;

        if (isBoss) // Assuming you have a flag isBoss to identify the boss enemy
        {
            WaveSpawner.BossAlive--;
        }
        else
        {
            WaveSpawner.EnemiesAlive--;
        }
        
    }
}

    

