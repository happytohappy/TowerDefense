using UnityEngine;
using UnityEngine.U2D;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] private SpriteAtlas m_atlas_common;
    [SerializeField] private SpriteAtlas m_atlas_treasure;

    public Sprite GetSprite(Atlas in_atlas, string in_name)
    {
        switch (in_atlas)
        {
            case Atlas.Common:
                return m_atlas_common.GetSprite(in_name);
            case Atlas.Treasure:
                return m_atlas_treasure.GetSprite(in_name);
        }

        return null;
    }
}