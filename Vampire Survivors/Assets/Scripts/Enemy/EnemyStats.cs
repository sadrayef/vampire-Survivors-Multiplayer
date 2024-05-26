using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    [HideInInspector]
    public float currentMoveSpeed;
   
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;

    public float despawnDistance = 20f;
    Transform hero;

    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

     void Start()
    {
        hero = FindObjectOfType<HeroStats>().transform;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, hero.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }


    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) {
            HeroStats hero = collision.gameObject.GetComponent<HeroStats>();    
            hero.TakeDamage(currentDamage);

        }
    }

    private void OnDestroy()
    {
        EnemySpawner eSpawner = FindObjectOfType<EnemySpawner>();
        eSpawner.OnEnemyKilled();
    }

    void ReturnEnemy()
    {
        EnemySpawner eSpawner = FindObjectOfType<EnemySpawner>();
        transform.position = hero.position + eSpawner.relativeSpawnPoints[Random.Range(0, eSpawner.relativeSpawnPoints.Count)].position;
    }
}
