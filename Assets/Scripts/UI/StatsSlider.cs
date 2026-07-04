using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class StatsSlider : MonoBehaviour //, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Slider")]
    [SerializeField] private Slider Slider;
    [SerializeField] private TMP_Text Text;
    [SerializeField] private StatType statType;
    [SerializeField] private GameObject HideObject;

    private int maxtStat;
    private int curStat;
    private string extraText;

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    Text.gameObject.SetActive(true);
    //    Debug.Log("Mouse on Slider");
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    Text.gameObject.SetActive(false);
    //}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Text.gameObject.SetActive(true);
        //HideObject = Slider.transform.parent.gameObject;

        switch (statType)
        {
            case StatType.Health:
                maxtStat = Stats.HP;
                extraText = "Health: "; 
                break;
            case StatType.Movement:
                maxtStat = Stats.MovePt;
                extraText = "Steps: ";
                break;
            case StatType.Mana:
                maxtStat = Stats.MP;
                extraText = "Mana: ";
                    break;

            default:
                Debug.Log("No Stat Set");
                return;

        }


        if(StatType.Mana <= 0 && statType == StatType.Mana) { 
            HideObject.SetActive(true); 
            //Debug.Log("Hidden"); 
        } else { HideObject.SetActive(false); 
            //Debug.Log("Visible"); 
        }

        Slider.maxValue = maxtStat;

    }

    // Update is called once per frame
    void Update()
    {
        switch (statType)
        {
            case StatType.Health:
                curStat = Stats.HP;
                break;
            case StatType.Movement:
                curStat = Stats.MovePt;
                break;
            case StatType.Mana:
                curStat = Stats.MP;
                break;

            default:
                Debug.Log("No Stat Set");
                return;
        }

        Slider.value = curStat;

        Text.text =  curStat.ToString() + "/" + maxtStat.ToString();

    }

    public enum StatType
    {
        Health,
        Movement,
        Mana
    }
}
