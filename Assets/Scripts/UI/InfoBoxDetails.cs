using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class InfoBoxDetails : MonoBehaviour
{
    [HideInInspector] public static float timer = 0;
    [HideInInspector] public static float setTimer = 3;

    //UI
    public TMP_Text Title;
    public TMP_Text Description;
    public TMP_Text Level;
    public TMP_Text XpCost;
    public TMP_Text CoinCost;

    public Slider BuySlider;

    private SkillSystem skill;

    GameObject PartGBSlider, PartGBXP, PartGBCost;

    private void OnEnable()
    {
        MainStaticData.showInfo = true;
    }

    private void OnDisable()
    {
        MainStaticData.showInfo = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BuySlider.maxValue = setTimer;
        PartGBSlider = BuySlider.transform.parent.gameObject;
        PartGBXP = XpCost.transform.parent.gameObject;
        PartGBCost = CoinCost.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        BuySlider.value = skill.timer;
        Title.text = skill.SkillName;
        Description.text = skill.SkillDescription;

        XpCost.text = skill.xpCost.ToString();
        CoinCost.text = skill.coinCost.ToString();

        if(skill.currentLevel == skill.maxLevel)
        {
            PartGBSlider.SetActive(false);
            PartGBXP.SetActive(false);
            PartGBCost.SetActive(false);
            Level.text = "Max Level";
        } else { 
            PartGBSlider.SetActive(true);
            PartGBXP.SetActive(true);
            PartGBCost.SetActive(true);
            Level.text = skill.currentLevel.ToString() + "/" + skill.maxLevel.ToString();
        }
    }


    public void ShowDetails(SkillSystem info)
    {
        skill = info;

    }
}
