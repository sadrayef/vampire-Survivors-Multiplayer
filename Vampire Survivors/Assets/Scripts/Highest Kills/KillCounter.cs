using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class KillCounter : MonoBehaviour
{
    public Text highestDisplay;


    private void Awake()
    {
        highestDisplay.text = PlayerPrefs.GetFloat("Killed Enemies").ToString();
    }
}
