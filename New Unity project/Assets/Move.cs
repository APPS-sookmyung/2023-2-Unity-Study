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
                                 , target, 2f);  //MoveToward 매개변수 : 현재위치, 목표위치, 속도
   

        //2.SmoothDamp (속도 값이 작을수록 빠름)
         Vector3 velo = Vector3.up * 50;

        transform.position =
            Vector3.SmoothDamp(transform.position
                            , target, ref velo, 0.1f); //ref : 참조 접근 -> 실시간으로 바뀌는 값 적용 가능
    

        //3.Lerp (선형 보간)
         transform.position =
             Vector3.Lerp(transform.position
                             , target, 1f);
        

        //4.SLerp (구면 선형 보간, 호를 그리며 이동)
        transform.position =
            Vector3.Slerp(transform.position
                            , target, 0.1f);

    }
}
