using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidden : MonoBehaviour
{
    GameObject HiddenUI;       // 상호작용 UI
    [SerializeField] GameObject HiddenRecordUI;  // 비밀 기록 자막
    [SerializeField] GameObject[] objects;      // outline 효과 줄 오브젝트 배열
    AudioSource hiddenRecord;   // 비밀 기록 사운드

    public ExitArea secretCount;

    private void Start()
    {
        HiddenUI = GameObject.Find("GameUI").transform.Find("HiddenUI").gameObject;
        hiddenRecord = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // 비밀 기록이 재생 중일 때만 자막 활성화
        if (hiddenRecord.isPlaying)
            HiddenRecordUI.SetActive(true);
        else
            HiddenRecordUI.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        // E버튼(UI) 나타나도록 하고
        HiddenUI.SetActive(true);
        // 아웃라인 효과
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].GetComponent<Outline>().enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.E))     // e버튼(상호작용 버튼)이 한번 눌렸을 때 true 반환
        {
            secretCount.secretCount++;
            // Box Collider 끄고
            gameObject.GetComponent<BoxCollider>().enabled = false;
            // UI 끄고
            HiddenUI.SetActive(false);

            // 아웃라인 끄기
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].GetComponent<Outline>().enabled = false;
            }

            // 장애물 움직이기
            hiddenRecord.Play();
            Debug.Log("비밀기록을 재생합니다");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        // UI 끄고
        HiddenUI.SetActive(false);
        // 아웃라인 끄기
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].GetComponent<Outline>().enabled = false;
        }
    }
}
