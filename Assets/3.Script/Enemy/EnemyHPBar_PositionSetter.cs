using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBar_PositionSetter : MonoBehaviour
{
    [SerializeField] private Vector3 distance = Vector3.up * 35f;

    private GameObject target;
    private RectTransform uiTransform;

    public void Setup(GameObject target)
    {
        this.target = target;
        uiTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(!target.activeSelf)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(target.transform.position);

        uiTransform.position = screenPosition + distance;
    }
}
