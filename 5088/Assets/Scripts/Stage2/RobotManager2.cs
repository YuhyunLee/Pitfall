using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RobotManager2 : MonoBehaviour
{
    [SerializeField]
    Robot2[] robots = new Robot2[2];    // 로봇 2개 배열로 저장                             
    int robotLR;                        // 0이 Left, 1이 Right

    Stage2Game stage2game;      // 로봇이 속한 게임매니저 가져오기

    private void Awake()
    {
        // 스테이지2 게임 매니저 할당
        stage2game = GameObject.Find("Stage2GameManager").GetComponent<Stage2Game>();
    }

    private void OnEnable()
    {
        StartMove();
    }

    void StartMove()
    {
        StartCoroutine("MoveRobot", 10.0f);
    }
    
    IEnumerator MoveRobot(float seconds)
    {
        // 처음엔 10초 후, 그 다음부턴 5초 후
        yield return new WaitForSeconds(seconds);

        // 처음에는 오른쪽 로봇 +1단계
        if(robotLR == 0)
        {
            // 오른쪽 로봇 +1단계
            robots[1].SendMessage("Move");
            robotLR = 1;
        }
        else if(robotLR == 1)
        {
            // 왼쪽 로봇 +1단계
            robots[0].SendMessage("Move");
            robotLR = 0;
        }

        StartCoroutine("MoveRobot", 5.0f);  // 이후 5초마다 번갈아 level+1
    }

    public void BackRobot(int whichRobot)
    {
        // 왼쪽 로봇일 때
        if (whichRobot == 0)
            robots[0].SendMessage("Back");
        // 오른쪽 로봇일 때
        else
            robots[1].SendMessage("Back");
    }

    void AnimEnd()
    {
        // 스테이지2 게임의 Dead 함수 실행
        stage2game.Dead();
    }
}
