using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriterenderer;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Jump
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }

        // Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.3f, rigid.velocity.y);
        }

        //Direction Sprite
        if (Input.GetButton("Horizontal"))
        {
            spriterenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        //Animation
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            anim.SetBool("isWalking", false);

        else
            anim.SetBool("isWalking", true);  
    }

    void FixedUpdate()
    {
        //Move By Key Control
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed) //Right Max Spped
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);

        else if (rigid.velocity.x < maxSpeed*(-1)) //Left Max Speed
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        //Landing Platform
        if(rigid.velocity.y < 0){
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayhit.collider != null)
            {
                if (rayhit.distance < 0.5f)
                    anim.SetBool("isJumping", false);
            }
        }

    }
}
