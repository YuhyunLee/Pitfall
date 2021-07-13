using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Story : MonoBehaviour
{
    public Stage5Manager stage5Manager;
    public bool s5_2;
    public bool s5_4;

    public AudioSource t1;
    public AudioSource t2;
    public AudioSource t3;
    public AudioSource t4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stage5Manager.s5_1)
        {
            stage5Manager.s5_1 = false;
            t1.Play();
            Invoke("delay1", 7f);
            Debug.Log("s5_1");
        }
        if (s5_2)
        {
            t1.Stop();
            t2.Play();
            s5_2 = false;
            Debug.Log("s5_2");
        }
        if (stage5Manager.s5_3)
        {
            t2.Stop();
            t3.Play();
            stage5Manager.s5_3 = false;
            Invoke("delay2", 7f);
            Debug.Log("s5_3");
        }
        if (s5_4)
        {
            t3.Stop();
            t4.Play();
            s5_4 = false;
            Debug.Log("s5_4");
        }
        

    }

    void delay1()
    {
        s5_2 = true;
    }
    void delay2()
    {
        s5_4 = true;
    }
    
}
