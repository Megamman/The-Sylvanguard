using UnityEngine;

public interface IStatsDataPersistence
{
    void LoadStatsData(StatsData data);
    void SaveStatsData(ref StatsData data);
}
