using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using TMPro;

public static partial class Util
{
    public static void Ex_SetActive(this Behaviour in_behaviour, bool in_active)
    {
        if (in_behaviour == null)
            return;

        in_behaviour.gameObject.SetActive(in_active);
    }

    public static void Ex_SetActive(this GameObject in_game_object, bool in_active)
    {
        if (in_game_object == null)
            return;

        in_game_object.SetActive(in_active);
    }

    public static void Ex_SetValue(this RectTransform in_scroll_rect, EScrollDir in_dir, float in_value)
    {
        if (in_scroll_rect == null)
            return;

        if (in_dir == EScrollDir.Horizontal)
            in_scroll_rect.anchoredPosition = new Vector2(in_value, 0f);
        else if (in_dir == EScrollDir.Vertical)
            in_scroll_rect.anchoredPosition = new Vector2(0f, in_value);
        else
            in_scroll_rect.anchoredPosition = new Vector2(in_value, in_value);
    }

    public static void Ex_SetColor(this Image in_image, Color in_color)
    {
        if (in_image == null)
            return;

        in_image.color = in_color;
    }

    public static void Ex_SetImage(this Image in_image, Sprite in_sprite)
    {
        if (in_image == null)
            return;

        in_image.sprite = in_sprite;
    }

    public static void Ex_SetText(this TMP_Text in_text, string in_value)
    {
        if (in_text == null)
            return;

        in_text.text = in_value;
    }

    public static void Ex_SetValue(this Slider in_slider, float in_value)
    {
        if (in_slider == null)
            return;

        in_slider.value = in_value;
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