using TMPro;
using UnityEngine;
using System.Collections;

public class CombatScript : MonoBehaviour
{
    //EnemyScript
    EnemyStats Enemy;
    public GameObject DmgCounter;
    private Transform pCanvasTransform;

    private void Start()
    {
        pCanvasTransform = DmgCounter.transform.parent;
    }

    public void Fight(EnemyStats EnemyScript)
    {

        if(EnemyScript != null)
        {
            //Debug.Log("Attaking Enemy " + EnemyScript.Name);
            Enemy = EnemyScript;

            if (Enemy.SneakAttack)
            { Enemy.SneakAttack = false; Enemy.ActivateAnimation(); } //Sneak attack
            else { EnemyDamage(); }

            PlayerDamage();
        }
    }


// Damage to Enemy
    private void EnemyDamage() //How much damge the enemy recives
    {
        bool CanDodge = true;
        int Damage = Stats.Attack;
        float ExtraDamage = 1;
        bool weekPoint = false;
        bool Dodged = false;

        if (Enemy.WeakToPhysical) { CanDodge = false; ExtraDamage = 2; weekPoint = true; }
        if(Enemy.ResistToPhysical && Stats.Attack < 1) { ExtraDamage = 0.5f; }
        
        //Player Useing Magic
        if(Stats.UsingMagicAttack)
        {
            if(Enemy.WeakToMagic) { CanDodge = false; ExtraDamage = 2; Damage += Stats.MagicAttack; weekPoint = true; }
            if(Enemy.ResistToMagic && Stats.Attack < 1) { ExtraDamage = 0.5f; }
            if (Enemy.ResistToMagic && Enemy.WeakToMagic) { Damage += Stats.MagicAttack; }

            Stats.UsingMagicAttack = false;
        }

        Damage = Mathf.RoundToInt(Stats.Attack * ExtraDamage);

        if (Stats.CritChance > 0) { Damage += Random.Range(1, Stats.CritMax + 1); }


        if (Enemy.DodgeChance > 0 && CanDodge) { if (Stats.GetRandom(1, 101, Enemy.DodgeChance)) { Damage = 0; Dodged = true; } }

        if (Damage < 0) { Damage = 0; }




        if (MainStaticData.ShowData)
        {

            if (Damage == Stats.Attack) { DamageCounter(Damage.ToString(), Color.red, Enemy.transform); }
            else if (Dodged) { DamageCounter("Dodged".ToString(), Color.gray, Enemy.transform); }
            else if (weekPoint) { DamageCounter(Damage.ToString(), Color.yellow, Enemy.transform); }
            else if (Damage == 0) { DamageCounter(Damage.ToString(), Color.gray, Enemy.transform); }
            else if (Damage > Stats.Attack) { DamageCounter(Damage.ToString(), Color.magenta, Enemy.transform); }
            else if (Damage < Stats.Attack) { DamageCounter(Damage.ToString(), Color.cyan, Enemy.transform); }
        }

        Enemy.HP -= Damage;

        if (Enemy.HP <= 0) {
            Stats.XP += Random.Range(Enemy.XPMin, Enemy.XPMax);
            Stats.Coins += Random.Range(Enemy.CoinsMin, Enemy.CoinsMax);

            Destroy(Enemy.transform.gameObject);
        }
    }

// Damage to Player
    private void PlayerDamage() //How much Damage the player recieves
    {
        bool CanDodge = true;
        bool Dodged = false;
        int Damage = Enemy.Attack;
        // float ExtraDamage = 1;

        //Critical Hit
        if (Enemy.CritChance > 0)
        {
            if (Stats.GetRandom(1, 101, Enemy.CritChance))
            {
                Damage += Random.Range(1, Enemy.CritMaxDamage);
                CanDodge = false;
            }
        }

        if (CanDodge && Stats.DodgeChance > 0) { if (Stats.GetRandom(0, 101, Stats.DodgeChance)) { Damage = 0; Dodged = true; } }

        if (Enemy.UsingMagicAttack && !CanDodge && Stats.MagicDef > 0) { //Magic Defence
            Stats.MagicDef -= Damage; //Damge is taken by magic defence
            Damage = (-Stats.MagicDef); // The negative amount the defence reaches is the remain the player will get
            Stats.MagicDef = 0;
        } //Make sure that the shield is not negative couseing more damge later
        else if(!CanDodge) { Damage -= Stats.Def;
        } //Defence

        if (Damage < 0) { Damage = 0;
        } //Making sure that the attack does not heal instead

        Stats.HP -= Damage;



        if (MainStaticData.ShowData)
        {

            if (Damage == Stats.Attack) { DamageCounter(Damage.ToString(), Color.red, transform); }
            else if (Dodged) { DamageCounter("Dodged".ToString(), Color.gray, transform); }
            else if (Damage == 0) { DamageCounter(Damage.ToString(), Color.gray, transform); }
            else if (Damage > Stats.Attack) { DamageCounter(Damage.ToString(), Color.magenta, transform); }
            else if (Damage < Stats.Attack) { DamageCounter(Damage.ToString(), Color.cyan, transform); }
        }

    }

    public void DamageCounter(string dmg, Color color, Transform Position)
    {
        GameObject DC = Instantiate(DmgCounter, Position.position, Quaternion.identity);
        DC.SetActive(true);
        //DC.transform.SetParent(pCanvasTransform.transform);

        DC.transform.GetChild(0);

        TMP_Text tmp = DC.gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        tmp.color = color;
        tmp.text = dmg;
    }

}
