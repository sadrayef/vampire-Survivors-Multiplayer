using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicController : WeapenController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedGarlic = Instantiate(weaponData.Prefab);
        spawnedGarlic.transform.position = transform.position; 
        spawnedGarlic.transform.parent = transform;
    }
}
