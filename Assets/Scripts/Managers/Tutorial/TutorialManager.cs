using UnityEngine;
using System.Collections.Generic;
using Abu;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject m_tutorial_bg = null;
    [SerializeField] private Animator m_tutorial_ani = null;

    public bool TutorialProgress { get; set; } = false;
   
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

    public void TutorialStart(GameObject in_go)
    {
        TutorialProgress = true;

        m_tutorial_bg.transform.SetAsLastSibling();
        m_tutorial_bg.gameObject.SetActive(true);

        m_cashing_highlight = in_go.AddComponent<TutorialHighlight>();
        m_cashing_ani = in_go.AddComponent<Animator>();
        m_cashing_ani.runtimeAnimatorController = m_tutorial_ani.runtimeAnimatorController;
    }

    public void TutorialEnd()
    {
        if (TutorialProgress == false)
            return;

        TutorialProgress = false;

        Destroy(m_cashing_ani);
        Destroy(m_cashing_highlight);
        m_tutorial_bg.gameObject.SetActive(false);
    }

    public void TutorialClear(int in_index)
    {
        if (Managers.User.UserData.ClearTutorial.Contains(in_index))
            return;

        Managers.User.UserData.ClearTutorial.Add(in_index);
    }
}