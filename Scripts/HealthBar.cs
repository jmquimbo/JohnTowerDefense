using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    //RectTransform canvasRectTransform;
    
    Quaternion firstRot;
    public GameObject TargetEnemy;
    private float xOffset = 0f;
    private float yOffset = 4f;
    private float zOffset = 0f;

    void Start()
    {
        firstRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.Euler(lockPos, lockPos, lockPos);

    }

    void LateUpdate()
    {
        transform.rotation = firstRot;
        transform.position = new Vector3(TargetEnemy.transform.position.x + xOffset, TargetEnemy.transform.position.y + yOffset, TargetEnemy.transform.position.z + zOffset);
        
    }
}
