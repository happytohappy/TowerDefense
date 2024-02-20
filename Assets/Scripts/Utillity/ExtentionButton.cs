using UnityEngine;
using UnityEngine.UI;

public class ExtentionButton : Button
{
    public AudioClip m_AudioSource;

    protected override void Start()
    {
        base.Start();

        this.onClick.AddListener(ClickSound);
    }

    private void ClickSound() 
    {
        Managers.Sound.PlaySFX(m_AudioSource);
    }
}