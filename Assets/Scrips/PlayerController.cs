   using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D characterRigidbody;
    private float horizontalInput;
    private bool jumpInput;
    public static Animator characterAnimator;
    private Animator animator;

    [SerializeField] private float characterSpeed = 4.5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int healthPoints = 5;

    private bool isAttacking;
    [SerializeField] private Transform attackHitBox;
    [SerializeField] private float attackRadius = 1;

    private bool isMoving;

    void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();

        if (Input.GetButtonDown("Jump") && GroundSensor.isGrounded && !isAttacking)
        {
            Jump();
        }

        if (Input.GetButtonDown("Attack") && GroundSensor.isGrounded && !isAttacking)
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        
        characterRigidbody.velocity = new Vector2(horizontalInput * characterSpeed, characterRigidbody.velocity.y);
            
      
    }

    void Movement()
    {
        if(isAttacking)
        {
            horizontalInput = 0;
        }
        else
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }
       
        if (horizontalInput < 0)
        {
            if (!isAttacking)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            characterAnimator.SetBool("IsRunning", true);
        }
        else if (horizontalInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            characterAnimator.SetBool("IsRunning", true);
        }
        else
        {
            characterAnimator.SetBool("IsRunning", false);
        }
    }

    void Jump()
    {
        characterRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        characterAnimator.SetBool("IsJumping", true);
    }

    void Attack()
    {
        StartCoroutine(AttackAnimation());

        if(horizontalInput == 0)
        {
            characterAnimator.SetTrigger("Attack");
        }

        else
        {
            characterAnimator.SetTrigger("RunAttack");
        }
    }
  
    IEnumerator AttackAnimation()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackHitBox.position, attackRadius);
        foreach (Collider2D enemy in colliders)
        {
            if (enemy.gameObject.CompareTag("Mimico"))
            {
                Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();
                enemyRigidbody.AddForce(transform.right + transform.up * 2, ForceMode2D.Impulse);
                
                Mimico mimicoScript = enemy.GetComponent<Mimico>();
                mimicoScript.TakeDamage();
            }
        }
        isAttacking = true;

        yield return new WaitForSeconds(0.4f);

        isAttacking = false;
    }
    /*void RunAttack()
    {
        isAttacking = true;
        characterAnimator.SetBool("RunAttack", true); 
        StartCoroutine(ResetRunAttack());
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.4f); // Esperar el tiempo de la animación de ataque
        characterAnimator.SetBool("RunAttack", false);
        isAttacking = false; // Desactivar el estado de ataque
    }
    IEnumerator ResetRunAttack()
    {
        yield return new WaitForSeconds(0.6f); 
        characterAnimator.SetBool("RunAttack", false); 
        isAttacking = false; 
    }*/

    void TakeDamage()
    {
        healthPoints--;
        characterAnimator.SetTrigger("IsHurt");
        
        if (healthPoints <= 0)
        {
            Die();  
        }
        else
        {
            characterAnimator.SetTrigger("IsHurt");
        }
    }

    void Die()
    {
        characterAnimator.SetTrigger("IsDead");
        Destroy(gameObject, 0.6f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            TakeDamage();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackHitBox.position, attackRadius);
    }
}