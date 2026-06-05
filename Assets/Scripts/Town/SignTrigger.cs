using UnityEngine;

public class SignTrigger : MonoBehaviour
{
    public GameObject Text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Text.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Text.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Text.SetActive(false);
        }
    }
}
