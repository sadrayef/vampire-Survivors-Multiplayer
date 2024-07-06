using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AllCoins : MonoBehaviour
{
    public Text AllCoinsDisplay;


    private void Awake()
    {
        AllCoinsDisplay.text = PlayerPrefs.GetFloat("All coins").ToString();
    }
}

