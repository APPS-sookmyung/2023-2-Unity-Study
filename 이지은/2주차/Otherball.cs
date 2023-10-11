using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Otherball : MonoBehaviour
{
    MeshRenderer mesh; //������Ʈ�� ���� ������ MeshRenderer�� ���ؼ�
    Material mat;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mat = mesh.material;
    }

    void OnCollisionEnter(Collision collision) //CollisionEnter : ������ �浹�� ���۵� �� ȣ��Ǵ� �Լ�
    {
       if (collision.gameObject.name == "My ball")
            mat.color = new Color(0, 0, 0);
    }

    /*
    void OnCollisionStay() //CollisionStay : ������ �浹�� ���ӵ� �� ȣ��Ǵ� �Լ�
    {

    }
    */

    void OnCollisionExit(Collision collision) //CollisionExit : ������ �浹�� ����� �� ȣ��Ǵ� �Լ�
    {
        if (collision.gameObject.name == "My ball")
            mat.color = new Color(1, 1, 1);
    }
    

}
