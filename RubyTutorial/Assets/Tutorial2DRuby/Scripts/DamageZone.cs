using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collision)
    {
        //gets the compoenents of the collision
        RubyController controller = collision.GetComponent<RubyController>();
        //if the collision is not empty
        if(controller != null){
            //of the controller has more than 0 hp
            if (controller.health > 0)
            {
                //remove 1 hp 
                controller.ChangeHealth(-1);
            }
        }
    }
}
