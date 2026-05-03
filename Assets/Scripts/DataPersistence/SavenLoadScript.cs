using Unity.VisualScripting;
using UnityEngine;

public class SavenLoadScript : MonoBehaviour, IStatsDataPersistence
{
    [Header("Main Skill Tree")]
    public bool FocuseSkillTree;
    public GameObject[] SkillTreeIcons;
    private bool[] SkillTreeActives;
    private int[] SkillTreeLvls;

    [Header("Blacksmith Skill Tree")]
    public bool FocuseBlacksmith;
    public GameObject[] BlacksmithIcons;
    private bool[] BlacksmithActives;
    private int[] BlacksmithLvls;

    [Header("Alchemist Skill Tree")]
    public bool FocuseAlchamist;
    public GameObject[] AlchemistIcons;
    private bool[] AlchemistActives;
    private int[] AlchemistLvls;

    public GameObject InfoBox, Actives;


    private void Awake()
    {
        MainStaticData.InfoBox = InfoBox;

        Actives.SetActive(true);
        InfoBox.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadData();
    }

    private void Update()
    {
        Actives.SetActive(!MainStaticData.showInfo);
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

            if (FocuseSkillTree) {
                for (int i = 0; i < SkillTreeIcons.Length; i++)
                {
                    SkillTreeActives = new bool[SkillTreeIcons.Length];
                    SkillTreeLvls = new int[SkillTreeIcons.Length];

                    SkillTreeActives[i] = data.ActivesSkillTree[i];
                    SkillTreeLvls[i] = data.LvlsSkillTree[i];

                    SkillTreeIcons[i].GetComponent<SkillSystem>().currentLevel = SkillTreeLvls[i];
                }
            }

            if (FocuseBlacksmith) {
                for (int i = 0; i < BlacksmithIcons.Length; i++)
                {
                    BlacksmithActives = new bool[BlacksmithIcons.Length];
                    BlacksmithLvls = new int[BlacksmithIcons.Length];

                    BlacksmithActives[i] = data.ActivesBlacksmith[i];
                    BlacksmithLvls[i] = data.LvlsBlacksmith[i];

                    BlacksmithIcons[i].GetComponent<SkillSystem>().currentLevel = BlacksmithLvls[i];
                }
            }

            if (FocuseAlchamist) {
                for (int i = 0; i < AlchemistIcons.Length; i++)
                {
                    //Debug.Log("Length: " + AlchemistIcons.Length + ", at " + i);

                    AlchemistActives = new bool[AlchemistIcons.Length];
                    AlchemistLvls = new int[AlchemistIcons.Length];

                    AlchemistActives[i] = data.ActivesAlchemist[i];
                    AlchemistLvls[i] = data.LvlsAlchemist[i];

                    AlchemistIcons[i].GetComponent<SkillSystem>().currentLevel = AlchemistLvls[i];

                }
            }
        }
    }

    public void SaveStatsData(ref StatsData data)
    {
        if (data != null)
        {
            data.HP = Stats.HP;
            data.MovePt =  Stats.MovePt;
            data.Attack = Stats.Attack;
            data.MP = Stats.MP;

            data.Coins = Stats.Coins;
            data.XP = Stats.XP;

            data.DodgeChance = Stats.DodgeChance;
            data.Def = Stats.Def;
            data.CritChance = Stats.CritChance;
            data.CritMax = Stats.CritMax;
            data.SkipMove = Stats.SkipMove;

            data.HPRegen = Stats.HPRegen;
            data.StepsToHPRegen = Stats.StepsToHPRegen;
            data.StepsToMPRegen = Stats.StepsToMPRegen;
            data.HPRegenIsOn = Stats.HPRegenIsOn;
            data.MPRegenIsOn = Stats.MPRegenIsOn;

            data.MagicAttack = Stats.MagicAttack;
            data.MagicDef = Stats.MagicDef;
            data.DefMPCost = Stats.DefMPCost;
            data.AttMpCost = Stats.AttMpCost;

            data.MaxPotions = Stats.MaxPotions;
            data.PotionHeal = Stats.PotionHeal;

            data.MagicAttackIsActive = Stats.MagicAttackIsActive;
            data.UsingMagicAttack = Stats.UsingMagicAttack;
            data.MagicShieldIsActive = Stats.MagicShieldIsActive;
            data.PotionUseIsActive = Stats.PotionUseIsActive;
            data.QuickPotion = Stats.QuickPotion;

            if (FocuseSkillTree) {
                for (int i = 0; i < SkillTreeIcons.Length; i++)
                {
                    SkillTreeLvls[i] = SkillTreeIcons[i].GetComponent<SkillSystem>().currentLevel;

                    data.ActivesSkillTree[i] = SkillTreeActives[i];
                    data.LvlsSkillTree[i] = SkillTreeLvls[i];
                }
            }

            if (FocuseBlacksmith) {
                for (int i = 0; i < BlacksmithIcons.Length; i++)
                {
                    BlacksmithLvls[i] = BlacksmithIcons[i].GetComponent<SkillSystem>().currentLevel;
                   
                    data.ActivesBlacksmith[i] = BlacksmithActives[i];
                    data.LvlsBlacksmith[i] = BlacksmithLvls[i];
                }
            }

            if (FocuseAlchamist) {
                for (int i = 0; i < AlchemistIcons.Length; i++)
                {
                    AlchemistLvls[i] = AlchemistIcons[i].GetComponent<SkillSystem>().currentLevel;

                    data.ActivesAlchemist[i] = AlchemistActives[i];
                    data.LvlsAlchemist[i] = AlchemistLvls[i];
                }
            }
        }
    }
}
