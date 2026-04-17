using UnityEngine;
using TMPro;
using static SkillSystem;

public class StatsUpdateSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text statText;
    [SerializeField] private StatType statType;
    float _statFloat;

    // Update is called once per frame
    void Update()
    {
        switch (statType)
        {
            case StatType.Attack:
                _statFloat = Stats.Attack;
                statText.text = _statFloat.ToString();
                break;
            case StatType.XP:
                _statFloat = Stats.XP;
                statText.text = _statFloat.ToString();
                break;
            case StatType.Coin:
                _statFloat = Stats.Coins;
                statText.text = _statFloat.ToString();
                break;

            case StatType.HpRegenSteps:
                _statFloat = Stats.HPRegen;
                statText.text = _statFloat.ToString();
                break;
            case StatType.Defence:
                _statFloat = Stats.Def;
                statText.text = _statFloat.ToString();
                break;
            case StatType.Dodge:
                _statFloat = Stats.DodgeChance;
                statText.text = _statFloat.ToString() + "<size=50%>%</size>";
                break;

            case StatType.MagicAttack:
                _statFloat = Stats.MagicAttack;
                statText.text = _statFloat.ToString();
                break;
            case StatType.MagicShield:
                _statFloat = Stats.MagicDef;
                statText.text = _statFloat.ToString();
                break;
            case StatType.MpRegenSteps:
                _statFloat = Stats.StepsToMPRegen;
                statText.text = _statFloat.ToString();
                break;

            default:
                Debug.Log("No Stat Set");
                return;
        }

        //if( _statFloat == 0 ) { statText.transform.gameObject.SetActive(false); }
        //else { statText.transform.gameObject.SetActive(true); }

    }

    public enum StatType
    {
        Attack,
        XP,
        Coin,
        HpRegenSteps,
        Defence,
        Dodge,
        MagicAttack,
        MagicShield,
        MpRegenSteps

        //CritChance,
        //SkipMovement
        //PotionHeal
        // autoUsePotionSwitch
    }
}
