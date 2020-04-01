using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    public float despawnTime = 1.0f;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine("GoAway");
    }

    IEnumerator GoAway()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
