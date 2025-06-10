using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgMusic;
    public AudioSource sfxSource;
    public AudioClip shootClip;
    public AudioClip hitClip;
    public AudioClip pickupClip;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}