using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S4CameraAnim : MonoBehaviour
{
    Animator anim;  // 애니메이터

    [SerializeField] GameObject S4GameUI;   // 스테이지4 게임 UI
    [SerializeField] RobotManager4 robot4;  // 로봇 매니저

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ResetState()
    {
        // 애니메이터 초기화
        anim.Rebind();
    }

    public void AnimPlay()
    {
        Debug.Log("AnimPlay실행");
        // 사망 애니메이션
        anim.SetTrigger("isDead");
    }

    public void AnimEnd()
    {
        // 로봇 사망 애니메이션 실행
        robot4.Dead();
    }

    public void RotateD()
    {
        // 오른쪽
        anim.SetTrigger("D");
    }

    public void RotateA()
    {
        // 왼쪽
        anim.SetTrigger("A");
    }

    void ShowUI()
    {
        S4GameUI.SetActive(true);
    }

    void HideUI()
    {
        S4GameUI.SetActive(false);
    }
}
