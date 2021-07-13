using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotManager5 : MonoBehaviour
{
    public int robotLevel;
    [SerializeField] GameObject[] RobotImg_L;     // 왼쪽 로봇 이미지 배열
    [SerializeField] GameObject[] RobotImg_R;     // 오른쪽 로봇 이미지 배열
    Stage5Game s5game;  // 로봇이 속한 게임 매니저

    Animator anim;      // 애니메이터
    [SerializeField] AudioSource[] robotSound;  // 효과음 소스
    [SerializeField] AudioClip[] clips;         // 효과음 클립

    bool isDead = false;        // 사망 상태인지 확인

    private void Awake()
    {
        anim = GetComponent<Animator>();
        s5game = GameObject.Find("S5GameManager").GetComponent<Stage5Game>();
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
        if (robotLevel >= 3 && Stage5Game.proceed < 200)
        {
            if(!isDead)
            {
                Stage5Game.s5_game = Stage5Game.S5GAME.DEAD;
                isDead = true;
            }
        }
            
    }

    public void LevelUp()
    {
        // 로봇 레벨 +1
        RobotImg_L[robotLevel].SetActive(true);
        RobotImg_R[robotLevel].SetActive(true);
        robotLevel += 1;
        // 로봇 애니메이션 재생
        anim.SetInteger("robotLevel", robotLevel);
        // 로봇 이동 효과음
        for(int i=0; i < 2; i++)
        {
            robotSound[i].clip = clips[0];
            robotSound[i].Play();
        }
        
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
        RobotImg_L[robotLevel].SetActive(false);
        RobotImg_R[robotLevel].SetActive(false);
        // 로봇 애니메이션
        anim.SetTrigger("Back");
        anim.SetInteger("robotLevel", robotLevel);
        // 로봇 실망 효과음

    }

    // 카메라 애니메이션 끝나면 호출
    public void Dead()
    {
        // 사망 애니메이션
        anim.SetInteger("robotLevel", robotLevel);
        // 사망 효과음
        robotSound[0].clip = clips[1];
        robotSound[0].Play();
    }

    // 사망 애니메이션 끝나면 호출
    void AnimEnd()
    {
        // 스테이지5 메인게임의 Dead함수 실행
        s5game.Dead();
    }

    void ResetRobot()
    {
        // 활성화시 초기화
        robotLevel = 0;
        // 로봇 이미지 초기화
        for (int i = 0; i < 3; i++)
        {
            RobotImg_L[i].SetActive(false);
            RobotImg_R[i].SetActive(false);
        }
            
        // 애니메이터 초기화
        anim.Rebind();

        isDead = false;
    }
}
