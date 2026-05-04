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

    public Slider BuySlider;

    private SkillSystem skill;

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
    }

    // Update is called once per frame
    void Update()
    {
        BuySlider.value = skill.timer;
        Title.text = skill.SkillName;
        Description.text = skill.SkillDescription;
        Level.text = skill.currentLevel.ToString() + "/" + skill.maxLevel.ToString();
    }


    public void ShowDetails(SkillSystem info)
    {
        skill = info;
    }
}
