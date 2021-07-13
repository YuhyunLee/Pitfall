using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot2 : MonoBehaviour
{
    [SerializeField]
    Animator anim;      // 애니메이터
    [SerializeField]
    GameObject[] robotImg;   // 로봇 이미지 배열
    AudioSource robotSound;             // 로봇 이동 효과음
    [SerializeField] AudioClip[] clip;  // 효과음 클립

    [SerializeField]
    int robotPos;               // 0이면 Left, 1이면 Right
    public int robotLevel = 0;  // 현재 로봇 단계

    bool isDead = false;        // 사망 상태인지 확인

    private void Awake()
    {
        robotSound = GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        // 비활성화될때마다 초기화
        ResetRobot();
        Debug.Log(robotPos + "번 로봇 비활성화");
    }

    void Update()
    {
        // 로봇이 4단계이고 스테이지2가 클리어가 아닐 때
        if (robotLevel >= 4 && Stage2Game.st2_game != Stage2Game.ST2GAME.CLEAR)
        {
            // 이미 사망 상태가 아니라면
            if(!isDead)
            {
                // 스테이지2 게임 사망 상태로 전환
                Stage2Game.st2_game = Stage2Game.ST2GAME.DEAD;
                isDead = true;
            }
        }

        // 음수면 0으로 클램핑
        if (robotLevel < 0)
            robotLevel = 0;
    }

    void Move()
    {
        robotImg[robotLevel].SetActive(true);
        robotLevel += 1;
        // 효과음
        robotSound.clip = clip[0];
        robotSound.Play();

        Debug.Log(robotPos + "번 로봇 앞으로 한 단계 이동");
    }

    public void Level4()
    {
        // 사망 애니메이션 실행
        anim.SetTrigger("isDead");
        // 효과음
        robotSound.clip = clip[1];
        robotSound.Play();
    }

    void Back()
    {
        // 로봇 레벨 -1
        robotLevel -= 1;
        // 음수면 함수 종료
        if (robotLevel < 0)
            return;

        robotImg[robotLevel].SetActive(false);
        Debug.Log(robotPos + "번 로봇 뒤로 한 단계 이동");
    }

    void ResetRobot()
    {
        // 레벨 초기화
        robotLevel = 0;
        // 애니메이터 초기화
        anim.Rebind();

        // 로봇 레벨 이미지 비활성화
        for (int i = 0; i < 4; i++)
            robotImg[i].SetActive(false);
    }

}
