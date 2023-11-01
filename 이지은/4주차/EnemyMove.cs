using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriterenderer;
    public int nextMove;

    void Awake()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        Invoke("Think", 5); //5초간의 딜레이
    }
        

    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayhit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayhit.collider == null)
            Turn();
    }

    void Think()
    {
        //set next active
        nextMove = Random.Range(-1, 2);

        //sprite animation
        anim.SetInteger("walkSpeed", nextMove);

        //flip sprite
        if (nextMove != 0)
            spriterenderer.flipX = nextMove == 1;

            

        //recursive
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        nextMove *= -1;
        spriterenderer.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 2);
    }
}
