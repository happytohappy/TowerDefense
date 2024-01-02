using UnityEngine;

public class Poolable : MonoBehaviour
{
	[SerializeField] private bool m_IsUsing;

	public bool IsUsing { get { return m_IsUsing; } set { m_IsUsing = value; } }
}