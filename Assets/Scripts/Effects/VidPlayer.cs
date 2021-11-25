using System;
using UnityEngine;
using UnityEngine.Video;

namespace Effects
{
    public class VidPlayer : MonoBehaviour
    {
        [SerializeField] VideoPlayer vidPlayer;
    
        readonly string _vidPath =  System.IO.Path.Combine(Application.streamingAssetsPath, "Nice.webm");

        void Awake()
        {
            vidPlayer.url = _vidPath;
        }

        void OnEnable()
        {
            vidPlayer.Play();
        }

        void OnDisable()
        {
            vidPlayer.Stop();
        }
    }
}
