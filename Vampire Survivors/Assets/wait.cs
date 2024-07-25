using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class wait : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(waiting());
    }

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(20);
        SceneManager.LoadScene("Title Screen");
    }

   
}
