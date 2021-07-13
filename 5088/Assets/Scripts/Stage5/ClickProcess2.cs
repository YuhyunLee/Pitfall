using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickProcess2 : MonoBehaviour
{
    int clickCnt = 0;   // 클릭 카운트
    int waitCnt = 6;   // 감소 카운트

    [SerializeField] Animator Clickanim; // 클릭 애니메이터

    [SerializeField] GameObject ProceedAnim1;   // 진행 애니메이션1
    [SerializeField] GameObject ProceedAnim2;   // 진행 애니메이션2

    [SerializeField] RobotManager5 robotManager5;   // 로봇 매니저 가져오기

    private void OnEnable()
    {
        // 활성화시 초기화
        ResetProcess();
    }

    private void Update()
    {
        // 클리어 또는 사망 상태면 코루틴 함수 정지
        if (Stage5Game.s5_game == Stage5Game.S5GAME.CLEAR || Stage5Game.s5_game == Stage5Game.S5GAME.DEAD)
            StopCoroutine("Decreasing");
    }

    public void Clicked()
    {
        // 클릭할 때마다 진행도 +1
        Stage5Game.proceed += 1;
        clickCnt++;
        // 클릭 UI
        Clickanim.SetTrigger("Press");
        // 진행 애니메이션
        ProceedAnim1.SetActive(true);
        ProceedAnim2.SetActive(true);

        // 진행도가 +20이면
        if (clickCnt == 20)
        {
            // 로봇 레벨 +1
            robotManager5.LevelUp();
            // 카운트 초기화
            clickCnt = 0;
        }
    }

    // 클릭한 순간부터 계속 실행 (재귀)
    // 1초에 진행도 0.5씩 감소시키는 함수
    IEnumerator Decreasing()
    {
        yield return new WaitForSeconds(1.0f);  // 1초 후

        // 진행도 감소
        Stage5Game.proceed -= 0.5f;
        waitCnt--;
        // 진행 애니메이션 비활성화
        ProceedAnim1.SetActive(false);
        ProceedAnim2.SetActive(false);

        // 진행도 -3 동안 누르지 않았다면
        if (waitCnt == 0)
        {
            // 로봇 레벨 -1
            robotManager5.LevelDown();
            // 카운트 초기화
            waitCnt = 6;
            // 클릭 카운트 초기화 (다시 20번 누르면 로봇 레벨 증가하도록)
            clickCnt = 0;
        }

        // 재귀 호출 (종료 조건 : 클리어 또는 사망)
        StartCoroutine("Decreasing");
    }

    void ResetProcess()
    {
        // 초기화
        clickCnt = 0;
        waitCnt = 6;
    }

    public void PointerDown()
    {
        // 코루틴 함수 정지
        StopCoroutine("Decreasing");
        // 감소 카운트 초기화 (연속으로 실행되지 않았기 때문에)
        waitCnt = 6;
    }

    public void PointerUp()
    {
        // 코루틴 함수 시작
        StartCoroutine("Decreasing");
    }
}
