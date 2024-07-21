using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enable : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.LogError("Enabled");
    }

    private void OnDisable()
    {
        Debug.LogError("Disabled");
    }
}
