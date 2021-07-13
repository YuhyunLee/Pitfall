using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S4StartTrigger : MonoBehaviour
{
    public bool s4_1 = false;
    public bool s4_3 = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            s4_1 = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))     // e버튼(상호작용 버튼)이 한번 눌렸을 때 true 반환
        {

            s4_3 = true;
            

        }
    }
}
