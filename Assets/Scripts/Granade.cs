using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    public float throwDistance = 2;
    public float effectRadius = 3;
    Rigidbody2D rigidbody2d;
    SpriteRenderer spriteRenderer;
    ParticleSystem explosionSystem;
    bool exploded = false;

    Vector3 initialPosition;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        explosionSystem = GetComponentInChildren<ParticleSystem>();
    }

    public void Launch(Vector2 direction, float force)
    {
        initialPosition = transform.position;
        rigidbody2d.AddForce(direction * force);
    }

    void Update()
    {
        float distance = Vector3.Distance(initialPosition, transform.position);
        if (distance > throwDistance && !exploded)
        {
            ExplossionEffect();
        }
    }

    private void ExplossionEffect()
    {
        rigidbody2d.velocity = Vector3.zero;
        spriteRenderer.enabled = false;
        explosionSystem.Play();
        Destroy(gameObject, explosionSystem.main.duration);
        exploded = true;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, effectRadius, Vector2.up);
        foreach(RaycastHit2D hit in hits)
        {
            EnemyController enemy = hit.collider.GetComponent<EnemyController>();
            if(enemy != null)
            {
                enemy.Fix();
                EnemiesManager.instance.FixRobot();
            }

            HardEnemy hardEnemy = hit.collider.GetComponent<HardEnemy>();
            if (hardEnemy != null)
            {
                hardEnemy.Fix();
                EnemiesManager.instance.FixRobot();
            }
        }
    }

}
