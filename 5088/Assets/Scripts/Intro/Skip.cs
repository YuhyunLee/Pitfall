using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip : MonoBehaviour
{
    [SerializeField] GameObject LoadingUI;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            LoadingUI.SetActive(true);
            SceneManager.LoadScene("MachineDungeon");
        }
    }
}
