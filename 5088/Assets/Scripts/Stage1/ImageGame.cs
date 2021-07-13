using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageGame : MonoBehaviour
{
    public bool s1_2;
    public bool s1_5;
    public int ccount = 0;
    
    GameObject ImageGameUI;     // 이미지 게임 UI 저장할 변수

    int clearNum = 0;   // 클리어 횟수

    GameObject card;    // 현재 카드 오브젝트
    GameObject last;    // 직전 카드 오브젝트

    static public int cardNum = 0;      // 클릭한 카드 번호
    int lastNum = 0;                    // 직전의 카드 번호

    int[] isCard = new int[17];         // 카드 비활성화 여부 (1-16 인덱스만 씀) ==> 0 - 활성화, 1 - 비활성화

    float waitTime = 0.0f;      // 카드 선택 후 대기시간
    bool isFlipping = false;    // 카드 뒤집는 중

    //// 총 6개 상태
    // 시작, 카드를 클릭했을 때, 대기 상태, 같은 카드일 때, 틀린 카드일 때, 같은 카드를 모두 찾았을 때
    public enum STATE
    {
        START, HIT, WAIT, IDEAL, WRONG, CLEAR
    };
    static public STATE state;


    void Start()
    {
        ImageGameUI = gameObject.transform.Find("ImageGameUI").gameObject;
        ImageGameUI.SetActive(true);    // 이미지 게임 UI 띄우기
        state = STATE.START;            // 플레이어 상태 설정
    }

    void Update()
    {
        switch(state)
        {
            case STATE.START:
                // 그림 한번 다 보여주고 가림
                StartGame();
                break;
            case STATE.WAIT:
                // 대기 상태
                break;
            case STATE.HIT:
                // 같은 그림인지 판별
                Check();
                break;
            case STATE.IDEAL:
                // 같은 그림인 두 카드를 destroy
                IdealCard();
                break;
            case STATE.WRONG:
                // 두 카드를 다시 close
                WrongCard();
                break;
            case STATE.CLEAR:
                // 계속하기 버튼 또는 취소 버튼
                Clear();
                break;
        }
    }


    void StartGame()
    {
        // 게임 초기화
        ResetGame();

        // 카드 뒤집어졌으면
        if (FlipCard())
            state = STATE.WAIT;     // 대기상태로 전환
    }

    // 게임 초기화
    void ResetGame()
    {
        // 카드 상태 초기화
        for(int i = 1; i <= 16; i++)
        {
            isCard[i] = 0;  // 카드 상태 초기화
            ImageGameUI.transform.GetChild(i).gameObject.SetActive(true);  // 모든 카드 오브젝트 활성화
        }

        // 카드 뒤집기가 실행되고 있지 않다면
        if(!isFlipping)
            // 카드 뒤집기
            FlipCard();
    }

    bool FlipCard()
    {
        // 뒤집고 있다
        isFlipping = true;

        waitTime += Time.deltaTime;

        // 5초가 끝나면 모든 카드 뒤집기
        if (waitTime > 5)
        {
            for (int i = 1; i <= 16; i++)
                ImageGameUI.transform.GetChild(i).gameObject.SendMessage("CloseCard");
            waitTime = 0.0f;    // waitTime 초기화
            isFlipping = false; // 뒤집기 끝났다
            return true;
        }

        return false;
    }

    void Check()
    {
        // 첫 번째 카드 터치?
        if(lastNum == 0)
        {
            lastNum = cardNum;
            state = STATE.WAIT;
            return;
        }
        // 같은 카드 중복 터치?
        if (lastNum == cardNum)
        {
            Debug.Log("같은 카드 중복 터치입니다. 다른 카드를 골라주세요.");
            state = STATE.WAIT;
            return;
        }

        //// 둘 다 아니라면 비교 진행 ////
        
        card = ImageGameUI.transform.Find("Card" + cardNum).gameObject;   // 현재 카드 오브젝트 찾기
        last = ImageGameUI.transform.Find("Card" + lastNum).gameObject;   // 직전 카드 오브젝트 찾기

        // 이미지 번호 찾기
        int n1 = card.GetComponent<Card>().imageNumber;     // 현재 카드 이미지 번호
        int n2 = last.GetComponent<Card>().imageNumber;     // 직전 카드 이미지 번호

        // 틀린 카드
        if (n1 != n2)
        {
            state = STATE.WRONG;
            return;
        }

        // 같은 카드
        state = STATE.IDEAL;
    }

    void IdealCard()
    {
        // 1초간 대기 후,
        waitTime += Time.deltaTime;

        if(waitTime > 1.5)
        {
            // 두 카드 비활성화
            card.SetActive(false);
            last.SetActive(false);

            // 카드 사라짐 여부를 true로
            isCard[cardNum] = 1;
            isCard[lastNum] = 1;

            cardNum = 0;    // 현재 카드 번호 초기화
            lastNum = 0;    // 직전 카드 번호 초기화

            // 남아있는 카드가 있는가?
            for (int i = 1; i <= 16; i++)
            {
                if (isCard[i] == 0)
                {
                    // 한개라도 남아있다면
                    waitTime = 0.0f;        // waitTime 초기화
                    state = STATE.WAIT;     // 대기상태로 전환 후
                    return;                 // 함수 종료
                }
            }
            // 모두 사라졌다면
            waitTime = 0.0f;        // waitTime 초기화
            clearNum += 1;          // 클리어 횟수 증가
            state = STATE.CLEAR;    // 클리어 상태로 전환

        }
    }

    void WrongCard()
    {
        // 1초간 대기 후,
        waitTime += Time.deltaTime;

        if(waitTime > 1.5)
        {
            // 두 카드 다시 close
            card.SendMessage("CloseCard");
            last.SendMessage("CloseCard");

            cardNum = 0;    // 현재 카드 번호 초기화
            lastNum = 0;    // 직전 카드 번호 초기화

            state = STATE.WAIT;     // 대기 상태로 전환
            waitTime = 0.0f;        // waitTime 초기화
        }
    }

    void Clear()
    {
        if (ccount == 0)
        {
            s1_2 = true;
            ccount = 1;
        }

        
        // Continue 메뉴 UI 띄워줌
        ImageGameUI.transform.Find("Continue").gameObject.SetActive(true);

        // 예 또는 아니오 버튼 클릭 이벤트가 들어오면 ContinueMenu()가 호출될 것
    }

    public void ContinueMenu(bool isContinue)
    {
        // 예
        if (isContinue)
        {
            // 다시 시작
            cardNum = 0;            // 현재 카드 번호 초기화
            lastNum = 0;            // 직전 카드 번호 초기화
            state = STATE.START;    // 시작 상태로 전환

            // 메뉴 UI 닫기
            ImageGameUI.transform.Find("Continue").gameObject.SetActive(false);
        }
        // 아니오 버튼
        else if (!isContinue)
        {
            // 클리어 횟수가 1회 이상이면
            if (clearNum > 0)
            {
                // 닫기 버튼 생성
                ImageGameUI.transform.Find("CloseButton").gameObject.SetActive(true);
                // 닫기 버튼을 누르면 ExitGame()이 호출될 것
            }
            else
            {
                // 다시 시작
                cardNum = 0;            // 현재 카드 번호 초기화
                lastNum = 0;            // 직전 카드 번호 초기화
                state = STATE.START;    // 시작 상태로 전환

                // 메뉴 UI 닫기
                ImageGameUI.transform.Find("Continue").gameObject.SetActive(false);
            }
        }
    }

    public void ExitGame()
    {
        
        if (ccount == 1)
        {
            s1_5 = true;
            ccount = 2;
        }
            
        // 스테이지1 상태 메인게임으로 전환
        Stage1Manager.stage1 = Stage1Manager.STAGE1.MAINGAME;

        // 퀘스트 종료
        Destroy(GameObject.Find("Quest"));
        // ImageGame 종료
        ImageGameUI.SetActive(false);   // UI 종료
        gameObject.SetActive(false);    // 이미지 게임 종료
    }
}
