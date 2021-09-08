using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    [SerializeField] private SharedInt playerMoney;
    [SerializeField] private int startingMoney;
    private void Start()
    {
        playerMoney.Value = startingMoney;
    }
}
