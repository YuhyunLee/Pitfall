using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllStageManager : MonoBehaviour
{
    [SerializeField]
    SaveNLoad theSaveNLoad;
    static public bool isLoad = false;    // 처음인지 로드됐는지

    // Player Control 객체 저장할 변수
    PlayerControl playerControl;
    // Camera Manager 객체 저장할 변수
    CameraManager camera;
    // GameOver UI
    GameObject GameOverUI;

    // 스테이지 클리어 단계
    static public int clearStage;

    //// 스테이지 7개
    public enum STAGE
    {
        WAIT, GAMEOVER, RELOAD, STAGE1, STAGE2, STAGE3, STAGE4, STAGE5, STAGE6, STAGE7
    };
    static public STAGE stage = STAGE.WAIT;       // 처음에는 대기상태

    // 각 스테이지 관리 객체 생성
    private Stage1Manager S1Manager;
    private Stage2Manager S2Manager;
    private Stage3Manager S3Manager;
    private Stage4Manager S4Manager;
    private Stage5Manager S5Manager;
    private Stage6Manager S6Manager;
    private Stage7Manager S7Manager;

    //// 플레이어 상태
    public enum PLAYER_STATE
    {
        FREE, GAME
    };
    static public PLAYER_STATE p_state = PLAYER_STATE.FREE;     // 디폴트는 자유이동모드

    // Start is called before the first frame update
    void Start()
    {
        // 플레이어, 카메라, 게임오버 메뉴 할당
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        camera = GameObject.Find("Camera Manager").GetComponent<CameraManager>();
        GameOverUI = GameObject.Find("GameUI").transform.Find("GameOverUI").gameObject;

        // 각 스테이지 객체 할당
        S1Manager = GameObject.Find("Stage1Manager").GetComponent<Stage1Manager>();
        S2Manager = GameObject.Find("Stage2Manager").GetComponent<Stage2Manager>();
        S3Manager = GameObject.Find("Stage3Manager").GetComponent<Stage3Manager>();
        S4Manager = GameObject.Find("Stage4Manager").GetComponent<Stage4Manager>();
        S5Manager = GameObject.Find("Stage5Manager").GetComponent<Stage5Manager>();
        S6Manager = GameObject.Find("Stage6Manager").GetComponent<Stage6Manager>();
        //S7Manager = GameObject.Find("Stage7Manager").GetComponent<Stage7Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어 모드 계속 체크
        switch(p_state)
        {
            case PLAYER_STATE.FREE:
                ToFreeMode();
                break;
            case PLAYER_STATE.GAME:
                ToGameMode();
                break;
        }

    }

    // 게임모드 -> 자유모드
    void ToFreeMode()
    {
        playerControl.isFreeMode = true;            // 자유이동모드 활성화
        MouseManager.isGameMode = false;            // 게임모드 비활성화
        camera.MainCameraView();    // 메인 카메라로 전환
    }

    // 자유모드 -> 게임모드
    void ToGameMode()
    {
        playerControl.isFreeMode = false;           // 자유이동모드 비활성화(움직임 제한)
        MouseManager.isGameMode = true;             // 게임모드 활성화

        switch (stage)
        {
            case STAGE.WAIT:
                // 대기 상태
                break;
            case STAGE.GAMEOVER:
                // 게임 오버(사망)
                GameOver();
                break;
            case STAGE.RELOAD:
                // 재로드
                ReLoad();
                break;
            case STAGE.STAGE1:
                // 스테이지1 시작
                Stage1Start();
                break;
            case STAGE.STAGE2:
                // 스테이지2 시작
                Stage2Start();
                break;
            case STAGE.STAGE3:
                // 스테이지3 시작
                Stage3Start();
                break;
            case STAGE.STAGE4:
                // 스테이지4 시작
                Stage4Start();
                break;
            case STAGE.STAGE5:
                // 스테이지5 시작
                Stage5Start();
                break;
            
        }
    }

    void GameOver()
    {
        // GameOver UI 띄우고
        GameOverUI.SetActive(true);

        // 대기 상태로 전환
        stage = STAGE.WAIT;
    }

    public void ReLoad()
    {
        // 로드
        theSaveNLoad.LoadData();
        isLoad = true;

        // 로드된 스테이지 상태로 자동으로 전환됨
    }

    void Stage1Start()
    {
        camera.Stage1CameraView();  // 스테이지1 카메라로 전환
        S1Manager.GameStart();      // 스테이지1 시작

        // 자동 저장
        theSaveNLoad.SaveData();

        // 대기 상태로 전환
        stage = STAGE.WAIT;
    }

    void Stage2Start()
    {
        camera.Stage2CameraView();  // 스테이지2 카메라로 전환
        S2Manager.GameStart();      // 스테이지2 시작

        // 자동 저장
        theSaveNLoad.SaveData();

        // 대기 상태로 전환
        stage = STAGE.WAIT;
    }

    void Stage3Start()
    {
        camera.Stage3CameraView();  // 스테이지3 카메라로 전환
        S3Manager.GameStart();      // 스테이지3 시작

        // 자동 저장
        theSaveNLoad.SaveData();

        // 대기 상태로 전환
        stage = STAGE.WAIT;
    }

    void Stage4Start()
    {
        camera.Stage4CameraView();  // 스테이지4 카메라로 전환
        S4Manager.GameStart();      // 스테이지4 시작

        // 자동 저장
        theSaveNLoad.SaveData();

        // 대기 상태로 전환
        stage = STAGE.WAIT;
    }

    void Stage5Start()
    {
        camera.Stage5CameraView();  // 스테이지5 카메라로 전환
        S5Manager.GameStart();      // 스테이지5 시작

        // 자동 저장
        theSaveNLoad.SaveData();

        // 대기 상태로 전환
        stage = STAGE.WAIT;
    }

    
}
