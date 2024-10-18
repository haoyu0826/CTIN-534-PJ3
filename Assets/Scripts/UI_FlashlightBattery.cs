using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FlashlightBattery : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    private Scrollbar sb;

    private void Start()
    {
        sb = GetComponent<Scrollbar>();
    }

    private void Update()
    {
        sb.value = player.flashlightBattery / 100f;
    }
}

