using UnityEditor;
using UnityEngine;

public class EnemySpawnGrid : MonoBehaviour
{
    [Header("Spawn Details")]
    [SerializeField] private GameObject[] Entities;
    [SerializeField] private int MaxAmountToSpawn;
    [SerializeField] private int MinAmountToSpawn;
    private int SpawnAmountResult;

    [Header("Area Details")]
    [SerializeField] private int TopY;
    [SerializeField] private int LeftX;
    [SerializeField] private int RightX;
    [SerializeField] private int BotY;

    [SerializeField] private Color borderColor;
    [SerializeField] private LayerMask BlockSpawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartSpawn();
    }

    void StartSpawn()
    {
        SpawnAmountResult = Random.Range(MinAmountToSpawn, MaxAmountToSpawn);

        int spawned = 0;
        int safety = 0; // prevent infinite loop

        while (spawned < SpawnAmountResult && safety < 500) // limit attempts
        {
            int _PosY = Random.Range(BotY+1, TopY-1);
            int _PosX = Random.Range(RightX-1, LeftX+1);

            Vector3 spawnPos = new Vector3(_PosX, _PosY, 0);

            //Debug.Log("Selected Pos: " + spawnPos);
            if (Physics2D.OverlapPoint(spawnPos, BlockSpawn) == null && spawnPos != new Vector3(0, 0, 0))
            {
                int e = Random.Range(0, Entities.Length);
                GameObject enemy = Instantiate(Entities[e], spawnPos, Quaternion.identity);
                enemy.transform.SetParent(gameObject.transform);

                //Debug.Log("Point is Available. Postion: " + spawnPos);
                spawned++;
            }
            else
            {
                //Debug.Log("Point is NOT Available. Postion: " + spawnPos);
                safety++;
            }
        }
    }

        void OnDrawGizmos()
    {
        Gizmos.color = borderColor;
        Gizmos.DrawLine(new Vector3(RightX - 0.5f, TopY - 0.5f), new Vector3(LeftX + 0.5f, TopY - 0.5f));  //Bottem Horizontal Line
        Gizmos.DrawLine(new Vector3(RightX - 0.5f, BotY + 0.5f), new Vector3(LeftX + 0.5f, BotY + 0.5f));   //Top Horizontal Line
        Gizmos.DrawLine(new Vector3(RightX - 0.5f, TopY - 0.5f), new Vector3(RightX - 0.5f, BotY + 0.5f)); // Left Vertical Line
        Gizmos.DrawLine(new Vector3(LeftX + 0.5f, TopY - 0.5f), new Vector3(LeftX + 0.5f, BotY + 0.5f));// Right Vertical Line

        Vector3 pos1 = new Vector3(RightX - 0.5f, TopY - 0.5f);
        Vector3 pos2 = new Vector3(LeftX + 0.5f, TopY - 0.5f);
        Vector3 pos3 = new Vector3(RightX - 0.5f, BotY + 0.5f);
        Vector3 pos4 = new Vector3(LeftX + 0.5f, LeftX + 0.5f);

        //Handles.Label(pos1, $"{(RightX, TopY)}");
        //Handles.Label(pos2, $"{(LeftX, TopY)}");
        //Handles.Label(pos3, $"{(RightX, BotY)}");
        //Handles.Label(pos4, $"{(LeftX, BotY)}");
    }

    private void OnValidate()
{
    // Ensure Top is never lower than Bottom
    // If you move Top below Bottom, Top will snap to Bottom's value
    TopY = Mathf.Max(TopY, BotY);

    // Ensure Right is never to the left of Left
    RightX = Mathf.Max(RightX, LeftX);
    
    // Optional: If you want Bottom to stay below Top when moving Bottom
    BotY = Mathf.Min(BotY, TopY);
    LeftX = Mathf.Min(LeftX, RightX);
}

}
