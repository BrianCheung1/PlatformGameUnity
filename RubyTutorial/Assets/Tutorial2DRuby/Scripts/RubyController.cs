using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    public int maxHealth = 5;
    public int health {  get { return currentHealth; } }
    int currentHealth;
    public float timeInvincible = 2.0f;

    bool isInvincible;
    float invincibleTimer;

    public float speed = 3.0f;
    Rigidbody2D rb2D;
    float horizontal;
    float vertical;

    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Launch();
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rb2D.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rb2D.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if(amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
            animator.SetTrigger("Hit");
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rb2D.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }
}

