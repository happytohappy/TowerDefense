using UnityEngine;
using System.Linq;

public class Hud_HeroInfo : MonoBehaviour
{
    [SerializeField] private GameObject m_go_group_root = null;
    [SerializeField] private ExtentionButton m_btn_merge = null;

    private Vector3 m_offset = Vector3.zero;

    public Hero Target { get; set; }

    private void FixedUpdate()
    {
        if (Target == null)
            return;

        var position = Managers.WorldCam.WorldToViewportPoint(Target.transform.position);
        var view_position = Managers.UICam.ViewportToWorldPoint(position) + m_offset;
        this.transform.position = new Vector3(view_position.x, view_position.y, 0f);
    }

    public void Set(Vector3 in_offset)
    {
        m_offset = in_offset;
        m_go_group_root.Ex_SetActive(false);
    }

    public void ShowHeroInfo()
    {
        m_go_group_root.Ex_SetActive(true);

        var SpawnHeroList = GameController.GetInstance.LandInfo.FindAll(x => x.m_hero != null).ToList();
        var SameHero = SpawnHeroList.FindAll(x => x.m_hero.GetHeroData.m_info.m_kind == Target.GetHeroData.m_info.m_kind).ToList();
        m_btn_merge.Ex_SetActive(SameHero.Count >= 2);
    }

    public void CloseHeroInfo()
    {
        m_go_group_root.Ex_SetActive(false);
    }

    public void OnClickInfo()
    {
        Debug.LogError("OnClickInfo");
    }

    public void OnClickMerge()
    {
        var SpawnHeroList = GameController.GetInstance.LandInfo.FindAll(x => x.m_hero != null).ToList();
        var SameHero = SpawnHeroList.FindAll(x => x.m_hero.GetHeroData.m_info.m_kind == Target.GetHeroData.m_info.m_kind).ToList();

        var SelectLand = GameController.GetInstance.LandInfo.Find(x => x.m_hero == GameController.GetInstance.SelectHero);
        SameHero.Remove(SelectLand);
        GameController.GetInstance.EndLand = SelectLand;
        GameController.GetInstance.SelectHero = SameHero[UnityEngine.Random.Range(0, SameHero.Count)].m_hero;
        GameController.GetInstance.HeroMerge();
    }
}
