using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameSelectionSystem : MonoBehaviour, IGameDataPersistence
{

    [SerializeField] private GameObject GameClone;
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
        LoadList();
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
                GameSaveData.SaveData();
            }
        }
    }

    void LoadList()
    {

        if(SavedGame.Count != 0)
        {
            foreach (string item in SavedGame)
            {
                GameObject newItem = Instantiate(GameClone);
                newItem.transform.SetParent(Container);
                newItem.SetActive(true);

                GameNameData data = newItem.GetComponent<GameNameData>();

                if (item != null) { data.PlayerName = item; }
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
        SavedGame = new List<string>();
        SavedGame.Add(inputField.text);
        GameSaveData.SaveData();
        LoadList();
        Return();

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Town");
    }

}
