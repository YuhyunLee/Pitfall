using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitArea : MonoBehaviour
{

    public GameObject InteractiveUI;       // 상호작용 UI

    public int secretCount = 0;
    

   

    private void OnTriggerStay(Collider other)
    {
        // E버튼(UI) 나타나도록 하고
        InteractiveUI.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E))     // e버튼(상호작용 버튼)이 한번 눌렸을 때 true 반환
        {
            // Box Collider 끄고
            gameObject.GetComponent<BoxCollider>().enabled = false;
            // UI 끄고
            InteractiveUI.SetActive(false);
            gameObject.SetActive(false);

            if(secretCount >= 2)
            {
                SceneManager.LoadScene("HidED");
            }
            else
            {
                SceneManager.LoadScene("NorED");
            }





        }
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
