using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------------------------------

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemy")] // creating a titlw in the menu

public class EnemyScriptableObject : ScriptableObject
{
    //the properties for an enemy : speed - health - damage

    [SerializeField] //make the private variables accessible within the Unity editor without making them public 
    float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; } //using a lambda  function

    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }
}
