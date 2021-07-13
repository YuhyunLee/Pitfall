using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S7Story1 : MonoBehaviour
{
    public Hall2InteractiveObject Hall2InteractiveObject;
    public bool s6_3;
    public bool s6_4;
    public Stage6Manager stage6Manager;
    public bool s6_6;

    public AudioSource t1;
    public AudioSource t2;
    public AudioSource t3;
    public AudioSource t4;
    public AudioSource t5;
    public AudioSource t6;
    void Update()
    {
        if (Hall2InteractiveObject.s6_1)
        {
            Hall2InteractiveObject.s6_1 = false;
            t1.Play();
        }
        if (Hall2InteractiveObject.s6_2)
        {
            t1.Stop();
            t2.Play();
            Hall2InteractiveObject.s6_2 = false;
            Invoke("delay1", 3.5f);
        }
        if (s6_3)
        {
            t2.Stop();
            t3.Play();
            s6_3 = false;
            Invoke("delay2", 4.5f);
        }
        if (s6_4)
        {
            t3.Stop();
            t4.Play();
            s6_4 = false;
        }
        if (stage6Manager.s6_5)
        {
            t4.Stop();
            t5.Play();
            stage6Manager.s6_5 = false;
            Invoke("delay3", 5.2f);
        }
        if (s6_6)
        {
            t5.Stop();
            t6.Play();
            s6_6 = false;
        }

    }

    void delay1()
    {
        s6_3 = true;
    }
    void delay2()
    {
        s6_4 = true;
    }
    void delay3()
    {
        s6_6 = true;
    }
}
