using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField]
    AllStageManager.STAGE thisStage;// 스테이지 번호
    [SerializeField]
    GameObject InteractiveUI;       // 상호작용 UI

    private void OnTriggerStay(Collider other)
    {
        // E버튼(UI) 나타나도록 하고
        InteractiveUI.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E))     // e버튼(상호작용 버튼)이 한번 눌렸을 때 true 반환
        {
            // Box Collider 끄고
            gameObject.GetComponent<BoxCollider>().enabled = false;
            // UI 끄고
            InteractiveUI.SetActive(false);

            // 플레이어 게임모드로 변경
            AllStageManager.p_state = AllStageManager.PLAYER_STATE.GAME;
            // 현재 스테이지 번호 전달
            AllStageManager.stage = thisStage;

            Debug.Log("현재 스테이지는 " + thisStage + "입니다.");
            Debug.Log("자유이동모드에서 게임모드로 변경되었습니다.");


        }
    }

    private void OnTriggerExit(Collider other)
    {
        // UI 끄고
        InteractiveUI.SetActive(false);
    }
}
