using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudio : MonoBehaviour
{
    public AudioSource _audioSource;

    public GameObject sencor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Robot")
        {
            activateAudio();
            activateSencor();
            Debug.Log("충돌");
        }
    }

    void activateSencor()
    {
        sencor.SetActive(true);
        StartCoroutine("SencorDown");
    }

    IEnumerator SencorDown()
    {
        yield return new WaitForSeconds(2f);
        sencor.SetActive(false);
    }

    void activateAudio()
    {
        _audioSource.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
