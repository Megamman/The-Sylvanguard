using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActiveSkillsControl : MonoBehaviour
{
    [Header("Potion Button")]
    [SerializeField] private Button Potion;
    [SerializeField] private GameObject PotionText;
    [SerializeField] private Image PotionRenderer;
    [SerializeField] private Sprite PotionNotAvailable;
    [SerializeField] private GameObject PotionStatText;
    TMP_Text PStats;

    [Header("Magic Attack Button")]
    [SerializeField] private Button MagicAttack;
    [SerializeField] private GameObject MagicAttackText;
    [SerializeField] private Image MagicAttackRenderer;
    [SerializeField] private Sprite MagicAttackNotAvailable;
    [SerializeField] private GameObject MagicAttackStatText;
    TMP_Text MAStat;

    [Header("Magic Shield Button")]
    [SerializeField] private Button MagicShield;
    [SerializeField] private GameObject MagicShieldText;
    [SerializeField] private Image MagicShieldRenderer;
    [SerializeField] private Sprite MagicShieldNotAvailable;
    [SerializeField] private GameObject MagicShieldStatText;
    TMP_Text MSStat;

    [Header("TP Home Button")]
    [SerializeField] private Button Home;
    [SerializeField] private GameObject HomeText;
    [SerializeField] private Image HomeRenderer;
    [SerializeField] private Sprite HomeNotAvailable;
    [SerializeField] private GameObject HomeBox;

    int fullHP;
    Color ShadedText;

    private void Awake()
    {
        fullHP = Stats.HP;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Stats.PotionUseIsActive = true;

        Potion.interactable = Stats.PotionUseIsActive;
        PotionText.SetActive(Stats.PotionUseIsActive);
        PotionStatText.SetActive(Stats.PotionUseIsActive);
        PStats = PotionStatText.GetComponent<TMP_Text>();

        MagicAttack.interactable = Stats.MagicAttackIsActive;
        MagicAttackText.SetActive(Stats.MagicAttackIsActive);
        MagicAttackStatText.SetActive(Stats.MagicAttackIsActive);
        MAStat = MagicAttackStatText.GetComponent<TMP_Text>();

        MagicShield.interactable = Stats.MagicShieldIsActive;
        MagicShieldText.SetActive(Stats.MagicShieldIsActive);
        MagicShieldStatText.SetActive(Stats.MagicShieldIsActive);
        MSStat = MagicShieldStatText.GetComponent<TMP_Text> ();

        ShadedText = new Color(151f / 255f, 151f / 255f, 151f / 255f);

        HomeBox.SetActive(false);
    }

    public void UsePotion()
    {
        if (Stats.PotionUseIsActive && Stats.MaxPotions > 0)
        {
            int totalGeneratrd = Stats.HP + Stats.PotionHeal;

            if (totalGeneratrd > fullHP) { Stats.HP = fullHP; }
            else { Stats.HP += Stats.PotionHeal; }

            Stats.MaxPotions--;
        }
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
        HomeBox.SetActive (true);
    }


    private void Update()
    {
        PStats.text = Stats.MaxPotions.ToString() + " Potions, +" + Stats.PotionHeal.ToString() + " Heal";
        MAStat.text = "+" + Stats.MagicAttack.ToString() + " Damage, " + Stats.AttMpCost.ToString() + " MP Cost";
        MSStat.text = "+" + Stats.MagicDef.ToString() + " Defence, " +  Stats.DefMPCost.ToString() + " MP Cost";


        if (Stats.PotionUseIsActive && Stats.MaxPotions == 0)
        {
            PotionRenderer.sprite = PotionNotAvailable;
            PotionText.GetComponent<TextMeshProUGUI>().color = ShadedText;
        } 
        else if (Stats.PotionUseIsActive && Stats.MaxPotions >= 1)
        {
            PotionText.GetComponent<TextMeshProUGUI>().color = Color.white;
        }

        if (Stats.MagicAttackIsActive && Stats.MP < Stats.AttMpCost)
        {
            MagicAttackRenderer.sprite = MagicAttackNotAvailable;
            MagicAttackText.GetComponent<TextMeshProUGUI>().color = ShadedText;
        }
        else if (Stats.MagicAttackIsActive && Stats.MP >= Stats.AttMpCost)
        {
            MagicAttackText.GetComponent<TextMeshProUGUI>().color = Color.white;
        }

        if (Stats.MagicShieldIsActive && Stats.MP < Stats.DefMPCost)
        {
            MagicShieldRenderer.sprite = MagicShieldNotAvailable;
            MagicShieldText.GetComponent<TextMeshProUGUI>().color = ShadedText;
        }
        else if (Stats.MagicShieldIsActive && Stats.MP >= Stats.DefMPCost)
        {
            MagicShieldText.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
    }
}
