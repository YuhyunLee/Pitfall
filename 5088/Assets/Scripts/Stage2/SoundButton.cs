using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SoundButton : MonoBehaviour
{
    [SerializeField]
    int buttonLR;           // 0이면 Left, 1이면 Right

    [SerializeField]
    Text buttonText;            // 버튼 텍스트
    AudioSource clickSound;     // 클릭 효과음

    [SerializeField]
    RobotManager2 RManager2;    // 로봇매니저2 가져오기

    public float coolTime = 4.0f;  // 쿨타임 4초

    public enum BTNSTATE
    {
        WAIT, COOLTIME
    };
    public BTNSTATE btn_state = BTNSTATE.WAIT;


    void Start()
    {
        clickSound = GetComponent<AudioSource>();

        buttonText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        // 쿨타임이 끝났는지 검사
        if (coolTime <= 1)
        {
            // 끝났으면

            // 쿨타임 초기화
            coolTime = 4.0f;
            // 대기 상태로 전환
            btn_state = BTNSTATE.WAIT;
        }

        switch (btn_state)
        {
            case BTNSTATE.WAIT:
                // 대기 상태
                buttonText.text = "";
                break;
            case BTNSTATE.COOLTIME:
                // 쿨타임 세는 중
                CoolTime();
                buttonText.text = coolTime.ToString("F0");
                break;
        }
    }

    public void Clicked()
    {
        // 쿨타임 검사
        if (btn_state == BTNSTATE.COOLTIME)
        {
            Debug.Log("쿨타임입니다.");
            return;
        }

        // 효과음
        clickSound.Play();
        // 왼쪽 버튼이라면
        if (buttonLR == 0)
            RManager2.BackRobot(1); // 오른쪽 로봇 -1
        // 오른쪽 버튼이라면
        else
            RManager2.BackRobot(0); // 왼쪽 로봇 -1

        // 쿨타임 시작
        btn_state = BTNSTATE.COOLTIME;
    }

    void CoolTime()
    {
        // 쿨타임 시작
        coolTime -= Time.deltaTime;
    }

    public void ResetButton()
    {
        // 쿨타임 초기화
        coolTime = 4.0f;
        // 대기 상태로 전환
        btn_state = BTNSTATE.WAIT;

        Debug.Log("버튼 리셋되었습니다.");
    }
}
