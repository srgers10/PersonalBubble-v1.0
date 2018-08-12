using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager manager;
    public GameObject blobPrefab;
    public GameObject minePrefab;
    public GameObject boostPrefab;
    public GameObject playerPrefab;
    public GameObject popVFX;

    public AudioSource popSound;
    public AudioSource eatSound;
    public Transform[] spawnPoint;
    public bool isTimed;
    public bool noRespawn;

    private PlayerController player;
    public float roundAmount;

    int numBlobs;
    public List<Blob> blobs;

    public float range = 49;
    public int bubbleCount = 100;
    public int foodCount;
    public int mineCount;
    public int powerCount;

    private void Awake()
    {
        player = PlayerController.reff;
        blobs = new List<Blob>();
        manager = this;
        for (int i = 0; i < bubbleCount; i++)
        {
            CreateBlob();
        }
        for (int i = 0; i < foodCount; i++)
        {
            CreateFood(5, RandomPos(range), 5);
        }
        for (int i = 0; i < mineCount; i++)
        {
            CreateMine();
        }
        for (int i = 0; i < powerCount; i++)
        {
            CreateBoost();
        }
        FoodSpawn();
    }

    IEnumerator FoodSpawn()
    {
        CreateFood(3, RandomPos(range), 15);
        yield return new WaitForSeconds(2f);
        FoodSpawn();
    }


    public void CreateBlob()
    {
        numBlobs++;
        
        Vector3 spawnPos = RandomPos(range);
        Blob blob = Instantiate(blobPrefab, spawnPos, Quaternion.identity, this.transform).GetComponent<Blob>();
        blob.Resize(Random.Range(.5f, 1f));
        blob.name = "B-Bot #" + numBlobs;

        blobs.Add(blob);
    }

    public Vector3 RandomPos(float range)
    {

        float x = Random.Range(-range, range);
        float y = Random.Range(0, 40);
        float z = Random.Range(-range, range);

        if (player != null)
            return new Vector3(x, y, z) + new Vector3(player.transform.position.x ,0,player.transform.position.z);
        else
            return new Vector3(x, y, z);
    }

    public void Restart()
    {
        int i = Random.Range(0, spawnPoint.Length);
        GameObject go = Instantiate(playerPrefab, spawnPoint[i].position, spawnPoint[i].rotation);
        UIManager.ui.SetPlayerScore(0);
        UIManager.ui.cam.GetComponent<CopyTransform>().target = go.GetComponentInChildren<Camera>().transform;
    }
    public void CreateFood(int num, Vector3 area, float spread)
    {
        for (int i = 0; i < num; i++)
        {
            Vector3 offset = RandomPos(spread);
            Vector3 spawnPos = area + offset;
            Blob food = Instantiate(blobPrefab, spawnPos, Quaternion.identity, this.transform).GetComponent<Blob>();
            food.Resize(.3f);
            food.name = "Food ";
            food.isFood = true;
        }
    }
    
    public void RemoveBlob(Blob b)
    {
        GameObject p = Instantiate(popVFX, b.transform.position, Quaternion.identity);
        blobs.Remove(b);
        Destroy(b.gameObject);
        Destroy(p, .5f);
        //UIManager.ui.SetPlayerCount(blobs.Count);
        CreateBlob();
 
    }
    public void PlayPop()
    {
        popSound.Play();
    }

    void CreateMine()
    {
        GameObject go = Instantiate(minePrefab, RandomPos(range), Quaternion.identity);
        go.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
    void CreateBoost()
    {
        GameObject go = Instantiate(boostPrefab, RandomPos(range), Quaternion.identity);
        
    }
}
