using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card2 : MonoBehaviour, IPointerClickHandler
{
    bool isClicked = false; // 카드 클릭 여부
    float seconds = 3.0f;   // 카드 바뀌는 시간 간격

    [SerializeField]
    int imgNum;     // 카드 정답 이미지 번호
    int randNum;    // 난수

    [SerializeField]
    Sprite firstImg;// 처음 이미지 스프라이트
    Sprite nowImg;  // 현재 이미지 스프라이트

    [SerializeField]
    Image clickedImg;   // 클릭됐을 때 나타나는 이미지

    AudioSource clickSound; // 클릭 효과음

    void Awake()
    {
        clickSound = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        // 클릭이 false일 때 그림 바꾸기 시작
        if(!isClicked)
            StartCoroutine("Random");
    }

    IEnumerator Random()
    {
        yield return new WaitForSeconds(seconds);

        randNum = UnityEngine.Random.Range(1, 7);   // 1~6 사이의 난수 생성
        // 해당 이미지 가져오고 할당
        nowImg = Resources.Load<Sprite>("Images/Stage2/Cardkey1_" + randNum);
        gameObject.GetComponent<Image>().sprite = nowImg;

        // 1.5초보다 크면 -0.3초, 1.5초면 그대로
        if (seconds > 1.5f)
            seconds -= 0.3f;
        // 재귀 호출
        StartCoroutine("Random", seconds);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // 그림 멈추기
        StopCoroutine("Random");
        // 효과음
        clickSound.Play();

        // 클릭 true
        isClicked = true;
        // 클릭 이미지
        clickedImg.enabled = true;

        Stage2Game.countCard += 1;
        if (randNum == imgNum)
            Stage2Game.countAnswer += 1;
        else
            return;
    }

    public void ResetCard()
    {
        // 다시 처음 이미지로
        gameObject.GetComponent<Image>().sprite = firstImg;

        // 카드 시간 초기화
        seconds = 3.0f;
        // 클릭 여부 초기화
        isClicked = false;
        // 클릭 이미지 초기화
        clickedImg.enabled = false;

        Debug.Log("카드 리셋되었습니다.");
    }
}
