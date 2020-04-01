using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTrigger : MonoBehaviour
{
    public GameObject enableObject;
    void OnTriggerEnter2D(Collider2D other)
    {
        enableObject.SetActive(true);
        Destroy(gameObject);
    }
}
