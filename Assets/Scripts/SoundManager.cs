using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AudioType
{
    Win,
    Jump,
    Lose,
    Destroy
}


public class SoundManager : MonoBehaviour
{
    
    private AudioSource audioSource;
    public AudioClip winSound;
    public AudioClip jumpSound;
    public AudioClip loseSound;
    public AudioClip destroySound;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
    }

    public void Play(AudioType audioType)
    {
        switch (audioType)
        {
            case AudioType.Win:
                PlayWinSound();
                break;
            case AudioType.Jump:
                PlayJumpSound();
                break;
            case AudioType.Lose:
                PlayLoseSound();
                break;
            case AudioType.Destroy:
                PlayDestroySound();
                break;
            default:
                break;
        }
    }

    private void PlayWinSound()
    {
        audioSource.PlayOneShot(winSound);
    }
    private void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }
    private void PlayLoseSound()
    {
        audioSource.PlayOneShot(loseSound);
    }
    private void PlayDestroySound()
    {
        audioSource.PlayOneShot(destroySound);
    }
}
