using System.Collections.Generic;
using TMPro;
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
    private PlayerMovement controls;
    private GameObject[] Games;
    VirtualKeyboard vk = new VirtualKeyboard();
    bool isKeyOn;


    public GameObject warningTextOBJ;
    public TMP_Text warningText;
    private void Awake()
    {
        controls = new PlayerMovement();
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Return();
        warningTextOBJ.SetActive(false);
        //LoadList();
    }

    private void Update()
    {
        ////Virtual Keyboard system (Not working)

        //controls.Main.UsingKeyboard.performed += ctx => isKeyOn = true;
        //controls.Main.UsingKeyboard.performed += ctx => vk.HideTouchKeyboard();

        //if (MainStaticData.SelectedGame == "") { PlayButton.interactable = false; } else { PlayButton.interactable = true; }
        //if(inputField.isFocused) { if (!string.IsNullOrWhiteSpace(inputField.text)) { vk.ShowTouchKeyboard(); isKeyOn = true; } }
        //else
        //{
        //    {  isKeyOn = false; }
        //}
        //if (isKeyOn)
        //{
        //    vk.HideTouchKeyboard();
        //}
    }

    public void CreateNewGame()
    {
        //vk.HideTouchKeyboard();

        if (!string.IsNullOrWhiteSpace(inputField.text))
        {
            for (int i = 0; i < SavedGame.Count; i++)
            {
                if (string.Equals(SavedGame[i], inputField.text))
                {
                    warningTextOBJ.SetActive(true);
                    warningText.text = "Name Already In Use";
                    GameDataPersistenceManager.instance.NewFileData();
                    GameSaveData.SaveData();
                }
            }
        }else
        {
            warningTextOBJ.SetActive(true);
            warningText.text = "Name Is Empty";
        }
    }

    void LoadList()
    {

        //GameDataPersistenceManager.instance.LoadFileData();

        if (SavedGame.Count != 0)
        {
            // Clear old UI objects first if needed safely
            foreach (Transform child in Container)
            {
                Destroy(child.gameObject);
            }

        }
        foreach (string item in SavedGame)
        {
            if (item == null) continue;

            GameObject newItem = Instantiate(GameClone, Container);
            newItem.SetActive(true);

            GameNameData data = newItem.GetComponent<GameNameData>();

            if (data != null) { data.PlayerName = item; }
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
        warningTextOBJ.SetActive(false);
    }

    public void Return()
    {
        GameCreator.SetActive(false);
        GameSelection.SetActive(true);
        warningTextOBJ.SetActive(false);
    }

    public void SubmitNewGameName()
    {

        if (!string.IsNullOrWhiteSpace(inputField.text))
        {
            //foreach (string item in SavedGame)
            for (int i = 0; i < SavedGame.Count; i++)
            {
                if (string.Equals(SavedGame[i], inputField.text))
                {

                    warningTextOBJ.SetActive(true);
                    warningText.text = "Name Already In Use";
                    Debug.Log("Name Already In Use");
                    break; // since we found a match, exit the for loop.
                }
                warningTextOBJ.SetActive(false);
            }
            Debug.Log("Creating Game");
            SavedGame.Add(inputField.text);
            GameSaveData.SaveData();
            LoadList();
            Return();
        }

        else
        {
            warningTextOBJ.SetActive(true);
            warningText.text = "Name Is Empty";
        }
    }

    public void StartGame()
    {

        if (MainStaticData.SelectedGame != null)
        {
            warningTextOBJ.SetActive(false);
            SceneManager.LoadScene("Town");
        }
        else
        {

            warningTextOBJ.SetActive(true);
            warningText.text = "Game Not Selected";
        }
    }

    public void DeleteGame()
    {
        MainStaticData.gameData.RemoveGameData();
        MainStaticData.gameDataHandler.DeleteSaveFile();       
        LoadList();
    }

}
