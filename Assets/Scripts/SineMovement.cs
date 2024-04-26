using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SineMovement : MonoBehaviour
{
    [SerializeField] Light2D _light;

    [SerializeField] private float _hoverSpeed; // Frequency of the sine wave
    [SerializeField] private float _maxHoverOffset; // Maximum offset in height

    private float _elapsedTime; // Accumulated time passed (multiplied by speed in order to get proper result)

    private void Start()
    {
        _light = GetComponent<Light2D>();
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime * _hoverSpeed;
        float newHeight = Mathf.Abs(Mathf.Sin(_elapsedTime) * _maxHoverOffset);
        _light.intensity = newHeight;
    }
}
