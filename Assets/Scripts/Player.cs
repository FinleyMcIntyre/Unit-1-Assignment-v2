using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed;
    float nspeed;
    Rigidbody2D rb;
    bool grounded;
    HelperScript helper;
    public GameObject weapon;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 10f;
        nspeed = -10f;
        helper = gameObject.AddComponent<HelperScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("d") == true)
        {
            print("Player pressed d");
            //tradnsform.position = new Vector2(transform.position.x + (speed * Time.deltaTime), transform.position.y);
            rb.velocity = new Vector2( speed, rb.velocity.y );

        }
        
        if (Input.GetKey("a") == true)
        {
            print("Player pressed a");
            //transform.position = new Vector2(transform.position.x + (nspeed * Time.deltaTime), transform.position.y);
        }   rb.velocity = new Vector2( nspeed, rb.velocity.y );
        grounded = helper.DoRayCollisionCheck ();   
       
        if (Input.GetKeyDown("space") == true && grounded == true)
        {
            print("Player pressed space");
            rb.AddForce(new Vector3(0, 15f, 0), ForceMode2D.Impulse);
        }

        int moveDirection = 1;
        if (Input.GetKeyDown("c"))
        {
            GameObject clone;
            clone = Instantiate(weapon, transform.position, transform.rotation);
            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector3(15 * moveDirection, 0, 0);
            rb.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z + 1);
        }

    }
}
