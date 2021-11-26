using System.Collections;
using UnityEngine;

namespace UI
{
    public class BGMController : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;

        [Space]
        [SerializeField] AudioClip[] bgm;
        bool _bgmPlaying;
        IEnumerator _playingCoroutine;

        bool mute;

        public void StartPlaying()
        {
            if (_playingCoroutine != null) StopCoroutine(_playingCoroutine);
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
            audioSource.mute = doMute;
        }

        IEnumerator Play()
        {
            _bgmPlaying = true;
            var index = 0;
            var maxIndex = bgm.Length;
            var breakBetweenClips = 3f;

            while (_bgmPlaying)
            {
                audioSource.clip = bgm[index];
                var clipLenght = bgm[index].length;

                audioSource.Play();
                yield return new WaitForSeconds(clipLenght + 0.1f);

                audioSource.Stop();
                yield return new WaitForSeconds(breakBetweenClips);

                index++;
                if (index >= maxIndex) index = 0;
            }

            yield return null;
        }
    }
}