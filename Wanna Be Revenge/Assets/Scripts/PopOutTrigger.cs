using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopOutTrigger : MonoBehaviour
{
    public bool oneTime;
    public bool triggerEnter = true;
    public bool triggerExit = false;
    public MovingObject objectToMove;
    public float delay;

    bool hasGone;

    void Start()
    {
        MovingObject.CallBack goBack = new MovingObject.CallBack(objectToMove.GoBack);
        objectToMove.AddForwardCallBack(goBack);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasGone && triggerEnter)
        {
            if (objectToMove.IsResting())
            {
                if (other.gameObject.tag == "Player")
                {
                    StartCoroutine(TriggerEnter(other));
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
                objectToMove.GoBack();
                if (oneTime)
                    hasGone = true;
            }
        }
    }

    IEnumerator TriggerEnter(Collider2D other)
    {
        yield return new WaitForSeconds(delay);
        objectToMove.GoForward();
    }
}
