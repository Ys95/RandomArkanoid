using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    
    [Space]
    [SerializeField] AudioClip[] bgm;
  
    bool mute;
    bool _bgmPlaying;
    IEnumerator _playingCoroutine;

    public void StartPlaying()
    {
        if(_playingCoroutine!=null) StopCoroutine(_playingCoroutine);
        _playingCoroutine = Play();
        StartCoroutine(_playingCoroutine);
    }

    public void StopPlaying()
    {
        _bgmPlaying = false;
        audioSource.Stop();
    }

    public void Mute(bool doMute)
    {
        audioSource.mute=doMute;
    }
    
    IEnumerator Play()
    {
        _bgmPlaying = true;
        int index = 0;
        int maxIndex = bgm.Length;

        while (_bgmPlaying)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = bgm[index];
                audioSource.Play();
                index++;
                if (index >= maxIndex) index = 0;
            }
            yield return null;
        }
    }
}
