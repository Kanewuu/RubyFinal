using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogsCollectable : MonoBehaviour
{
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        Rubycontroller1 controller = other.GetComponent<Rubycontroller1>();

        if (controller != null)
        {
            controller.ChangeCogs(4);
            Destroy(gameObject);
            controller.PlaySound(collectedClip);
        }
    }
}
