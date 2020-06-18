using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator animator;
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    Rigidbody2D rb2D;
    float timer;
    int direction = 1;

    bool broken = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;

        if(timer < 0)
        { 
            direction = -direction;
            timer = changeTime;
        }
    }

    private void FixedUpdate()
    {

        if (!broken)
        {
            return;
        }

        Vector2 position = rb2D.position;
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else if (!vertical)
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move Y", 0);
            animator.SetFloat("Move X", direction);
        }

        rb2D.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RubyController player = collision.gameObject.GetComponent<RubyController>();
        

        if(player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        rb2D.simulated = false;
        animator.SetTrigger("Fixed");
    }
}
