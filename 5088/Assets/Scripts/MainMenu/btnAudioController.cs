using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnAudioController : MonoBehaviour
{
    public AudioSource btnSound;   

    

    public void btnSoundOn() // 버튼 소리 재생
    {
        btnSound.PlayOneShot(btnSound.clip);
    }
}
