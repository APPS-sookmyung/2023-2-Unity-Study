using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Myball : MonoBehaviour
{
    Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        // rigid.AddForce(Vector3.up * 50, ForceMode.Impulse); 
    }

    // Update is called once per frame
    void FixedUpdate() //물리엔진 관련 코드 작성
    {
        // rigid.velocity = new Vector3(2, 4, 3); //1. 속력바꾸기

        //2. 힘을 가하기
        
        if (Input.GetButtonDown("Jump"))
        {
            rigid.AddForce(Vector3.up * 25, ForceMode.Impulse);
            Debug.Log(rigid.velocity);
        }

        Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal"), 0,
            Input.GetAxisRaw("Vertical"));

        rigid.AddForce(vec, ForceMode.Impulse);
        

        //3. 회전력 AddTorque(vec) : vec 방향을 축으로 회전력이 생김
        // rigid.AddTorque(Vector3.back);
    }
    void OnTriggerStay(Collider other) //TriggerStay : 콜라이더가 계속 충돌하고 있을 때 호출
    {
        if (other.name == "Cube")
            rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }

    public void Jump()
    {
        rigid.AddForce(Vector3.up * 20, ForceMode.Impulse);
    }
}
