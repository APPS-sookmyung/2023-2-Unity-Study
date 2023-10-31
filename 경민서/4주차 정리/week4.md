# 4주차 스터디

- [x]  **[2D 플랫포머 - 플레이어 점프 구현하기 [유니티 기초 강좌 B16]](https://www.youtube.com/watch?v=2SikOdH7xvQ&list=PLO-mt5Iu5TeYI4dbYwWP8JqZMC9iuUIW2&index=18&pp=iAQB)**
- [x]  **[2D 플랫포머 - 타일맵으로 플랫폼 만들기 [유니티 기초 강좌 B17]](https://www.youtube.com/watch?v=f8ixw9IpnD8&list=PLO-mt5Iu5TeYI4dbYwWP8JqZMC9iuUIW2&index=19&pp=iAQB)**
- [ ]  **[2D 플랫포머 - 몬스터 AI 구현하기 [유니티 기초 강좌 B18]](https://www.youtube.com/watch?v=7MYUOzgZTf8&list=PLO-mt5Iu5TeYI4dbYwWP8JqZMC9iuUIW2&index=20&pp=iAQB)**

# 2D 플랫포머 - 플레이어 점프 구현하기 [유니티 기초 강좌 B16]

---

## 01 물리력에 의한 점프

```jsx
// Jump
if (Input.GetButtonDown("Jump"))
    rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
```

### 중력 바꾸기

1. Project setting > Physics 2D 에서 중력 값 설정 가능
2. RigidBody > Gravity Scale ; 오브젝트에 적용되는 중력

## 02 점프-착지 애니메이션

sprites → Player로 끌어당기기

Animator에서 변수 설정 isJumping

transition 만들기

## 03 레이캐스트

### Raycast

오브젝트 검색을 위해 Ray(빛)를 쏘는 방식

Debug.DrawRay() 에디터 상에서만 ray를 그려주는 함수

![Untitled](4%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%209295f3c1c2da4fd5833fce45b880e4ce/Untitled.png)

RaycastHit : Ray에 닿은 오브젝트

RaycastHit 변수의 콜라이더로 검색 확인 가능

![스크린샷 2023-10-31 오후 8.01.36.png](4%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%209295f3c1c2da4fd5833fce45b880e4ce/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-10-31_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_8.01.36.png)

```jsx
// landing PLatform
        if(rigid.velocity.y < 0)
        {

            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    anim.SetBool("isJumping", false);
                }

            }
        }
```

## 04 무한 점프 막기

```jsx
// Jump
if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
{
    rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    anim.SetBool("isJumping", true);
}
```

# 2D 플랫포머 - 타일맵으로 플랫폼 만들기 [유니티 기초 강좌 B17]

---

## 01 타일 팔레트

Tile Palette 타일을 사용하기 위해 모아둔 프리펩

![스크린샷 2023-10-31 오후 9.02.57.png](4%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%209295f3c1c2da4fd5833fce45b880e4ce/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-10-31_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_9.02.57.png)

## 02 타일 맵

![스크린샷 2023-10-31 오후 9.33.53.png](4%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%209295f3c1c2da4fd5833fce45b880e4ce/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-10-31_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_9.33.53.png)

## 03 물리 적용

### Tilemap Colllider 2D

![스크린샷 2023-10-31 오후 9.33.29.png](4%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%209295f3c1c2da4fd5833fce45b880e4ce/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-10-31_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_9.33.29.png)

## 04 물리 모양 설정

### Sprites editor → 설정했는데, 잘 안됨..

![스크린샷 2023-10-31 오후 9.43.08.png](4%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%209295f3c1c2da4fd5833fce45b880e4ce/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-10-31_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_9.43.08.png)

# 2D 플랫포머 - 몬스터 AI 구현하기 [유니티 기초 강좌 B18]

---

## 01 준비하기

## 02 기본 이동

### 기본 왼쪽 움직임

```jsx
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(-1, rigid.velocity.y);
    }
}
```

## 03 행동 설정

## 어떤 행동을 하는지 생각

### Random 클래스

```jsx
// 재귀 함수 
    void Think()
    {
        int random = Random.Range(-1, 2);

        Invoke("Think", 5);
    }
```

## 04 지능 높이기

CancelInvoke 현재 작동중인 Invoke 정지

```jsx
void FixedUpdate()
{
    // Move
    rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

    // Platform Check
    Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
    Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

    RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));

    if (rayHit.collider == null)
    {
        nextMove = nextMove * -1;
        CancelInvoke();
        Invoke("Think", 5);
    }
}
```

## 05 애니메이션

이동 방향에 따라 보는 방향 바꾸기 = FlipX

```jsx
// 재귀 함수 
void Think()
{
    // Set Next Active
    nextMove = Random.Range(-1, 2);

    // Sprite Animation
    anim.SetInteger("WalkSpeed", nextMove);

    // Recursive
    float nextThinkTime = Random.Range(2f, 5f);
    Invoke("Think", 5);
}

void Turn()
{
    nextMove = nextMove * -1;
    spriteRenderer.flipX = (nextMove == 1);

    CancelInvoke();
    Invoke("Think", 2);
}
```