using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Hud_HeroInfo : MonoBehaviour
{
    [SerializeField] private GameObject m_go_group_root = null;
    [SerializeField] private ExtentionButton m_btn_merge = null;
    [SerializeField] private Image m_img_unit_type = null;

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
        m_img_unit_type.Ex_SetImage(Util.GetUnitType(Target.GetHeroData.m_info.m_kind));
    }

    public void ShowHeroInfo()
    {
        m_go_group_root.Ex_SetActive(true);

        var SpawnHeroList = GameController.GetInstance.LandInfo.FindAll(x => x.m_hero != null).ToList();
        var SameHero = SpawnHeroList.FindAll(x => x.m_hero.GetHeroData.m_info.m_kind == Target.GetHeroData.m_info.m_kind).ToList();
        m_btn_merge.Ex_SetActive(SameHero.Count >= 2);

        CheckTutorial();
    }

    private void CheckTutorial()
    {
        if (Managers.User.UserData.ClearTutorial.Contains(6))
            return;

        // 머지 버튼
        Managers.Tutorial.TutorialStart(m_btn_merge.gameObject, ETutorialDir.Center, new Vector3(0f, -100f, 0f), "#NONE TEXT 합성 설명");
    }

    public void CloseHeroInfo()
    {
        m_go_group_root.Ex_SetActive(false);
    }

    public void OnClickInfo()
    {
        UnitInfoParam param = new UnitInfoParam();
        param.m_kind = Target.GetHeroData.m_info.m_kind;

        Managers.UI.OpenWindow(WindowID.UIPopupUnit, param);
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

        GameController.GetInstance.InputInit();

        var gui = Managers.UI.GetWindow(WindowID.UIWindowGame, false) as UIWindowGame;
        gui.OnCheckHeroSynergy();

        if (Managers.Tutorial.TutorialProgress)
        {
            Managers.Tutorial.TutorialEnd();

            var uiGame = Managers.UI.GetWindow(WindowID.UIWindowGame, false) as UIWindowGame;
            if (uiGame != null)
            {
                uiGame.OnClickTutoMissionStart();
            }
        }
    }
}