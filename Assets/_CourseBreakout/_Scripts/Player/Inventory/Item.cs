using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    bool usingAbility;

    public string itemName;

    [HideInInspector]
    public float charges;

    private void Start()
    {
        charges = 100;
    }

    void Update()
    {
        if (!usingAbility && charges < 100)
            IncreaseCharges();

    }

    public void SetUsingAbility(bool usingAbility)
    {
        this.usingAbility = usingAbility;
    }

    public void IncreaseCharges()
    {
        charges += Time.deltaTime * 2.5f;
        if (charges > 100)
            charges = 100;
    }

    public void ReduceCharges()
    {
        charges -= Time.deltaTime * 6f;
        if (charges < 0)
            charges = 0;
    }
}
