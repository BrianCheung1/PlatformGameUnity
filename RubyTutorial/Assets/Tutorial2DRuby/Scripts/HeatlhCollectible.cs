using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatlhCollectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        //get the collison components
        RubyController controller = other.GetComponent<RubyController>();

        //if the collision is not empty
        if(controller != null)
        {
            //if the players health is less than their max health
            if (controller.health < controller.maxHealth)
            {
                //add hp to the player
                controller.ChangeHealth(1);
                //destroy this object
                Destroy(gameObject);
            }
        }
    }
}
