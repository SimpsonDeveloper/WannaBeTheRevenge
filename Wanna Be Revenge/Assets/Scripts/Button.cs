using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Sprite unarmedSprite;
    public Sprite armedSprite;
    public BossLevelManager blm;

    bool triggered;
    SpriteRenderer spriteRenderer;
    float pauseTime = 1.0f;
    
    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        triggered = false;
        spriteRenderer.sprite = unarmedSprite;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !triggered)
        {
            triggered = true;
            blm.Pause();
            spriteRenderer.sprite = armedSprite;
            StartCoroutine("ButtonEffect");
        }
    }

    IEnumerator ButtonEffect()
    {
        yield return new WaitForSeconds(pauseTime);
        Unpause();
    }

    void Unpause()
    {
        blm.TakeHit();
    }
}
