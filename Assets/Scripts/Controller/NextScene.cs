using UnityEngine.SceneManagement;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    public string sceneToLoad;  // The scene name to load
    public Vector2 point1;  // First corner (x, y) of the area
    public Vector2 point2;  // Second corner (x', y') of the area

    void Update()
    {
        // Check if the player is within the rectangular area defined by (point1) and (point2)
        if (transform.position.x >= Mathf.Min(point1.x, point2.x) && transform.position.x <= Mathf.Max(point1.x, point2.x) &&
            transform.position.y >= Mathf.Min(point1.y, point2.y) && transform.position.y <= Mathf.Max(point1.y, point2.y))
        {
            Debug.Log("Player entered the area!");

            // Load the specified scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
