# 7주차 스터디

- [ ]  **[탑다운 2D RPG - 대화 시스템 구현하기 [유니티 기초 강좌 B23]](https://www.youtube.com/watch?v=qJjfYvEYKiE&list=PLO-mt5Iu5TeYI4dbYwWP8JqZMC9iuUIW2&index=26&pp=iAQB)**
- [ ]  **[탑다운 2D RPG - 퀘스트 시스템 구현하기 [유니티 기초 강좌 B24]](https://www.youtube.com/watch?v=RwndWebxbmo&list=PLO-mt5Iu5TeYI4dbYwWP8JqZMC9iuUIW2&index=27&pp=iAQB)**

# 탑다운 2D RPG - 대화 시스템 구현하기 [유니티 기초 강좌 B23]

---

## 오브젝트 관리

id와 isNpc로 관리

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "안녕", "이 곳에 처음 왔구나?" });
        talkData.Add(100, new string[] { "평범한 나무상자다." });
        talkData.Add(200, new string[] { "누군가 사용했던 흔적이 있는 책상이다." });

    }
    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }
}
```

## 대화 시스템

대화 하나에는 여러문장이 들어있으므로 string[] 배열을 사용

id 로 대화 Get → talkData로 대화의 한 문장을 Get

대화가 모두 끝나야 액션이 끝나도록 설정을 바꿔야 함

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "안녕", "이 곳에 처음 왔구나?" });
        talkData.Add(2000, new string[] { "여어", "이 호수는 정말 아름답지?", "사실 이 호수에는 무언가의 비밀이 숨겨져 있다고 해." });
        talkData.Add(100, new string[] { "평범한 나무 상자이다." });
        talkData.Add(200, new string[] { "누군가 사용한 흔적이 보이는 책상이다." });

    }
    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
```

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction = false;
    public int talkIndex;

    public void Action(GameObject scanObj)
    {
       
        scanObject = scanObj;
        ObjectData objData = scanObject.GetComponent<ObjectData>();
        Talk(objData.id, objData.isNpc);

        
        talkPanel.SetActive(isAction);

    }

    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }

        if(isNpc)
        {
            talkText.text = talkData;
        }
        else
        {
            talkText.text = talkData;
        }

        isAction = true;
        talkIndex++;

    }
}
```

## 초상화

보통 대화창과 NPC의 초상화가 같이 뜬다. 

스프라이트 배열을 생성하여 id와 함께 딕셔너리 생성

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "안녕?:0", "이 곳에 처음 왔구나?:1" });
        talkData.Add(2000, new string[] { "여어:1", "이 호수는 정말 아름답지?:0", "사실 이 호수에는 무언가의 비밀이 숨겨져 있다고 해.:1" });
        talkData.Add(100, new string[] { "평범한 나무 상자이다." });
        talkData.Add(200, new string[] { "누군가 사용한 흔적이 보이는 책상이다." });

        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
    }
    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
```

```csharp
void Talk(int id, bool isNpc)
{
    string talkData = talkManager.GetTalk(id, talkIndex);

    if(talkData == null)
    {
        isAction = false;
        talkIndex = 0;
        return;
    }

    if(isNpc)
    {
        talkText.text = talkData.Split(':')[0];
        portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
        portraitImg.color = new Color(1, 1, 1, 1);
    }
    else
    {
        talkText.text = talkData;
        portraitImg.color = new Color(1, 1, 1, 0);
    }

    isAction = true;
    talkIndex++;

}
```

# 탑다운 2D RPG - 퀘스트 시스템 구현하기 [유니티 기초 강좌 B24]

---

## 퀘스트 대화

int[] 에는 해당 퀘스트에 연관된 NPC id 를 입력

NPC id를 받고 퀘스트 번호를 반환하는 함수 생성

퀘스트 매니저를 변수로 생성 후 , 퀘스트 번호를 가져옴

퀘스트번호 + NPC id = 퀘스트 대화 데이터 id

