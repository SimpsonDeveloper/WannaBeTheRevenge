using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float force = 5.0f;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //launch the player and activate animation
            animator.SetTrigger("Spring");
            other.gameObject.SendMessage("SetVerticalVelocity", Mathf.Cos(DegToRads(transform.eulerAngles.z)) * force);
        }
    }

    float DegToRads(float deg)
    {
        return deg * Mathf.PI / 180.0f;
    }
}
