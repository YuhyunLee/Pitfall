using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject MainCamera;       // 메인 카메라
    public GameObject Stage1Camera;     // 스테이지 1 카메라
    public GameObject Stage2Camera;     // 스테이지 2 카메라
    public GameObject Stage3Camera;     // 스테이지 3 카메라
    public GameObject Stage4Camera;     // 스테이지 4 카메라
    public GameObject Stage5Camera;     // 스테이지 5 카메라
    public GameObject Stage6Camera;     // 스테이지 6 카메라
    public GameObject Stage7Camera;     // 스테이지 7 카메라

    private void Start()
    {
        // 디폴트 값
        MainCamera.SetActive(true);
    }

    public void MainCameraView()
    {
        MainCamera.SetActive(true);
        Stage1Camera.SetActive(false);
        Stage2Camera.SetActive(false);
        Stage3Camera.SetActive(false);
        Stage4Camera.SetActive(false);
        Stage5Camera.SetActive(false);
        Stage6Camera.SetActive(false);
        Stage7Camera.SetActive(false);
    }

    public void Stage1CameraView()
    {
        MainCamera.SetActive(false);
        Stage1Camera.SetActive(true);
    }

    public void Stage2CameraView()
    {
        MainCamera.SetActive(false);
        Stage2Camera.SetActive(true);
    }

    public void Stage3CameraView()
    {
        MainCamera.SetActive(false);
        Stage3Camera.SetActive(true);
    }

    public void Stage4CameraView()
    {
        MainCamera.SetActive(false);
        Stage4Camera.SetActive(true);
    }

    public void Stage5CameraView()
    {
        MainCamera.SetActive(false);
        Stage5Camera.SetActive(true);
    }

    public void Stage6CameraView()
    {
        MainCamera.SetActive(false);
        Stage6Camera.SetActive(true);
        Debug.Log("카메라6 작동");
    }

    public void Stage7CameraView()
    {
        MainCamera.SetActive(false);
        Stage7Camera.SetActive(true);
    }
}
