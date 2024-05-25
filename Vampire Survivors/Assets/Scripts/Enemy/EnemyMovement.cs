using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    Transform hero;
    EnemyStats enemy;
    

    private void Start()
    {
        enemy = GetComponent<EnemyStats>();
        hero = FindObjectOfType<HeroMovement>().transform;

    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, hero.transform.position, enemy.currentMoveSpeed * Time.deltaTime);
    }
}
