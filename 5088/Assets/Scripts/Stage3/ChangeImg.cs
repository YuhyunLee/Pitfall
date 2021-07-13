using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImg : MonoBehaviour
{
    GameObject thisCard;
    GameObject nextCard;

    // 바뀌는 순서
    // 5 -> 1 -> 9 -> 2 -> 6 -> 4 -> 12 -> 8 -> 3 -> 11 -> 7 -> 10
    int[] order = { 0, 5, 1, 9, 2, 6, 4, 12, 8, 3, 11, 7, 10 };     // 1~12 인덱스까지

    [SerializeField] GameObject[] cards = new GameObject[13];

    private void Start()
    {
        cards[0] = null;
    }

    public void ChangeCard()
    {
        // 첫 번째 카드 이미지 저장
        int firstImgNum = cards[order[1]].GetComponent<Card3>().imgNum;
        Sprite firstSprite = cards[order[1]].GetComponent<Image>().sprite;

        // 처음부터 11번째까지 바꿈
        for (int i = 1; i <= 11; i++)
        {
            thisCard = cards[order[i]];
            nextCard = thisCard.GetComponent<Card3>().nextCard;

            // 이미지 번호와 스프라이트 바꿔주기
            thisCard.GetComponent<Card3>().imgNum = nextCard.GetComponent<Card3>().imgNum;
            thisCard.GetComponent<Image>().sprite = nextCard.GetComponent<Image>().sprite;
        }

        // 마지막 카드에 첫 번째 카드 정보 저장
        thisCard = cards[order[12]];

        thisCard.GetComponent<Card3>().imgNum = firstImgNum;
        thisCard.GetComponent<Image>().sprite = firstSprite;
    }
}
