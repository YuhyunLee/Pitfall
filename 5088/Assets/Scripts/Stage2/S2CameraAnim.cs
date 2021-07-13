using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2CameraAnim : MonoBehaviour
{
    Animator anim;  // 애니메이터

    [SerializeField] GameObject Stage2GameUI;   // 스테이지2 게임 UI
    [SerializeField] Robot2 robot2;             // 스테이지2 로봇

    private void Start()
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
        // 로봇 4단계 애니메이션 실행
        robot2.Level4();
    }

    public void RotateW()
    {
        // 위로
        anim.SetTrigger("W");
    }

    public void RotateS()
    {
        // 아래로
        anim.SetTrigger("S");
    }

    void ShowUI()
    {
        Stage2GameUI.SetActive(true);
    }

    void HideUI()
    {
        Stage2GameUI.SetActive(false);
    }
}
