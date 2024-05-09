using UnityEngine;

public class Egg : MonoBehaviour
{
    private RectTransform eggTransform;
    private Vector3 initialScale;
    private Quaternion initialRotation;

    [SerializeField] private float shrinkFactor = 0.9f;
    [SerializeField] private float maxRotationOffset = 5f;

    private void Start()
    {
        eggTransform = GetComponent<RectTransform>();
        initialScale = eggTransform.localScale;
        initialRotation = eggTransform.rotation;
    }

    public void OnMouseDown()
    {
        eggTransform.localScale = initialScale * shrinkFactor;

        float randomRotationOffset = Random.Range(-maxRotationOffset, maxRotationOffset);
        eggTransform.Rotate(Vector3.forward, randomRotationOffset);
    }

    public void OnMouseUp()
    {
        eggTransform.localScale = initialScale;
        eggTransform.rotation = initialRotation;
    }
}
