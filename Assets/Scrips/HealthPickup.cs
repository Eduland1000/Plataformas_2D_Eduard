using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour

{
    public int healthAmount = 1; 
    private PlayerController playerScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerScript = other.gameObject.GetComponent<PlayerController>();
            
            playerScript.AddHealth(healthAmount);

            Destroy(gameObject);
        }
    }

}

