using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SkillSystem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //[Header("Skill")]
    //public UpgradeLevel upgradeLevel;
    [Header("Skill")]
    public string SkillName;
    public String SkillDescription;
    public StatsDetails[] Upgrade;
    public int maxLevel;
    public ActiveSkill[] ActriveSkill;

    [Header("Cost")]
    public int xpCost;
    public int coinCost;

    [HideInInspector] public int currentLevel;
    [HideInInspector] public bool LevelIsBlocked = false;

    private bool OnHover;
    private bool Purchesable;

    [HideInInspector] public float timer = 0;
    private float setTime = 3;

    //public SpriteRenderer IconBackground; // not needed as it is this object

    private GameObject InfoBox;
    private InfoBoxDetails infoScript;
    private List<SkillSwitch> SkillSwitches;
    private SkillTreeMovement controls;

    SpriteRenderer sr;

    [HideInInspector] public bool MaxLevelReached = false;
    bool isPurchasing;
    Color _orange = new Color(1.0f, 0.64f, 0.0f);

    private void Start()
    {
        if (InfoBox == null)
        {
            InfoBox = MainStaticData.InfoBox.transform.gameObject;
            infoScript = InfoBox.GetComponent<InfoBoxDetails>();
        }

        controls = new SkillTreeMovement();
        controls.Enable();
        sr = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        if(currentLevel != 0) { transform.GetComponent<SkillSwitch>().SkillPurchase = true; }

        UpdateCheck();
        InfoBoxDetails.timer = timer;

        //sr.enabled = OnHover;
        if (Purchesable) { sr.color = Color.green; }
        if (!Purchesable) { sr.color = Color.red; }
        if (MaxLevelReached) { sr.color = _orange; }

        if (OnHover) { 
            controls.Player.Interect.started += ctx => isPurchasing = true;
        }

        controls.Player.Interect.canceled += ctx => isPurchasing = false;            

        if (isPurchasing) {
            PurchaseSkill();
        }
    }

    private void UpdateCheck()
    {
        //use this function to check if there are any update
        //used for short Checks

        if(currentLevel == maxLevel) { MaxLevelReached = true; }

        //check if skill is purchasable
        if( Stats.XP >= xpCost && Stats.Coins >= coinCost ) { Purchesable = true; } 
        else { Purchesable = false; }
    }

    public void PurchaseSkill()
    {

        //Debug.Log("Purches: " + Purchesable + ", Hover: " + OnHover + 
            //", Max Level: " + MaxLevelReached + ", Blocked: " + LevelIsBlocked);

        if (Purchesable && OnHover && !MaxLevelReached && !LevelIsBlocked)
        {
            //Debug.Log("Purcahsing: " + timer);

            if (timer < setTime) { timer += Time.deltaTime; }
            if (timer >= setTime) { GetSkill(); }
        }           
    }

    void GetSkill()
    {
        for (int i = 0; i < Upgrade.Length; i++)
        {
                
            StatsDetails GetStat = Upgrade[i];
            switch (GetStat.UpgradeEffect)
            {
                //Intigers
                case UpgradeStat.Health:
                    Stats.HP += GetStat.SkillIncrease;
                    break;
                case UpgradeStat.Movement:
                    Stats.MovePt += GetStat.SkillIncrease;
                    break;
                case UpgradeStat.Attack:
                    Stats.Attack += GetStat.SkillIncrease;
                    break;
                case UpgradeStat.MagicPoints:
                    Stats.MP += GetStat.SkillIncrease;
                    break;
                case UpgradeStat.Dodge:
                    Stats.DodgeChance += GetStat.SkillIncrease;
                    break;
                case UpgradeStat.Defence:
                    Stats.Def += GetStat.SkillIncrease;
                    break;
                case UpgradeStat.Crit:
                    Stats.CritChance += GetStat.SkillIncrease;
                    break;
                case UpgradeStat.CritMax:
                    Stats.CritMax += GetStat.SkillIncrease;
                    break;
                case UpgradeStat.SkipMovement:
                    Stats.SkipMove += GetStat.SkillIncrease;
                    break;
                case UpgradeStat.HpRegen:
                    Stats.HPRegen += GetStat.SkillIncrease;
                    break;
                case UpgradeStat.HpRegenSteps:
                    Stats.StepsToHPRegen -= GetStat.SkillIncrease;
                    break;
                case UpgradeStat.MpRegenSteps:
                    Stats.StepsToMPRegen -= GetStat.SkillIncrease;
                    break;
                case UpgradeStat.MagicAttack:
                    Stats.MagicAttack += GetStat.SkillIncrease;
                    break;
                case UpgradeStat.MagicShield:
                    Stats.MagicDef += GetStat.SkillIncrease;
                    break;
                case UpgradeStat.Potions:
                    Stats.MaxPotions += GetStat.SkillIncrease;
                    break;
                case UpgradeStat.PotionHeal:
                    Stats.PotionHeal += GetStat.SkillIncrease;
                    break;

                //Booleans
                case UpgradeStat.HpRegenStepsActive:
                    Stats.HPRegenIsOn = true;
                    break;
                case UpgradeStat.MpRegenStepsActive:
                    Stats.MPRegenIsOn = true;
                    break;
                case UpgradeStat.MagicAttackIsActive:
                    Stats.MagicAttackIsActive = true;
                    Stats.AttMpCost = GetStat.SkillIncrease; // when activated it makes sure to update the cost too.
                    break;
                case UpgradeStat.MagicShieldIsActive:
                    Stats.MagicShieldIsActive = true;
                    Stats.DefMPCost = GetStat.SkillIncrease; // when activated it makes sure to update the cost too.
                    break;
                case UpgradeStat.PoitionUseIsActive:
                    Stats.PotionUseIsActive = true;
                    break;
                case UpgradeStat.QuickPoitionUse:
                    Stats.QuickPotion = true;
                    break;

                case UpgradeStat.NULL:
                    break;

                default:
                    Debug.Log("No Stat Set");
                    return;
            }

            Debug.Log("Skill Recieved " + GetStat.UpgradeEffect.ToString());

            Stats.XP -= xpCost;
            Stats.Coins -= coinCost;
            currentLevel++;
            timer = 0;
            CheckActriveSkill();

            SkillSwitches = GetSkillList();

            foreach (SkillSwitch _switch in SkillSwitches)
            {
                _switch.CheckSkill();
            }

            SavenLoadScript.SaveData(); //Save File code
        }
    }

    #region CheckHover
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHover = true;
        InfoBox.SetActive(OnHover);
        infoScript.ShowDetails(this);
        //Debug.Log("Mouse is Hovering Over");


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHover = false;
        InfoBox.SetActive(OnHover);
        infoScript.ShowDetails(null);
        timer = 0;
    }

    #endregion

    #region Skill Setting
    [Serializable]
    public class StatsDetails
    {
        public UpgradeStat UpgradeEffect;
        public int SkillIncrease;
    }

    public enum UpgradeStat
    {
        Attack,
        Crit,
        CritMax,
        Defence,
        Dodge,
        Health,
        HpRegen,
        HpRegenSteps,
        HpRegenStepsActive,
        MagicAttack,
        MagicAttackIsActive,
        MagicPoints,
        MagicShield,
        MagicShieldIsActive,
        MpRegenSteps,
        MpRegenStepsActive,
        Movement,
        Potions,
        PotionHeal,
        PoitionUseIsActive,
        SkipMovement,
        UsingMagicAttack,
        QuickPoitionUse,
        NULL,
    }
    #endregion

    private void CheckActriveSkill()
    {
        for (int i = 0; i < ActriveSkill.Length; i++)
        { ActriveSkill[i].CheckCounter(); }
    }

    private List<SkillSwitch> GetSkillList()
    {
        IEnumerable<SkillSwitch> _SkillSwitch =
            FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None)
            .OfType<SkillSwitch>();

        return new List<SkillSwitch>(_SkillSwitch);
    }
    
}
