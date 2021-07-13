using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S1Story : MonoBehaviour
{
    public ImageGame imageGame;
    public bool s1_3;
    public bool s1_4;
    public bool s1_6;
    public bool s1_7;
    public bool s1_8;
    public bool s1_9;
    public bool s1_10;
    public Stage1Manager stage1Manager;
    public s1_12Trigger s1_12Trigger;

    public GameObject trigger;

    public bool checker1 = false;

    public AudioSource t1;
    public AudioSource t2;
    public AudioSource t3;
    public AudioSource t4;
    public AudioSource t5;
    public AudioSource t6;
    public AudioSource t7;
    public AudioSource t8;
    public AudioSource t9;
    public AudioSource t10;
    public AudioSource t11;
    public AudioSource t12;

    // Start is called before the first frame update
    void Start()
    {
        t1.Play();
        Debug.Log("s1_1");
    }

    // Update is called once per frame
    void Update()
    {
        if (imageGame.s1_2)
        {
            imageGame.s1_2 = false;
            t1.Stop();
            t2.Play();
            Invoke("delay1", 5.5f);
            Debug.Log("s1_2");
        }
        if (!checker1)
        {
            if (s1_3)
            {
                s1_3 = false;
                t2.Stop();
                t3.Play();
                Invoke("delay2", 7.5f);
                Debug.Log("s1_3");
            }
            if (s1_4)
            {
                t3.Stop();
                t4.Play();
                s1_4 = false;
                Debug.Log("s1_4");
            }
        }
        
        if (imageGame.s1_5)
        {
            t2.Stop();
            t3.Stop();
            t4.Stop();
            t5.Play();
            checker1 = true;
            imageGame.s1_5 = false;
            Invoke("delay3", 5f);
            Debug.Log("s1_5");
        }
        if (s1_6)
        {
            t5.Stop();
            t6.Play();
            s1_6 = false;
            Invoke("delay4", 7f);
            Debug.Log("s1_6");
        }
        if (s1_7)
        {
            t6.Stop();
            t7.Play();
            s1_7 = false;
            Invoke("delay5", 8.5f);
            Debug.Log("s1_7");
        }
        if (s1_8)
        {
            t7.Stop();
            t8.Play();
            s1_8 = false;
            Invoke("delay6", 13.5f);
            Debug.Log("s1_8");
        }
        if (s1_9)
        {
            t8.Stop();
            t9.Play();
            s1_9 = false;
            Invoke("delay7", 11.5f);
            Debug.Log("s1_9");
        }
        if (s1_10)
        {
            t9.Stop();
            t10.Play();
            s1_10 = false;
            Debug.Log("s1_10");
        }
        if (stage1Manager.s1_11)
        {
            t10.Stop();
            t11.Play();
            stage1Manager.s1_11 = false;
            trigger.SetActive(true);
            Debug.Log("s1_11");
        }
        if (s1_12Trigger.s1_12)
        {
            t11.Stop();
            t12.Play();
            s1_12Trigger.s1_12 = false;
            Debug.Log("s1_12");
        }
    }

    void delay1()
    {
        s1_3 = true;
    }
    void delay2()
    {
        s1_4 = true;
    }
    void delay3()
    {
        s1_6 = true;
    }
    void delay4()
    {
        s1_7 = true;
    }
    void delay5()
    {
        s1_8 = true;
    }
    void delay6()
    {
        s1_9 = true;
    }
    void delay7()
    {
        s1_10 = true;
    }
}
