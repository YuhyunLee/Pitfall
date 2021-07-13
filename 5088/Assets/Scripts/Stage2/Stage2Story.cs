using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Story : MonoBehaviour
{
    public Stage2StartTrigger stage2StartTrigger;
    public bool s2_3;
    public Stage2Manager stage2Manager;
    public bool s2_5;
    public s2_6Trigger s2_6Trigger;

    public GameObject Trigger;

    public AudioSource t1;
    public AudioSource t2;
    public AudioSource t3;
    public AudioSource t4;
    public AudioSource t5;
    public AudioSource t6;

    // Start is called before the first frame update
    void Update()
    {
        if (stage2StartTrigger.s2_1)
        {
            stage2StartTrigger.s2_1 = false;
            t1.Play();
            Debug.Log("s2_1");
        }
        if (stage2StartTrigger.s2_2)
        {
            stage2StartTrigger.s2_2 = false;
            t1.Stop();
            t2.Play();
            Invoke("delay1", 6f);
            Debug.Log("s2_2");
        }
        if (s2_3)
        {
            t2.Stop();
            t3.Play();
            s2_3 = false;
            Debug.Log("s2_3");
        }
        if (stage2Manager.s2_4)
        {
            t3.Stop();
            t4.Play();
            stage2Manager.s2_4 = false;
            Invoke("delay2", 4f);
            Debug.Log("s2_4");
        }
        if (s2_5)
        {
            t4.Stop();
            t5.Play();
            s2_5 = false;
            Trigger.SetActive(true);
            Debug.Log("s2_5");
        }
        if (s2_6Trigger.s2_6)
        {
            t5.Stop();
            t6.Play();
            s2_6Trigger.s2_6 = false;
            Debug.Log("s2_6");
        }


    }

    void delay1()
    {
        s2_3 = true;
    }
    void delay2()
    {
        s2_5 = true;
    }

}
