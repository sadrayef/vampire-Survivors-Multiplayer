using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField]
    private Transform enemyPrefab;
    private Transform enemyObjectTransform;

    
    private void Update()
    {

        
        if(!IsOwner)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            enemyObjectTransform =  Instantiate(enemyPrefab); 
            enemyObjectTransform.GetComponent<NetworkObject>().Spawn(true);
        }

        if(Input.GetKeyDown(KeyCode.Y)) 
        {
            Destroy(enemyObjectTransform.gameObject);
            enemyObjectTransform.GetComponent<NetworkObject>().Despawn(true);
        }
        Vector3 moveDir = new Vector3(0,0,0);

        if(Input.GetKeyUp(KeyCode.W))
        {
            moveDir.y = +1f;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            moveDir.y = -1f;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            moveDir.x = +1f;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            moveDir.x = -1f;
        }

        float moveSpeed = 3f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }












    /*[ServerRpc]
    //remember when you use server rpc it doesnt work on client it just runs on server.
    private void TestServerRpc(ServerRpcParams serverRpcParams)
    {
        Debug.Log("Test ServerRpc  " + OwnerClientId + "," + serverRpcParams.Receive.SenderClientId);
    }

    [ClientRpc]
    //we can only use this to send a message from server to client
    private void TestClientRpc(ClientRpcParams clientRpcParams)
    {
        Debug.Log("TestChilentRpc");
    }
    */
}
