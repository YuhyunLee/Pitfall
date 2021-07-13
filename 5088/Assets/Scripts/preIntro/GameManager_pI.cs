using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_pI : MonoBehaviour
{
    public Image blackImage; // 검은 패널 이미지

    void nextScene() // 인트로 씬으로 넘어가기
    {
        SceneManager.LoadScene("Intro");
    }

    public void FadeIn() // 페이드 인 함수
    {
        StartCoroutine("FadeInCoroutine");
    }
    public void FadeOut() // 페이드 아웃 함수
    {
        StartCoroutine("FadeOutCoroutine");
    }

    IEnumerator FadeInCoroutine() // 페이드 인 코루틴
    {
        float fadeCount = 1; // 초기 투명도 on
        while (fadeCount > 0f) // 투명도 off될 때까지 -0.01
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            blackImage.color = new Color(0, 0, 0, fadeCount);
        }
    }

    IEnumerator FadeOutCoroutine() // 페이드 아웃 코루틴
    {
        float fadeCount = 0; // 초기 투명도 off
        while (fadeCount<1.0f)// 투명도 on될 때까지 +0.01
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            blackImage.color = new Color(0, 0, 0, fadeCount);
        }
    }
    void Start()
    {
        FadeIn(); // 페이드 인은 바로 실행
        Invoke("FadeOut", 4f); // 4초후 페이드 아웃 진행
        Invoke("nextScene", 8f); // 8초후 다음 씬으로 넘어감
    }


}
