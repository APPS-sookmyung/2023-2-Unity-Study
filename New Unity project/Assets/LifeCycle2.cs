using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle2 : MonoBehaviour
{
    void Start()
    {
        //������Ʈ�� ���� transform�� �׻� ������ ����
        // Vector3 vec = new Vector3(5, 0, 0); //���� ��
        // transform.Translate(vec); //Translate : ���� ���� ���� ��ġ�� ���ϴ� �Լ�

      //  int number = 4; //��Į�� ��
    }

    void Update()
    {
        Vector3 vec = new Vector3(
            Input.GetAxis("Horizontal"), 
            Input.GetAxis("Vertical"), 0); //���� ��
        transform.Translate(vec);
    }
    
    
}
