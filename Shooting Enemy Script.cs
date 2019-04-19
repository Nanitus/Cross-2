using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ShootingEnemy : MonoBehaviour
{
    public float ShootSpeed;                      // Speed of shot
    public float MissileDestroy;                  // Amount of time for shot to dissapear

    public Transform Launcher;                  // Object that shoots

    public GameObject EnemyBullet;              // Object shot
    public GameObject CollidedObject = null;    // Ensures the code works

    private Animator Animation;                 // Enables animations

    bool isFired = false;                       // Bool to check if bullet is being shot

    private void Start()
    {
        // What object needs to be animated to look for
        Animation = GameObject.FindWithTag("Enemy_Shooter").GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // When player enters the enemy's vision, the enemy will shoot towards the player
        if (collision.gameObject.tag == "Player" && !isFired)
        {
            GameObject Missile = Instantiate(EnemyBullet, Launcher.position, Quaternion.identity);
            Rigidbody2D rb = Missile.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(-ShootSpeed, 0.0f));
            Destroy(Missile, MissileDestroy);
            bool isFired = true;
            StartCoroutine(BulletTimer());
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

    // Shooting timer
    IEnumerator BulletTimer()
    {
        yield return new WaitForSeconds(0.75f);
        isFired = false;
        StopAllCoroutines();
    }
}