using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayDisplay : MonoBehaviour
{
    [SerializeField] private SharedInt day;
    [SerializeField] private TMP_Text text;

    private void Awake()
    {
        day.valueChangeEvent.AddListener(Display);
    }

    private void Display()
    {
        text.text = "DAY " + (day.Value + 1).ToString();
    }

    private void OnDestroy()
    {
        day.valueChangeEvent.RemoveListener(Display);
    }
}
