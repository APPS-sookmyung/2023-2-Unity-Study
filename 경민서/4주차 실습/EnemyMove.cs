using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    Animator anim;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("Think", 5);

    }

    void FixedUpdate()
    {
        // Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        // Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));

        if (rayHit.collider == null)
        {
            Turn();
        }
    }

    // 재귀 함수 
    void Think()
    {
        // Set Next Active
        nextMove = Random.Range(-1, 2);

    
        // Sprite Animation
        anim.SetInteger("WalkSpeed", nextMove);



        // Recursive
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", 5);
    }

    void Turn()
    {
        nextMove = nextMove * -1;
        spriteRenderer.flipX = (nextMove == 1);

        CancelInvoke();
        Invoke("Think", 2);
    }
}
