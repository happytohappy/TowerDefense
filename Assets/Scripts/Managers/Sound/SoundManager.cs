using UnityEngine;
using System;
using System.Collections.Generic;

public enum AudioEnum
{
    Hit,
    GameStart,
    MainBGM,
    BattleBGM,
}

[Serializable]
public class Audio
{
    public AudioEnum m_type;
    public AudioClip m_clip;
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSourceBG = null;
    [SerializeField] private AudioSource m_AudioSourceSFX = null;

    [SerializeField] private List<Audio> m_AudioClip = new List<Audio>();

    private Dictionary<AudioEnum, AudioClip> AudioDic = new Dictionary<AudioEnum, AudioClip>();
         
    public void Init()
    {
        foreach (var e in m_AudioClip)
            AudioDic.Add(e.m_type, e.m_clip);
    }

    public void SFXVolumChange()
    {
        m_AudioSourceSFX.volume = Managers.User.UserData.SFXSoundVolum;
    }

    public void BGMVolumChange()
    {
        m_AudioSourceBG.volume = Managers.User.UserData.BGMSoundVolum;
    }

    public void PlayBGM()
    {
        m_AudioSourceBG.Play();
    }

    public void PlayBGM(AudioEnum in_audio_type)
    {
        if (AudioDic.TryGetValue(in_audio_type, out var clip))
        {
            m_AudioSourceBG.clip = clip;
            PlayBGM();
        }
    }

    public void PlaySFX()
    {
        m_AudioSourceSFX.Play();
    }

    public void PlaySFX(AudioClip in_audio_clip)
    {
        m_AudioSourceSFX.PlayOneShot(in_audio_clip);
    }

    public void PlaySFX(AudioEnum in_audio_type)
    {
        if (AudioDic.TryGetValue(in_audio_type, out var clip))
            PlaySFX(clip);
    }
}