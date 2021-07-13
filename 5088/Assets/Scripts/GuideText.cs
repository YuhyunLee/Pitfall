using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideText : MonoBehaviour
{
    void Update()
    {
        // 스페이스 바가 눌러지면
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // UI 비활성화
            gameObject.SetActive(false);
        }
    }
}
