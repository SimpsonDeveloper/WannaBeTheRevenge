using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject bloodEffect;
    public int currentLevel = 0;
    bool spawnBlood;
    Vector3 bloodLocation;

    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        SceneManager.sceneLoaded += OnSceneLoaded;
        _instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (spawnBlood)
        {
            float xOffset;
            float yOffset;
            float torque;
            float forceMulitplier = 20.0f;
            //spawn blood things
            for (int i = 0; i < 8; i++)
            {
                xOffset = Random.Range(-0.125f, 0.125f);
                yOffset = Random.Range(-0.03125f, 0.03125f);
                torque = Random.Range(-0.5f, 0.5f);

                Vector3 spawnPos = new Vector3(bloodLocation.x + xOffset, bloodLocation.y + yOffset, bloodLocation.z);
                GameObject particle = Object.Instantiate(bloodEffect, spawnPos, new Quaternion());
                Rigidbody2D pBody = particle.GetComponent<Rigidbody2D>();
                pBody.AddForce(new Vector2(xOffset, yOffset) * forceMulitplier, ForceMode2D.Impulse);
                pBody.AddTorque(torque * forceMulitplier, ForceMode2D.Impulse);
            }
            spawnBlood = false;
        }
    }

    public void Respawn(Vector3 deathLocation)
    {
        spawnBlood = true;
        bloodLocation = deathLocation;
        SceneManager.LoadScene(currentLevel);
    }

    public void CompleteLevel()
    {
        currentLevel++;
        SceneManager.LoadScene(currentLevel);
    }
}
