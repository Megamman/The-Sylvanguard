using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;

public class StatsDataPersistenceManager : MonoBehaviour
{
    private string GameFileName = "SavedData";

    private StatsData fileData;

    private List<IStatsDataPersistence> statsDataPersistence;
    private StatsDataHandler DataHandler;
    public static StatsDataPersistenceManager instance { get; private set; }

    private bool dataLoadedSuccessfully = false;

    void Awake()
    {
        if (instance != null)
        {
            //Debug.LogError("Found more than one Data Persistence Manager in the scene.");
            Destroy(gameObject); // Important: stop the duplicate from running!
        }
        instance = this;
    }

    void Start()
    {
        string fileName = GameFileName; //DataHolder.GetFileName();

        DataHandler = new StatsDataHandler(Application.persistentDataPath, fileName);

        statsDataPersistence = FindAllStatsData();

        LoadStatsData();
        MainStaticData.statsData = fileData; // call data file to reach script to delete a save file

    }

    public void NewStatsData()
    {
        fileData = new StatsData();
    }

    public void LoadStatsData()
    {
        fileData = DataHandler.Load();

        if(fileData == null) { NewStatsData(); }

        foreach(IStatsDataPersistence dataObj in statsDataPersistence)
        { dataObj.LoadStatsData(fileData); }

        dataLoadedSuccessfully = true;
    }

    public void SaveStatsData()
    {
        if (!dataLoadedSuccessfully) { return; }

        statsDataPersistence = FindAllStatsData();

        if (statsDataPersistence == null || fileData == null) { NewStatsData(); }

        foreach(IStatsDataPersistence dataObj in statsDataPersistence)
        { dataObj.SaveStatsData(ref fileData); }

        DataHandler.Save(fileData);
    }

    private List<IStatsDataPersistence> FindAllStatsData()
    {
        IEnumerable<IStatsDataPersistence> _fileDataPersistences =
            FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None)
            .OfType<IStatsDataPersistence>();

        return new List<IStatsDataPersistence>(_fileDataPersistences);
    }
}
