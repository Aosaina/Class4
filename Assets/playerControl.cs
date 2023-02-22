using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{

    float horizontalMove;
    public float speed;

    Rigidbody2D myBoby;


    bool grounded = false;

    public float castDist = 0.2f;
    public float gravityScale = 5f;
    public float gravityFall = 40f;
    public float jumpLimit = 2f;


    bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        myBoby = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        Debug.Log(horizontalMove);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }

    }

    void FixedUpdate()
    {
        float moveSpeed = horizontalMove * speed;

        if (jump)
        {
            myBoby.AddForce(Vector2.up * jumpLimit, ForceMode2D.Impulse);
            jump = false;
        }

        if (myBoby.velocity.y > 0)
        {
            myBoby.gravityScale = gravityScale;
        }
        else if (myBoby.velocity.y < 0)
        {
            myBoby.gravityScale = gravityFall;

        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);
        Debug.DrawRay(transform.position, Vector2.down, Color.red);

        if (hit.collider != null) {
            if (hit.collider.CompareTag("ground"))
            {
                grounded = true;
                Debug.Log("grounded!");
            }
        }
        else //if (hit.transform.tag != "ground")
        {
            grounded = false;
            Debug.Log("ungrounded!");
        }

        myBoby.velocity = new Vector3(moveSpeed, myBoby.velocity.y, 0);
    }
}