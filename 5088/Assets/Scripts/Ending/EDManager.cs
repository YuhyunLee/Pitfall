using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EDManager : MonoBehaviour
{
    public float delayTime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ToMainMenu", delayTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
