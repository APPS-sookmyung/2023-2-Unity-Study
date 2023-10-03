using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    void Awake() //초기화 영역
    {
        Debug.Log("플레이어 데이터가 준비되었습니다.");
    }

    void OnEnable() // 활성화 (초기화와 물리엔진 사이)
    {
        Debug.Log("플레이어가 로그인했습니다.");
    }

    void Start() //초기화 영역
    {
        Debug.Log("사냥 장비를 챙겼습니다.");
    }

    void FixedUpdate() // 물리 연산 업데이트 : 고정된 실행주기 -> CPU 많이 사용
    {
        Debug.Log("이동~");
    }

    void Update() // 게임 로직 업데이트 : 환경에 따라 실행주기가 달라짐
    {
        Debug.Log("몬스터 사냥!!"); // 60프레임으로 실행
    }

    void LateUpdate() // 모든 업데이트 끝난 후 (카메라, 후처리)
    {
        Debug.Log("경험치 획득.");
    }

    void OnDisable() // 게임 오브젝트가 비활성화 되었을 때 (게임로직과 해체 사이)
    {
        Debug.Log("플레이어가 로그아웃했습니다.");
    }

    void OnDestroy() // 해체 (게임 오브젝트가 삭제될 때)
    {
        Debug.Log("플레이어 데이터가 해제하였습니다.");
    }
}
