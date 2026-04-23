using UnityEngine;
using UnityEngine.UI;

public class ActiveSkillsControl : MonoBehaviour
{
    [Header("Potion Button")]
    [SerializeField] private Button Potion;
    [SerializeField] private GameObject PotionText;

    [Header("Magic Attack Button")]
    [SerializeField] private Button MagicAttack;
    [SerializeField] private GameObject MagicAttackText;

    [Header("Magic Shield Button")]
    [SerializeField] private Button MagicShield;
    [SerializeField] private GameObject MagicShieldText;

    [Header("TP Home Button")]
    [SerializeField] private Button Home;
    [SerializeField] private GameObject HomeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Potion.interactable = Stats.PotionUseIsActive;
        PotionText.SetActive(Stats.PotionUseIsActive);

        MagicAttack.interactable = Stats.MagicAttackIsActive;
        MagicAttackText.SetActive(Stats.MagicAttackIsActive);

        MagicShield.interactable = Stats.MagicShieldIsActive;
        MagicShieldText.SetActive(Stats.MagicShieldIsActive);
        
    }

    public void UsePotion()
    {
        PlayerController.CallUsePotion();
    }

    public void UseMagicAttack()
    {
        if (Stats.MP >= Stats.AttMpCost)
        {
            Stats.UsingMagicAttack = true;
            Stats.MP -= Stats.AttMpCost;
        }
    }

    public void UseMagicShield() 
    {
        if (Stats.MP >= Stats.DefMPCost)
        {
            Stats.CurMagicDef += Stats.MagicDef;
            Stats.MP -= Stats.DefMPCost;
        }
    }

    public void UseHome() 
    {
        DungeanSaveData.SaveData();
        //Change Scene
    }

    private void Update()
    {
        
    }
}
