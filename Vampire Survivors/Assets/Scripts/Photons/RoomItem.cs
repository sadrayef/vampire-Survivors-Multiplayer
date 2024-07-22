using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public Text roomName2;
    LobbyMnager manager;

    private void Start()
    {
        manager = FindObjectOfType<LobbyMnager>();
    }


    public void SetRoomName(string _roomName)
    {
        roomName2.text = _roomName;
    }

    public void OnClickItem()
    {
        manager.JoinRoom(roomName2.text);
    }

}
