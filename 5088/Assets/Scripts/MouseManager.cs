using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public EscapeMenu paused;

    public static bool isGameMode = false;

    // Start is called before the first frame update
    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;       // 마우스 커서 잠금
       Cursor.visible = false;                         // 마우스 커서 안보이게
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameMode)          // 게임모드이거나 esc 눌렀을 때
        {
            // 마우스 커서 보임
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if(!paused.paused)
        {
            // 마우스 커서 잠금
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
