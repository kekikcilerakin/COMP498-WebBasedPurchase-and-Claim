using TMPro;
using UnityEngine;

public class TabBetween : MonoBehaviour
{
    [SerializeField] private TMP_InputField nextInputField;
    private TMP_InputField inputField;


    private void Start()
    {
        if (nextInputField == null)
        {
            Destroy(this);
            return;
        }

        inputField = GetComponent<TMP_InputField>();
    }

    private void Update()
    {
        if (inputField.isFocused && Input.GetKeyDown(KeyCode.Tab))
        {
            nextInputField.ActivateInputField();
        }
    }
}
