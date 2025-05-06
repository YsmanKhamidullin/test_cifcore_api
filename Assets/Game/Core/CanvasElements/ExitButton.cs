using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    private void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(ExitApp);
    }

    private void ExitApp()
    {
        Application.Quit();
    }
}