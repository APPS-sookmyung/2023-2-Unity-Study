# 3주차 스터디

### 유니티 입문

- [x]  **[기초만 꾹꾹 눌러담은 3D 게임 만들기 [유니티 입문 강좌 BE1]](https://www.youtube.com/watch?v=pTc1dakebow&list=PLO-mt5Iu5TeYI4dbYwWP8JqZMC9iuUIW2&index=14&pp=iAQB)**

### **2D 플랫포머**

- [x]  **[2D 플랫포머 - 프로젝트 준비하기 [유니티 기초 강좌 B13 + 에셋 다운로드]](https://www.youtube.com/watch?v=v_Y5FH_tCpc&list=PLO-mt5Iu5TeYI4dbYwWP8JqZMC9iuUIW2&index=15&pp=iAQB)**
- [x]  **[2D 플랫포머 - 아틀라스와 애니메이션 [유니티 기초 강좌 B14]](https://www.youtube.com/watch?v=IkvYstCzcoc&list=PLO-mt5Iu5TeYI4dbYwWP8JqZMC9iuUIW2&index=16&pp=iAQB)**
- [ ]  **[2D 플랫포머 - 플레이어 이동 구현하기 [유니티 기초 강좌 B15]](https://www.youtube.com/watch?v=Z4iULRbiSTg&list=PLO-mt5Iu5TeYI4dbYwWP8JqZMC9iuUIW2&index=17&pp=iAQB)**

# 기초만 꾹꾹 눌러담은 3D 게임 만들기 [유니티 입문 강좌 BE1]

---

### 결승점

게임 매니저 : 형태가 없고 전반적인 로직을 가진 오브젝트

Find 함수는 부하를 초래할 수 있으므로 비권장

- GameManager.cs
    
    ```jsx
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    public class GameManager : MonoBehaviour
    {
        public int totallItemCount;
    
      
    }
    ```
    
- PlayerBall.cs
    
    ```jsx
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    public class PlayerBall : MonoBehaviour
    {
        public float jumpPower;
        bool isJump;
        Rigidbody rigid;
        public int itemCount;
        AudioSource audioSource;
        public GameManager manager;
    
        void Awake()
        {
            isJump = false;
            rigid = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
    
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
                GetComponent<AudioSource>().Play();
                other.gameObject.SetActive(false);
            }
            else if (other.tag == "Finished")
            {
                if(itemCount == manager.totallItemCount)
                {
                    //Game Clear
                }
                else
                {
                    //Restart
                }
              
            }
        }
    }
    ```
    

### 장면이동

`using UnityEngine.SceneManagement;`

장면을 관리하는 기본 클래스

`SceneManager.LoadScene("SampleScene");`

장면 불러오기

Scene을 불러오려면 꼭 Build Setting에서 추가하기!

- GameManager.cs
    
    ```jsx
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    public class GameManager : MonoBehaviour
    {
        public int totallItemCount;
        public int stage;
      
    }
    ```
    
- PlayerBall.cs
    
    ```jsx
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    
    public class PlayerBall : MonoBehaviour
    {
        public float jumpPower;
        bool isJump;
        Rigidbody rigid;
        public int itemCount;
        AudioSource audioSource;
        public GameManager manager;
    
        void Awake()
        {
            isJump = false;
            rigid = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
    
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
                GetComponent<AudioSource>().Play();
                other.gameObject.SetActive(false);
            }
            else if (other.tag == "Finished")
            {
                if(itemCount == manager.totallItemCount)
                {
                    //Game Clear
                    SceneManager.LoadScene("Example1_" + (manager.stage + 1).ToString());
                }
                else
                {
                    //Restart
                    SceneManager.LoadScene("Example1_" + manager.stage.ToString());
                }
              
            }
        }
    }
    ```
    

### 스테이지 추가

### 유저 인터페이스

UI 로 Text 추가

```jsx
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalItemCount;
    public int stage;
    public Text stageCountText;
    public Text playerCountText;

    void Awake()
    {
        stageCountText.text = "/" + totalItemCount;
    }

    public void GetItem(int count)
    {
        playerCountText.text = count.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            SceneManager.LoadScene(stage);
    }

}
```

### 빌드

→ build setting

# 2D 플랫포머 - 프로젝트 준비하기 [유니티 기초 강좌 B13 + 에셋 다운로드]

---

### 00:00 프로젝트 생성

### 02:22 스프라이트

스프라이츠 = 2D 오브젝트

### 03:49 렌더 순서

순서변경

- z축 이용
    - 에디터에서 depth가 느껴진다.
    - Scene에서 바로 볼 수 있어 직관적임.
- ordering layer 숫자가 클수록 앞으로 온다.

### 11:11 픽셀아트(도트) 조정

필터모드 = 포인트

컴프레션(압축률) = None

이미지 크기로 pixels per unit 설정하면 이미지가 그리드 한칸으로 딱 맞게됨

![스크린샷 2023-10-11 오후 10.07.18.png](3%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20cffbf9d3f2184b9d9008114ff8b5a1fc/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-10-11_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_10.07.18.png)

### 19:17 물리 적용

이미지 여백 제거

![스크린샷 2023-10-11 오후 10.20.53.png](3%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20cffbf9d3f2184b9d9008114ff8b5a1fc/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-10-11_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_10.20.53.png)

# 2D 플랫포머 - 아틀라스와 애니메이션 [유니티 기초 강좌 B14]

### 00:00 아틀라스 자르기

![스크린샷 2023-10-11 오후 10.23.40.png](3%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20cffbf9d3f2184b9d9008114ff8b5a1fc/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-10-11_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_10.23.40.png)

스프라이트 모드 = 멀티플

![스크린샷 2023-10-11 오후 10.24.22.png](3%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20cffbf9d3f2184b9d9008114ff8b5a1fc/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-10-11_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_10.24.22.png)

![스크린샷 2023-10-11 오후 10.25.38.png](3%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20cffbf9d3f2184b9d9008114ff8b5a1fc/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-10-11_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_10.25.38.png)

Cell By Size 입력한 사이즈대로 균일하게 자르는 모드, 여백을 포함하도록 만듦

Cell by Count 입력한 갯수대로 균일하게 자르는 모드

![스크린샷 2023-10-11 오후 10.27.02.png](3%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20cffbf9d3f2184b9d9008114ff8b5a1fc/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-10-11_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_10.27.02.png)

아틀라스의 스프라이트 끼리는 Batch가 늘지 않음.

### 10:47 스프라이트 애니메이션

애니메이션 컨트롤러(Animator), 애니메이션 파일 생성

![스크린샷 2023-10-11 오후 10.36.13.png](3%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20cffbf9d3f2184b9d9008114ff8b5a1fc/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-10-11_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_10.36.13.png)

### 17:45 애니메이터

![스크린샷 2023-10-11 오후 10.40.52.png](3%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20cffbf9d3f2184b9d9008114ff8b5a1fc/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-10-11_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_10.40.52.png)

애니메이터 컨트롤러

애니메이션 속도조절

- 애니메이션 키프레임 조작
- 인스펙터에서 조절
    
    ![스크린샷 2023-10-11 오후 10.44.19.png](3%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20cffbf9d3f2184b9d9008114ff8b5a1fc/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-10-11_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_10.44.19.png)
    

# 2D 플랫포머 - 플레이어 이동 구현하기 [유니티 기초 강좌 B15]

---

### 00:00 물리력에 의한 이동

rigidbody 설정

collider 설정

마찰력 = 0 으로 설정

```jsx
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed) //right max speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1)) //left max speed
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

    }
}
```

### 2:03 저항 수치 설정

Linear Drag 공기 저항, 이동 시 속도를 느리게 해줌

![Untitled](3%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20cffbf9d3f2184b9d9008114ff8b5a1fc/Untitled.png)

플레이어의 방향? = 단위벡터 이용하기

```jsx
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //stop speed
        if (Input.GetButtonUp("Horizontal"))
        {
            
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed) //right max speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1)) //left max speed
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

    }
}
```

![스크린샷 2023-10-11 오후 11.11.29.png](3%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20cffbf9d3f2184b9d9008114ff8b5a1fc/%25E1%2584%2589%25E1%2585%25B3%25E1%2584%258F%25E1%2585%25B3%25E1%2584%2585%25E1%2585%25B5%25E1%2586%25AB%25E1%2584%2589%25E1%2585%25A3%25E1%2586%25BA_2023-10-11_%25E1%2584%258B%25E1%2585%25A9%25E1%2584%2592%25E1%2585%25AE_11.11.29.png)

회전 억제

### 20:55 애니메이션 순환

문워크 현상 → 스프라이트 뒤집기

![Untitled](3%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20cffbf9d3f2184b9d9008114ff8b5a1fc/Untitled%201.png)

```jsx
private void Update()
    {
         //방향 전환
        if (Input.GetButtonDown("Horizontal"))
            spriteRenderer.flipX = (Input.GetAxisRaw("Horizontal") == -1);
    }
```

has exit time: 애니메이션이 끝날 때까지 상태를 유지

![Untitled](3%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20cffbf9d3f2184b9d9008114ff8b5a1fc/Untitled%202.png)

```jsx
private void Update()
    {
        //애니메이션
				if(Mathf.Abs(rigid.velocity.x) < 0.3)        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }
    }
```