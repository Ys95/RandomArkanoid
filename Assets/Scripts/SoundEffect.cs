using UnityEngine;

[CreateAssetMenu(fileName = "_SoundEffect", menuName = "Sound/SoundEffect")]
public class SoundEffect : ScriptableObject
{
    [SerializeField] AudioClip clip;
    [Range(0f, 3f)] [SerializeField] float volume =1f;
    [Range(0, 3f)] [SerializeField] float pitch=1f;

    public float Length => clip.length;

    public void Play(AudioSource source)
    {
        source.volume = volume;
        source.pitch = pitch;
        
        source.PlayOneShot(clip);
    }
    
    public void PlayLoop(AudioSource source)
    {
        source.volume = volume;
        source.pitch = pitch;

        source.loop = true;
        source.clip = clip;
        source.Play();
    }
}
