using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] GameObject ObstacleUI;       // 상호작용 UI
    [SerializeField] GameObject[] objects;  // outline 효과 줄 오브젝트 배열

    Animation anim;     // 애니메이션

    private void Start()
    {
        anim = GetComponent<Animation>();
    }

    private void OnTriggerStay(Collider other)
    {
        // E버튼(UI) 나타나도록 하고
        ObstacleUI.SetActive(true);
        // 아웃라인 효과
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].GetComponent<Outline>().enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.E))     // e버튼(상호작용 버튼)이 한번 눌렸을 때 true 반환
        {
            // 조건이 만족되었다면
            if (AllStageManager.clearStage >= 2)
            {
                // Box Collider 끄고
                gameObject.GetComponent<BoxCollider>().enabled = false;
                // UI 끄고
                ObstacleUI.SetActive(false);

                // 아웃라인 끄기
                for (int i = 0; i < objects.Length; i++)
                {
                    objects[i].GetComponent<Outline>().enabled = false;
                }

                // 장애물 움직이기
                anim.Play();
                Debug.Log("장애물 움직입니다");
            }
            else
                Debug.Log("스테이지2를 클리어하십시오");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // UI 끄고
        ObstacleUI.SetActive(false);
        // 아웃라인 끄기
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].GetComponent<Outline>().enabled = false;
        }
    }
}
