using UnityEngine;

[CreateAssetMenu(fileName = "_SoundEffect", menuName = "Sound/SoundEffect")]
public class SoundEffect : ScriptableObject
{
    [SerializeField] AudioClip clip;
    [Range(0f, 3f)] [SerializeField] float volume =1f;
    [Range(0, 3f)] [SerializeField] float pitch=1f;

    GameObject _soundObject;
    Transform _soundObjectTransform;
    AudioSource _soundPlayer;

    public float Length => clip.length;

    void CreateSoundPlayer()
    {
        _soundObject = Instantiate(new GameObject());
        _soundObject.AddComponent<AudioSource>();
        _soundPlayer = _soundObject.GetComponent<AudioSource>();
        _soundObjectTransform = _soundObject.transform;
    }
    
    public void PlayDetached(Vector2 pos)
    {
        if(_soundPlayer == null) CreateSoundPlayer();
        
        _soundObjectTransform.position = pos;
        
        _soundPlayer.volume = volume;
        _soundPlayer.pitch = pitch;
        
        _soundPlayer.PlayOneShot(clip);
    }
    
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
