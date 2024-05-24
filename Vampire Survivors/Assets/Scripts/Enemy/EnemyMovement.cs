using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    Transform hero;

    

    private void Start()
    {
        hero = FindObjectOfType<HeroMovement>().transform;

    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, hero.transform.position, enemyData.MoveSpeed * Time.deltaTime);
    }
}
