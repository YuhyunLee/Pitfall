using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeyCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 originPos;  // 초기 위치

    public OnDrop CheckArea;    // 카드 키 검사 영역

    private void Start()
    {
        // 초기 위치 저장
        originPos = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 카드 키의 위치는 마우스의 위치
        transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 카드 키의 위치는 마우스의 위치
        transform.position = eventData.position;

        // 레이캐스트 블록 꺼줌
        this.GetComponent<Image>().raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 원래 자리로
        transform.position = originPos;

        // 레이캐스트 블록 켜줌
        this.GetComponent<Image>().raycastTarget = true;

        // 검사 영역에 드롭되었고 스테이지3 메인 게임이 대기 상태일 때
        if (CheckArea.isDrop && Stage3MainGame.s3_main == Stage3MainGame.S3MAINGAME.WAIT)
        {
            // isDrop 다시 초기화
            CheckArea.isDrop = false;

            // 카드 키 비활성화
            gameObject.SetActive(false);     

            // 스테이지3 게임 체크 상태로 전환
            Stage3MainGame.s3_main = Stage3MainGame.S3MAINGAME.CHECK;
        }

    }
}
