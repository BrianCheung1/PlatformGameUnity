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
        if(transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rb2D.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        if(enemy != null)
        {
            enemy.Fix();
        }
       
        Destroy(gameObject);
    }
}
