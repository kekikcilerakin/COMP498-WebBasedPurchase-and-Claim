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
    private float regenRange = 5;

    [SerializeField] private int healthMultiplier = 10;
    [SerializeField] private float regenMultiplier = 1;

    [SerializeField] private Image healthBar;
    [SerializeField] private Image bonusBar;

    [SerializeField] private float shrinkFactor = 0.9f;
    [SerializeField] private float maxRotationOffset = 5f;


    private void Start()
    {
        eggTransform = GetComponent<RectTransform>();
        initialRotation = eggTransform.rotation;

        maxHealth = curHealth = DBManager.level * healthMultiplier;
        //regenPerSecond = DBManager.level * regenMultiplier;

        StartCoroutine(RegenerateHealth());

        SetRandomEggColorAndScale();
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

        float minRegen = DBManager.level - regenRange;
        float maxRegen = DBManager.level + regenRange;

        float regenAmount = Random.Range(minRegen, maxRegen);

        regenAmount = Mathf.Max(regenAmount, 0);

        if (curHealth < maxHealth)
        {
            curHealth += regenAmount;
            if (curHealth > maxHealth)
            {
                curHealth = maxHealth;
            }
            healthBar.fillAmount = curHealth / maxHealth;
        }

        damagePopupManager.InstantiateDamagePopup((int)regenAmount, true, false);
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

        SetRandomEggColorAndScale();

        healthBar.fillAmount = 1;
    }

    public void SetRandomEggColorAndScale()
    {
        float randomScale = Random.Range(1f, 1.25f);
        eggTransform.localScale = new Vector3(randomScale, randomScale, 1f);

        initialScale = eggTransform.localScale;

        Color _color = new Color(Random.value, Random.value, Random.value);

        healthBar.color = _color;
        this.GetComponent<Image>().color = _color;
    }

}
