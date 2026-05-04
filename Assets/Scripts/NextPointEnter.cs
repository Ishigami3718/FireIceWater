using UnityEngine;

public class NextPointEnter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered the next point");
            GameManager.Instance.StopMoving();
             GameManager.Instance.canPlay = true;
             GameManager.Instance.isClicable = true;
        }
    }
}
