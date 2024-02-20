using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSourceBG = null;
    [SerializeField] private AudioSource m_AudioSourceSFX = null;

    public void PlayBGM()
    {
        m_AudioSourceBG.Play();
    }

    public void PlaySFX()
    {
        m_AudioSourceSFX.Play();
    }

    public void PlaySFX(AudioClip in_audio_clip)
    {
        m_AudioSourceSFX.PlayOneShot(in_audio_clip);
    }
}