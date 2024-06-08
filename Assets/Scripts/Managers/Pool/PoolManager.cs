using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region Pool
    private class Pool
    {
        public GameObject Original { get; private set; }

        private Stack<Poolable> m_PoolStack = new Stack<Poolable>();

        public void Init(GameObject original, int count = 5)
        {
            Original = original;

            for (int i = 0; i < count; i++)
                Push(Create());
        }

        private Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return Util.GetOrAddComponent<Poolable>(go);
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Managers.Pool.transform;
            poolable.Ex_SetActive(false);
            poolable.IsUsing = false;

            m_PoolStack.Push(poolable);
        }

        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (m_PoolStack.Count > 0)
                poolable = m_PoolStack.Pop();
            else
                poolable = Create();

            poolable.Ex_SetActive(true);
            poolable.transform.parent = parent;
            poolable.IsUsing = true;

            return poolable;
        }
    }
    #endregion

    private Dictionary<string, Pool> m_Pool = new Dictionary<string, Pool>();
    private Transform m_Root;

    public void Init()
    {
        m_Root = this.transform;
    }

    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);

        m_Pool.Add(original.name, pool);
    }

    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;
        if (m_Pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        m_Pool[name].Push(poolable);
    }

    public Poolable Pop(GameObject original, Transform parent = null)
    {
        if (m_Pool.ContainsKey(original.name) == false)
            CreatePool(original);

        return m_Pool[original.name].Pop(parent);
    }

    public GameObject GetOriginal(string name)
    {
        if (m_Pool.ContainsKey(name) == false)
            return null;
        return m_Pool[name].Original;
    }

    public void Clear()
    {
        foreach (Transform child in m_Root)
            GameObject.Destroy(child.gameObject);

        m_Pool.Clear();
    }
}
