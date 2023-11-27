# 8주차 스터디

진도: B25-B26

- [x]  **[탑다운 2D RPG - 대화 애니메이션 느낌있게 만들기 [유니티 기초 강좌 B25]](https://www.youtube.com/watch?v=emGwkkM6bnc&list=PLO-mt5Iu5TeYI4dbYwWP8JqZMC9iuUIW2&index=28&pp=iAQB)**
- [x]  **[탑다운 2D RPG - 서브메뉴와 저장기능 만들기 [유니티 기초 강좌 B26]](https://www.youtube.com/watch?v=mYjUVmws2F0&list=PLO-mt5Iu5TeYI4dbYwWP8JqZMC9iuUIW2&index=29&pp=iAQB)**

# 탑다운 2D RPG - 대화 애니메이션 느낌있게 만들기 [유니티 기초 강좌 B25]

---

## 대화창  이펙트

- 대화창 애니메이션을 위한 애니메이터 컨트롤러 1개, 애니메이션 2개 생성

![Untitled](8%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20dc5d2dd496b9489a98d6824d622b85a5/Untitled.png)

- 대화 UI를 화면 아래로 내려서 안보이게 설정

![Untitled](8%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20dc5d2dd496b9489a98d6824d622b85a5/Untitled%201.png)

---

- 위 아래를 오고 가는 애니메이션 설정
    
    ![Untitled](8%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20dc5d2dd496b9489a98d6824d622b85a5/Untitled%202.png)
    

- 애니메이션 컨트롤을 위한 스트립트 작성
    
    ```jsx
    public void Action(GameObject scanObj)
        {
            // Get Current Object
            scanObject = scanObj;
            ObjectData objData = scanObject.GetComponent<ObjectData>();
            Talk(objData.id, objData.isNpc);
    
            // Visible Talk for Action
            talkPanel.SetBool("isShow", isAction);
        }
    ```
    

---

## 초상화 이펙트

- 표정이 바뀌면 초상화가 위아래로
- 애니메이터, 애니메이션 1개씩 초상화 Image에 생성
- 한번만 실행하는 것이므로 trigger 사용

![Untitled](8%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20dc5d2dd496b9489a98d6824d622b85a5/Untitled%203.png)

```csharp
// Continue Talk
if(isNpc)
{
    talkText.text = talkData.Split(':')[0];
    portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
    portraitImg.color = new Color(1, 1, 1, 1);
    if(prevPortrait != portraitImg.sprite)
    {
        portraitAnim.SetTrigger("doEffect");
        prevPortrait = portraitImg.sprite;
    }
}
```

## 타이핑 이펙트

- 글자가 하나씩 나오게
- 나올때 마다 소리가 나오도록
- 나오는 도중 action을 하면 문장 전체가 한번에 다 나와야 함

```csharp
public class TypeEffect : MonoBehaviour
{
    public int CharPerSeconds;
    public GameObject EndCursor;
    public bool isAnim;

    string targetMsg;
    Text msgText;
    int index;
    float interval;
    AudioSource audioSource;
    

    private void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMsg(string msg)
    {
        if (isAnim)
        {
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }

    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        EndCursor.SetActive(false);

        interval = 1.0f / CharPerSeconds;
        Debug.Log(interval);

        isAnim = true;
        Invoke("Effecting", interval); // 1글자가 나오는 딜레이
    }

    void Effecting()
    {
        if(msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }
        msgText.text += targetMsg[index];
        

        //Sound
        if(targetMsg[index] != ' ' || targetMsg[index] != '.')
            audioSource.Play();

        index++;
        Invoke("Effecting", 1 / CharPerSeconds); // 1글자가 나오는 딜레이

    }

    void EffectEnd()
    {
        isAnim = false;
        EndCursor.SetActive(true);
    }
}
```

- gameManager 에서 대화 중인지 인식해야함

```csharp
void Talk(int id, bool isNpc)
    {
        // Set Talk data
        int questTalkIndex = 0;
        string talkData = "";

        if (talk.isAnim)
        {
            talk.SetMsg("");
            return;
        }    
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }
```

# 탑다운 2D RPG - 서브메뉴와 저장기능 만들기 [유니티 기초 강좌 B26]

---

## UI 구축하기

- 배경이 될 이미지 생성하고 투명한 검은색으로 설정

![Untitled](8%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20dc5d2dd496b9489a98d6824d622b85a5/Untitled%204.png)

## 계속하기 버튼

- ESC를 누르면 On/Off

![Untitled](8%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20dc5d2dd496b9489a98d6824d622b85a5/Untitled%205.png)

```csharp
{
       
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuSet.activeSelf)
                menuSet.SetActive(false);
            else
                menuSet.SetActive(true);
        }
        
    }
```

## 퀘스트 확인 창

![Untitled](8%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20dc5d2dd496b9489a98d6824d622b85a5/Untitled%206.png)

```csharp
void Start()
{
    questText.text = questManager.CheckQuest();
}
```

## 종료하기 버튼

![Untitled](8%E1%84%8C%E1%85%AE%E1%84%8E%E1%85%A1%20%E1%84%89%E1%85%B3%E1%84%90%E1%85%A5%E1%84%83%E1%85%B5%20dc5d2dd496b9489a98d6824d622b85a5/Untitled%207.png)

```csharp
public void GameExit()
{
    Application.Quit();
}
```

## 저장하기 버튼

```csharp
public void GameSave()
{
    PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
    PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
    PlayerPrefs.SetFloat("QuestId", questManager.questId);
    PlayerPrefs.SetFloat("PlayerX", questManager.questActionIndex);

    PlayerPrefs.Save();
}
```

```csharp
public void GameLoad()
{
    if (!PlayerPrefs.HasKey("PlayerX"))
    {
        return;
    }

    float x = PlayerPrefs.GetFloat("PlayerX");
    float y = PlayerPrefs.GetFloat("PlayerY");
    int questId = PlayerPrefs.GetInt("QuestId");
    int questActionIndex = PlayerPrefs.GetInt("PlayerX");

    player.transform.position = new Vector3(x, y, 0);
    questManager.questId = questId;
    questManager.questActionIndex = questActionIndex;
    questManager.ControlObject();
}
```