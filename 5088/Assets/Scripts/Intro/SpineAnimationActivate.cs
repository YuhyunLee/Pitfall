using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpineAnimationActivate : MonoBehaviour
{
    private Animator _animator;
    public GameObject image;
    public GameObject cut7;
    public Image blackImage;

    public AudioSource intro1;
    public AudioSource intro2;
    public AudioSource intro3;
    public AudioSource intro4;
    public AudioSource intro5;
    public AudioSource intro6;
    public AudioSource intro7;
    public AudioSource intro8;

    public float time1, time2, time3, time4, time5, time6;

    [SerializeField] GameObject LoadingUI;  // 로딩중 UI

    private void Awake()
    {
        _animator = GetComponent<Animator>(); 
    }

    private void Start()
    {
        // 시간 딜레이
        intro1.Play();
        Invoke("C1ToC2", time1);
        Invoke("C2ToC3", time2);
        Invoke("C3ToC4", time3);
        Invoke("C4ToC5", time4);
        Invoke("C5ToC6", time5);
        Invoke("C6ToC7", time6);

        
    }

    private void Update()
    {
        

    }
    
    // 스파인 애니메이션 재생을 위한 함수
    void C1ToC2()// 컷1에서 2로 진행
    {
        
        _animator.SetInteger("C1ToC2", 1);
        intro2.Play();
        Debug.Log("2");
    }

    void C2ToC3()// 컷2에서 3로 진행
    {
        _animator.SetInteger("C2ToC3", 2);
        intro3.Play();
        Debug.Log("3");
    }

    void C3ToC4()// 컷3에서 4로 진행
    {
        _animator.SetInteger("C3ToC4", 3);
        intro4.Play();
        intro5.PlayDelayed(3f);
        Debug.Log("4");
    }

    void C4ToC5()// 컷4에서 5로 진행
    {
        _animator.SetInteger("C4ToC5", 4);
        intro6.Play();
        Debug.Log("5");
    }

    void C5ToC6()// 컷5에서 6로 진행
    {
        _animator.SetInteger("C5ToC6", 5);
        intro7.Play();
        Debug.Log("6");
    }

    void C6ToC7()// 컷6에서 7로 진행 후 다음 씬으로
    {
        image.SetActive(true);
        cut7.SetActive(true);
        FadeIn();
        intro8.Play();
        Invoke("FadeOut", 4f);
        Invoke("NextScene", 7f);

    }

    public void FadeIn() // 페이드 인 함수
    {
        StartCoroutine("FadeInCoroutine");
    }
    public void FadeOut() // 페이드 아웃 함수
    {
        StartCoroutine("FadeOutCoroutine");
    }

    IEnumerator FadeInCoroutine() // 페이드 인 코루틴
    {
        float fadeCount = 1; // 초기 투명도 on
        while (fadeCount > 0f) // 투명도 off될 때까지 -0.01
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            blackImage.color = new Color(0, 0, 0, fadeCount);
        }
    }

    IEnumerator FadeOutCoroutine() // 페이드 아웃 코루틴
    {
        float fadeCount = 0; // 초기 투명도 off
        while (fadeCount < 1.0f)// 투명도 on될 때까지 +0.01
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            blackImage.color = new Color(0, 0, 0, fadeCount);
        }
    }

    //다음씬으로
    void NextScene()
    {
        LoadingUI.SetActive(true);
        SceneManager.LoadScene("MachineDungeon");
    }
}
