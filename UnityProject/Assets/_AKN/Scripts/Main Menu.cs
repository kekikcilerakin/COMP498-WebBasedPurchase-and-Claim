using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text usernameDisplay;

    private void Start()
    {
        if (DBManager.LoggedIn)
        {
            usernameDisplay.text = "Logged in as: " + DBManager.username;
        }
    }

    public void GoToRegister()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToLogIn()
    {
        SceneManager.LoadScene(2);
    }

    public void GoTogGame()
    {
        SceneManager.LoadScene(3);
    }
}
