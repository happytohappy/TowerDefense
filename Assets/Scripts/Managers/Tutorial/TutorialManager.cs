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

    // Ʃ�丮�� �����ؼ��� �ϵ��ڵ� ����.
    // 1. ����
    // 2. �±�
    // 3. ������
    // 4. ����
    // 5. ����

    // 6. �������� ����

    // 7. ���
    // 8. ����
    // 9. �⼮
    // 10. �̼�

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