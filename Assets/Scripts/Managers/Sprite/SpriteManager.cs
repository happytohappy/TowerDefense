using UnityEngine;
using UnityEngine.U2D;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] private SpriteAtlas m_atlas;

    public Sprite GetSprite(string in_name)
    {
        return m_atlas.GetSprite(in_name);
    }
}