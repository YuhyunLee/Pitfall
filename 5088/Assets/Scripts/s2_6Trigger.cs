using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s2_6Trigger : MonoBehaviour
{
    public bool s2_6 = false;

    private void OnTriggerEnter(Collider other)
    {
        s2_6 = true;
        // Box Collider 끄고
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

}
