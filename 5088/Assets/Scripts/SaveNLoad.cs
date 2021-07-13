using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// 데이터를 저장할 클래스
[System.Serializable]   // 직렬화
public class SaveData
{   
    // 1. 플레이어 위치
    public Vector3 playerPos;
    // 2. 플레이어 상태
    public bool playerState;
    // 3. 스테이지 정보
    public AllStageManager.STAGE stageInfo;
}

public class SaveNLoad : MonoBehaviour
{
    // 데이터 저장 객체
    private SaveData saveData = new SaveData();

    // 데이터 저장할 경로
    private string SAVE_DATA_DIRECTORY;
    // 파일 이름
    private string SAVE_FILENAME = "/SaveFile.txt";

    // 플레이어 객체
    private PlayerControl thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";     // 게임 폴더(프로젝트 폴더) 안의 Saves 폴더에 저장

        // Saves 폴더가 없다면 폴더를 만들어줘라
        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
    }

    public void SaveData()
    {
        // 플레이어 위치 저장
        thePlayer = FindObjectOfType<PlayerControl>();
        saveData.playerPos = thePlayer.transform.position;
        // 플레이어 상태 저장
        saveData.playerState = thePlayer.isFreeMode;
        // 스테이지 정보 저장
        saveData.stageInfo = AllStageManager.stage;


        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);

        Debug.Log("저장 완료");
        Debug.Log(json);
    }

    public void LoadData()
    {
        if(File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string loadJSon = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);

            saveData = JsonUtility.FromJson<SaveData>(loadJSon);

            // 플레이어 위치 로드
            thePlayer = FindObjectOfType<PlayerControl>();
            thePlayer.transform.position = saveData.playerPos;
            // 플레이어 상태 로드
            thePlayer.isFreeMode = saveData.playerState;
            // 스테이지 정보 로드
            AllStageManager.stage = saveData.stageInfo;

            Debug.Log("로드 완료");
        }
        else
        {
            Debug.Log("세이브 파일이 없습니다");
        }
    }
}
