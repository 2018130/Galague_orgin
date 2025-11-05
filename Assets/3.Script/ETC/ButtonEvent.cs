using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    [SerializeField] private Text score;

    private void Start()
    {
        if(score != null)
        {
            score.text = PlayerPrefs.GetInt($"Score").ToString();
        }
    }
    public void SceneLoader(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
