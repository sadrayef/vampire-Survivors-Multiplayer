using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

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


    [Header("Damage Feedback")]
    public Color damageColor = new Color(1, 0, 0, 1);
    public float damageFlashDuration = 0.2f;
    public float deathFadeTime = 0.6f;

    Color originalColor;
    SpriteRenderer sr;
    EnemyMovement movement;


    

    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

     void Start()
    {
        hero = FindObjectOfType<HeroStats>().transform;


        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        movement = GetComponent<EnemyMovement>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, hero.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }


    public void TakeDamage(float dmg, Vector2 sourcePosition, float knochbackForce = 5f /*how far the knockback will be */, float knockbackDuration = 0.2f /*how long the knockback will last */)
    {
        currentHealth -= dmg;
        StartCoroutine(DamageFlash());


        if(knochbackForce > 0)
        {
            Vector2 dir = (Vector2)transform.position - sourcePosition; //vector ab = b-a
            movement.KnockBack(dir.normalized * knochbackForce, knockbackDuration);
        }

        if (currentHealth <= 0)
        {
            Kill();
        }
    }


    IEnumerator DamageFlash()
    {
        sr.color = damageColor;
        yield return new WaitForSeconds(damageFlashDuration);
        sr.color = originalColor;
    }


    public void Kill()
    {
        StartCoroutine(KillFade());
    }

    //A new coroutine
    IEnumerator KillFade()
    {
        //waits for a single frame
        WaitForEndOfFrame w = new WaitForEndOfFrame();
        float t = 0, origAlpha = sr.color.a;

        while(t < deathFadeTime)
        {
            yield return w;
            t += Time.deltaTime;

            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, (1 - t / deathFadeTime) * origAlpha);
        }

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
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        EnemySpawner eSpawner = enemySpawner;
        eSpawner.OnEnemyKilled();
    }

    void ReturnEnemy()
    {
        EnemySpawner eSpawner = FindObjectOfType<EnemySpawner>();
        transform.position = hero.position + eSpawner.relativeSpawnPoints[Random.Range(0, eSpawner.relativeSpawnPoints.Count)].position;
    }
}
