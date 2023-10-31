using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        public float movingSpeed;
        public float jumpForce;
        private float moveInput;

        private bool facingRight = false;
        [HideInInspector]
        public bool deathState = false;

        private bool isGrounded;
        public Transform groundCheck;

        private Rigidbody2D rigidbody;
        private Animator animator;
        private GameManager gameManager;

        public GameObject weapon;

        Vector2 startPos;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            startPos = transform.position;
        }


        private void OnTriggerEnter2D(Collider2D Collision)
        {
            if (Collision.CompareTag("Obstacle"))
            {
                Die();
            }

        }

        void Die()
        {
            Respawn();

        }
        void Respawn()
        {
            transform.position = startPos;
        }

       
       
        

       



       

        private void FixedUpdate()
        {
            CheckGround();
        }

        void Update()
        {
            if (Input.GetButton("Horizontal")) 
            {
                moveInput = Input.GetAxis("Horizontal");
                Vector3 direction = transform.right * moveInput;
                transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, movingSpeed * Time.deltaTime);
                animator.SetInteger("playerState", 1); // Turn on run animation
            }
            else
            {
                if (isGrounded) animator.SetInteger("playerState", 0); // Turn on idle animation
            }
            if(Input.GetKeyDown(KeyCode.Space) && isGrounded )
            {
                rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }
            if (!isGrounded)animator.SetInteger("playerState", 2); // Turn on jump animation

            if(facingRight == false && moveInput > 0)
            {
                Flip();
            }
            else if(facingRight == true && moveInput < 0)
            {
                Flip();
            }

            int moveDirection = 1;
            if (Input.GetKeyDown("enter") && facingRight == true)
            {
                GameObject clone;
                clone = Instantiate(weapon, transform.position, transform.rotation);
                Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector3(15 * moveDirection, 0, 0);
                rb.transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 1);
                Destroy(clone,2f);
            }

            int moveDirectionn = 1;
            if (Input.GetKeyDown("enter") && facingRight == false)
            {
                GameObject clone;
                clone = Instantiate(weapon, transform.position, transform.rotation);
                Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector3(-15 * moveDirectionn, 0, 0);
                rb.transform.position = new Vector3(transform.position.x + -1, transform.position.y, transform.position.z + 1);
                Destroy(clone,2f);


            }
        }

        private void Flip()
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }

        private void CheckGround()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.2f);
            isGrounded = colliders.Length > 1;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                deathState = true; // Say to GameManager that player is dead
            }
            else
            {
                deathState = false;
            }
        }

        
        
    }
}
