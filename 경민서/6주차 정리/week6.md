# 6주차 스터디

- [x]  **[탑다운 2D RPG - 쯔꾸르식 액션 구현하기 [유니티 기초 강좌 B21]](https://www.youtube.com/watch?v=bZVa6C6vRBQ&list=PLO-mt5Iu5TeYI4dbYwWP8JqZMC9iuUIW2&index=24&pp=iAQB)**
- [ ]  **[탑다운 2D RPG - 대화창 UI 구축하기 [유니티 기초 강좌 B22]](https://www.youtube.com/watch?v=jVBiGhlwhZg&list=PLO-mt5Iu5TeYI4dbYwWP8JqZMC9iuUIW2&index=25&pp=iAQB)**

- pixel perpect → 최선..
    
    ![Untitled](6%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%201b3875e95c0347d6afc9c1be851f15e1/Untitled.png)
    

# 탑다운 2D RPG - 쯔꾸르식 액션 구현하기 [유니티 기초 강좌 B21]

---

## 플레이어 십자 이동

- 쯔꾸르 게임 같은 느낌이 나려면 대각선 이동을 제한
- h, v 방향키를 동시에 눌렀다가 하나를 때면 플레이어가 멈춤

```csharp
void Update()
{
    // Move Value
    h = Input.GetAxisRaw("Horizontal");
    v = Input.GetAxisRaw("Vertical");

    // Check Button Down & Up
    bool hDown = Input.GetButtonDown("Horizontal");
    bool vDown = Input.GetButtonDown("Vertical");
    bool hUp = Input.GetButtonUp("Horizontal");
    bool vUp = Input.GetButtonUp("Vertical");

    // Check Horizontal Move
    if (hDown || vUp)
        isHorizonMove = true;
    else if (vDown || hUp)
        isHorizonMove = false;

}

void FixedUpdate()
{
    // Move
    Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
    rigid.velocity = moveVec * Speed;
}
```

## 애니메이션 설정

- 방향 전환이 바로 되도록 Any State 사용
- Transition을 연속적으로 태우면 애니메이션이 작동되지 않음

![Untitled](6%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%201b3875e95c0347d6afc9c1be851f15e1/Untitled%201.png)

```csharp
void Update()
{
    // Animation
    if(anim.GetInteger("hAxisRaw") != h){
        anim.SetBool("isChange", true);
        anim.SetInteger("hAxisRaw", (int)h);
    }
    else if (anim.GetInteger("vAxisRaw") != v){
        anim.SetBool("isChange", true);
        anim.SetInteger("vAxisRaw", (int)v);
    }
    else
    {
        anim.SetBool("isChange", false);
    }
} 
```

## 조사 액션

![Untitled](6%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%201b3875e95c0347d6afc9c1be851f15e1/Untitled%202.png)

- ray 그리기
- 오브젝트에 콜라이더 설정해야함!
- 조사 가능한 오브젝트를 다른 Layer로 설정

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
 
  GameObject scanObject;

  void Update()
  {
      //Sacn Object
      if (Input.GetButtonDown("Jump") && scanObject != null) //space bar
      {
          Debug.Log(scanObject.name);
      }

  }
	
  void FixedUpdate()
  {
			 //Ray
      Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
      RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object")); 

      if(rayHit.collider != null)
          scanObject = rayHit.collider.gameObject;
      else
          scanObject = null;
  }
}
```

# 탑다운 2D RPG - 대화창 UI 구축하기 [유니티 기초 강좌 B22]

---

## 대화창 UI

- 이미지 왜곡 방지
    - 이미지 타입 = slice
    - sprite edit 먼저 필요
        - 변하지 않는 영역 border 값 입력
        
        ![Untitled](6%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%201b3875e95c0347d6afc9c1be851f15e1/Untitled%203.png)
        
- 대화창 위치 설정

![Untitled](6%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%201b3875e95c0347d6afc9c1be851f15e1/Untitled%204.png)

## 데이터 전달

- 대화창에 조사 오브젝트 넣기

```csharp
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text talkText;
    public GameObject scanObject;

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        talkText.text = "이것의 이름은 " + scanObject.name + "이라고 한다.";
    }
}
```

```csharp
public GameManager manager;

//Sacn Object
if (Input.GetButtonDown("Jump") && scanObject != null) //space bar
{
    manager.Action(scanObject);
}
```

![Untitled](6%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%201b3875e95c0347d6afc9c1be851f15e1/Untitled%205.png)

## 상태 전환

- 대화창이 항상 떠있지 않게
- 대화할 때 플레이어가 움직여서 이탈할 수도 있음
    - 상태변수, flag 를 사용하여 플레이어 이동을 제한

```csharp
public void Action(GameObject scanObj)
    {
        if (isAction) //Exit Action
        {
            isAction = false;
        }
        else //Enter Action
        {
            isAction = true;
            scanObject = scanObj;
            talkText.text = "이것의 이름은 " + scanObject.name + "이라고 한다.";
        }
        talkPanel.SetActive(isAction);

    }
```

```csharp
void Update()
{
    // Move Value
    h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
    v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

    // Check Button Down & Up
    bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal");
    bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical");
    bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal");
    bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");
}
```

## UI 애니메이션

- set native sixe: 원본 크기대로 이미지 사이즈를 맞춤
- 커서가 위아래로 움직이도록
- 애니메이션 적용된 오브젝트를 선택하면 애니메이션 편집 가능
- 앵커 적용된 UI는 Anchored Position 선택
- 반복성 애니메이션은 파일을 클릭해서 Loop Time 체크

![Untitled](6%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%201b3875e95c0347d6afc9c1be851f15e1/Untitled%206.png)