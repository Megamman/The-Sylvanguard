using UnityEngine;

public class LineRend : MonoBehaviour
{

    private LineRenderer lr;

    public Transform EndPoint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        // Invoke("LateStart", 1f);
        

    }


    // Update is called once per frame
    void Update()
    {
        Vector3 firstPos = new Vector3(transform.position.x, transform.position.y, 1);
        Vector3 secPos = new Vector3(EndPoint.position.x, EndPoint.position.y, 1);


        lr.SetPosition(0, firstPos);
        lr.SetPosition(1, secPos);
    }
}
