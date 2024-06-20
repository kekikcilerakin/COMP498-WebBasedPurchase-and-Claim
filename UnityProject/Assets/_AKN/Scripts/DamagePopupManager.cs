using System.Collections;
using TMPro;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    [SerializeField] private GameObject damagePopupPrefab;
    [SerializeField] private GameObject slashImagePrefab;
    [SerializeField] private Canvas canvas;
    [SerializeField] private float floatDuration = 1.0f;
    [SerializeField] private float floatSpeed = 50.0f;
    [SerializeField] private Transform eggRegenPopupPosition;
    [SerializeField] private Transform autoDamagePopupPosition;


    public void InstantiateDamagePopup(int dmg, bool isRegen, bool isAutoDamage)
    {
        if (dmg == 0) return;

        Vector3 position = isRegen ? eggRegenPopupPosition.position : (isAutoDamage ? autoDamagePopupPosition.position : Input.mousePosition);
        string text = (isRegen ? "+" : "-") + dmg.ToString();
        Color color = isRegen ? Color.red : Color.green;

        GameObject dmgPopup = Instantiate(damagePopupPrefab, canvas.transform);
        TMP_Text popupText = dmgPopup.GetComponent<TMP_Text>();
        popupText.color = color;
        popupText.text = text;
        dmgPopup.transform.position = position;

        StartCoroutine(FloatAndDestroy(dmgPopup));
    }

    public void InstantiateSlashImage()
    {
        Vector3 mousePosition = Input.mousePosition;
        GameObject floatingImage = Instantiate(slashImagePrefab, canvas.transform);

        floatingImage.transform.position = mousePosition;
        floatingImage.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

        StartCoroutine(RemoveAfterDelay(floatingImage, 0.2f));
    }

    private IEnumerator FloatAndDestroy(GameObject dmgPopup)
    {
        RectTransform rectTransform = dmgPopup.GetComponent<RectTransform>();
        float elapsedTime = 0f;

        while (elapsedTime < floatDuration)
        {
            rectTransform.position += new Vector3(0, floatSpeed * Time.deltaTime, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(dmgPopup);
    }

    private IEnumerator RemoveAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }
}
