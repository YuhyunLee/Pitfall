using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void ReTry()
    {
        // 올스테이지 매니저 로드 상태로 전환
        // 죽은 스테이지부터 다시 시작
        AllStageManager.stage = AllStageManager.STAGE.RELOAD;

        // 게임오버 메뉴 비활성화
        gameObject.SetActive(false);
    }

    public void ToMainMenu()
    {
        // 메인 메뉴 씬으로 전환
        SceneManager.LoadScene("MainMenu");
        Debug.Log("메인메뉴");
    }
}
