using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Otherball : MonoBehaviour
{
    MeshRenderer mesh; //오브젝트의 재질 접근은 MeshRenderer을 통해서
    Material mat;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mat = mesh.material;
    }

    void OnCollisionEnter(Collision collision) //CollisionEnter : 물리적 충돌이 시작될 때 호출되는 함수
    {
       if (collision.gameObject.name == "My ball")
            mat.color = new Color(0, 0, 0);
    }

    /*
    void OnCollisionStay() //CollisionStay : 물리적 충돌이 지속될 때 호출되는 함수
    {

    }
    */

    void OnCollisionExit(Collision collision) //CollisionExit : 물리적 충돌이 종료될 때 호출되는 함수
    {
        if (collision.gameObject.name == "My ball")
            mat.color = new Color(1, 1, 1);
    }
    

}
