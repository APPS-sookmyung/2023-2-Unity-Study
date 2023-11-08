# 5주차 스터디

진도: B19-B20

- [x]  **[2D 플랫포머 - 플레이어 피격 이벤트 구현하기 [유니티 기초 강좌 B19]](https://www.youtube.com/watch?v=epZFE5Hpbdc&list=PLO-mt5Iu5TeYI4dbYwWP8JqZMC9iuUIW2&index=21&pp=iAQB)**
- [x]  **[2D 플랫포머 - 스테이지를 넘나드는 게임 완성하기 [유니티 기초 강좌 BE2]](https://www.youtube.com/watch?v=GHUJMXtHKL0&list=PLO-mt5Iu5TeYI4dbYwWP8JqZMC9iuUIW2&index=22&pp=iAQB)**
- [x]  **[탑다운 2D RPG - 도트 타일맵으로 쉽게 준비하기 [유니티 기초 강좌 B20 + 에셋 다운로드]](https://www.youtube.com/watch?v=JY-KFx3OsJo&list=PLO-mt5Iu5TeYI4dbYwWP8JqZMC9iuUIW2&index=23&pp=iAQB)**

# 2D 플랫포머 - 플레이어 피격 이벤트 구현하기 [유니티 기초 강좌 B19]

---

## 함정(가시) 추가

Grid-Spike 타일 맵 추가

tag와 layer로 플레이어, 몬스터, 가시를 구분한다.

## 물리 레이어 설정

![스크린샷 2023-11-08 오후 10.29.39.png](5%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20b212d2d860da40c0a872cb106f90d655/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-11-08_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_10.29.39.png)

Enemy와 Enemy의 레이어가 충돌되지 않도록

PlayerDamaged와 Enemy가 충돌되지 않도록 = 무적효과!

## 몬스터와의 충돌 이벤트

```csharp
void OnCollisionEnter2D(Collision2D collision)
{
    if(collision.gameObject.tag == "Enemy")
    {
        Debug.Log("플레이어가 맞음 ");
    }
}
```

## 무적 시간 설정

```csharp
void OnDamaged(Vector2 targetPos) // 충돌한 위치를 받아옴
    {
        // Change Layer
        gameObject.layer = 11; //Layer 번호

        //View Alpha 맞는 순간 약간 투명하게 전환
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //Reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1; // 플레이어 위치가 충돌 위치보다 오른쪽 : 앞으로 튕김
        rigid.AddForce(new Vector2(dirc, 1)*7, ForceMode2D.Impulse);
    }
```

## 무적 시간 해제

```csharp
void OnDamaged(Vector2 targetPos)
    {
        // Change Layer
        gameObject.layer = 11; //Layer 번호

        //View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //Reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1)*7, ForceMode2D.Impulse);

        //시간 차
        Invoke("OffDamaged", 3);
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
```

## 애니메이션

![스크린샷 2023-11-08 오후 11.04.26.png](5%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20b212d2d860da40c0a872cb106f90d655/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-11-08_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_11.04.26.png)

Any State → exit 현재 상태 상관없이 실행 후 복귀

Trigger : 값이 없다

# 2D 플랫포머 - 스테이지를 넘나드는 게임 완성하기 [유니티 기초 강좌 BE2]

---

## 몬스터 밟아서 잡기

```csharp
void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            // Attack 몬스터보다 위에 있음 + 아래로 낙하 중 = 밟음 
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }
            else // 데미지 입음
            {
                OnDamaged(collision.transform.position);
            }
        }
    }

    void OnAttack( Transform enemy)
    {
        // Point

        // Reaction Force
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        // Enemy Die
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
    }
```

```csharp
public void OnDamaged()
    {
        //Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //Sprite Flip Y
        spriteRenderer.flipY = true;

        //Collider Disable
        boxCollider.enabled = false;

        //Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        //Destroy 5초 뒤에
        Invoke("DeActive", 5);

    }
    void DeActive()
    {
        gameObject.SetActive(false);
    }
```

## 아이템

콜라이더 + 스프라이트 + 애니메이션  작업으로 아이템 구현

```csharp
void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            //Point

            //Deactive Item 충돌하면 사라지도록 
            collision.gameObject.SetActive(false);
        }
    }
```

## 결승점

```csharp
void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            //Point

            //Deactive Item
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Finish")
        {
            //Next Stage : 매니저가 담당 

        }
    }
```

## 게임 매니저 추가

매니저는 점수와 스테이지를 관리

GameManager script 추가, Player에 게임 매니저 추가 

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMove player;

    public void NextStage()
    {
        stageIndex++;

        totalPoint += stagePoint;
        stagePoint = 0;
    }

    public void HealthDown()
    {
        if(health > 1)
        {
            health--;
        }
        else
        {
            //Player Die Effect
            player.OnDie();

            //Result UI
            Debug.Log("died");

            //Retry Button UI
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            //Player Reposition
            if(health > 0)
            {
                collision.attachedRigidbody.velocity = Vector2.zero;
                collision.transform.position = new Vector3(0, 0, -1);
            }

            //Health Down
            HealthDown();

        }
    }
}
```

## 스테이지 추가

![스크린샷 2023-11-09 오전 2.17.57.png](5%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20b212d2d860da40c0a872cb106f90d655/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-11-09_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%258C%25E1%2585%25A5%25E1%2586%25AB_2.17.57.png)

![스크린샷 2023-11-09 오전 2.18.11.png](5%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20b212d2d860da40c0a872cb106f90d655/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-11-09_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%258C%25E1%2585%25A5%25E1%2586%25AB_2.18.11.png)

![스크린샷 2023-11-09 오전 2.18.30.png](5%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20b212d2d860da40c0a872cb106f90d655/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-11-09_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%258C%25E1%2585%25A5%25E1%2586%25AB_2.18.30.png)

## 유저 인터페이스

![스크린샷 2023-11-09 오전 1.35.02.png](5%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20b212d2d860da40c0a872cb106f90d655/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-11-09_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%258C%25E1%2585%25A5%25E1%2586%25AB_1.35.02.png)

## 사운드

- public 으로 선언 후 할당

# 탑다운 2D RPG - 도트 타일맵으로 쉽게 준비하기 [유니티 기초 강좌 B20 + 에셋 다운로드]

---

## 플러그인 설치

[Top-Down 2D RPG Assets Pack](https://assetstore.unity.com/packages/2d/characters/top-down-2d-rpg-assets-pack-188718)

https://github.com/Unity-Technologies/2d-extras/releases

## 룰 타일 만들기

![스크린샷 2023-11-09 오전 3.24.17.png](5%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20b212d2d860da40c0a872cb106f90d655/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-11-09_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%258C%25E1%2585%25A5%25E1%2586%25AB_3.24.17.png)

Create -> 2D -> Tiles 에 보면 Rule Tile

방향규칙을 정한다. 

## 애니메이션 타일

![스크린샷 2023-11-09 오전 3.24.53.png](5%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20b212d2d860da40c0a872cb106f90d655/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-11-09_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%258C%25E1%2585%25A5%25E1%2586%25AB_3.24.53.png)

output = animaition

## 외부 경계 설정

![Untitled](5%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20b212d2d860da40c0a872cb106f90d655/Untitled.png)

Visible inside Mask가 적용되지 않음

## 픽셀 퍼펙트

![스크린샷 2023-11-09 오전 3.26.27.png](5%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20b212d2d860da40c0a872cb106f90d655/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-11-09_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%258C%25E1%2585%25A5%25E1%2586%25AB_3.26.27.png)

적용이 안됨…..