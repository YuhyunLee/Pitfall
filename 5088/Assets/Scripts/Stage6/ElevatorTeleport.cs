using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTeleport : MonoBehaviour
{
    public FadeEffect fadeEffect;
    public AudioSource audioSource;

    public GameObject InteractiveUI;       // 상호작용 UI
    public GameObject panel;

    public GameObject goal;
    public GameObject boss;
    public GameObject player;

    public bool s7_1 = false;

    

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
            //gameObject.SetActive(false);
            player.transform.position = goal.transform.position; // 플레이어 승강기로 이동
            StartCoroutine("goBoss");

        }
    }

    IEnumerator goBoss()
    {
        yield return new WaitForSeconds(1f);
        panel.SetActive(true);
        fadeEffect.FadeOut();
        audioSource.Play();
        yield return new WaitForSeconds(4f);
        player.transform.position = boss.transform.position; // 플레이어 지하1층으로 이동
        yield return new WaitForSeconds(2f);
        fadeEffect.FadeIn();
        yield return new WaitForSeconds(2f);
        panel.SetActive(false);
        s7_1 = true;

    }

    // 충돌 범위 밖으로 나가면 E 인터페이스 끄기
    private void OnTriggerExit(Collider other)
    {
        // UI 끄고
        InteractiveUI.SetActive(false);
    }
}
