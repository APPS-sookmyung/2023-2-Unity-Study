using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle2 : MonoBehaviour
{
    void Start()
    {
        //오브젝트는 변수 transform을 항상 가지고 있음
        // Vector3 vec = new Vector3(5, 0, 0); //벡터 값
        // transform.Translate(vec); //Translate : 벡터 값을 현재 위치에 더하는 함수

      //  int number = 4; //스칼라 값
    }

    void Update()
    {
        Vector3 vec = new Vector3(
            Input.GetAxis("Horizontal"), 
            Input.GetAxis("Vertical"), 0); //벡터 값
        transform.Translate(vec);
    }
    
    
}
