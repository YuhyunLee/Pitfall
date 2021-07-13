using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour
{
    void Awake()
    {
        // 게임 중 화면이 꺼지지 않게
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        // 해상도 고정
        Screen.SetResolution(1920, 1080, true);
    }

}
