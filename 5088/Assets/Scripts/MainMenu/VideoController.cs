using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public GameObject myVideo;
    public VideoPlayer videoClip;

    private void Start()
    {
        videoClip.Play();
    }

}
