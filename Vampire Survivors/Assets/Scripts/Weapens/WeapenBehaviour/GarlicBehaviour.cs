using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicBehaviour : MeleeWeaponBehaviour
{
    List<GameObject> poisonedEnemies;
    protected override void Start()
    {
        base.Start();
        poisonedEnemies = new List<GameObject>();
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") && !poisonedEnemies.Contains(col.gameObject))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage(),transform.position);

            poisonedEnemies.Add(col.gameObject);  //Mark the enemy
        }
        else if (col.gameObject.TryGetComponent(out BreakableProps breakable) && !poisonedEnemies.Contains(col.gameObject))
        {
            breakable.TakeDamage(GetCurrentDamage());
            poisonedEnemies.Add(col.gameObject);
        }
    }
}
