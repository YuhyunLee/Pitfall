using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager_MM : MonoBehaviour
{
    public float delayTime = 1f;
    public Image blackImage;
    public GameObject Image;

    private void Start()
    {
        Image.SetActive(false); // 패널 이미지가 버튼 클릭 방해하는 거 방지
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void delayActive(string functionName) // 씬 넘어가기 전에 효과음+페이드 효과
    {
        Image.SetActive(true);
        FadeOut();
        Invoke(functionName, delayTime);
    }

    public void FadeOut() // 페이드 아웃 함수
    {
        StartCoroutine("FadeOutCoroutine");
    }

    public void startGame()// preIntro로 넘어감
    {
        SceneManager.LoadScene("preIntro");
    }

    public void exitGame() // 게임 종료
    {
        Application.Quit();
        Debug.Log("종료");
    }

    IEnumerator FadeOutCoroutine() // 페이드 아웃 코루틴
    {
        float fadeCount = 0; // 초기 투명도 off
        while (fadeCount < 1.0f) // 투명도가 on이 될때까지 0.01씩 더해줌
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            blackImage.color = new Color(0, 0, 0, fadeCount);
        }
    }
}
