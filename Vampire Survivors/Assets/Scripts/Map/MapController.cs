using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    public List<GameObject> terrainChunks;

    public GameObject player;
    public float checkerRadius;
    public Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    public GameObject currentChunk;

    

    HeroMovement heroMove;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    public GameObject lastestchunk;
    public float maxOpDist; // greater than 20
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownDur;


    void Start()
    {
        heroMove = FindObjectOfType<HeroMovement>();
    }

    void Update()
    {
        ChunkChecker();
        ChunkChecker();
       
        ChunkOptimizer();

    
        
        
    }

    void ChunkChecker()
    {
        
        if (!currentChunk)
        {
            return;
        }
        
      

        if (heroMove.moveDir.x > 0 && heroMove.moveDir.y == 0)
            {
                if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkerRadius, terrainMask))
                {

                    noTerrainPosition = currentChunk.transform.Find("Right").position; //Right
                    SpawnChunk();
                    //noTerrainPosition = currentChunk.transform.Find("Right Up").position; //Right up
                    //SpawnChunk();
                    //noTerrainPosition = currentChunk.transform.Find("Right Down").position; //Right down
                    //SpawnChunk();

                }
            }
            else if (heroMove.moveDir.x < 0 && heroMove.moveDir.y == 0)
            {
                if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkerRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Left").position; //Left
                    SpawnChunk();
                    //noTerrainPosition = currentChunk.transform.Find("Left Down").position; //Left down
                    //SpawnChunk();
                    //noTerrainPosition = currentChunk.transform.Find("Left Up").position; //Left up
                    //SpawnChunk();
                }
            }
            else if (heroMove.moveDir.y > 0 && heroMove.moveDir.x == 0)
            {
                if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkerRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Up").position; //Up
                    SpawnChunk();
                    //noTerrainPosition = currentChunk.transform.Find("Left Up").position; //Left up
                    //SpawnChunk();
                    //noTerrainPosition = currentChunk.transform.Find("Right Up").position; //Right up
                    //SpawnChunk();
                }
            }
            else if (heroMove.moveDir.y < 0 && heroMove.moveDir.x == 0)
            {
                if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkerRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Down").position; //Down
                    SpawnChunk();
                    //noTerrainPosition = currentChunk.transform.Find("Left Down").position; //Left down
                    //SpawnChunk();
                    //noTerrainPosition = currentChunk.transform.Find("Right Down").position; //Right down
                    //SpawnChunk();
                }
            }
            else if (heroMove.moveDir.x > 0 && heroMove.moveDir.y > 0)
            {
                if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Up").position, checkerRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Right Up").position; //Right up
                    SpawnChunk();
                    //noTerrainPosition = currentChunk.transform.Find("Up").position; //Up
                    //SpawnChunk();
                    //noTerrainPosition = currentChunk.transform.Find("Right").position; //Right
                    //SpawnChunk();
                }
            }
            else if (heroMove.moveDir.x > 0 && heroMove.moveDir.y < 0)
            {
                if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Down").position, checkerRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Right Down").position; //Right down
                    SpawnChunk();
                    //noTerrainPosition = currentChunk.transform.Find("Right").position; //Right
                   // SpawnChunk();
                    //noTerrainPosition = currentChunk.transform.Find("Down").position; //Down
                    //SpawnChunk();
                }
            }
            else if (heroMove.moveDir.x < 0 && heroMove.moveDir.y > 0)
            {
                if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Up").position, checkerRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Left Up").position; //Left up
                    SpawnChunk();
                    //noTerrainPosition = currentChunk.transform.Find("Up").position; //Up
                    //SpawnChunk();
                    //noTerrainPosition = currentChunk.transform.Find("Left").position; //Left
                    //SpawnChunk();
                }
            }
            else if (heroMove.moveDir.x < 0 && heroMove.moveDir.y < 0)
            {
                if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Down").position, checkerRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Left Down").position; //Left down
                    SpawnChunk();
                    //noTerrainPosition = currentChunk.transform.Find("Left").position; //Left
                    //SpawnChunk();
                    //noTerrainPosition = currentChunk.transform.Find("Down").position; //Down
                    //SpawnChunk();
                }


            }
        }

    void SpawnChunk()
    {
        int random = UnityEngine.Random.Range(0, terrainChunks.Count);
        lastestchunk = Instantiate(terrainChunks[random], noTerrainPosition, Quaternion.identity);
        spawnedChunks.Add(lastestchunk);
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0) 
        {
            optimizerCooldown = optimizerCooldownDur;

        }
        else
        {
            return;
        }


        foreach (GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if(opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}