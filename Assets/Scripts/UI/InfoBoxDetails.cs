using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoBoxDetails : MonoBehaviour
{
    [HideInInspector] public static float timer = 0;
    [HideInInspector] public static float setTimer = 3;

    //UI
    public TMP_Text Title;
    public TMP_Text Description;
    public TMP_Text Level;

    public Slider BuySlider;

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
        
    }


    public void ShowDetails(SkillSystem info)
    {
        Title.text = info.SkillName;
        Description.text = info.SkillDescription;
        Level.text = info.currentLevel.ToString() + "/" + info.maxLevel.ToString();
        BuySlider.value = info.timer;
    }
}
