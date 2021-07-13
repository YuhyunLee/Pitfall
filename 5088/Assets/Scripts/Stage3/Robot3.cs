using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot3 : MonoBehaviour
{
    Animator anim;          // 애니메이터
    Animation deadAnim;     // 사망 애니메이션
    AudioSource robotSound; // 효과음

    Stage3MainGame s3main;  // 로봇이 속한 게임 매니저

    Vector3 originPos;

    void Awake()
    {
        anim = GetComponent<Animator>();
        deadAnim = GetComponent<Animation>();
        robotSound = GetComponent<AudioSource>();

        s3main = GameObject.Find("S3MainGameManager").GetComponent<Stage3MainGame>();
        originPos = transform.position;
    }

    private void OnEnable()
    {
        ResetState();
    }

    // 경보 해제 시 호출
    public void AlarmOff()
    {
        anim.SetTrigger("AlarmOff");
    }

    // 카메라 애니메이션 끝나면 호출
    public void Dead()
    {
        // 사망 애니메이션
        deadAnim.Play();
        // 효과음
        robotSound.Play();
    }

    // 사망 애니메이션 끝나면 호출
    void AnimEnd()
    {
        // 스테이지3 메인게임의 Dead함수 실행
        s3main.Dead();
    }

    void ResetState()
    {
        // 애니메이터 초기화
        anim.Rebind();
        // 로봇 위치 초기화
        transform.position = originPos;
    }
}
