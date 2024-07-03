using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------------------------------

public class EnemyMovement : MonoBehaviour
{
    //---------------------------------------------------------------------

    Transform hero; //To Track the hero
    EnemyStats enemy;

    //---------------------------------------------------------------------

    Vector2 knockbackVelocity;
    float KnockbackDuration;

    //---------------------------------------------------------------------

    private void Start()
    {
        enemy = GetComponent<EnemyStats>();
        hero = FindObjectOfType<HeroMovement>().transform; //To know the movement & location of the hero

    }

    //---------------------------------------------------------------------

    private void Update()
    {

        if (KnockbackDuration > 0)
        {
            transform.position += (Vector3)knockbackVelocity * Time.deltaTime; // for knockback
            KnockbackDuration -= Time.deltaTime;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, hero.transform.position, enemy.currentMoveSpeed * Time.deltaTime); // track the hero
        }
    }

    //---------------------------------------------------------------------

    public void KnockBack(Vector2 velocity, float duration)
    {
        if (KnockbackDuration > 0) return;

        knockbackVelocity = velocity;
        KnockbackDuration = duration;
    }

    //---------------------------------------------------------------------
    


}
