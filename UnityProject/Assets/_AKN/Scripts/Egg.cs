using System.Collections;
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
    [SerializeField] private Image bonusBar;

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

        SetRandomEggColor();
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

        damagePopupManager.InstantiateDamagePopup((int)regenPerSecond, true, false);
        game.eggHealthDisplay.text = ((int)curHealth).ToString();
    }

    public bool TakeDamage(int dmg, bool isAutoClick)
    {
        PlayClickAnimation();
        damagePopupManager.InstantiateDamagePopup(dmg, false, isAutoClick);

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

        SetRandomEggColor();

        healthBar.fillAmount = 1;
    }

    public void SetRandomEggColor()
    {
        Color _color = new Color(Random.value, Random.value, Random.value);

        healthBar.color = _color;
        this.GetComponent<Image>().color = _color;
    }

}
