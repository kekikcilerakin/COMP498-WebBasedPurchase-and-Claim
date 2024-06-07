using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class Egg : MonoBehaviour
{
    [SerializeField] private Game game;
    [SerializeField] private DamagePopupManager damagePopupManager;
    private RectTransform eggTransform;
    private Vector3 initialScale;
    private Quaternion initialRotation;

    [SerializeField] private float maxHealth = 10;
    public float curHealth = 10;
    private float regenPerSecond = 1;

    [SerializeField] private int healthMultiplier = 10;
    [SerializeField] private float regenMultiplier = 1;

    [SerializeField] private Image healthBar;

    [SerializeField] private List<Color> eggColors;

    [SerializeField] private float shrinkFactor = 0.9f;
    [SerializeField] private float maxRotationOffset = 5f;

    private void Start()
    {
        eggTransform = GetComponent<RectTransform>();
        initialScale = eggTransform.localScale;
        initialRotation = eggTransform.rotation;

        maxHealth = curHealth = DBManager.level * healthMultiplier;
        regenPerSecond = DBManager.level * regenMultiplier;

        StartCoroutine(RegenerateHealth());

        eggColors = new List<Color>
        {
            new Color32(180, 123, 200, 255),
            new Color32(90, 200, 150, 255),
            new Color32(210, 160, 90, 255),
            new Color32(70, 220, 180, 255),
            new Color32(150, 100, 210, 255),
            new Color32(200, 70, 120, 255),
            new Color32(120, 190, 80, 255),
            new Color32(230, 130, 220, 255),
            new Color32(100, 170, 230, 255),
            new Color32(160, 80, 250, 255),
            new Color32(250, 170, 120, 255),
            new Color32(90, 230, 100, 255),
            new Color32(220, 90, 200, 255),
            new Color32(130, 210, 70, 255),
            new Color32(60, 180, 230, 255),
            new Color32(240, 60, 180, 255),
            new Color32(80, 160, 250, 255),
            new Color32(190, 70, 230, 255),
            new Color32(110, 220, 100, 255),
            new Color32(200, 110, 210, 255),
            new Color32(100, 240, 90, 255),
            new Color32(230, 80, 160, 255),
            new Color32(140, 200, 60, 255),
            new Color32(70, 190, 240, 255),
            new Color32(250, 100, 220, 255),
            new Color32(150, 250, 130, 255),
            new Color32(210, 150, 230, 255),
            new Color32(80, 240, 140, 255),
            new Color32(220, 60, 250, 255),
            new Color32(120, 190, 60, 255),
            new Color32(250, 120, 200, 255),
            new Color32(160, 250, 90, 255),
            new Color32(230, 160, 240, 255),
            new Color32(100, 250, 170, 255),
            new Color32(240, 90, 250, 255),
            new Color32(130, 230, 80, 255),
            new Color32(60, 220, 250, 255),
            new Color32(240, 140, 230, 255),
            new Color32(110, 250, 110, 255),
            new Color32(250, 60, 220, 255),
            new Color32(190, 230, 100, 255),
            new Color32(70, 220, 250, 255),
            new Color32(250, 170, 250, 255),
            new Color32(160, 250, 160, 255),
            new Color32(250, 110, 230, 255),
            new Color32(210, 240, 70, 255),
            new Color32(100, 220, 250, 255)


        };

        healthBar.color = eggColors[DBManager.level - 1];
        this.GetComponent<Image>().color = eggColors[DBManager.level - 1];
    }

    private void PlayClickAnimation()
    {
        eggTransform.localScale = initialScale * shrinkFactor;

        float randomRotationOffset = Random.Range(-maxRotationOffset, maxRotationOffset);
        eggTransform.Rotate(Vector3.forward, randomRotationOffset);

        Invoke(nameof(RevertEggScaleAndRotation), 0.05f);
    }

    private void RevertEggScaleAndRotation()
    {
        //Revert scale and roration to default values.
        eggTransform.localScale = initialScale;
        eggTransform.rotation = initialRotation;
    }

    private IEnumerator RegenerateHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Regenerate();
        }
    }

    private void Regenerate()
    {
        if (curHealth < maxHealth)
        {
            curHealth += regenPerSecond;
            if (curHealth > maxHealth)
            {
                curHealth = maxHealth;
            }
            healthBar.fillAmount = curHealth / maxHealth;
        }

        game.eggHealthDisplay.text = ((int)curHealth).ToString();
    }

    public bool TakeDamage(int dmg, bool isAutoClick)
    {
        PlayClickAnimation();
        damagePopupManager.InstantiateDamagePopup(dmg);

        if (!isAutoClick)
        {
            damagePopupManager.InstantiateSlashImage();

        }

        curHealth -= dmg;
        healthBar.fillAmount = curHealth / maxHealth;

        return curHealth <= 0;

    }


    public void ResetEgg()
    {
        maxHealth = curHealth = DBManager.level * healthMultiplier;
        regenPerSecond = DBManager.level * regenMultiplier;

        healthBar.color = eggColors[DBManager.level - 1];
        healthBar.fillAmount = 1;
        this.GetComponent<Image>().color = eggColors[DBManager.level - 1];
    }

}
