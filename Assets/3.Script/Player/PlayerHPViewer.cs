using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPViewer : MonoBehaviour
{
    [SerializeField]
    private Slider hpSlider;

    [SerializeField]
    private PlayerController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        hpSlider = GetComponent<Slider>();
    }

    private void Update()
    {
        hpSlider.value = player.CurrentHP / player.MaxHP;
    }
}
