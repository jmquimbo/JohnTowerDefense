using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialNode : MonoBehaviour
{
    // Start is called before the first frame update

    public float speedIncreaseAmount = 2f;
    public float rangeIncreaseAmount = 3f;
    public int damageIncreaseAmount = 20;
    public bool isSpeed = false;
    public bool isRange = false;
    public bool isDamage = false;



    public void BoostTurret()
    {
        Turret turret = FindObjectOfType<Turret>();

        if (isSpeed)
        {
            
            if (turret != null)
            {
                turret.IncreaseFireRate(speedIncreaseAmount); // Call the function in your Turret script to increase the fire rate
            }
        }

        if (isRange)
        {
            
            if (turret != null)
            {
                turret.IncreaseRange(rangeIncreaseAmount); // Call the function in your Turret script to increase the fire rate
            }
        }

        if(isDamage)
        {
            if (turret != null)
            {
                turret.IncreaseDamage(damageIncreaseAmount); // Call the function in your Turret script to increase the fire rate
            }
        }
    }
}
