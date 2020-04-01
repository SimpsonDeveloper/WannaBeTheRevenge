using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform objectToFollow;
    public float speed;
    public float turnSpeed;
    Rigidbody2D rb2d;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //direction.SetFromToRotation(rb2d.position, objectToFollow.position);
        direction = ((Vector2)objectToFollow.position - rb2d.position).normalized;
    }

    void FixedUpdate()
    {
        // Quaternion temp = direction;
        // temp.SetFromToRotation(rb2d.position, objectToFollow.position);
        // direction = Quaternion.RotateTowards(direction, temp, turnSpeed * Time.deltaTime);

        // Vector2 offset = new Vector2(Mathf.Cos(direction.eulerAngles.z * Mathf.Deg2Rad) * -1,
        //     Mathf.Sin(direction.eulerAngles.z * Mathf.Deg2Rad)) * Time.deltaTime * speed;
        // rb2d.position = rb2d.position + offset;


        direction = Vector3.RotateTowards(direction, (Vector2)objectToFollow.position - rb2d.position, turnSpeed * Mathf.Deg2Rad, 1.0f);
        rb2d.position += (Vector2)direction.normalized * Time.deltaTime * speed;
    }


}
