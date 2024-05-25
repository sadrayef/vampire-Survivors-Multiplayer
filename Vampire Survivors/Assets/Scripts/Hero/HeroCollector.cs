using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCollector : MonoBehaviour
{
    HeroStats hero;
    CircleCollider2D heroCollector;
    public float pullSpeed;

    void Start()
    {
        hero = FindObjectOfType<HeroStats>();
        heroCollector = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        heroCollector.radius = hero.currentMagnet; 
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out ICollectible collectible))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            Vector2 forceDircetion = (transform.position - collision.transform.position).normalized;
            rb.AddForce(forceDircetion * pullSpeed);
            collectible.Collect();
        }
    }
}
