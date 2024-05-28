using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void SceneChange(string name){
        SceneManager.LoadScene(name); 
        Time.timeScale = 1f;
    }


}
