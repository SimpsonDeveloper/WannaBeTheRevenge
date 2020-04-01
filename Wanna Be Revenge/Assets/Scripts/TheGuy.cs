using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheGuy : PhysicsObject
{
    public float jumpTakeOffSpeed = 6;
    public float maxSpeed = 3;
    public Transform startLocation;
    bool canClimb;
    bool isClimbing;
    Animator animator;
    SpriteRenderer sp;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    protected override void ComputeVelocity()
    {

        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     Time.timeScale = 6.0f;
        // }
        // else if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     Time.timeScale = 1.0f;
        // }
        float vAxis = Input.GetAxis("Vertical");
        float hAxis = Input.GetAxis("Horizontal");
        //if we can't climb
        if (!canClimb)
        {
            isClimbing = false;
        }
        //otherwise, if we can climb and we initiate climbing
        else if (vAxis != 0 && !isClimbing)
        {
            isClimbing = true;
        }
        //otherwise, if we are climbing and we choose to stop
        else if(Input.GetButtonDown("Jump") && isClimbing)
        {
            isClimbing = false;
        }

        //if we're not climbing then do our normal physics
        if (!isClimbing)
        {
            useGravity = true;
            updateAnim(hAxis);
            Vector2 move = Vector2.zero;
            move.x = hAxis;
            if (Input.GetButtonDown("Jump") && grounded)
            {
                velocity.y = jumpTakeOffSpeed;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * 0.5f;
                }
            }

            targetVelocity = move * maxSpeed;
        }
        //otherwise climb physics
        else
        {
            useGravity = false;
            velocity.y = 0;
            updateAnim(hAxis);
            Vector2 move = new Vector2(hAxis, 0);
            velocity.y = vAxis * maxSpeed;
            targetVelocity = move * maxSpeed;
        }
    }

    void updateAnim(float hAxis)
    {
        if (hAxis < 0)
        {
            sp.flipX = true;
        }
        else if (hAxis > 0)
        {
            sp.flipX = false;
        }

        animator.SetFloat("Direction", Mathf.Abs(hAxis));
    }

    void ToggleClimb(bool canClimb)
    {
        this.canClimb = canClimb;
    }

    public void StopClimb()
    {
        this.canClimb = false;
    }

    void Respawn()
    {
        GameManager.Instance.Respawn(gameObject.transform.position);
    }

    void SetVerticalVelocity(float v)
    {
        while (velocityLock)
        {
            //wait for the lock to get released
        }
        velocity.y = v;
    }

    void SetPlatformVelocity(Vector2 v)
    {
        platformVelocity = v;
    }

    void ReverseGravity()
    {
        gravityModifier *= -1;
    }
}
