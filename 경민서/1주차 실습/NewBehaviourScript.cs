using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    string title = "전설의";
    string playerName = "나검사";
    int level = 5;
    float strength = 15.5f;
    int exp = 1500;
    int health = 30;
    int mana = 25;
    bool inFullLevel = false;

    void Start()
    {
        Debug.Log("Hello Unity!");

        // 1. 변수
        //int level = 5;
        //float strength = 15.5f;
        //string playerName = "나검사";
        //bool inFullLevel = false;

        // 2. 그룹형 변수
        string[] monsters = { "슬라임", "사막뱀", "악마" };
        int[] monsterLevel = new int[3];
        monsterLevel[0] = 1;
        monsterLevel[0] = 6;
        monsterLevel[0] = 20;

        List<string> items = new List<string>();
        items.Add("생명물약 30");
        items.Add("마나물약 30");

        // 3. 연산자
        //int exp = 1500;

        exp = 1500 + 320;
        exp = exp - 10;
        level = exp / 300;
        strength = level * 3.1f;

        int nextExp = 300 - (exp % 300);

        //string title = "전설의";
        Debug.Log(title + " " + playerName);

        int fullLevel = 90;
        inFullLevel = level == fullLevel;

        bool isEndTutorial = level > 10;

        //int mana = 25;
        //bool isBadConditon = health <= 50 && mana <= 20;
        bool isBadConditon = health <= 50 || mana <= 20;

        string condition = isBadConditon ? "나쁨" : " 좋음"; // 삼항연산자

        // 4. 키워드
        //int float string bool new List

        // 5. 조건문
        if (condition == "나쁨")
        {
            Debug.Log("플레이어 상태가 나쁘니 아이템을 사용하세요. ");
        }
        else
        {
            Debug.Log("플레이어 상태가 좋습니다. ");
        }

        if (isBadConditon && items[0] == "생명물약 30")
        {
            items.RemoveAt(0);
            health += 30;
            Debug.Log("생명물약을 사용하였습니다. ");
        }
        else if (isBadConditon && items[0]=="마나물약 30")
        {
            items.RemoveAt(0);
            mana += 30;
            Debug.Log("마나포션 30을 사용하였습니다.");
        }

        switch (monsters[0])
        {
            case "슬라임":
            case "사막뱀":
                Debug.Log("소형 몬스터가 출현 ");
                break;
            case "악마":
                Debug.Log("중형 몬스터가 출현 ");
                break;
            case "골렘":
                Debug.Log("대형 몬스터가 출현 ");
                break;
            default:
                Debug.Log("알 수 없는 크기");
                break;
        }

        // 6. 반복문
        while(health > 0)
        {
            health--;
            if (health > 0)
                Debug.Log("독 데미지를 입음 " + health);
            else
                Debug.Log("사망 ");

            if(health == 10)
            {
                Debug.Log("해독제를 사용 ");
                break;
            }
        }

        for(int count=0; count<10; count++)
        {
            health++;
            Debug.Log("붕대로 치료 중 .." + health);
        }

        for (int index = 0; index < monsters.Length; index++)
        {
            Debug.Log("이 지역에 있는 몬스터 : "+ monsters[index]);
        }

        foreach (string monster in monsters)
        {
            Debug.Log("이 지역에 있는 몬스터 : " + monster);
        }

        //health = Heal(health);
        Heal();

        for (int index=0; index < monsters.Length; index++)
        {
            Debug.Log("용사는" + monsters[index] + "에게" + Battle(monsterLevel[index]));
        }

        //8. 클래스
        player player = new player();
        player.id = 0;
        player.name = "나법사";
        player.title = "현명한";
        player.strength = 2.4f;
        player.weapon = "나무지팡이";
        Debug.Log(player.Talk());
        Debug.Log(player.Hasweapon());

        player.LevelUp();
        Debug.Log(player.name + "의 레벨은 " + player.level + "입니다.");

        Debug.Log(player.move());
    }

    // 7. 함수 (메소드) 지역변수와 전역변        int health = 30;

    void Heal()
    {
        health += 10;
        Debug.Log("힐을 받았습니다." + health);
    }

    string Battle(int monsterLevel)
    {
        string result;
        if (level >= monsterLevel)
            result = "이겼습니다 ";
        else
            result = "졌습니다 ";

        return result;
    }
}

