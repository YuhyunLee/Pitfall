using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2StartTrigger : MonoBehaviour
{
    public bool s2_1 = false;
    public bool s2_2 = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            s2_1 = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))     // e버튼(상호작용 버튼)이 한번 눌렸을 때 true 반환
        {

            s2_2 = true;


        }
    }
}
