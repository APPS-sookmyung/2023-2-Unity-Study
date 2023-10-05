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
    void FixedUpdate() //�������� ���� �ڵ� �ۼ�
    {
        // rigid.velocity = new Vector3(2, 4, 3); //1. �ӷ¹ٲٱ�

        //2. ���� ���ϱ�
        
        if (Input.GetButtonDown("Jump"))
        {
            rigid.AddForce(Vector3.up * 25, ForceMode.Impulse);
            Debug.Log(rigid.velocity);
        }

        Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal"), 0,
            Input.GetAxisRaw("Vertical"));

        rigid.AddForce(vec, ForceMode.Impulse);
        

        //3. ȸ���� AddTorque(vec) : vec ������ ������ ȸ������ ����
        // rigid.AddTorque(Vector3.back);
    }
    void OnTriggerStay(Collider other) //TriggerStay : �ݶ��̴��� ��� �浹�ϰ� ���� �� ȣ��
    {
        if (other.name == "Cube")
            rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }

    public void Jump()
    {
        rigid.AddForce(Vector3.up * 20, ForceMode.Impulse);
    }
}
