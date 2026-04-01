using System;

public static class Stats
{
    //Main Stats
    public static int HP; //Health Point - Redused when reciing attack - GameOver when 0
    public static int MovePt; //Steps that can be taken
    public static int Attack; //how much damage can be dealt
    public static int MP; //Magic Point - Used to use magic active skills

    //Collection
    public static int Coins;
    public static int XP;

    //Secondary Stats
    public static int DodgeChance; //Dodge is by chance but will not get damage
    public static int Def; //Defence is always but may still get damage if it is higher then defence
    public static int CritChance; //What are the chance to get a crittical hit
    public static int CritMax; //Max Crit possible
    public static int SkipMove;

    //Regeneration Stats
    public static int HPRegen; //How much hp is regenerated
    public static int StepsToHPRegen; //How much steps are needed to regenerate health
    public static int StepsToMPRegen; //How much steps are needed to regenerate magic
    public static bool HPRegenIsOn; //If HP can be regenerated
    public static bool MPRegenIsOn; //If MP can be regenerated

    //Maigc Stats
    public static int MagicAttack; //What additional damage deals with magic
    public static int MagicDef; //Defence amount when reciveing magic attack
    public static int DefMPCost; //How much MP is needed and used to activate skill Magic Defence
    public static int AttMpCost; //How much MP is needed and used to activate skill Magic Attack

    //Potions
    public static int MaxPotions; //How many potions can be used
    public static int PotionHeal; //How much health a potion can heal

    //Actives
    public static bool MagicAttackIsActive; //If the active skill Magic Attack is accuared and usable
    public static bool UsingMagicAttack;
    public static bool MagicShieldIsActive; //If the active skill Magic Shield is accuared and usable
    public static bool PoitionUseIsActive; //If the potions are accuared and usable


    //A qiuck access to randomness
    public static bool GetRandom(int min, int max, int chance)
    {
        int result = UnityEngine.Random.Range(min, max);

        if (result < chance) return true;
        else return false;
    }
}
