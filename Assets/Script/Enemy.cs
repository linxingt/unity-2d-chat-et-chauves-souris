using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public int damage;

    public float flashTime;

    public GameObject bloodEffect;

    private SpriteRenderer sr;
    private Color originalColor;
    private PlayerHealth PH;

    // Start is called before the first frame update
    public void Start()
    {
        PH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    // Update is called once per frame
    public void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
        FlashColor(flashTime);
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        GameControl.camShake.Shake();
    }

    void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);
    }

    void ResetColor()
    {
        sr.color = originalColor;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")// && other.ToString() == "UnityEngine.CapsuleCollider2D"
        {
            //other.GetComponent<PlayerHealth>().DamagePlayer(damage);
            if(PH != null)
            {
                PH.DamagePlayer(damage);
            }
        }
    }
}
