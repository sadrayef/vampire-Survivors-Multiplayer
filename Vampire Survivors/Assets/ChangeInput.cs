using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeInput : MonoBehaviour
{
    EventSystem _system;

    private void Start()
    {
        _system = EventSystem.current;
    }

    private void Update()
    {
        //not usable really 
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Selectable next = _system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if(next != null)
            {
                next.Select();
            }
        }
    }
}
