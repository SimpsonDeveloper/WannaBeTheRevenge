using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIndexTrigger : MonoBehaviour
{
    public int enterIndex;
    public int exitIndex;
    public bool oneTime;
    public bool triggerEnter = true;
    public bool triggerExit = true;
    public MovingObject objectToMove;

    bool hasGone;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasGone && triggerEnter)
        {
            if (objectToMove.IsResting())
            {
                if (other.gameObject.tag == "Player")
                {
                    objectToMove.GoToIndex(enterIndex);
                    if (oneTime)
                        hasGone = true;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!hasGone && triggerExit)
        {
            if (other.gameObject.tag == "Player")
            {
                objectToMove.GoToIndex(exitIndex);
                if (oneTime)
                    hasGone = true;
            }
        }
    }
}
