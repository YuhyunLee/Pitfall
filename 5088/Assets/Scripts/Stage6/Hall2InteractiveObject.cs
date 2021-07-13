using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hall2InteractiveObject : MonoBehaviour
{
    public Stage6Manager isStageManager;
    
    public GameObject InteractiveUI;       // 상호작용 UI
    
    public GameObject GuideText; // 도움말
    public GameObject S6GUI;       // 게임 UI
    public GameObject player;

    public GameObject camera6; // 스테이지6카메라
    public GameObject mainCamera; // 메인 카메라

    public bool s6_1;
    public bool s6_2;

    private void OnTriggerEnter(Collider other)
    {
        s6_1 = true;
    }

    private void OnTriggerStay(Collider other)
    {

        // E버튼(UI) 나타나도록 하고
        InteractiveUI.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E))     // e버튼(상호작용 버튼)이 한번 눌렸을 때 true 반환
        {
            s6_2 = true;
            // Box Collider 끄고
            gameObject.GetComponent<BoxCollider>().enabled = false;
            // UI 끄고
            InteractiveUI.SetActive(false);

            // 플레이어 게임모드로 변경
            AllStageManager.p_state = AllStageManager.PLAYER_STATE.GAME;
            // 카메라 변환
            Stage6CameraView();
            isStageManager.status = "GUIDE"; // 현상태를 GUIDE로 만들어줌 -> Stage6Manager로
            gameObject.SetActive(false);


            Debug.Log("자유이동모드에서 게임모드로 변경되었습니다.");


        }
    }

    // 충돌 범위 밖으로 나가면 E 인터페이스 끄기
    private void OnTriggerExit(Collider other)
    {
        // UI 끄고
        InteractiveUI.SetActive(false);
    }

    // 스테이지6 카메라 활성화, 메인 카메라 비활성화
    public void Stage6CameraView()
    {
        mainCamera.SetActive(false);
        camera6.SetActive(true);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
