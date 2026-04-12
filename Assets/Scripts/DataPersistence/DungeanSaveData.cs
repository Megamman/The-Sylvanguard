using UnityEngine;

public class DungeanSaveData : MonoBehaviour
{
    void Start()
    {
        LoadData();
    }

    public static void SaveData()
    {
        StatsDataPersistenceManager.instance.SaveStatsData();
    }

    public static void LoadData()
    {
        StatsDataPersistenceManager.instance.LoadStatsData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void LoadStatsData(StatsData data)
    {
        if (data != null)
        {
            Stats.HP = data.HP;
            Stats.MovePt = data.MovePt;
            Stats.Attack = data.Attack;
            Stats.MP = data.MP;

            Stats.Coins = data.Coins;
            Stats.XP = data.XP;

            Stats.DodgeChance = data.DodgeChance;
            Stats.Def = data.Def;
            Stats.CritChance = data.CritChance;
            Stats.CritMax = data.CritMax;
            Stats.SkipMove = data.SkipMove;

            Stats.HPRegen = data.HPRegen;
            Stats.StepsToHPRegen = data.StepsToHPRegen;
            Stats.StepsToMPRegen = data.StepsToMPRegen;
            Stats.HPRegenIsOn = data.HPRegenIsOn;
            Stats.MPRegenIsOn = data.MPRegenIsOn;

            Stats.MagicAttack = data.MagicAttack;
            Stats.MagicDef = data.MagicDef;
            Stats.DefMPCost = data.DefMPCost;
            Stats.AttMpCost = data.AttMpCost;

            Stats.MaxPotions = data.MaxPotions;
            Stats.PotionHeal = data.PotionHeal;

            Stats.MagicAttackIsActive = data.MagicAttackIsActive;
            Stats.UsingMagicAttack = data.UsingMagicAttack;
            Stats.MagicShieldIsActive = data.MagicShieldIsActive;
            Stats.PotionUseIsActive = data.PotionUseIsActive;
            Stats.QuickPotion = data.QuickPotion;
        }
    }

    public void SaveStatsData(ref StatsData data)
    {
        if (data != null)
        {
            data.Coins = Stats.Coins;
            data.XP = Stats.XP;
        }
    }


}
