using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockMove : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public AudioSource audioSource; // 전용 사운드

    public GunPManager gunManager;
    public float speed;
    private int count = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (count == 0)
        {
            Vector3 moveDirection = new Vector3(1, 0f, 0f);

            transform.Translate(moveDirection * speed * Time.deltaTime);
        }
        else if (count == 1)
        {
            Vector3 moveDirection = new Vector3(-1, 0f, 0f);

            transform.Translate(moveDirection * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("충돌");
        if (col.gameObject.tag == "borderR")
        {
            count = count + 1;

        }
        else if (col.gameObject.tag == "borderL")
        {
            count = count - 1;

        }
    }

    void SpaceKey()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gunManager.GunStatus == "PLAY")
        {
            
            gunManager.clickCount++;
            audioSource.Play();
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "cleararea")
        {
            Invoke("SpaceKey", 2f);
        }

    }
}
