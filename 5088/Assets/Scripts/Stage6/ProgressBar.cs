using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    

    public int maximum;
    public int minimum;
    public int current = 0;
    public Image mask;
    public bool btnCheck = false;

    public GameObject stopBtn;
    public GameObject playBtn;

    public GameObject soundBtnOff;
    public GameObject soundBtnOn;

    private void Awake()
    {
        btnCheck = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("BarProcessing");
    }

    // Update is called once per frame
    void Update()
    {
        SoundBtn();
        GetCurrentFill();
    }

    void GetCurrentFill() // 진행바 채우기
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;
    }

    public void btnStopChecker() // 정지 버튼을 눌렀는가
    {
        btnCheck = true;
        stopBtn.SetActive(false);
        playBtn.SetActive(true);

    }

    public void btnPlayChecker() // 재생 버튼을 눌렀는가
    {
        btnCheck = false;
        stopBtn.SetActive(true);
        playBtn.SetActive(false);
    }


    void barCheck() // 진행률 상승 조건 체크
    {
        if (current < 180 && btnCheck == false)
        {
            current++;
        }
    }

    IEnumerator BarProcessing() // 진행바 진행 딜레이
    {
        
        while(true)
        {
            yield return new WaitForSeconds(1f);
            barCheck();
            
        }
        
    }

    public void SoundBtn()
    {
        if (btnCheck)
        {
            soundBtnOn.SetActive(false);
            soundBtnOff.SetActive(true);
        }
        else
        {
            soundBtnOn.SetActive(true);
            soundBtnOff.SetActive(false);
        }

    }


}
