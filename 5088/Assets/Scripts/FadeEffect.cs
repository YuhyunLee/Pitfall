using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    public Image blackImage; // 이곳에 화면을 검은색으로 가려줄 패널 이미지를 넣으세용
    
    // 페이드 효과를 주고자 할 때 사용하세요

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
        while (fadeCount < 1.0f)// 투명도 on될 때까지 +0.01
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            blackImage.color = new Color(0, 0, 0, fadeCount);
        }
    }
}
