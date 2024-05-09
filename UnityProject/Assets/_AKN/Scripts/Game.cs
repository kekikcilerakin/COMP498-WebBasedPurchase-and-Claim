using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Game : MonoBehaviour
{
    [SerializeField] private TMP_Text usernameDispay;
    [SerializeField] private TMP_Text goldDispay;
    [SerializeField] private TMP_Text scoreDispay;

    private void Awake()
    {
        if (DBManager.username == null) UnityEngine.SceneManagement.SceneManager.LoadScene("Login");

        usernameDispay.text = "Player: " + DBManager.username;
        goldDispay.text = "Gold: " + DBManager.gold;
        scoreDispay.text = DBManager.score.ToString();
    }

    public void CallSaveData()
    {
        StartCoroutine(SavePlayerData());
    }

    private IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", DBManager.username);
        form.AddField("gold", DBManager.gold);
        form.AddField("score", DBManager.score);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/comp498/savedata.php", form);
        yield return www.SendWebRequest();

        if (www.downloadHandler.text == "0")
        {
            Debug.Log("Game Saved.");
        }
        else
        {
            Debug.Log("Save failed.Error #" + www.downloadHandler.text);
        }

        DBManager.LogOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Login");
    }

    public void IncreaseStats()
    {
        DBManager.gold++;
        DBManager.score++;

        goldDispay.text = "Gold: " + DBManager.gold;
        scoreDispay.text = DBManager.score.ToString();
    }
}
