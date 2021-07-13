using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Story : MonoBehaviour
{
    DialogueManager S1Dialogue;

    private void Start()
    {
        S1Dialogue = GetComponent<DialogueManager>();
    }


    public void Story1()
    {
        Debug.Log("스토리1 진행");

        for (int i = 0; i < 3; i++)
            S1Dialogue.StartCoroutine("Play");
    }

    public void Story2()
    {
        Debug.Log("스토리2 진행");

        for (int i = 0; i < 6; i++)
            S1Dialogue.StartCoroutine("Play");
    }

    public void Story3()
    {
        Debug.Log("스토리3 진행");
        for (int i = 0; i < 1; i++)
            S1Dialogue.StartCoroutine("Play");
    }

    public void Story4()
    {
        Debug.Log("스토리4 진행");
        // 마지막 스토리 진행
        for (int i = 0; i < 1; i++)
            S1Dialogue.StartCoroutine("Play");
        // 스테이지1 종료
        // Stage1Manager.stage1 = Stage1Manager.STAGE1.END;
    }

}
