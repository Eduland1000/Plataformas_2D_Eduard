using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
   public static bool isGrounded;

   void OnTriggerEnter2D(Collider2D collider)
   {
    if(collider.gameObject.layer==6)
    {
        isGrounded = true;
        PlayerController.characterAnimator.SetBool("IsJumping", false);
    }
   }

   void OnTriggerStay2D(Collider2D collider)
   {
        if(collider.gameObject.layer == 6)
    {
        isGrounded = true;
    }
    
   }

     void OnTriggerExit2D(Collider2D collider)
   {
        if(collider.gameObject.layer ==6)
   {
        isGrounded = false;
         PlayerController.characterAnimator.SetBool("IsJumping",true);
   }
   }
}
