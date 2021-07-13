using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetManager : MonoBehaviour
{
    public ProgressBar barChecker;
    public Stage6Manager statusChecker;
    public Robot6Controller levelChecker1;
    public Robot6Controller levelChecker2;
    public RobotUpController levelChecker3;
    public RobotBackController levelChecker4;

    public Animator RobotAnimator1;
    public Animator RobotAnimator2;
    public Animator RobotAnimator3;
    public Animator RobotAnimator4;
    public Animator Camera6Animator;

    public GameObject GameOverUI;
    public GameObject GameOverUI2;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StatusReset()
    {
        barChecker.current = 0;
        barChecker.btnCheck = true;
        statusChecker.status = "GUIDE";
        statusChecker.isDead = false;
        levelChecker1.level = 1;
        levelChecker2.level = 1;
        levelChecker3.level = 1;
        levelChecker4.level = 1;
        RobotAnimator1.Rebind();
        RobotAnimator2.Rebind();
        RobotAnimator3.Rebind();
        RobotAnimator4.Rebind();
        GameOverUI.SetActive(false);
        GameOverUI2.SetActive(false);

    }

    
}
