using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card3 : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject ImageGame3UI;

    [SerializeField] int cardNum;   // 카드 고유 번호
    public int imgNum;              // 이미지 번호 (계속 바뀜)

    public int nextNum;           // 다음 이미지를 가져올 카드 번호
    public GameObject nextCard;   // 다음 이미지를 가져올 카드

    Animation anim;         // 클릭 애니메이션
    AudioSource clickSound; // 클릭 효과음

    private void Awake()
    {
        anim = GetComponent<Animation>();
        clickSound = GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("카드를 클릭하셨습니다.");

        // 이미지 게임3이 대기 상태일 때
        if(ImgGameManager3.img_3 == ImgGameManager3.IMGGAME3.WAIT)
        {
            // 클릭 애니메이션
            anim.Play();
            // 효과음
            clickSound.Play();

            // 카드 번호 넘겨주고
            ImgGameManager3.thisNum = cardNum;
            // 이미지 게임3 상태 HIT으로 전환
            ImgGameManager3.img_3 = ImgGameManager3.IMGGAME3.HIT;
        }
    }
}
