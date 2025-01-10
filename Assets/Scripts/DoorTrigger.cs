using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    
    public string sceneToLoad;

    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
       
        if (other.CompareTag("Player"))
        {
          
            Debug.Log("Player entered the door trigger!");

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
