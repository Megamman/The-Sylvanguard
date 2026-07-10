using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static SkillSystem;

public class InfoBoxDetails : MonoBehaviour
{
    // UI References
    public TMP_Text Title;
    public TMP_Text Description;
    public TMP_Text Level;
    public TMP_Text XpCost;
    public TMP_Text CoinCost;
    public Slider BuySlider;

    public GameObject SkillHolder;
    public Transform SkillParent;

    public float timer = 0;

    // Parent panel containers for visibility control
    [SerializeField]private GameObject PartGBSlider, PartGBXP, PartGBCost;

    // Sprites
    public Sprite HP, MP, Steps, Att, HPReg, MPReg, Def, Dog, Mdef, Matt, Crit, CritMax, SkipMove, HPSteps, Potions, PotionHeal;

    // State tracking
    private SkillSystem currentSkill;

    private void OnEnable()
    {
        MainStaticData.showInfo = true;
    }

    private void OnDisable()
    {
        MainStaticData.showInfo = false;
    }

    void Awake()
    {
        PartGBSlider = BuySlider.transform.parent.gameObject;
        PartGBXP = XpCost.transform.parent.gameObject;
        PartGBCost = CoinCost.transform.parent.gameObject;
    }

    void Update()
{
    // Safe check: If no skill is loaded, do not execute operations
    if (currentSkill == null) return;

    // Use the timer value that SkillSystem is pushing into this script
    timer = currentSkill.timer;
    BuySlider.value = timer;
    UpdateData();
}


    public void UpdateData()
    {
        Title.text = currentSkill.SkillName;
        Description.text = currentSkill.SkillDescription;
        XpCost.text = currentSkill.xpCost.ToString();
        CoinCost.text = currentSkill.coinCost.ToString();
    }
    // Called instantly by SkillSystem when mouse enters or exits a node
    public void ShowDetails(SkillSystem info)
    {
        // Case A: Mouse exited the node entirely
        if (info == null)
        {
            currentSkill = null;
            ClearSkillUI();
            return;
        }

        // Case B: Mouse shifted to a completely brand-new skill node
        currentSkill = info;

        // Populate text details immediately (Runs once per hover action instead of every frame)
        BuySlider.maxValue = 2f; // Hardcoded fallback matching your setTime state variable

        // Handle Max Level Visibility Layout changes
        if (currentSkill.currentLevel == currentSkill.maxLevel)
        {
            PartGBSlider.SetActive(false);
            PartGBXP.SetActive(false);
            PartGBCost.SetActive(false);
            Level.text = "Max Level";
        }
        else
        {
            if(PartGBSlider != null) PartGBSlider.SetActive(true);
            if(PartGBXP != null) PartGBXP.SetActive(true);
            if(PartGBCost != null) PartGBCost.SetActive(true);
            Level.text = $"{currentSkill.currentLevel}/{currentSkill.maxLevel}";
        }

        // Wipe old icons and rebuild the list for this precise skill
        SkillSetUp();
    }

    private void ClearSkillUI()
    {
        // Bulletproof drain loop: detaches transform bounds before wiping items
        while (SkillParent.childCount > 0)
        {
            Transform child = SkillParent.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }

    void SkillSetUp()
    {
        ClearSkillUI();

        if (currentSkill.Upgrade == null) return;

        // Safe loop creation sequence
        for (int i = 0; i < currentSkill.Upgrade.Length; i++)
        {
            GameObject _skillObj = Instantiate(SkillHolder, SkillParent);

            RectTransform rect = _skillObj.GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.localPosition = Vector3.zero;
                rect.localScale = Vector3.one;
            }

            Image img = _skillObj.transform.GetChild(0).GetComponentInChildren<Image>();
            TMP_Text text = _skillObj.GetComponentInChildren<TMP_Text>();

            StatsDetails GetStat = currentSkill.Upgrade[i];
            text.text = GetStat.SkillIncrease.ToString();

            switch (GetStat.UpgradeEffect)
            {
                case UpgradeStat.Health: img.sprite = HP; break;
                case UpgradeStat.Movement: img.sprite = Steps; break;
                case UpgradeStat.Attack: img.sprite = Att; break;
                case UpgradeStat.MagicPoints: img.sprite = MP; break;
                case UpgradeStat.Dodge: img.sprite = Dog; break;
                case UpgradeStat.Defence: img.sprite = Def; break;
                case UpgradeStat.MagicAttack: img.sprite = Matt; break;
                case UpgradeStat.MagicShield: img.sprite = Mdef; break;
                case UpgradeStat.HpRegen: img.sprite = HPReg; break;
                case UpgradeStat.MpRegenSteps: img.sprite = MPReg; break;


                case UpgradeStat.Crit: img.sprite = Crit; break;
                case UpgradeStat.CritMax: img.sprite = CritMax; break;
                case UpgradeStat.SkipMovement: img.sprite = SkipMove; break;
                case UpgradeStat.HpRegenSteps: img.sprite = HPSteps; break;
                case UpgradeStat.Potions: img.sprite = Potions; break;
                case UpgradeStat.PotionHeal: img.sprite = PotionHeal; break;
                default:
                    // Keeps fallback component styling intact for unassigned elements
                    break;
            }
        }
    }
}