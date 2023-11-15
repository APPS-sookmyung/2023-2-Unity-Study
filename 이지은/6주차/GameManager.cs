using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public TextMeshProUGUI talkText;
    public GameObject scanObject;
    public bool isAction; // 상태 저장용 변수

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
    
}
