using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S3CameraAnim : MonoBehaviour
{
    Animation anim;     // 애니메이션
    [SerializeField] Robot3 robot3;     // 로봇 가져오기

    Quaternion originRotation;     // 초기 회전값

    private void Start()
    {
        // 애니메이션 가져오기
        anim = GetComponent<Animation>();
        // 초기 회전 값 저장
        originRotation = transform.rotation;
    }

    public void AnimPlay()
    {
        Debug.Log("AnimPlay실행");
        // 사망 애니메이션
        anim.Play();
    }

    public void AnimEnd()
    {
        // 로봇 사망 애니메이션 실행
        robot3.Dead();
    }

    public void ResetState()
    {
        // 카메라 rotation 초기화
        transform.rotation = originRotation;
    }
}
