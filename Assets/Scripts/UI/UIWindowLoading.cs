using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIWindowLoading : UIWindowBase
{
    [SerializeField] private Slider m_Slider = null;

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

        m_Slider.value = 0f;
        StartCoroutine(LoadScene(info));
    }

    private IEnumerator LoadScene(LoadingParam info)
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(info.SceneIndex);
        //op.allowSceneActivation = false;

        // info.SceneIndex == 2 게임씬이라 리소스 로딩 하자
        if (info.SceneIndex == 2)
        {
            var maxCount = 0;
            List<string> aaa = new List<string>();
            //for (int z = 0; z < 30; z++)
            //{
                for (int i = 1; i <= 8; i++)
                {
                    var heroList = Managers.User.GetUserHeroInfoGroupByTier(i);
                    if (heroList == null)
                        continue;

                    foreach (var hero in heroList)
                    {
                        maxCount++;

                        var heroInfoData = Managers.Table.GetHeroInfoData(hero.m_kind);
                        aaa.Add(heroInfoData.m_path);
                        //var go = Managers.Resource.Instantiate(heroInfoData.m_path);
                        //if (go != null)
                        //    Managers.Resource.Destroy(go);
                    }
                }
            //}

            maxCount = maxCount + (int)(maxCount * 0.5f);
            var successCnt = 0;
            foreach (var path in aaa)
            {
                var go = Managers.Resource.Instantiate(path);
                if (go != null)
                    Managers.Resource.Destroy(go);

                successCnt++;

                m_Slider.value = successCnt / (float)maxCount;
                yield return null;
            }

            
            float timer = 0.0f;

            while (true)
            {
                yield return null;

                timer += Time.deltaTime;

                //if (op.progress >= 0.9f)
                //{
                    m_Slider.value = Mathf.Lerp(m_Slider.value, 1f, timer);

                    if (m_Slider.value == 1.0f)
                    {
                        //op.allowSceneActivation = true;

                        if (info.NextWindow != WindowID.None)
                            Managers.UI.OpenWindow(info.NextWindow);
                        else
                            Managers.UI.CloseLast(true);
                    }
                //}
            }
        }
        else
        {
            float timer = 0.0f;
            while (true)
            {
                yield return null;

                timer += Time.deltaTime;

                if (op.progress >= 0.9f)
                {
                    m_Slider.value = Mathf.Lerp(m_Slider.value, 1f, timer);

                    if (m_Slider.value == 1.0f)
                    {
                        op.allowSceneActivation = true;
                        yield return null;

                        if (info.NextWindow != WindowID.None)
                            Managers.UI.OpenWindow(info.NextWindow);
                        else
                            Managers.UI.CloseLast(true);
                    }
                }
                else
                {
                    m_Slider.value = Mathf.Lerp(m_Slider.value, op.progress, timer);
                    if (m_Slider.value >= op.progress)
                    {
                        timer = 0f;
                    }
                }
            }
        }
    }
}