using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool vertical;                                                   //vertical bool to change if enemies are moving in a certian direction
    bool broken = true;                                                     //robots are broken from the start

    public float speed;                                                     //robot walking speeds
    public float changeTime = 3.0f;                                         //how long before the robot change direcitons
    float timer;                                                            //timer for robot to change direcitons
    int direction = 1;                                                      //sets the direction in which the robot is walking in, left,right,up,down

    Rigidbody2D rb2D;
    Animator animator;
    public ParticleSystem smokeEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        //if the robot is no longer broken, stop the code
        if (!broken)
        {
            return;
        }

        //remove time from timer
        timer -= Time.deltaTime;

        //if timer is less than 0 do something
        if(timer < 0)
        {
            //change direction robot is walking in
            vertical = !vertical;
            if (vertical == true)
            {
                direction = -direction;
            }
            //set timer back
            timer = changeTime;
        }
    }

    private void FixedUpdate()
    {
        //if robot is fixed, stop the code
        if (!broken)
        {
            return;
        }

        //returns the position of the robot
        Vector2 position = rb2D.position;

        //if the robot is moving vertically
        if (vertical)
        {
            //change the y postiion 
            position.y = position.y + Time.deltaTime * speed * direction;
            //plays the animations corresponding to to y movement
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else 
        {
            //change the x position
            position.x = position.x + Time.deltaTime * speed * direction;
            //plays the animations corresponding to to x movement
            animator.SetFloat("Move Y", 0);
            animator.SetFloat("Move X", direction);

        }

        //moves the robot by the position set
        rb2D.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //gets the compeoents of the collsion detected
        RubyController player = collision.gameObject.GetComponent<RubyController>();
        
        //if the player collision does not equal nothing
        if(player != null)
        {
            //remove 1 hp from player
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        //sets robot state to fixed
        broken = false;
        //removed their rigid bodies
        rb2D.simulated = false;
        //plays the fixed animation
        animator.SetTrigger("Fixed");
        //Stops the smoke particles
        smokeEffect.Stop();
    }
}
