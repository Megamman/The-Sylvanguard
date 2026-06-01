using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class EndGame : MonoBehaviour
{
    public GameObject EndScene;
    public TMP_Text _Title;
    public Transform Container;
    public GameObject imgPrefab;

    public TMP_Text Coins;
    public TMP_Text XP;

    private int CoinStart;
    private int XpStart;


    public static List<Sprite> _Sprites;

    private void Start()
    {
        _Sprites = new List<Sprite>();
        EndScene.SetActive(false);

        CoinStart = Stats.Coins;
        XpStart = Stats.XP;
    }

    public static void DeafetedEnemy(Sprite img)
    {
        _Sprites.Add(img);
    }

    public void GetEndGame(string title)
    {
        EndScene.SetActive(true);
        MainStaticData.HoldMovement = true;
        _Title.text = title;


        for (int i = 0; i < _Sprites.Count; i++)
        {
            GameObject e = Instantiate(imgPrefab, Container);
            Image img = e.GetComponent<Image>();
            img.sprite = _Sprites[i];

            Coins.text = (Stats.Coins - CoinStart).ToString();
            XP.text = (Stats.XP - XpStart).ToString();
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        DungeanSaveData.SaveData();
    }

    public void ReturnHome()
    {
        SceneManager.LoadScene("Town");
        DungeanSaveData.SaveData();
    }
}
