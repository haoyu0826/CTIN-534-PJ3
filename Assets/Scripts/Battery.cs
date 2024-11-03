using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : InteractableObject
{
    private void Start()
    {
        isChecked = true;
    }

    protected override void ProcessObject()
    {
        base.ProcessObject();

        PlayerManager.instance.pm.flashlightBattery = 100f;
        Destroy(gameObject);
    }
}
