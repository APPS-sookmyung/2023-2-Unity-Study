# 2주차 스터디

진도: B10-BE1(04)

# ✅ 강의 리스트

### 유니티 입문

- [x]  **[힘을 이용하여 물체 움직여보기 [유니티 입문 강좌 B10]](https://www.youtube.com/watch?v=h_MA36TGFsc&list=PLO-mt5Iu5TeYI4dbYwWP8JqZMC9iuUIW2&index=11&pp=iAQB)**
- [x]  **[물리 충돌 이벤트 [유니티 입문 강좌 B11]](https://www.youtube.com/watch?v=salZ7t98xi8&list=PLO-mt5Iu5TeYI4dbYwWP8JqZMC9iuUIW2&index=12&pp=iAQB)**
- [x]  **[게임 인터페이스를 알아봅시다, UGUI 기초 [유니티 입문 강좌 B12]](https://www.youtube.com/watch?v=k4YUJy-otDs&list=PLO-mt5Iu5TeYI4dbYwWP8JqZMC9iuUIW2&index=13&pp=iAQB)**
- [x]  **[기초만 꾹꾹 눌러담은 3D 게임 만들기 [유니티 입문 강좌 BE1]](https://www.youtube.com/watch?v=pTc1dakebow&list=PLO-mt5Iu5TeYI4dbYwWP8JqZMC9iuUIW2&index=14&pp=iAQB)**

# 힘을 이용하여 물체 움직여보기 [유니티 입문 강좌 B10]

---

### Rigidbody 컴포넌트 가져오기

```csharp
rigid = GetComponent<Rigidbody>();
```

### 물체 속도 설정해서 움직이기

Rigidbody 속성은 FixedUpdate에서 다루는 것이 권장됨.

### 물체에 힘을 가해서 움직이기

```csharp
void FixedUpdate()
    {
        rigid.velocity = Vector3.forward;
    }
```

### 버튼 입력으로 움직이기

```csharp
void FixedUpdate()
    {
        if (Input.GetButton("Jump"))
        {
            rigid.AddForce(Vector3.up * 3, ForceMode.Impulse); //윗방향으로 3의 힘만큼
        }

        Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); //방향키 입력 받기

        rigid.AddForce(vec, ForceMode.Impulse);
        
    }
```

### 회전력을 주어 움직이기

```csharp
void FixedUpdate()
    {
       rigid.AddTorque(Vector3.up); //y축 방향을 축으로 삼아 회전

    }
```

# 물리 충돌 이벤트 [유니티 입문 강좌 B11]

---

### 컴포넌트 가져오기

오브젝트의 재질 접근은 MeshRenderer 이용

### 물리충돌 이벤트

Collision : 충돌 정보 클래스

실제 물리적인 충돌로 발생하는 이벤트

OnCollisionEnter/OnCollisionStay/OnCollisionExit 물리적 충돌이 시작/유지/종료 할 때 호출되는 함수

```csharp
public class Otherball : MonoBehaviour
{
    MeshRenderer mesh;
    Material mat;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mat = mesh.material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "MyBall")
            mat.color = new Color(0, 0, 0);   
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "MyBall")
            mat.color = new Color(1, 1, 1);
    }

}
```

### 트리거 이벤트

콜라이더 충돌로 발생하는 이벤트

OnTriggerEnter/OnTriggerStay/OnTriggerExit

```csharp
private void OnTriggerStay(Collider other)
    {
        if (other.name == "Cube")
            rigid.AddForce(Vector3.up * 2, ForceMode.Impulse);
    }
```

# 게임 인터페이스를 알아봅시다, UGUI 기초 [유니티 입문 강좌 B12]

---

### 캔버스

UI가 그려지는 도화지 역할인 컴포넌트

### 스크린

게임이 표시되는 화면, 해상도로 크기 결정

![스크린샷 2023-10-04 오후 9.53.05.png](2%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%209312d5a6749946cebd7fd5c50f47c7f8/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-10-04_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_9.53.05.png)

World 좌표계 : 카메라 포함

Screen 좌표계 : 마우스 커서 포함

### 텍스트

Canvas→Text

![스크린샷 2023-10-04 오후 10.05.10.png](2%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%209312d5a6749946cebd7fd5c50f47c7f8/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-10-04_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_10.05.10.png)

### 이미지

Texture Type = Sprite 로 설정

Image Type = Filed : 쿨타임 표현 가능

### 버튼

```csharp
public void Jump()
    {
        rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }
```

public 으로 선언한 함수가 보임.

![스크린샷 2023-10-04 오후 10.22.42.png](2%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%209312d5a6749946cebd7fd5c50f47c7f8/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-10-04_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_10.22.42.png)

### 앵커

해상도에 따라 UI가 변하지 않게 해줌.

빨간점 = 기준점

파란점: 컴포넌트에서의 기준점 (shift)

![스크린샷 2023-10-04 오후 10.26.48.png](2%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%209312d5a6749946cebd7fd5c50f47c7f8/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-10-04_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_10.26.48.png)

![스크린샷 2023-10-04 오후 10.30.16.png](2%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%209312d5a6749946cebd7fd5c50f47c7f8/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-10-04_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_10.30.16.png)

# 기초만 꾹꾹 눌러담은 3D 게임 만들기 [유니티 입문 강좌 BE1]

---

### 계획하기

1. 이름: 굴러서 템머기
2. 장르: 캐주얼 액션
3. 목표: 지형을 뛰어넘어 굴러서 템을 먹고 목표지점에 도달.
4. 구성: 공(Player), 아이템(Item), 지형(Platform), 결승점(Point)

### 플레이어

기본적인 이동+점프 기능

무한점프 막기

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    public float jumpPower;
    bool isJump;
    Rigidbody rigid;

    void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);

        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Floor")
            isJump = false;
    }
}
```

![스크린샷 2023-10-04 오후 11.04.37.png](2%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%209312d5a6749946cebd7fd5c50f47c7f8/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-10-04_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_11.04.37.png)

### 아이템

- PlayerBall.cs
    
    아이템과 충돌하면 오디오 재생
    
    tag → 같은 종류의 오브젝트를 묶어 이벤트 처리 가능
    
    ```csharp
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    public class PlayerBall : MonoBehaviour
    {
        public float jumpPower;
        bool isJump;
        Rigidbody rigid;
        public int itemCount;
        AudioSource audio;
    
        void Awake()
        {
            isJump = false;
            rigid = GetComponent<Rigidbody>();
            audio = GetComponent<AudioSource>();
    
        }
    
        void Update()
        {
            if (Input.GetButtonDown("Jump") && !isJump)
            {
                isJump = true;
                rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
    
            }
        }
    
        void FixedUpdate()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
        }
    
        void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "Floor")
                isJump = false;
        }
    
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Item")
            {
                itemCount++;
                audio.Play();
                other.gameObject.SetActive(false);
            }
        }
    }
    ```
    
- Item.cs
    
    회전하는 기능
    
    ```csharp
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    public class Item : MonoBehaviour
    {
        public float rotateSpeed;
    
        void Update()
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
        }
    }
    ```
    

![스크린샷 2023-10-04 오후 11.38.20.png](2%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%209312d5a6749946cebd7fd5c50f47c7f8/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-10-04_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_11.38.20.png)

### 카메라

카메라가 플레이어를 따라가도록 만들기

LateUpdate : 업데이트 이후 실행, 카메라, UI 가 주로 해당됨.

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    Transform playerTransform;
    Vector3 Offset;

    // Start is called before the first frame update
    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Offset = transform.position - playerTransform.position; // 고정값
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = playerTransform.position + Offset;
    }
}
```