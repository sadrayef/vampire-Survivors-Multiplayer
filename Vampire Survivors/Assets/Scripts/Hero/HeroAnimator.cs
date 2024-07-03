using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimator : MonoBehaviour
{
    //-------------------------------------------------------------

    HeroMovement herom;
    SpriteRenderer sr;
    Animator am;

    //-------------------------------------------------------------

    void Start()
    {
        herom = GetComponent<HeroMovement>();
        sr = GetComponent<SpriteRenderer>();
        am = GetComponent<Animator>();
    }

    //-------------------------------------------------------------

    
    void Update()
    {
        if(herom.moveDir.x != 0 || herom.moveDir.y != 0)
        {
            am.SetBool("Move", true);
            SpriteDirectionChecker();
        }
        else
        {
            am.SetBool("Move", false);
        }
    }

    //-------------------------------------------------------------

    void SpriteDirectionChecker()
    {
        //X>0 : Right -> No Flip
        //X<0 : Left -> Flip
        if (herom.lastHorizontalV < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    //-------------------------------------------------------------

    public void SetAnimatorController(RuntimeAnimatorController c)
    {
        if (!am)
        {
            am = GetComponent <Animator>();
            am.runtimeAnimatorController = c;
        }
    }
}
