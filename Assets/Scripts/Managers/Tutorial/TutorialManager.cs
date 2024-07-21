using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using Abu;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject m_tutorial_bg = null;
    [SerializeField] private TutorialFadeImage m_tutorial_fade_image = null;
    [SerializeField] private Animator m_tutorial_ani = null;

    public Transform TutorialBG => m_tutorial_bg.transform;
    public bool TutorialProgress { get; set; } = false;

    private GameObject m_tuto_object = null;
    private Animator m_cashing_ani = null;
    private TutorialHighlight m_cashing_highlight;

    // 튜토리얼 관련해서는 하드코딩 하자.
    // 1. 모집
    // 2. 승급
    // 3. 레벨업
    // 4. 마을
    // 5. 진입

    // 6. 스테이지 진행

    // 7. 장비
    // 8. 보물
    // 9. 출석
    // 10. 미션

    public void TutorialStart(GameObject in_go, ETutorialDir in_dir = ETutorialDir.None, Vector3 in_offset = new Vector3(), string in_str_key = "")
    {
        StartCoroutine(CoTutorialStart(in_go, in_dir, in_offset, in_str_key));
    }

    private IEnumerator CoTutorialStart(GameObject in_go, ETutorialDir in_dir = ETutorialDir.None, Vector3 in_offset = new Vector3(), string in_str_key = "")
    {
        yield return null;

        TutorialProgress = true;

        m_tuto_object = in_go;

        m_tutorial_bg.transform.SetAsLastSibling();
        m_tutorial_bg.gameObject.SetActive(true);

        m_cashing_highlight = in_go.AddComponent<TutorialHighlight>();
        m_tutorial_fade_image.AddHole(m_cashing_highlight.Hole);

        m_cashing_ani = in_go.AddComponent<Animator>();
        m_cashing_ani.runtimeAnimatorController = m_tutorial_ani.runtimeAnimatorController;

        if (in_dir != ETutorialDir.None)
            Util.OpenTutorialToolTip(in_dir, in_go.transform, in_offset, in_str_key);
    }

    public void TutorialEnd(bool in_destroy = true)
    {
        if (TutorialProgress == false)
            return;

        TutorialProgress = false;

        if (m_cashing_highlight != null)
            m_tutorial_fade_image.RemoveHole(m_cashing_highlight.Hole);

        m_tuto_object.transform.localScale = Vector3.one;
        m_tuto_object = null;

        Destroy(m_cashing_ani);
        Destroy(m_cashing_highlight);

        m_cashing_ani = null;
        m_cashing_highlight = null;

        m_tutorial_bg.gameObject.SetActive(false);

        Util.CloseTutorialToolTip();
    }

    public void TutorialClear(int in_index)
    {
        if (Managers.User.UserData.ClearTutorial.Contains(in_index))
            return;

        Managers.User.UserData.ClearTutorial.Add(in_index);
    }
}