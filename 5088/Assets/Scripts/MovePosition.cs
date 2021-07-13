using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePosition : MonoBehaviour
{
    [SerializeField]
    SaveNLoad theSaveNLoad;

    [SerializeField]
    Vector3 ToPosition;     // 이동할 위치
    [SerializeField]
    string thisRoom;        // 이동할 방 이름
    [SerializeField]
    GameObject InteractiveUI;   // 상호작용 UI

    // 플레이어 객체
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerStay(Collider other)
    {
        // E버튼(UI) 나타나도록 하고
        InteractiveUI.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E))     // e버튼(상호작용 버튼)이 한번 눌렸을 때 true 반환
        {
            // UI 끄고
            InteractiveUI.SetActive(false);

            // 플레이어 위치 이동
            player.transform.position = ToPosition;
            Debug.Log(thisRoom + "(으)로 이동했습니다.");

            // 자동 저장
            theSaveNLoad.SaveData();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // UI 끄고
        InteractiveUI.SetActive(false);
    }
}
