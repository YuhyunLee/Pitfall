using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s1_12Trigger : MonoBehaviour
{
    public bool s1_12 = false;

    private void OnTriggerEnter(Collider other)
    {
        s1_12 = true;
        // Box Collider 끄고
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
