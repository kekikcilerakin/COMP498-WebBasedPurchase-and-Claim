using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Game : MonoBehaviour
{
    [SerializeField] private TMP_Text usernameDispay;
    [SerializeField] private TMP_Text goldDisplay;
    [SerializeField] private TMP_Text levelDisplay;
    public TMP_Text eggHealthDisplay;

    [SerializeField] private Egg egg;

    private void Awake()
    {
        if (DBManager.username == null) UnityEngine.SceneManagement.SceneManager.LoadScene("Login");

        usernameDispay.text = "Player: " + DBManager.username;
        goldDisplay.text = "Gold: " + DBManager.gold.ToString();
        levelDisplay.text = "Level: " + DBManager.level.ToString();
    }

    private void Start()
    {
        InvokeRepeating(nameof(OnEggAutoClick), 1.0f, 1.0f);
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
        form.AddField("level", DBManager.level);

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

    public void OnEggClick()
    {
        DBManager.gold++;
        if (egg.TakeDamage(DBManager.damage, false))
        {
            NextLevel();
        }

        goldDisplay.text = "Gold: " + DBManager.gold;
        eggHealthDisplay.text = ((int)egg.curHealth).ToString();
    }
    public void OnEggAutoClick()
    {
        Debug.Log("Auto click for " + DBManager.autoClickDamage + " damage.");
        DBManager.gold++;
        if (egg.TakeDamage(DBManager.autoClickDamage, true))
        {
            NextLevel();
        }

        goldDisplay.text = "Gold: " + DBManager.gold;
        eggHealthDisplay.text = ((int)egg.curHealth).ToString();
    }

    private void NextLevel()
    {
        DBManager.level++;
        levelDisplay.text = "Level: " + DBManager.level;

        egg.ResetEgg();
    }

    public void GoToMarket()
    {
        Application.OpenURL("http://localhost/comp498/index.php");
    }
}
