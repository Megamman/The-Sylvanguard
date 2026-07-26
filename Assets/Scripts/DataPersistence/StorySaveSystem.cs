using UnityEngine;

public class StorySaveSystem : MonoBehaviour, IStatsDataPersistence
{

    public GameObject ObjT2, ObjT3, ObjT4, ObjT5, ObjT6, ObjT7;

    public void LoadStatsData(StatsData data)
    {
        StoryStatic.T2 = data.T2;
        StoryStatic.T3 = data.T3;
        StoryStatic.T4 = data.T4;
        StoryStatic.T5 = data.T5;
        StoryStatic.T6 = data.T6;
        StoryStatic.T7 = data.T7;

    }

    public void SaveStatsData(ref StatsData data)
    {
        data.T2 = StoryStatic.T2;
        data.T3 = StoryStatic.T3;
        data.T4 = StoryStatic.T4;
        data.T5 = StoryStatic.T5;
        data.T6 = StoryStatic.T6;
        data.T7 = StoryStatic.T7;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (ObjT2 != null) { ObjT2.SetActive(StoryStatic.T2); }
        if (ObjT3 != null) { ObjT3.SetActive(StoryStatic.T3); }
        if (ObjT4 != null) { ObjT4.SetActive(StoryStatic.T4); }
        if (ObjT5 != null) { ObjT5.SetActive(StoryStatic.T5); }
        if (ObjT6 != null) { ObjT6.SetActive(StoryStatic.T6); } 
        if (ObjT7 != null) { ObjT7.SetActive(StoryStatic.T7); }
    }
}
