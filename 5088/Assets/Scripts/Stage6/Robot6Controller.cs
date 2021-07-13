using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot6Controller : MonoBehaviour
{

    public Stage6Manager statusCheck;
    public ProgressBar barChecker;
    public AudioSource _audioSourceF;
    public AudioSource _audioSourceC;

    public GameObject censor;

    public Animator _animator; // 사용할 애니메이터

    public int level = 1;
    public float delayTime;
    public float backTime;
    public float nextTime = 0f;

    private void OnEnable()
    {
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 로봇 단계 클리핑
        if (level <= 1)
            level = 1;

        // 로봇이 4단계고 진행률이 180 미만이면
        if (level >= 4 && barChecker.current < 180)
        {
            if (statusCheck.status != "DEAD")
            {
                statusCheck.status = "DEAD";
            }
        }
        
        if(statusCheck.status == "PLAY")
        {
            RobotMove();
        }
    }


    // 로봇 이동
    void RobotMove()
    {
        //후퇴
        if (barChecker.btnCheck == true)
        {
            if (nextTime >= backTime)
            {
                _animator.SetInteger("Level", level);
                _audioSourceF.Play();
                activateSencor();
                level--;
                nextTime = 0;
            }
            else
            {
                nextTime += Time.deltaTime;
            }
        }
        //전진
        else if (barChecker.btnCheck == false)
        {
            if (nextTime >= delayTime)
            {
                _animator.SetInteger("Level", level); // 애니메이션 재생
                _audioSourceC.Play();
                activateSencor();
                level++;
                nextTime = 0;
            }
            else
            {
                nextTime += Time.deltaTime;
            }
        }
    }

    void activateSencor()
    {
        censor.SetActive(true);
        StartCoroutine("SencorDown");
    }

    IEnumerator SencorDown()
    {
        yield return new WaitForSeconds(2f);
        censor.SetActive(false);
    }

}
