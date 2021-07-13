using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S5CameraAnim : MonoBehaviour
{
    Animator anim;  // 애니메이터

    [SerializeField] GameObject S5GameUI;   // 스테이지5 게임 UI
    [SerializeField] RobotManager5 robot5;  // 로봇 매니저

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
        robot5.Dead();
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
        S5GameUI.SetActive(true);
    }

    void HideUI()
    {
        S5GameUI.SetActive(false);
    }
}
