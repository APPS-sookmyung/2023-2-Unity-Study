using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    void Awake() //�ʱ�ȭ ����
    {
        Debug.Log("�÷��̾� �����Ͱ� �غ�Ǿ����ϴ�.");
    }

    void OnEnable() // Ȱ��ȭ (�ʱ�ȭ�� �������� ����)
    {
        Debug.Log("�÷��̾ �α����߽��ϴ�.");
    }

    void Start() //�ʱ�ȭ ����
    {
        Debug.Log("��� ��� ì����ϴ�.");
    }

    void FixedUpdate() // ���� ���� ������Ʈ : ������ �����ֱ� -> CPU ���� ���
    {
        Debug.Log("�̵�~");
    }

    void Update() // ���� ���� ������Ʈ : ȯ�濡 ���� �����ֱⰡ �޶���
    {
        Debug.Log("���� ���!!"); // 60���������� ����
    }

    void LateUpdate() // ��� ������Ʈ ���� �� (ī�޶�, ��ó��)
    {
        Debug.Log("����ġ ȹ��.");
    }

    void OnDisable() // ���� ������Ʈ�� ��Ȱ��ȭ �Ǿ��� �� (���ӷ����� ��ü ����)
    {
        Debug.Log("�÷��̾ �α׾ƿ��߽��ϴ�.");
    }

    void OnDestroy() // ��ü (���� ������Ʈ�� ������ ��)
    {
        Debug.Log("�÷��̾� �����Ͱ� �����Ͽ����ϴ�.");
    }
}
