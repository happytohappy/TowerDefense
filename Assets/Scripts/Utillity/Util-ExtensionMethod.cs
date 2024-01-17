using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public static partial class Util
{
    public static void Ex_SetActive(this Image in_image, bool in_active)
    {
        if (in_image == null)
            return;

        in_image.gameObject.SetActive(in_active);
    }

    public static void Ex_SetActive(this GameObject in_gameobject, bool in_active)
    {
        if (in_gameobject == null)
            return;

        in_gameobject.SetActive(in_active);
    }

    public static void Ex_Play(this Animator in_ani, string in_state, MonoBehaviour in_mono = null, Action in_callback = null)
    {
        in_ani.Play(in_state, -1, 0);
        in_ani.Update(0);

        if (in_mono == null || in_callback == null)
            return;

        var info = in_ani.GetCurrentAnimatorStateInfo(0);
        in_mono.StartCoroutine(WaitCoroutine(info.length, in_callback));
    }

    private static IEnumerator WaitCoroutine(float in_time, Action in_callback)
    {
        yield return new WaitForSeconds(in_time);
        in_callback.Invoke();
    }
}