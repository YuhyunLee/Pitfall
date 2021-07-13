using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private int cardNumber;     // 카드 고유 번호
    public int imageNumber;     // 이미지 번호
    public Sprite backImg;      // 뒷면 스프라이트
    public Sprite frontImg;     // 앞면 스프라이트

    Animation anim;     // 클릭 애니메이션
    AudioSource clickSound;  // 클릭 효과음

    private void Awake()
    {
        anim = GetComponent<Animation>();
        clickSound = GetComponent<AudioSource>();
        
        // 처음에 생성되었을 때는 무조건 앞면
        gameObject.GetComponent<Image>().sprite = frontImg;
    }

    private void OnEnable()
    {
        // 활성화될때마다 앞면
        gameObject.GetComponent<Image>().sprite = frontImg;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // 카드가 닫힌 상태면서 대기상태일 때
        if (ImageGame.state == ImageGame.STATE.WAIT)
        {
            Debug.Log("카드를 클릭하셨습니다.");
            anim.Play();                            // 클릭 애니메이션
            clickSound.Play();                      // 클릭 효과음

            ImageGame.cardNum = cardNumber;         // 카드 번호 넘겨줌
            ImageGame.state = ImageGame.STATE.HIT;  // 클릭 상태로 전환
            OpenCard();                             // 카드 오픈
        }
    }

    public void OpenCard()
    {
        Debug.Log("카드 앞면을 보여드립니다.");
        // 앞면으로 스프라이트 변경
        gameObject.GetComponent<Image>().sprite = frontImg;
    }

    public void CloseCard()
    {
        Debug.Log("카드 뒷면을 보여드립니다.");
        // 뒷면으로 스프라이트 변경
        gameObject.GetComponent<Image>().sprite = backImg;
    }

    // 애니메이션 & 사운드 추후에 추가
}
