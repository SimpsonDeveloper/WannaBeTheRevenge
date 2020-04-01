using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevelManager : MonoBehaviour
{
    public FireballSpawner fireballSpawner;
    public TheGuy player;
    public GameObject ladderTileMap;
    public GameObject clickTheButtonTileMap;
    public GameObject iAmTheGuyTileMap;
    public GameObject button;
    public float spawnSecondsTime = 15;
    public float vulnerabilityTime = 10;

    int bossHealth = 3;
    bool keepGoing = true;
    bool spawnFireballs = true;
    float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        fireballSpawner.StartSpawning();
        timer = spawnSecondsTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (keepGoing)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                if (spawnFireballs)
                {
                    //switch to vulnerability
                    timer = vulnerabilityTime;
                    fireballSpawner.StopSpawning();
                    spawnFireballs = false;

                    ladderTileMap.SetActive(false);
                    player.StopClimb();
                    clickTheButtonTileMap.SetActive(true);
                    button.SetActive(true);
                }
                else
                {
                    //switch to fireballs
                    timer = spawnSecondsTime;
                    fireballSpawner.StartSpawning();
                    spawnFireballs = true;

                    ladderTileMap.SetActive(true);
                    clickTheButtonTileMap.SetActive(false);
                    button.SetActive(false);
                }
            }
        }
    }

    public void Pause()
    {
        keepGoing = false;
    }

    public void TakeHit()
    {
        bossHealth--;
        if (bossHealth <= 0)
        {
            //switch to win screen
            ladderTileMap.SetActive(false);
            player.StopClimb();
            clickTheButtonTileMap.SetActive(false);
            iAmTheGuyTileMap.SetActive(true);
            button.SetActive(false);
            keepGoing = false;
        }
        else
        {
            //switch to fireballs
            keepGoing = true;
            spawnFireballs = true;
            ladderTileMap.SetActive(true);
            clickTheButtonTileMap.SetActive(false);
            button.SetActive(false);
            timer = spawnSecondsTime;
            fireballSpawner.StartSpawning();
        }
    }
}
