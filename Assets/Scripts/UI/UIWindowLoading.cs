using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIWindowLoading : UIWindowBase
{
    [SerializeField] private Image m_Progress = null;

    public override void Awake()
    {
        Window_ID = WindowID.UIWindowLoading;
        Window_Mode = WindowMode.WindowClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);
        var info = wp as LoadingParam;

        StartCoroutine(LoadScene(info));
    }

    private IEnumerator LoadScene(LoadingParam info)
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(info.SceneIndex);
        op.allowSceneActivation = false;

        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            if (op.progress >= 0.9f)
            {
                m_Progress.fillAmount = Mathf.Lerp(m_Progress.fillAmount, 1f, timer);

                if (m_Progress.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;

                    if (info.NextWindow != WindowID.None)
                        Managers.UI.OpenWindow(info.NextWindow);
                    else
                        Managers.UI.CloseLast(true);
                }
            }
            else
            {
                m_Progress.fillAmount = Mathf.Lerp(m_Progress.fillAmount, op.progress, timer);
                if (m_Progress.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
        }
    }
}