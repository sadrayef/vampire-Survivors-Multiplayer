using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PowerUp : MonoBehaviour
{

    public int num;
    public int health;
    public int speed;
    public GameObject labelHealth;
    public GameObject labelSpeed;
    int hCount;
    int sCount;
    public Text AllCoinsDisplay;
    public Text AllCoinsDisplay2;


    public void Awake()
    {

        num = (int)PlayerPrefs.GetFloat("All coins");
        health = 0;
        speed = 0;

        hCount = (int)PlayerPrefs.GetFloat("hCount");
        sCount = (int)PlayerPrefs.GetFloat("sCount");

        labelHealth.SetActive(true);
        labelSpeed.SetActive(true);

        AllCoinsDisplay.text = num.ToString();
        AllCoinsDisplay2.text = num.ToString();
    }

    public void Update()
    {
        num = (int)PlayerPrefs.GetFloat("All coins");



        if(hCount < 5 && num >= 10)
        {
            labelHealth.SetActive(false);
        }
        else
        {
            labelHealth.SetActive(true);
        }



        if (sCount < 5 && num >= 10)
        {
            labelSpeed.SetActive(false);
        }
        else
        {
            labelSpeed.SetActive(true);
        }

        AllCoinsDisplay.text = num.ToString();
        AllCoinsDisplay2.text = num.ToString();
    }


    public void IncreaseHealth()
    {
        if (hCount < 5 && num >= 10)
        {
            health += 50;
            hCount++ ;

            num -= 10;
            PlayerPrefs.SetFloat("All coins", num);
            PlayerPrefs.Save();
        }
    
        else
        {
            
        }
    }

    public void IncreaseSpeed()
    {
        if (sCount < 5 && num >= 10)
        {
            speed += 50;
            sCount++ ;

            num -= 10;
            PlayerPrefs.SetFloat("All coins", num );
            PlayerPrefs.Save();
        }
        else
        {

        }
    }

    public void closeScreen()
    {
        PlayerPrefs.SetFloat("Health", health);
        PlayerPrefs.SetFloat("Speed", health);
        PlayerPrefs.SetFloat("All coins", num);
        PlayerPrefs.SetFloat("hCount", hCount);
        PlayerPrefs.SetFloat("sCount", sCount);
        PlayerPrefs.Save();
    }


}
