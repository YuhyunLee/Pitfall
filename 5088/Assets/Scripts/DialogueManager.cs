using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] AudioClip[] dialog;    // 오디오 클립 배열
    AudioSource audioSource;                // 오디오 소스 컴포넌트

    [SerializeField] string[] txt;      // 자막 배열
    Text dialogText;                    // 텍스트 컴포넌트

    int clip_cnt = 0;   // 재생한 클립 카운트
    int txt_cnt = 0;    // 텍스트 카운트

    public bool isPlaying = false;  // 재생 중인지 여부

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dialogText = GetComponentInChildren<Text>();
        StartCoroutine("Play");
        StartCoroutine("ShowText");
    }

    IEnumerator Play()
    {
        Debug.Log("오디오" + clip_cnt + "실행되었습니다");

        // 재생 중 true
        isPlaying = true;
        // 클립 넣어주고 카운트
        audioSource.clip = dialog[clip_cnt++];

        // 재생
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);  // 재생 시간만큼 기다린 후

        // 재생 중 false
        isPlaying = false;

    }

    IEnumerator ShowText()
    {
        // 텍스트 활성화
        dialogText.enabled = true;
        // 순서인 자막 넣어주기
        dialogText.text = txt[txt_cnt++];

        yield return new WaitForSeconds(1.0f);

        // 텍스트 비활성화
        dialogText.enabled = false;
    }
}
