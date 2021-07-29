using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private TrailConfig trailConfig;

    private Renderer _renderer;

    [SerializeField] private Color targetColor;
    private Color oldColor;
    private float _timer;

    private RoadColor roadColor;

    private void Awake()
    {
        roadColor = GetComponent<RoadColor>();
        _timer = trailConfig.timer;
    }

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        oldColor = _renderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (_timer != 0)
            {
                StopAllCoroutines();
                StartCoroutine(TimerForOldMaterial());
            }

            roadColor.enabled = true;
            roadColor.SetTargetColor(_renderer.material.color, targetColor);
        }
    }

    IEnumerator TimerForOldMaterial()
    {
        yield return new WaitForSeconds(_timer);

        roadColor.enabled = true;
        roadColor.SetTargetColor(_renderer.material.color, oldColor);
    }
}
