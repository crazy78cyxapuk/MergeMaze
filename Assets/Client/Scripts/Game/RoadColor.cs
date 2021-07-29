using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadColor : MonoBehaviour
{
    private Renderer _renderer;
    private RoadColor _thisRoadColor;

    public Color colorTarget;
    public Color colorOld;

    private float _timer = 0;
    private float maxValueTimer = 1;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _thisRoadColor = GetComponent<RoadColor>();
    }

    private void OnEnable()
    {
        _timer = 0;
        colorOld = _renderer.material.color;
    }

    void Update()
    {
        if (_timer <= maxValueTimer)
        {
            _timer += 3f * Time.deltaTime;
            _renderer.material.color = Color.Lerp(colorOld, colorTarget, _timer);
        }
        else
        {
            _timer = 10;
            _thisRoadColor.enabled = false;
        }
    }

    public void SetTargetColor(Color current, Color target)
    {
        colorOld = current;
        colorTarget = target;

        _timer = 0;
    }
}
