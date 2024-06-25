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

    private int autoDamageRange = 5;

    private void Awake()
    {
        if (DBManager.username == null) UnityEngine.SceneManagement.SceneManager.LoadScene("Login");

        UpdateUI();
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
        DBManager.gold += DBManager.goldMultiplier;

        int damageDealt = IsCritical() ? DBManager.damage * 2 : DBManager.damage;

        if (egg.TakeDamage(damageDealt, false))
        {
            NextLevel();
        }

        UpdateUI();
    }

    public void OnEggAutoClick()
    {
        int minAutoDamage = DBManager.autoClickDamage - autoDamageRange;
        int maxAutoDamage = DBManager.autoClickDamage + autoDamageRange;

        int autoDamageAmount = Random.Range(minAutoDamage, maxAutoDamage);

        autoDamageAmount = Mathf.Max(autoDamageAmount, 0);

        DBManager.gold += DBManager.goldMultiplier;

        if (egg.TakeDamage(autoDamageAmount, true))
        {
            NextLevel();
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        usernameDispay.text = "Player: " + DBManager.username;
        goldDisplay.text = "Gold: " + DBManager.gold;
        eggHealthDisplay.text = ((int)egg.curHealth).ToString();
        levelDisplay.text = "Level: " + DBManager.level;
    }

    private void NextLevel()
    {
        DBManager.level++;
        UpdateUI();

        egg.ResetEgg();
    }

    public void GoToMarket()
    {
        Application.OpenURL("http://localhost/comp498/index.php");
    }

    private bool IsCritical()
    {
        float randomNum = Random.Range(0f, 1f);

        if (randomNum <= DBManager.critChance / 100f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
