using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Hardware;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Destroys the projectile if they go too far away from the start position
        if(transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    //moves the projection in a certian direction and force that is used in the playercontroller script
    public void Launch(Vector2 direction, float force)
    {
        rb2D.AddForce(direction * force);
    }

    //if the projectile collides with something
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //gets the components of the collision
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

        //if the collision does not equal nothing
        if(enemy != null)
        {
            //set their states to fixed
            enemy.Fix();
        }
       
        //destory the game objects after they collided
        Destroy(gameObject);
    }
}
