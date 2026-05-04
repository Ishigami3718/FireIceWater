using UnityEngine;

public class SceneManager : MonoBehaviour
{


    public void LoadByName(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
