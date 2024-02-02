using BackEnd;
using UnityEngine;

public class BackendManager : MonoBehaviour
{
    public void Init()
    {
        // 뒤끝 초기화
        var bro = Backend.Initialize(true);

        if (bro.IsSuccess())
            Debug.Log("초기화 성공 : " + bro);
        else
            Debug.LogError("초기화 실패 : " + bro);
    }
}
