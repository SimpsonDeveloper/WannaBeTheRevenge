using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public bool moveOnStart;
    public bool playerStandsOn;
    public bool loops;
    public Vector2[] path;
    public int startingIndex = 0;
    public float forwardSpeed;
    public float backwardSpeed;

    Rigidbody2D rb2d;
    bool goForward;
    bool goBackward;
    int pathIndex = 0;
    Vector2 velocity;
    bool hasPlayer;
    GameObject player;
    bool destinationReached;
    int destinationIndex;
    bool goToIndex;

    public delegate void CallBack();
    CallBack reachDestination = new CallBack(DoNothing);

    // Start is called before the first frame update
    void Start()
    {
        pathIndex = startingIndex;
        if (moveOnStart)
        {
            goForward = true;
        }
        else
        {
            destinationReached = true;
        }

        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //go forward to next location
        if (goForward)
        {
            if ((path[pathIndex] - (Vector2)gameObject.transform.position).magnitude < 0.01f)
            {
                if (loops)
                {
                    pathIndex = (pathIndex + 1) % path.Length;
                }
                else
                {
                    if (goToIndex && pathIndex == destinationIndex)
                    {
                        destinationReached = true;
                        reachDestination();
                    }
                    else if (pathIndex < path.Length - 1)
                    {
                        pathIndex = pathIndex + 1;
                    }
                    else
                    {
                        destinationReached = true;
                        reachDestination();
                    }
                }
            }
            if (!destinationReached)
            {
                Vector2 direction = path[pathIndex] - (Vector2)gameObject.transform.position;
                velocity = direction.normalized * forwardSpeed;
                Vector2 offset = Vector2.ClampMagnitude(velocity * Time.deltaTime, direction.magnitude);
                velocity = offset / Time.deltaTime;
                rb2d.position += velocity * Time.deltaTime;
                if (hasPlayer)
                    player.SendMessage("SetPlatformVelocity", velocity);
            }
        }
        //otherwise, go backwards to next location
        else if (goBackward)
        {
            if ((path[pathIndex] - (Vector2)gameObject.transform.position).magnitude < 0.01f)
            {
                if (loops)
                {
                    pathIndex = pathIndex - 1;
                    if (pathIndex < 0)
                    {
                        pathIndex = path.Length - 1;
                    }
                }
                else
                {
                    if (goToIndex && pathIndex == destinationIndex)
                    {
                        destinationReached = true;
                    }
                    else if (pathIndex > 0)
                    {
                        pathIndex = pathIndex - 1;
                    }
                    else
                    {
                        destinationReached = true;
                    }
                }
            }
            if (!destinationReached)
            {
                Vector2 direction = path[pathIndex] - (Vector2)gameObject.transform.position;
                velocity = direction.normalized * backwardSpeed;
                Vector2 offset = Vector2.ClampMagnitude(velocity * Time.deltaTime, direction.magnitude);
                velocity = offset / Time.deltaTime;
                rb2d.position += velocity * Time.deltaTime;
                if (hasPlayer)
                    player.SendMessage("SetPlatformVelocity", velocity);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (playerStandsOn)
        {
            if (other.gameObject.tag == "Player")
            {
                hasPlayer = true;
                player = other.gameObject;
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (playerStandsOn)
        {
            if (other.gameObject.tag == "Player")
            {
                hasPlayer = false;
                player.SendMessage("SetPlatformVelocity", new Vector2(0.0f, 0.0f));
            }
        }
    }

    public void GoForward()
    {
        goToIndex = false;
        goBackward = false;
        goForward = true;
        if (pathIndex == 0)
            pathIndex = pathIndex + 1;
        destinationReached = false;
    }

    public void GoBack()
    {
        goToIndex = false;
        goBackward = true;
        goForward = false;
        if (pathIndex > 0)        
            pathIndex = pathIndex - 1;
        destinationReached = false;
    }

    public void GoToIndex(int idx)
    {
        goToIndex = true;
        destinationIndex = idx;
        if (idx == pathIndex)
        {
            goBackward = false;
            goForward = false;
            destinationReached = true;
        }
        else
        {
            int prevIndex = pathIndex;
            pathIndex = idx;
            int bDist = (path.Length - prevIndex) + pathIndex;
            int fDist = prevIndex + (path.Length - pathIndex);

            //take the shorter path out of forward and backward
            if (fDist <= bDist)
            {
                goForward = true;
                goBackward = false;
            }
            else
            {
                goForward = false;
                goBackward = true;
            }
            destinationReached = false;
        }
    }

    public bool IsResting()
    {
        return destinationReached;
    }

    public void AddForwardCallBack(CallBack x)
    {
        reachDestination = new CallBack(x);
    }

    static void DoNothing()
    {

    }
}
