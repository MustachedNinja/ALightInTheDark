using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightController : MonoBehaviour
{
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    [SerializeField] private float minIntensityDistance = 20f;
    [SerializeField] private float maxIntensityDistance = 5f;
    [SerializeField] private float maxIntensity = 1f;

    private Light2D[] lights;

    void Start() {
        lights = transform.GetComponentsInChildren<Light2D>(true);
    }

    void SetIntensity(float intensity) {
        foreach(Light2D l in lights) {
            l.intensity = intensity;
        }
    }

    float CalculateIntensity(Vector2 player1Pos, Vector2 player2Pos) {
        float distance = Vector2.Distance(player1Pos, player2Pos);
        if (distance > minIntensityDistance) {
            return 0;
        } else if (distance < maxIntensityDistance) {
            return maxIntensity;
        } else {
            return Mathf.InverseLerp(minIntensityDistance, maxIntensityDistance, distance) * maxIntensity;
        }
    }

    void Update() {
        SetIntensity(CalculateIntensity(player1.position, player2.position));
    }

}
