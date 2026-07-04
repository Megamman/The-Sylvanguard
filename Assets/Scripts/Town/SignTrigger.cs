using UnityEngine;

public class SignTrigger : MonoBehaviour
{
    public GameObject Text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Text.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            //Debug.Log("Trigger");

        if (collision.gameObject.CompareTag("Player"))
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
