using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private float rotationPerCycle;
    [SerializeField] private float minimumScale;
    [SerializeField] private float maximumScale;

    private void Start()
    {
        float randomScale = Random.Range(minimumScale, maximumScale);
        transform.localScale = new Vector2(randomScale, randomScale);
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotationPerCycle);
    }
}
