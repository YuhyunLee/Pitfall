using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundPManager : MonoBehaviour
{
    // 사운드 패턴 매니저
    public Stage7Manager statusChecker;
    public Stage7GameManager isRandNum;
    
    public GameObject SoundGUI; //GUI 비활성화

    public Animator Volume1;
    public Animator Volume2;
    public Animator Volume3;
    public Animator sGUI;
    public Animator GUI;

    public float setTime = 10f;
    public Text countdownText;

    public AudioSource audioSource;

    public int clickCount1 = 0; // 몇 번 버튼을 클릭 했는가
    public int clickCount2 = 0; // 몇 번 버튼을 클릭 했는가
    public int clickCount3 = 0; // 몇 번 버튼을 클릭 했는가

    public string SoundStatus = "WAIT";

    public bool timeOut = false;

    void Start()
    {
        countdownText.text = setTime.ToString();
    }

    void Update()
    {
        if(SoundStatus == "READY")
        {
            audioSource.Play();
            SoundStatus = "PLAY";
        }
        
        else if (SoundStatus == "PLAY")
        {
            
            BarActive1();
            BarActive2();
            BarActive3();
            Countdown(); // 카운트 다운
            ClearCheck();
            DeadCheck();
            countdownText.text = Mathf.Round(setTime).ToString();
            
        }
        else if (SoundStatus == "DEAD")
        {
            audioSource.Stop();
            sGUI.SetInteger("GUIcheck", 2); // 인터페이스 내리기
            GUI.SetInteger("GUIcheck", 1); // 다시 기본 인터페이스 올라오기
            Reset();
            statusChecker.status = "DEAD";
            SoundGUI.SetActive(false);
        }
        else if (SoundStatus == "CLEAR")
        {
            audioSource.Stop();
            sGUI.SetInteger("GUIcheck", 2); // 인터페이스 내리기
            GUI.SetInteger("GUIcheck", 1); // 다시 기본 인터페이스 올라오기
            isRandNum.isRandNum = false;
            SoundGUI.SetActive(false);
            Reset();
        }
    }
    

    // 버튼 상호작용
    public void ClickBtn1()
    {
        clickCount1++;
    }
    public void ClickBtn2()
    {
        clickCount2++;
    }
    public void ClickBtn3()
    {
        clickCount3++;
    }

    

    // 애니메이션 작동
    public void BarActive1()
    {
        Volume1.SetInteger("ClickCount", clickCount1);
    }
    public void BarActive2()
    {
        Volume2.SetInteger("ClickCount", clickCount2);
    }
    public void BarActive3()
    {
        Volume3.SetInteger("ClickCount", clickCount3);
    }
     // 카운트 다운
    void Countdown()
    {
        if (setTime > 0)
        {
            setTime -= Time.deltaTime;
        }
        else if(setTime < 0)
        {
            timeOut = true;
        }
        
    }
    // 죽음 조건 체크
    public void DeadCheck()
    {
        if(timeOut)
        {
            SoundStatus = "DEAD";
        }
    }

    // 승리 조건 체크
    public void ClearCheck()
    {
        // 시간초과 아니고 각 클릭 카운트가 모두 12이상일 경우 승리
        if(!timeOut && clickCount1 >= 12 && clickCount2 >= 12 && clickCount3 >= 12)
        {
            SoundStatus = "CLEAR"; // 클리어 상태
        }
    }

    public void Reset()
    {
        clickCount1 = 0;
        clickCount2 = 0;
        clickCount3 = 0;
        setTime = 9f;
        Volume1.Rebind();
        Volume2.Rebind();
        Volume3.Rebind();
        SoundStatus = "WAIT";
        timeOut = false;
    }
}
