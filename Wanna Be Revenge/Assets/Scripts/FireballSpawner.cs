using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    public float minSpawnSeconds;
    public float maxSpawnSeconds;
    public GameObject fireball;
    public Transform player;
    public float top;
    public float bottom;
    public float left;
    public float right;

    float timer = 0.0f;
    bool spawning = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                timer = Random.Range(minSpawnSeconds, maxSpawnSeconds);

                int incomingDirection = (int)Mathf.Round(Random.Range(1.0f, 4.0f));
                Vector2 center = Vector2.zero;
                Vector2 maxOffset = Vector2.zero;
                switch (incomingDirection)
                {
                    //top
                    case 1:
                        center = new Vector2(0, top);
                        maxOffset = new Vector2(right, 0);
                        break;
                    //bottom
                    case 2:
                        center = new Vector2(0, bottom);
                        maxOffset = new Vector2(right, 0);
                        break;
                    //left
                    case 3:
                        center = new Vector2(left, 0);
                        maxOffset = new Vector2(0, top);
                        break;
                    //right
                    case 4:
                        center = new Vector2(right, 0);
                        maxOffset = new Vector2(0, top);
                        break;
                }

                float rand = Random.Range(-1.0f, 1.0f);
                Vector2 spawnLocation = center + maxOffset * rand;
                GameObject newFireBall = Object.Instantiate(fireball, (Vector3)spawnLocation, new Quaternion());
                newFireBall.GetComponent<Follower>().objectToFollow = player;
            }
        }
    }

    public void StartSpawning()
    {
        spawning = true;
        timer = 0.0f;
    }
    
    public void StopSpawning()
    {
        spawning = false;
    }
}
