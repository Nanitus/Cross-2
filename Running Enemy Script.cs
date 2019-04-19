using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MeleeEnemy : MonoBehaviour
{

    public float RunSpeed;                      // Speed of enemy

    public GameObject CollidedObject = null;    // Ensures the code works

    private Animator Animation;                 // Enables animations

    private void Start()
    {
        // What object needs to be animated to look for
        Animation = GameObject.FindWithTag("Enemy_Melee").GetComponent<Animator>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        // When player enters the enemy's vision, the enemy will run towards the player
        if (collision.gameObject.tag == "Player")
        {
            Vector3 Dir = new Vector3(-RunSpeed, 0, 0);
            transform.position += Dir * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If enemy is hit by player's bullets
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Animation.SetTrigger("Damaged");
        }
    }
}
