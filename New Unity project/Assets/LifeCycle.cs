using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle : MonoBehaviour
{
   
    void Update()
    {
        if (Input.anyKeyDown) // ���� �� �Է��� �����ϴ� Ŭ���� (anyKeyDown : �ƹ� �Է��� ���ʷ� ���� �� true)
            Debug.Log("�÷��̾ �ƹ� Ű�� �������ϴ�.");

        //if (Input.anyKey)
        //Debug.Log("�÷��̾ �ƹ� Ű�� ������ �ֽ��ϴ�.");

        // key, Mouse : down, stay, up

        // GetKey : Ű���� ��ư �Է��� ������ true
        if (Input.GetKeyDown(KeyCode.Return)) // Return : Enter
            Debug.Log("�������� �����Ͽ����ϴ�.");

        if (Input.GetKey(KeyCode.LeftArrow))
            Debug.Log("�������� �̵� ��.");

        if (Input.GetKeyUp(KeyCode.RightArrow))
            Debug.Log("������ �̵��� ���߾����ϴ�.");

        //GetMouse : ���콺 ��ư �Է��� ������ true
        // 0 : ���콺 ���� ��ư
        if (Input.GetMouseButtonDown(0))
            Debug.Log("�̻��� �߻�!");

        if (Input.GetMouseButton(0))
            Debug.Log("�̻��� ������ ��.....");

        if (Input.GetMouseButtonUp(0))
            Debug.Log("���� �̻��� �߻�!!!");

        //GetButton : Input ��ư �Է��� ������ true
        if (Input.GetButtonDown("Jump"))
            Debug.Log("����!");

        if (Input.GetButton("Jump"))
            Debug.Log("���� ������ ��......");

        if (Input.GetButtonUp("Jump"))
            Debug.Log("���� ����!!!");

        if (Input.GetButton("Horizontal")) 
            Debug.Log("Ⱦ �̵� ��......" + Input.GetAxisRaw("Horizontal")); //GetAxisRaw : ����ġ ���� ���� -1, ������ 1 ��ȯ

        if (Input.GetButton("Vertical"))
            Debug.Log("�� �̵� ��......" + Input.GetAxisRaw("Verticalz"));
    }
}
