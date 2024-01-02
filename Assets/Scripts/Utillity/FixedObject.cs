using UnityEngine;

public class FixedObject : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;

        Screen.SetResolution(1920, 1080, true);

        DontDestroyOnLoad(gameObject);
    }
}
