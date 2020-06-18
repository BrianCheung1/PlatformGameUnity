using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    Vector2 lookDirection = new Vector2(1, 0);

    public int maxHealth = 5;
    public int health {  get { return currentHealth; } }
    int currentHealth;
    public float timeInvincible = 2.0f;
    public float speed = 3.0f;

    bool isInvincible;
    float invincibleTimer;

    Animator animator;
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
        //gets the inputs from the player
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //
        Vector2 move = new Vector2(horizontal, vertical);

        //if the player x || y does not equal 0, it means they are moving
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            //set their directions
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }


        //plays the animations in which the direciton the palyer is moving in
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        //if player is invincible
        if (isInvincible)
        {
            //remove time from invincibleTimer
            invincibleTimer -= Time.deltaTime;
            //if timer is less than 0
            if(invincibleTimer < 0)
            {
                //player is no longer invincible
                isInvincible = false;
            }
        }

        //key to fire projectiles
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //function to fire projectiles
            Launch();
        }
    }

    void FixedUpdate()
    {
        //position of the player
        Vector2 position = rb2D.position;
        //move the player depending on the inputs
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        //move the player to those positions
        rb2D.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        //if damange is taken
        if(amount < 0)
        {
            //if the player is invincible dont do anything
            if (isInvincible)
                return;
            //set player to invincible
            isInvincible = true;
            //set the timer
            invincibleTimer = timeInvincible;
            //play hit aniamtion
            animator.SetTrigger("Hit");
        }
        //sets player hp and ensures that the player hp can never go lower than 0 and above max health
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    void Launch()
    {
        //creates the projectileObject a little above the character model, this ensure that projectile comes out of the hands rather than the feet
        GameObject projectileObject = Instantiate(projectilePrefab, rb2D.position + Vector2.up * 0.5f, Quaternion.identity);
        //get the compoents of the projectile
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        //launch the projectiles in the direciton the player is looking in for 300 newton force
        projectile.Launch(lookDirection, 300);
        //play the launch aniamtions
        animator.SetTrigger("Launch");
    }
}

