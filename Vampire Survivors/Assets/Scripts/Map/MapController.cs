using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    public List<GameObject> terrainChunks;

    public GameObject player;
    public float checkerRadius;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    int counter;

    Vector3 heroLastPosition;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    public GameObject lastestchunk;
    public float maxOpDist; // greater than 20
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownDur;


    void Start()
    {
        heroLastPosition = player.transform.position;
    }

    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {
        //WE GET NULL POINTER EXEPTION IN HERE I GUESS . <SARA>
        if (!currentChunk)
        {
            return;
        }

        Vector3 moveDir = player.transform.position - heroLastPosition;
        heroLastPosition = player.transform.position;

        string dirName = GetDirectionName(moveDir);

        CheckAndSpawnChunk(dirName);

        if(dirName.Contains("Up"))
        {
            CheckAndSpawnChunk("Up");
        }

        if (dirName.Contains("Down"))
        {
            CheckAndSpawnChunk("Down");
        }

        if (dirName.Contains("Right"))
        {
            CheckAndSpawnChunk("Right");
        }

        if (dirName.Contains("Left"))
        {
            CheckAndSpawnChunk("Left");
        }
    }
    void CheckAndSpawnChunk(string dir)
    {
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find(dir).position, checkerRadius, terrainMask))
        {
            SpawnChunk(currentChunk.transform.Find(dir).position);
        }
    }

    string GetDirectionName(Vector3 dir)
    {
        dir = dir.normalized;
        if(Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if(dir.y > 0.5f)
            {
                return dir.x > 0 ? "Right Up" : "Left Up";
            }
            else if (dir.y < -0.5f)
            {
                return dir.x > 0 ? "Right Down" : " Left Down";
            }
            else
            {
                return dir.x > 0 ? "Right" : "Left";
            }
        }
        else
        {
            if (dir.x > 0.5f)
            {
                return dir.y > 0 ? "Right Up" : "Right Down";
            }
            else if (dir.x < -0.5f)
            {
                return dir.y > 0 ? "Left Up" : "Left Down";            }
            else
            {
                return dir.y > 0 ? "Up" : "Down";
            }

        }

    }

    void SpawnChunk(Vector3 spawnPosition)
    {
        int random = UnityEngine.Random.Range(0, terrainChunks.Count);
        lastestchunk = Instantiate(terrainChunks[random], spawnPosition, Quaternion.identity);
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