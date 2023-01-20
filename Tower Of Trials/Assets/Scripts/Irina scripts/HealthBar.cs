using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float minPos, maxPos;
    [SerializeField] private float maxHealth;
    [SerializeField] RectTransform target;

    [SerializeField] private Sprite[] hpFlaskStates;
    [SerializeField] private Sprite[] hpFlaskStatesBack;

    [SerializeField] Image flaskVisual;
    [SerializeField] Image flaskVisualBack;
    public static HealthBar Instance;

    public void SetHealth(float newHpValue)
    {
        float percent = Mathf.Clamp(newHpValue,0, maxHealth) / maxHealth;
        float newPos = (maxPos - minPos) * percent;
        newPos += minPos;

        int newState = Mathf.CeilToInt((hpFlaskStates.Length - 1) * percent);

        flaskVisual.sprite = hpFlaskStates[newState];
        flaskVisualBack.sprite = hpFlaskStatesBack[newState];

        target.anchoredPosition = new Vector2(target.anchoredPosition.x, newPos);
    }
    void Start()
    {
        Instance = this;
    }

    internal void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
    }
}
