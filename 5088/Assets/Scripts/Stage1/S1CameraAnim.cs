using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S1CameraAnim : MonoBehaviour
{
    Animator animator;      // 애니메이터

    [SerializeField] Stage1Robot robot;     // 로봇 가져오기
    [SerializeField] GameObject MainGameUI; // 메인 게임 UI

    private void Start()
    {
        // 애니메이터랑 애니메이션 가져오기
        animator = GetComponent<Animator>();
    }

    public void AnimPlay()
    {
        Debug.Log("AnimPlay실행");
        // 사망 애니메이션
        animator.SetTrigger("isDead");
    }

    public void AnimEnd()
    {
        // 로봇 4단계 애니메이션 실행
        robot.Level4Move();
    }

    public void RotateD()
    {
        // 오른쪽으로 회전
        animator.SetTrigger("D");
    }

    public void RotateA()
    {
        // 왼쪽으로 회전
        animator.SetTrigger("A");
    }

    public void ResetState()
    {
        // 애니메이터 초기화
        animator.Rebind();
    }

    void ShowUI()
    {
        MainGameUI.SetActive(true);
    }

    void HideUI()
    {
        MainGameUI.SetActive(false);
    }
}
