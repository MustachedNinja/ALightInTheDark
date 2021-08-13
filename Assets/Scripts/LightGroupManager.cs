using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightGroupManager : MonoBehaviour
{


    [SerializeField] private GameObject _player1;
    [SerializeField] private GameObject _player2;

    [SerializeField] private float _maxLightDistance;
    [SerializeField] private float _minLightDistance;

    [Range(0f, 2f)][SerializeField] private float _minIntensity;
    [Range(0f, 2f)][SerializeField] private float _maxIntensity;

    private Light2D[] _childLights;

    private void Awake() {
        _childLights = transform.GetComponentsInChildren<Light2D>(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetFlatLightIntensity(CalculateIntensity());
    }

    private float CalculateIntensity() {
        float distance = Vector2.Distance(_player1.transform.position, _player2.transform.position);
        if (distance > _maxLightDistance) {
            return _minIntensity;
        } else if (distance < _maxLightDistance && distance > _minLightDistance) {
            float percentage = 1 - ((distance - _minLightDistance) / (_maxLightDistance - _minLightDistance));
            return percentage * (_maxIntensity - _minIntensity) + _minIntensity;
        } else {
            return _maxIntensity;
        }
    }

    private void SetFlatLightIntensity(float intensity) {
        foreach (Light2D light in _childLights) {
            light.intensity = intensity;
        }
    }
}
