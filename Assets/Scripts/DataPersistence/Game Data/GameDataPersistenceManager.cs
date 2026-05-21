using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;

public class GameDataPersistenceManager : MonoBehaviour
{
    private string gameFileName = "SavedFiles";

    private GameData gameData;

    private List<IGameDataPersistence> gameDataPersistences;
    private GameDataHandler gameDataHandler;
    public static GameDataPersistenceManager instance { get; private set; }

    private bool dataLoadedSuccessfully = false;

    private void Awake()
    {
        if (instance != null)
        {
            //Debug.LogError("Found more than one Data Persistence Manager in the scene.");
            Destroy(gameObject); // Important: stop the duplicate from running!
        }
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string fileName = MainStaticData.SelectedGame;

        gameDataHandler = new GameDataHandler(Application.persistentDataPath, gameFileName);

        gameDataPersistences = FindAllFileData();

        LoadFileData();
        MainStaticData.gameData = gameData;
    }

    public void NewFileData()
    {
        gameData = new GameData();
    }

    public void LoadFileData()
    {
        this.gameData = gameDataHandler.Load();

        if (this.gameData == null)
        {
            NewFileData();
        }

        foreach (IGameDataPersistence dataObj in gameDataPersistences)
        {
            dataObj.LoadGameData(gameData);
        }

        dataLoadedSuccessfully = true;
    }

    public void SaveFileData()
    {
        if (!dataLoadedSuccessfully) { return; }

        gameDataPersistences = FindAllFileData();

        if (gameDataPersistences == null || gameData == null) { NewFileData(); }

        foreach (IGameDataPersistence dataObj in gameDataPersistences)
        {
            dataObj.SaveGameData(ref gameData);
        }

        gameDataHandler.Save(gameData);
    }

    private List<IGameDataPersistence> FindAllFileData()
    {
        IEnumerable<IGameDataPersistence> _fileDataPersistences =
            FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None)
            .OfType<IGameDataPersistence>();

        return new List<IGameDataPersistence>(_fileDataPersistences);
    }
}

