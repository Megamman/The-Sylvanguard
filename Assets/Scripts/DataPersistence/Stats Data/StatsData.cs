using UnityEngine;

[System.Serializable]
public class StatsData
{
    public Vector2 TownPos = new Vector2(0,0);

    public bool T2, T3, T4, T5, T6, T7;

    //Main Stats
    public int HP = 10; //Health Point - Redused when reciing attack - GameOver when 0
    public int MovePt = 10; //Steps that can be taken
    public int Attack = 1; //how much damage can be dealt
    public int MP; //Magic Point - Used to use magic active skills

    //Collection
    public int Coins;
    public int XP;

    //Secondary Stats
    public int DodgeChance; //Dodge is by chance but will not get damage
    public int Def; //Defence is always but may still get damage if it is higher then defence
    public int CritChance; //What are the chance to get a crittical hit
    public int CritMax; //Max Crit possible
    public int SkipMove;

    //Regeneration Stats
    public int HPRegen; //How much hp is regenerated
    public int StepsToHPRegen; //How much steps are needed to regenerate health
    public int StepsToMPRegen; //How much steps are needed to regenerate magic
    public bool HPRegenIsOn; //If HP can be regenerated
    public bool MPRegenIsOn; //If MP can be regenerated

    //Maigc Stats
    public int MagicAttack; //What additional damage deals with magic
    public int MagicDef; //Defence amount when reciveing magic attack
    public int DefMPCost; //How much MP is needed and used to activate skill Magic Defence
    public int AttMpCost; //How much MP is needed and used to activate skill Magic Attack

    //Potions
    public int MaxPotions; //How many potions can be used
    public int PotionHeal; //How much health a potion can heal

    //Actives
    public bool MagicAttackIsActive; //If the active skill Magic Attack is accuared and usable
    public bool UsingMagicAttack;
    public bool MagicShieldIsActive; //If the active skill Magic Shield is accuared and usable
    public bool PotionUseIsActive; //If the potions are accuared and usable
    public bool QuickPotion;


    public bool[] ActivesSkillTree = new bool[58];
    public int[] LvlsSkillTree = new int[58];

    public bool[] ActivesBlacksmith = new bool[58];
    public int[] LvlsBlacksmith = new int[58];

    public bool[] ActivesAlchemist = new bool[58];
    public int[] LvlsAlchemist = new int[58];

    public StatsData()
    {
        TownPos = Vector2.zero;

        T2 = true; T3 = true; T4 = true; T5 = true; T6 = true; T7 = true;

    //Main Stats
    HP = 10; //Health Point - Redused when reciing attack - GameOver when 0
        MovePt = 10; //Steps that can be taken
        Attack = 1; //how much damage can be dealt
        MP = 0; //Magic Point - Used to use magic active skills

        //Collection
        Coins = 0;
        XP = 0;

        //Secondary Stats
        DodgeChance = 0; //Dodge is by chance but will not get damage
        Def = 0; //Defence is always but may still get damage if it is higher then defence
        CritChance = 0; //What are the chance to get a crittical hit
        CritMax = 0; //Max Crit possible
        SkipMove = 0;

        //Regeneration Stats
        HPRegen = 0; //How much hp is regenerated
        StepsToHPRegen = 0; //How much steps are needed to regenerate health
        StepsToMPRegen = 0; //How much steps are needed to regenerate magic
        HPRegenIsOn = false; //If HP can be regenerated
        MPRegenIsOn = false; //If MP can be regenerated

        //Maigc Stats
        MagicAttack = 0; //What additional damage deals with magic
        MagicDef = 0; //Defence amount when reciveing magic attack
        DefMPCost = 0; //How much MP is needed and used to activate skill Magic Defence
        AttMpCost = 0; //How much MP is needed and used to activate skill Magic Attack

        //Potions
        MaxPotions = 0; //How many potions can be used
        PotionHeal = 0; //How much health a potion can heal

        //Actives
        MagicAttackIsActive = false; //If the active skill Magic Attack is accuared and usable
        UsingMagicAttack    = false;
        MagicShieldIsActive = false; //If the active skill Magic Shield is accuared and usable
        PotionUseIsActive  = false; //If the potions are accuared and usable
        QuickPotion = false;


        ActivesSkillTree = new bool[61];
        LvlsSkillTree = new int[61];
        ActivesSkillTree[0] = true;

        ActivesBlacksmith = new bool[58];
        LvlsBlacksmith = new int[58];

        ActivesAlchemist = new bool[58];
        LvlsAlchemist = new int[58];
    }
}
