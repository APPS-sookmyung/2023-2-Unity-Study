using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float Speed;
    public GameManager manager;

    float h;
    float v;
    bool isHorizonMove;
    Vector3 dirVec;
    GameObject scanObject;

    Rigidbody2D rigid;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // move value
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        // check button down & up
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");

        // check horizontal move
        if (hDown)
            isHorizonMove = true; // 수평 이동 
        else if (vDown)
            isHorizonMove = false; // 수직 이동
        else if (hUp || vUp) // 버튼 업에 대한 처리 -> 현재 AxisRaw 값에 따라 수평, 수직 판단
            isHorizonMove = h != 0;

        // Animation
        if (anim.GetInteger("hAxisRaw") != h){
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v) {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else 
            anim.SetBool("isChange", false);
        //h, v가 float 형이어서 명시적 형변환

        // Direction
        if (vDown && v == 1) //상
            dirVec = Vector3.up;

        else if (vDown && v == -1) //하
            dirVec = Vector3.down;

        else if (hDown && h == -1) //좌
            dirVec = Vector3.left;

        else if (hDown && h == 1) //우
            dirVec = Vector3.right;

        // scan Object
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            manager.Action(scanObject);
        }

    }

    void FixedUpdate()
    {
        // move
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;

        // Ray
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
            scanObject = null;
    }
}
