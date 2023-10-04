using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Vector3 target = new Vector3(8, 1.5f, 0);

    void Update()
    {
        //1.MoveTowards
        transform.position =
            Vector3.MoveTowards(transform.position
                                 , target, 2f);  //MoveToward �Ű����� : ������ġ, ��ǥ��ġ, �ӵ�
   

        //2.SmoothDamp (�ӵ� ���� �������� ����)
         Vector3 velo = Vector3.up * 50;

        transform.position =
            Vector3.SmoothDamp(transform.position
                            , target, ref velo, 0.1f); //ref : ���� ���� -> �ǽð����� �ٲ�� �� ���� ����
    

        //3.Lerp (���� ����)
         transform.position =
             Vector3.Lerp(transform.position
                             , target, 1f);
        

        //4.SLerp (���� ���� ����, ȣ�� �׸��� �̵�)
        transform.position =
            Vector3.Slerp(transform.position
                            , target, 0.1f);

    }
}
