using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    public Transform Player;

    public Transform Pos1, Pos2;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (MainStaticData.SpawnPosition == 2)
        {
            Player.position = Pos2.position;
        }
        else 
        {
            Player.position = Pos1.position;
        }
    }

}
