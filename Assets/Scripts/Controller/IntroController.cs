using UnityEngine;

public class IntroController : MonoBehaviour
{
    private void Awake()
    {
        Managers.GetInstance.Init();
    }
}