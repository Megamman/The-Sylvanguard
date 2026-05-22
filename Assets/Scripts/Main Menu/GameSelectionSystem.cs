using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameSelectionSystem : MonoBehaviour
{

    [SerializeField] private GameObject GameSelection;
    [SerializeField] private GameObject GameCreator;
    [SerializeField] private Transform Container;
    [SerializeField] private Button PlayButton;
    [SerializeField] private TMP_InputField inputField;
    [HideInInspector] public static List<string> SavedGame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Return();
    }

    private void Update()
    {
        if(MainStaticData.SelectedGame == "") { PlayButton.interactable = false; } else { PlayButton.interactable = true; }
    }

    bool found = false; // set it to false outside the loop
    public void CreateNewGame()
    {

        foreach (string item in SavedGame)
        {
            if (string.Equals(item, inputField.text))
            {
                Debug.Log("Name Already In Use");
                break; // since we found a match, exit the for loop.
            }
            else
            {
                GameDataPersistenceManager.instance.NewFileData();
            }
        }
    }

    void LoadList()
    {

        if(SavedGame.Count != 0)
        {
            foreach (string item in SavedGame)
            {
                GameObject newItem = Instantiate(GameSelection);
                newItem.transform.SetParent(Container);
                newItem.SetActive(true);

                GameNameData data = newItem.GetComponent<GameNameData>();

                data.PlayerName = item;
            }
        }
    }

    public void SaveGameData(ref GameData data)
    {
        data.SavedGame = SavedGame;

    }

    public void LoadGameData(GameData data)
    {
        SavedGame = data.SavedGame;

        LoadList();
    }

    public void ToCreate() {
        GameCreator.SetActive(true);
        GameSelection.SetActive(false);
    }

    public void Return()
    {
        GameCreator.SetActive(false);
        GameSelection.SetActive(true);
    }

    public void SubmitNewGameName()
    {
        GameDataPersistenceManager.instance.SaveFileData();
        SavedGame = new List<string>();
        SavedGame.Add(inputField.text);
        LoadList();
        Return();

    }

}
