    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
    private Rigidbody2D characterRigidbody;
    private float horizontalImput;
    private bool jumpInput;
    public static Animator characterAnimator;

    [SerializeField] private float characterSpeed = 4.5f;

    [SerializeField] private float jumpForce = 10f;

    [SerializeField] private int healthPoints;

    private bool isAttacking;

    void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
    }
        // Start is called before the first frame update
        void Start()
        {
            //characterRigidbody.AddForce(Vector2.up * jumpForce);
        }

        void Update()
        {
            Movement();

            if (Input.GetButtonDown("Jump") && GroundSensor.isGrounded && !isAttacking)
            {
                Jump();
            }

            if(Input.GetButtonDown("Attack") && GroundSensor.isGrounded && !isAttacking)
            {
                Attack();
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            
            if(!isAttacking)
            {
               characterRigidbody.velocity = new Vector2(horizontalImput * characterSpeed,characterRigidbody.velocity.y);
            }
            else
            {
                characterRigidbody.velocity = new Vector2(0 * characterSpeed,characterRigidbody.velocity.y);
            }
        }
        void Movement()
        {    
            horizontalImput = Input.GetAxis("Horizontal");
            if(horizontalImput < 0)
            {
               if(!isAttacking)
               {
                transform.rotation = Quaternion.Euler(0, 180, 0);

               }
                 
                characterAnimator.SetBool("IsRunning", true);
        
                
            }
            else if(horizontalImput > 0)
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
            StartCoroutine(AttackAnimaton());
            characterAnimator.SetTrigger("Attack");
        }

        IEnumerator AttackAnimaton()
        {
            isAttacking=true;

            yield return new WaitForSeconds(0.5f);

            isAttacking = false;
        }

        void TakeDamage()
        {
            healthPoints--;
            characterAnimator.SetTrigger("IsHurt");
            
            if(healthPoints <= 0)
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
            Destroy (gameObject, 0.6f);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.layer == 8)
            {
                TakeDamage();
            }
        }
    }
