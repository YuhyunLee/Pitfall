using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage7Story : MonoBehaviour
{
    public ElevatorTeleport s7_1;
    public Stage7Manager s7_2;
    public bool s7_3;
    
    

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
        if (s7_1.s7_1)
        {
            s7_1.s7_1 = false;
            t1.Play();
        }
        if (s7_2.s7_2)
        {
            t1.Stop();
            t2.Play();
            s7_2.s7_2 = false;
            Invoke("delay1", 4.5f);
        }
        if (s7_3)
        {
            t2.Stop();
            t3.Play();
            s7_3 = false;
        }
        if (s7_2.s7_4)
        {
            t3.Stop();
            t4.Play();
            s7_2.s7_4 = false;
        }
    }

    void delay1()
    {
        s7_3 = true;
    }
}
