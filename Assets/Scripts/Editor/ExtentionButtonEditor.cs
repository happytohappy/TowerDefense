using UnityEngine;

namespace UnityEditor.UI
{
    [CustomEditor(typeof(ExtentionButton))]
    [CanEditMultipleObjects]
    public class ExtentionButtonEditor : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ExtentionButton component = (ExtentionButton)target;

            component.m_AudioSource = (AudioClip)EditorGUILayout.ObjectField("OnClick Sound", component.m_AudioSource, typeof(AudioClip), true);
        }
    }
}