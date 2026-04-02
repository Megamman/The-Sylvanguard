using UnityEngine;

public class CombatScript : MonoBehaviour
{
    //EnemyScript
    EnemyStats Enemy;

    public void Fight(EnemyStats EnemyScript)
    {
Debug.Log("Attaking Enemy " + EnemyScript.Name);

        if(EnemyScript != null)
        {
            Enemy = EnemyScript;

            if (Enemy.SneakAttack)
            { Enemy.SneakAttack = false; }
            else { EnemyDamage(); }

            PlayerDamage();
        }
    }

    private void EnemyDamage() //How much damge the enemy recives
    {
        bool CanDodge = true;
        int Damage = 1;
        float ExtraDamage = 1;

        if(Enemy.WeakToPhysical) { CanDodge = false; ExtraDamage = 2; }
        if(Enemy.ResistToPhysical && Stats.Attack < 1) { ExtraDamage = 0.5f; }
        
        //Player Useing Magic
        if(Stats.UsingMagicAttack)
        {
            if(Enemy.WeakToMagic) { CanDodge = false; ExtraDamage = 2; Damage += Stats.MagicAttack; }
            if(Enemy.ResistToMagic && Stats.Attack < 1) { ExtraDamage = 0.5f; }
            if (Enemy.ResistToMagic && Enemy.WeakToMagic) { Damage += Stats.MagicAttack; }

            Stats.UsingMagicAttack = false;
        }

        Damage = Mathf.RoundToInt(Stats.Attack * ExtraDamage);

        if (Stats.CritChance > 0) { Damage += Random.Range(1, Stats.CritMax + 1); }


        if (Enemy.DodgeChance > 0 && CanDodge) { if(Stats.GetRandom(0, 101, Enemy.DodgeChance)) { Damage = 0; } }

        if (Damage < 0) { Damage = 0; }

        Enemy.HP -= Damage;

        if (Enemy.HP <= 0) {
            Stats.XP += Random.Range(Enemy.XPMin, Enemy.XPMax);
            Stats.Coins += Random.Range(Enemy.CoinsMin, Enemy.CoinsMax);

            Destroy(Enemy.transform.gameObject);
        }
    }

    private void PlayerDamage() //How much Damage the player recieves
    {
        bool CanDodge = true;
        int Damage = 1;
        // float ExtraDamage = 1;

        //Critical Hit
        if (Enemy.CritChance > 0)
        {
            if (Stats.GetRandom(0, 101, Enemy.CritChance))
            {
                Damage += Random.Range(1, Enemy.CritMaxDamage);
                CanDodge = false;
            }
        }

        if (CanDodge && Stats.DodgeChance > 0) { if (Stats.GetRandom(0, 101, Stats.DodgeChance)) { Damage = 0; } }

        if (Enemy.UsingMagicAttack && !CanDodge && Stats.MagicDef > 0) { //Magic Defence
            Stats.MagicDef -= Damage; //Damge is taken by magic defence
            Damage = (-Stats.MagicDef); // The negative amount the defence reaches is the remain the player will get
            Stats.MagicDef = 0; } //Make sure that the shield is not negative couseing more damge later
        else if(!CanDodge) { Damage -= Stats.Def; } //Defence

        if (Damage < 0) { Damage = 0; } //Making sure that the attack does not heal instead

        Stats.HP -= Damage;

        if (Stats.HP <= 0) { } //GameOver
    }
}
