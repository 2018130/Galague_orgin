using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPViewer : MonoBehaviour
{
    private Slider slider;
    private EnemyController enemy;

    /// <summary>
    /// Calling func when return from pool
    /// </summary>
    /// <param name="enemy"></param>
    public void Setup(EnemyController enemy)
    {
        this.enemy = enemy;
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        slider.value = enemy.CurrentHP / enemy.MaxHP;
    }
}
