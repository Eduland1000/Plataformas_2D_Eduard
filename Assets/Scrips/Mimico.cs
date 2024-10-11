using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimico : MonoBehaviour 
{   
        public float maxHealth = 3f; 
        private float currentHealth; 

        public AudioSource _audioSource;
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        SoundManager.instance.PlaySFX(_audioSource, SoundManager.instance.MimicoAudio);
    }


    // Update is called once per frame
    void Update()
    {

    }

    

    public void TakeDamage()
    {
        currentHealth -= 1;
          
        if(currentHealth <= 0)
        {
            Die();  
        }

        //currentHealth--;
    }
    
    void Die()
    {
        Destroy(gameObject);
    }

}
