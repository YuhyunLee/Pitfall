using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ImgGameManager3 : MonoBehaviour
{
    [SerializeField] GameObject ImageGame3UI;    // UI 저장

    static public int thisNum = 0;        // 현재 카드 번호
    private int lastNum = 0;        // 직전 카드 번호

    float waitTime;     // 대기 시간

    GameObject thisCard;    // 현재 카드 오브젝트
    GameObject lastCard;    // 직전 카드 오브젝트

    int[] isCard = new int[13];         // 카드 비활성화 여부 (1-12 인덱스만 씀) ==> 0 - 활성화, 1 - 비활성화

    [SerializeField] ChangeImg changeImg;  // 카드 섞기 수행할 객체

    public enum IMGGAME3
    {
        START, WAIT, HIT, IDEAL, WRONG, CLEAR
    };
    static public IMGGAME3 img_3 = IMGGAME3.START;      // 처음에는 대기 상태

    private void OnEnable()
    {
        // 초기화
        ResetGame();
        // 시작 상태로 전환
        img_3 = IMGGAME3.START;
    }

    void Update()
    {
        switch(img_3)
        {
            case IMGGAME3.START:
                // 시작 상태
                StartGame();
                break;
            case IMGGAME3.WAIT:
                // 대기 상태
                break;
            case IMGGAME3.HIT:
                // 카드가 클릭되었을 때
                CheckCard();
                break;
            case IMGGAME3.IDEAL:
                // 같은 그림일 때
                IDeal();
                break;
            case IMGGAME3.WRONG:
                // 다른 그림일 때
                Wrong();
                break;
            case IMGGAME3.CLEAR:
                // 같은 그림 찾기 모두 성공
                Clear();
                break;
        }
    }

    void StartGame()
    {
        // 카드 이미지 바꿔주기
        changeImg.ChangeCard();

        // 대기 상태로 전환
        img_3 = IMGGAME3.WAIT;
    }

    void CheckCard()
    {
        // 첫 번째 카드 터치?
        if (lastNum == 0)
        {
            lastNum = thisNum;
            img_3 = IMGGAME3.WAIT;
            return;
        }
        // 같은 카드 중복 터치?
        if (lastNum == thisNum)
        {
            Debug.Log("같은 카드 중복 터치입니다. 다른 카드를 골라주세요.");
            img_3 = IMGGAME3.WAIT;
            return;
        }

        //////// 둘 다 아니라면 비교 진행 ////////

        // 카드 오브젝트 가져오기
        thisCard = ImageGame3UI.transform.Find("Card" + thisNum).gameObject;   // 현재 카드 오브젝트 찾기
        lastCard = ImageGame3UI.transform.Find("Card" + lastNum).gameObject;   // 직전 카드 오브젝트 찾기

        // 이미지 번호 찾기
        int n1 = thisCard.GetComponent<Card3>().imgNum;
        int n2 = lastCard.GetComponent<Card3>().imgNum;

        // 틀린 카드라면
        if (n1 != n2)
        {
            img_3 = IMGGAME3.WRONG;
            return;
        }

        // 같은 카드면
        img_3 = IMGGAME3.IDEAL;
    }

    void IDeal()
    {
        // 1초간 대기 후,
        waitTime += Time.deltaTime;

        if (waitTime > 0.5)
        {
            Debug.Log("맞았습니다!");

            // 두 카드 비활성화
            thisCard.SetActive(false);
            lastCard.SetActive(false);

            // 카드 사라짐 여부를 true로
            isCard[thisNum] = 1;
            isCard[lastNum] = 1;

            thisNum = 0;    // 현재 카드 번호 초기화
            lastNum = 0;    // 직전 카드 번호 초기화

            // 남아있는 카드가 있는가?
            for (int i = 1; i <= 12; i++)
            {
                if (isCard[i] == 0)
                {
                    // 한개라도 남아있다면
                    waitTime = 0.0f;        // waitTime 초기화
                    img_3 = IMGGAME3.WAIT;  // 대기상태로 전환 후
                    return;                 // 함수 종료
                }
            }
            // 모두 사라졌다면
            waitTime = 0.0f;        // waitTime 초기화
            img_3 = IMGGAME3.CLEAR;    // 클리어 상태로 전환
        }
    }

    void Wrong()
    {
        // 1초간 대기 후,
        waitTime += Time.deltaTime;

        if (waitTime > 0.5)
        {
            Debug.Log("틀렸습니다!");

            thisNum = 0;    // 현재 카드 번호 초기화
            lastNum = 0;    // 직전 카드 번호 초기화

            waitTime = 0.0f;        // waitTime 초기화
            img_3 = IMGGAME3.WAIT;     // 대기 상태로 전환
        }
    }

    void Clear()
    {
        // 대기 상태로 전환
        img_3 = IMGGAME3.WAIT;

        // 메인 게임 상태를 경보 -> 경보 끄기로 전환
        Stage3MainGame.s3_main = Stage3MainGame.S3MAINGAME.ALARMOFF;

        // 이미지 게임 비활성화
        gameObject.SetActive(false);
    }

    void ResetGame()
    {
        // 카드 모두 다시 활성화
        for (int i = 1; i <= 12; i++)
        {
            ImageGame3UI.transform.GetChild(i).gameObject.SetActive(true);
        }

        // 초기화
        thisNum = 0;
        lastNum = 0;
        waitTime = 0.0f;

        // 카드 정답 여부도 초기화
        for (int i = 1; i <= 12; i++)
        {
            isCard[i] = 0;
        }
    }
}
