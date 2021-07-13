using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hall2ActiveTrigger : MonoBehaviour
{
    public GameObject Hall2sitTrigger;

    // Update is called once per frame
    void Update()
    {
        
    }

    // 인증실2 들어가면 Hall2의 컴퓨터 사용 가능

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Hall2sitTrigger.SetActive(true);
            Debug.Log("보안구역2 가능");
        }
        
    }

}
