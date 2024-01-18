using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraEffect : MonoBehaviour
{
    public float auraRadius = 7f;  
    public float speedMultiplier = 1.5f;
    public GameObject animationPrefab; // Assign the animation prefab in the Inspector
    private bool animationPlayed = false;
    private GameObject instantiatedAnimationPrefab;

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, auraRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy" && !collider.GetComponent<AuraEffect>())
            { 
                // Access the enemy's script or component that controls speed and modify it here
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.IncreaseSpeed(speedMultiplier);

                    if (!animationPlayed && animationPrefab != null)
                    {
                        //Instantiate(animationPrefab, collider.transform.position, Quaternion.identity);
                        //GameObject effectIns = (GameObject)Instantiate(animationPrefab, collider.transform.position, Quaternion.identity);
                        //Destroy(effectIns, 2f);
                        instantiatedAnimationPrefab = Instantiate(animationPrefab, collider.transform.position, Quaternion.identity);
                        Destroy(instantiatedAnimationPrefab, 2f);
                        animationPlayed = true;

                        
                    }
                }
            }
            
        }

        Collider[] allColliders = Physics.OverlapSphere(transform.position, auraRadius + 1f); // Add a buffer to the radius
        foreach (Collider collider in allColliders)
        {
            if (collider.tag == "Enemy" && !collider.GetComponent<AuraEffect>())
            {
                if (Vector3.Distance(collider.transform.position, transform.position) > auraRadius)
                {
                    Enemy enemy = collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.ResetSpeed();

                        if (instantiatedAnimationPrefab != null)
                        {
                            Destroy(instantiatedAnimationPrefab,1f);
                        }

                        animationPlayed = false;
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, auraRadius);
    }
}