퀘스트 번호 + 퀘스트 대화순서 = 퀘스트 대화 id

대화 진행을 위해 퀘스트 대화순서를 올리는 함수 생성

순서에 맞게 대화를 했을 때만 퀘스트 대화 순서를 올리도록 작성

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("마을 사람들과 대화하기", new int[] { 1000, 2000 }));

    }
    public int GetQuestTalkIndex(int id)
    {
        //퀘스트 번호 + 퀘스트 대화순서 = 퀘스트 대화 id
        return questId + questActionIndex;
    }
    public void CheckQuest(int id)
    {
        if(id == questList[questId].npcId[questActionIndex])
            questActionIndex++; 
    }
}
```

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        // Talk Data
        // NPC A: 1000, NPC B: 2000
        // Box:100, Desk: 200
        talkData.Add(1000, new string[] { "안녕?:0", "이 곳에 처음 왔구나?:1", "한번 둘러보도록해.:0" });
        talkData.Add(2000, new string[] { "여어.:1", "이 호수는 정말 아름답지?:0", "사실 이 호수에는 무언가의 비밀이 숨겨져 있다고 해.:1" });
        talkData.Add(100, new string[] { "평범한 나무 상자이다." });
        talkData.Add(200, new string[] { "누군가 사용한 흔적이 보이는 책상이다." });

        //Quest Talk
        talkData.Add(10 + 1000, new string[] { "어서 와:0", "이 마을에 놀라운 전설이 있다는데:1", "오른쪽 호수 쪽에 루도가 알려줄거야.:0" });
        talkData.Add(11 + 2000, new string[] { "여어:0", "이 호수의 전설을 들으러 온거야?:1", "그럼 일 좀 하나 해주면 좋을텐데....:0", "내 집 근처에 떨어진 동전 좀 주워줬으면 해.:1" });

        //Portrait Data
        //0: normal 1: Speak 2: Happy 3: Angry
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
    }
    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
```

## 퀘스트 진행

퀘스트 대화 순서가 끝에 도달했을 때 퀘스트 번호 증가

```csharp
void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }
```

```csharp
public string CheckQuest(int id)
    {
        //Next Talk Target
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        // Control Quest Object
        ControlObject();

        //Talk Complete & Next Quest
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        //Quest Name
        return questList[questId].questName;
    }
```

## 퀘스트 오브젝트

코인을 먹으면 사라지도록

```csharp
void ControlObject()
    {
        switch(questId)
        {
            case 10:
                if (questActionIndex == 2) //둘다 대화를 끝났을 때
                    gameObjects[0].SetActive(true);
                break;
            case 20:
                if (questActionIndex == 1) //동전을 먹었을 때 
                    gameObjects[0].SetActive(false);
                break;
        }
    }
```

## 예외처리

ContainsKey() 딕셔너리 안에 키가 있는지 검사

id가 없으면 퀘스트 대화순서 제거 후 재탐색

 

```csharp
public string GetTalk(int id, int talkIndex)
{
    if (!talkData.ContainsKey(id)) 
    {
        if(!talkData.ContainsKey(id - id % 10))
        {
            //퀘스트 맨 처음 대사마저 없을, (책상이나 상자)
            //기본 대사를 갖고 옴
            if (talkIndex == talkData[id - id % 100].Length)
                return null;
            else
                return talkData[id - id % 100][talkIndex];
        }
        else
        {
            //해당 퀘스트 진행 순서 대사가 없을때,
            //퀘스트 맨 처음 대사를 갖고 옴
            if (talkIndex == talkData[id - id % 10].Length)
                return null;
            else
                return talkData[id - id % 10][talkIndex];
        }
    }

    if (talkIndex == talkData[id].Length)
        return null;
    else
        return talkData[id][talkIndex];
}
```

## 코드 다듬기

매개변수 없는 생성자 오버로딩

```csharp
public string CheckQuest()
{
    //Quest Name
    return questList[questId].questName;
}
```