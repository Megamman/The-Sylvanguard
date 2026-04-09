using System;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    //[Header("Skill")]
    //public UpgradeLevel upgradeLevel;
    [Header("Skill")]
    public string SkillName;
    public String SkillDescription;
    public StatsDetails[] Upgrade;
    public int maxLevel;

    [Header("Cost")]
    public int xpCost;
    public int coinCost;

    private int currentLevel;

    private bool OnHover;
    private bool Purchesable;

    //public SpriteRenderer IconBackground; // not needed as it is this object


    [HideInInspector] public bool MaxLevelReached = false;

    void Update()
    {
        UpdateCheck();
    }

    private void UpdateCheck()
    {
        //use this function to check if there are any update
        //used for short Checks

        if(currentLevel == maxLevel) { MaxLevelReached = true; }


        //check if skill is purchasable
        if( Stats.XP <= xpCost || Stats.Coins <= coinCost ) { Purchesable = true; } else { Purchesable = false; }
    }

    public void PurchaseSkill()
    {

        if (Purchesable && OnHover && !MaxLevelReached) 
        {
            StatsDetails GetStat = Upgrade[currentLevel];
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
                    Stats.PoitionUseIsActive = true;
                    break;

                default:
                    Debug.Log("No Stat Set");
                    return;
            }

            Stats.XP -= xpCost;
            Stats.Coins -= coinCost;
            currentLevel++;
            UpdateCheck();
        }
    }

    #region CheckHover
    public void OnMouseIsOver()
    {
        OnHover = true;
        //Debug.Log("Mouse is Hovering Over");
    }

    public void OnMouseIsExit()
    {
        OnHover = false;
        //Debug.Log("Mouse is Exit");
    }
    #endregion

    #region Skill Setting
    //[Serializable]
    //public class UpgradeLevel
    //{
    //    [Header("Effect")]
    //    public StatsDetails[] Stat;
    //}

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
        UsingMagicAttack
    }
    #endregion
}
