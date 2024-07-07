using System.Collections.Generic;
using UnityEngine;

public class UIWindowManager : MonoBehaviour
{
    private readonly Dictionary<WindowID, UIWindowBase> dic_WindowInsts = new Dictionary<WindowID, UIWindowBase>();
    private readonly LinkedList<UIWindowBase> llist_Window = new LinkedList<UIWindowBase>();

    public void Init()
    {
        OpenWindow(WindowID.UIWindowIntro);
    }

    public UIWindowBase OpenWindow(WindowID windowID, WindowParam wp = null)
    {
        UIWindowBase openbase = null;
        openbase = GetWindow(windowID, true);
        if (openbase == null)
            return null;

        LinkedListNode<UIWindowBase> lastNode = llist_Window.Last;
        if (lastNode != null)
        {
            UIWindowBase closeWindow = lastNode.Value;

            if ((openbase.Window_Mode & WindowMode.WindowOverlay) != WindowMode.WindowOverlay)
                closeWindow.gameObject.SetActive(false);
        }

        llist_Window.AddLast(openbase);

        openbase.gameObject.SetActive(true);
        openbase.gameObject.transform.SetAsLastSibling();

        openbase.OpenUI(wp);
        return openbase;
    }

    public UIWindowBase GetWindow(WindowID windowID, bool existCreate)
    {
        UIWindowBase result = null;
        if (dic_WindowInsts.ContainsKey(windowID))
            return dic_WindowInsts[windowID];

        if (existCreate)
        {
            var prefabs = Util.LoadPrefab(windowID.ToString());
            if (prefabs == null)
                return null;

            prefabs.name = windowID.ToString();

            result = prefabs.GetComponent(typeof(UIWindowBase)) as UIWindowBase;
            dic_WindowInsts.Add(windowID, result);
        }

        return result;
    }

    public bool ActiveWindow(WindowID windowID)
    {
        if (dic_WindowInsts.ContainsKey(windowID))
        {
            return dic_WindowInsts[windowID].gameObject.activeInHierarchy;
        }
        else
        {
            return false;
        }
    }

    public void CloseLast(bool absolute = false)
    {
        if (llist_Window.Last == null)
            return;

        var closeWindow = llist_Window.Last.Value;
        closeWindow.OnClose();
        closeWindow.gameObject.SetActive(false);

        llist_Window.RemoveLast();
        if (absolute || (closeWindow.Window_Mode & WindowMode.WindowJustClose) == WindowMode.WindowJustClose)
            return;

        LinkedListNode<UIWindowBase> lastNode = llist_Window.Last;
        UIWindowBase openWindow = lastNode.Value;

        openWindow.OpenUI(openWindow.Window_Param);
    }

    public void Clear()
    {
        dic_WindowInsts.Clear();
        llist_Window.Clear();

        for (int i = 0; i < Managers.UICanvas.transform.childCount; i++)
        {
            var ui = Managers.UICanvas.transform.GetChild(i);
            if (ui == null || ui.name.Equals("Widget") || ui.name.Equals("Tutorial Fade"))
                continue;

            Destroy(ui.gameObject);
        }
    }
}