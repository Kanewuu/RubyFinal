using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SlowCollectable : MonoBehaviour
{
    public AudioClip collectedClip;
    public float slowEffectFactor = 0.5f;
    public float slowEffectTime = 3f;

    public Transform[] waypoints;
    public Vector3[] waypointPositions;

    public float speed = 5;

    int targetIndex = 0;
    float tolerance = 0.1f;
    Vector3 startPoint;
    Vector3 targetPoint;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        startPoint = transform.position;
        targetPoint = startPoint;

        waypointPositions = new Vector3[waypoints.Length];
        for(int i = 0; i < waypoints.Length; i++)
        {
            waypointPositions[i] = waypoints[i].position;
        }
    }

    void Update()
    {
        // calculate the direction and distance to the current target position
        Vector3 targetDirection = waypointPositions[targetIndex] - transform.position;
        float distanceToTarget = targetDirection.magnitude;

        if (distanceToTarget <= tolerance)
        {
            // if we're close enough to the target position, switch to the next one
            targetIndex = (targetIndex + 1) % waypoints.Length;
        }
        else
        {
            // otherwise, move towards the target position at the given speed
            transform.position += targetDirection.normalized * speed * Time.deltaTime;
        }

        if (targetDirection.x > 0) spriteRenderer.flipX = false;
        else spriteRenderer.flipX = true;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Rubycontroller1 controller = other.GetComponent<Rubycontroller1>();

        if (controller != null)
        {
            controller.SlowForSeconds(slowEffectFactor, slowEffectTime);
            Destroy(gameObject);
            controller.PlaySound(collectedClip);
        }
    }
}
