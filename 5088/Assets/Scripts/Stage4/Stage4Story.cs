using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4Story : MonoBehaviour
{
    public S4StartTrigger s4StartTrigger;
    public bool s4_2;
    public bool s4_4;
    public bool s4_5;
    public Stage4Manager stage4Manager;

    public bool checker1;
    public bool checker2;

    public AudioSource t1;
    public AudioSource t2;
    public AudioSource t3;
    public AudioSource t4;
    public AudioSource t5;
    public AudioSource t6;

    // Start is called before the first frame update
    void Update()
    {
        if (!checker1)
        {
            if (!checker2)
            {
                if (s4StartTrigger.s4_1)
                {
                    s4StartTrigger.s4_1 = false;
                    t1.Play();
                    Invoke("delay1", 6.5f);
                    checker2 = true;
                    Debug.Log("s4_1");
                }
                if (s4_2)
                {
                    t1.Stop();
                    t2.Play();
                    s4_2 = false;
                    Debug.Log("s4_2");
                }
            }
            
        }
        if (s4StartTrigger.s4_3)
        {
            checker1 = true;
            t1.Stop();
            t2.Stop();
            t3.Play();
            s4StartTrigger.s4_3 = false;
            Invoke("delay2", 6.2f);
            Debug.Log("s4_3");
        }
        if (s4_4)
        {
            t3.Stop();
            t4.Play();
            s4_4 = false;
            Invoke("delay3", 9.3f);
            Debug.Log("s4_4");
        }
        if (s4_5)
        {
            t4.Stop();
            t5.Play();
            s4_5 = false;
            Debug.Log("s4_5");
        }
        if (stage4Manager.s4_6)
        {
            t5.Stop();
            t6.Play();
            stage4Manager.s4_6 = false;
            Debug.Log("s4_6");
        }


    }

    void delay1()
    {
        s4_2 = true;
    }
    void delay2()
    {
        s4_4 = true;
    }
    void delay3()
    {
        s4_5 = true;
    }
}
