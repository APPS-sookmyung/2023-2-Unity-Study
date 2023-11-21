using System.Collections;
using System.Collections.Generic;

public class QuestData  // MonoBehaviour 삭제
{
    public string questName;
    public int[] npcId;

    // 생성자
    public QuestData(string name, int[] npc)
    {
        questName = name;
        npcId = npc;
    }
}
