using BackEnd;
using UnityEngine;

public class BackendManager : MonoBehaviour
{
    public void Init()
    {
        // �ڳ� �ʱ�ȭ
        var bro = Backend.Initialize(true);

        if (bro.IsSuccess())
            Debug.Log("�ʱ�ȭ ���� : " + bro);
        else
            Debug.LogError("�ʱ�ȭ ���� : " + bro);
    }
}
