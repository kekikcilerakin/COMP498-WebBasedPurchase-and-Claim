using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField passwordField;

    public TMP_Text errorText;

    public Button submitButton;

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
            Debug.Log("Err:" + www.error);
        }

        if (www.downloadHandler.text.Length > 0 && www.downloadHandler.text[0] == '0')
        {
            Debug.Log("Logged in successfully.");

            DBManager.username = nameField.text;
            DBManager.gold = int.Parse(www.downloadHandler.text.Split('\t')[1]);
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            errorText.text = "Login failed. Error #" + www.downloadHandler.text;
            Debug.Log("Login failed. Error #" + www.downloadHandler.text);
        }
    }
}
