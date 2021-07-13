using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotManager4 : MonoBehaviour
{
    public int robotLevel;
    [SerializeField] GameObject robotUp;          // 로봇 오브젝트 배열
    Vector3 robotPos = new Vector3();        // 로봇들 위치 저장할 배열
    [SerializeField] GameObject[] RobotImg;     // 로봇 이미지 배열

    Stage4Game s4game;  // 로봇이 속한 게임 매니저
    Animator anim;      // 애니메이터
    Animation deadAnim; // 사망 애니메이션

    [SerializeField] AudioSource[] robotSound;  // 효과음 소스

    bool isDead = false;        // 사망 상태인지 확인

    private void Awake()
    {
        anim = GetComponent<Animator>();
        deadAnim = GetComponent<Animation>();
        s4game = GameObject.Find("S4GameManager").GetComponent<Stage4Game>();

        // 로봇 위치 저장
        robotPos = robotUp.transform.position;
    }

    private void OnEnable()
    {
        ResetRobot();
    }

    // Update is called once per frame
    void Update()
    {
        // 로봇 단계 클리핑
        if (robotLevel <= 0)
            robotLevel = 0;

        // 로봇이 3단계고 진행률이 200 미만이면
        if (robotLevel >= 3 && Stage4Game.proceed < 200)
        {
            if(!isDead)
            {
                Stage4Game.s4_game = Stage4Game.S4GAME.DEAD;
                isDead = true;
            }
        }   
    }

    public void LevelUp()
    {
        // 로봇 레벨 +1
        RobotImg[robotLevel].SetActive(true);
        robotLevel += 1;
        // 로봇 애니메이션 재생
        anim.SetInteger("robotLevel", robotLevel);
        // 로봇 이동 효과음
        robotSound[0].Play();
    }

    public void LevelDown()
    {
        // 로봇 레벨 -1
        robotLevel -= 1;
        // 음수면 함수 종료
        if (robotLevel < 0)
            return;

        // 아니라면
        // 로봇 이미지 지우기
        RobotImg[robotLevel].SetActive(false);
        // 로봇 애니메이션
        anim.SetTrigger("Back");
        anim.SetInteger("robotLevel", robotLevel);
        // 로봇 실망 효과음

    }

    // 카메라 애니메이션 끝나면 호출
    public void Dead()
    {
        // 사망 애니메이션
        deadAnim.Play();
        // 사망 효과음
        robotSound[1].Play();
            
    }

    // 사망 애니메이션 끝나면 호출
    void AnimEnd()
    {
        // 스테이지4 메인게임의 Dead함수 실행
        s4game.Dead();
    }

    void ResetRobot()
    {
        // 활성화시 초기화
        robotLevel = 0;
        // 로봇 이미지 초기화
        for (int i = 0; i < 3; i++)
            RobotImg[i].SetActive(false);
        // 애니메이터 초기화
        anim.Rebind();

        // 로봇 위치 초기화
        robotUp.transform.position = robotPos;

        isDead = false;
    }
}
