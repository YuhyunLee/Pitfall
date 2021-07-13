using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Robot : MonoBehaviour
{
    Animator anim;                      // 로봇 애니메이션
    AudioSource robotSound;             // 로봇 이동 효과음
    [SerializeField] AudioClip[] clip;  // 효과음 클립

    private void Start()
    {
        // 애니메이터 가져오기
        anim = GameObject.Find("RobotManager").GetComponent<Animator>();
        // 효과음 가져오기
        robotSound = GetComponent<AudioSource>();
    }

    public void Move()
    {
        // 해당 단계 애니메이션 실행 (1-3단계)
        anim.SetInteger("robotLevel", RobotManager.robotLevel);
        // 효과음
        robotSound.clip = clip[0];
        robotSound.Play();
        Debug.Log("로봇 " + RobotManager.robotLevel + "단계. 한 단계 앞으로 움직입니다.");
    }
    
    public void Level4Move()
    {
        // 4단계 애니메이션 실행 (카메라 조건 만족)
        anim.SetTrigger("isCamera");
        // 효과음
        robotSound.clip = clip[1];
        robotSound.Play();
    }

    void Reset()
    {
        // 애니메이터 초기화
        anim.Rebind();
    }
}
