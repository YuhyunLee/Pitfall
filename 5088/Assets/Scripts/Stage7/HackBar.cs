using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackBar : MonoBehaviour
{
    // 값 인스펙터에서 확인하기 위한 것
    public int maximum;
    public int minimum;
    public int current = 0; // 현 수치
    public Image mask; // 바
    public bool btnCheck = true; // 이 값이 참이면 진행률 정지

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("BarProcessing");
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill(); // 진행바를 채워지도록 보여지는 로직
    }

    void GetCurrentFill() // 진행바 채우기
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;
    }

    void barCheck() // 진행률 상승 조건 체크
    {
        if (current < 160 && btnCheck == false)
        {
            current++;
        }
    }

    IEnumerator BarProcessing() // 진행바 진행 딜레이
    {

        while (true)
        {
            yield return new WaitForSeconds(1f);
            barCheck();

        }

    }

}
