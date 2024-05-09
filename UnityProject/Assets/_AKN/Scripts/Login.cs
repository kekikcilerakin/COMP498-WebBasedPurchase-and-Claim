using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField passwordField;

    public TMP_Text errorText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(LoginPlayer());
        }
    }

    public void GoToRegister()
    {
        Application.OpenURL("http://localhost/comp498/auth/register.php");
    }

    public void CallLogin()
    {
        StartCoroutine(LoginPlayer());
    }

    private IEnumerator LoginPlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", nameField.text);
        form.AddField("password", passwordField.text);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/comp498/auth/login_unity.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            errorText.text = "Error:" + www.error;
            Debug.Log("Error:" + www.error);
        }

        if (www.downloadHandler.text.Length > 0 && www.downloadHandler.text[0] == '0')
        {
            Debug.Log("www.downloadHandler.text" + www.downloadHandler.text);
            Debug.Log("Logged in successfully.");

            DBManager.username = nameField.text;
            DBManager.gold = int.Parse(www.downloadHandler.text.Split('\t')[1]);
            DBManager.score = int.Parse(www.downloadHandler.text.Split('\t')[2]);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }
        else
        {
            errorText.text = "Login failed. Error #" + www.downloadHandler.text;
            Debug.Log("Login failed. Error #" + www.downloadHandler.text);
        }
    }
}
