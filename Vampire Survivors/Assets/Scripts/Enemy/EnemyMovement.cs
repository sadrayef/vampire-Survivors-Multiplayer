using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform hero;

    public float enemySpeed;

    private void Start()
    {
        hero = FindObjectOfType<HeroMovement>().transform;

    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, hero.transform.position, enemySpeed * Time.deltaTime);
    }
}
