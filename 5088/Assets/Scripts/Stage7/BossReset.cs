using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossReset : MonoBehaviour
{
    public Stage7Manager statusChecker;
    public Stage7GameManager isRandNum;
    public HackBar barChecker;
    public GameObject overMenu;
    public FadeEffect fade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void bossReset()
    {
        barChecker.current = 0;
        statusChecker.status = "GUIDE";
        statusChecker.isDead = false;
        overMenu.SetActive(false);
        fade.FadeIn();
        barChecker.btnCheck = true;
        isRandNum.isRandNum = false;
    }
}
