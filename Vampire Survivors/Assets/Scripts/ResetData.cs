using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetData : MonoBehaviour
{
    public void resetData()
    {
        PlayerPrefs.SetFloat("hCount", 0);
        PlayerPrefs.SetFloat("sCount", 0);
        PlayerPrefs.SetFloat("Speed", 0);
        PlayerPrefs.SetFloat("Health", 0);
        PlayerPrefs.SetFloat("mCount", 0);
        PlayerPrefs.SetFloat("Magnet", 0);
    }

}
