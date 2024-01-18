using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenu : MonoBehaviour
{
    
    private Animator anim;
    private CharacterController controller;
    private string animate = "";
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
       
        anim.SetInteger("moving", 1);

        AnimationClip AttackAnimation = null;
        foreach (AnimationClip clip in anim.runtimeAnimatorController.animationClips)
        {
            
            if (clip.name == "torment")
            {
                AttackAnimation = clip;
                animate = "torment";
                break;
            }

            if (clip.name == "stomp")
            {
                animate = "stomp";
                break;
            }

        }


        if (AttackAnimation != null)
        {
            StartCoroutine(AnimationSequence(AttackAnimation.length, animate));
        }
        else
        {
            StartCoroutine(AnimationSequence(5f, animate));
        }



    }

    IEnumerator AnimationSequence(float delay, string currentAnimation)
    {
        anim.Play(currentAnimation);
        yield return new WaitForSeconds(delay);
    }

    
}
