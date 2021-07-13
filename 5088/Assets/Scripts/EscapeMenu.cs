using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    public GameObject ESCmenu;

    public bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        ESCmenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
    }

    // esc메뉴 활성화시 일시정지
    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }
        if (paused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            ESCmenu.SetActive(true);
            Time.timeScale = 0;
        }
        if (!paused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            ResumeBtn();
        }
    }

    public void QuitBtn()
    {
        Application.Quit();
        Debug.Log("종료");
    }

    public void ResumeBtn()
    {
        
        ESCmenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
