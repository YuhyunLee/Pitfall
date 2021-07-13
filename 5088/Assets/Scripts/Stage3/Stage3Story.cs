using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Story : MonoBehaviour
{
    public S3StartTrigger s3StartTrigger;
    public Stage3MainGame stage3MainGame;

    public AudioSource t1;
    public AudioSource t2;
    public AudioSource t3;
    public AudioSource t4;
    public AudioSource t5;
    public AudioSource t6;
    public AudioSource t7;
    public AudioSource t8;
    public AudioSource t9;

    // Start is called before the first frame update
    void Update()
    {
        if (s3StartTrigger.s3_1)
        {
            s3StartTrigger.s3_1 = false;
            t1.Play();
            Debug.Log("s3_1");
        }
        if (s3StartTrigger.s3_2)
        {
            t1.Stop();
            t2.Play();
            s3StartTrigger.s3_2 = false;
            Debug.Log("s3_2");
        }
        if (stage3MainGame.s3_3)
        {
            t2.Stop();
            t3.Play();
            stage3MainGame.s3_3 = false;
            Debug.Log("s3_3");
        }
        if (stage3MainGame.s3_4)
        {
            t3.Stop();
            t4.Play();
            stage3MainGame.s3_4 = false;
            Debug.Log("s3_4");
        }
        if (stage3MainGame.s3_5)
        {
            t4.Stop();
            t5.Play();
            stage3MainGame.s3_5 = false;
            Debug.Log("s3_5");
        }
        if (stage3MainGame.s3_6)
        {
            t5.Stop();
            t6.Play();
            stage3MainGame.s3_6 = false;
            Debug.Log("s3_6");
        }
        if (stage3MainGame.s3_7)
        {
            t6.Stop();
            t7.Play();
            stage3MainGame.s3_7 = false;
            Debug.Log("s3_7");
        }
        if (stage3MainGame.s3_8)
        {
            t7.Stop();
            t8.Play();
            stage3MainGame.s3_8 = false;
            Debug.Log("s3_8");
        }
        if (stage3MainGame.s3_9)
        {
            t8.Stop();
            t9.Play();
            stage3MainGame.s3_9 = false;
            Debug.Log("s3_9");
        }
        


    }

}
