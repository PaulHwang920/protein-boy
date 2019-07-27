using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public GameObject protein;
    public GameObject[] junks;
    public GameObject gapEmpty;
    public GameObject invincibilityToken;
    public GameObject heartToken;
    private float proteinTimer;
    private float junkTimer = 2.0f;
    private float gapFoodTimer = 1.0f;
    private float invincibilityTimer = 20.0f;
    private float heartTimer = 30.0f;

    private void Start()
    {
        proteinTimer = Random.Range(0.5f, 2.5f); ;
        junkTimer = Random.Range(0.5f, 2.5f); ;
        gapFoodTimer = Random.Range(20.0f, 30.0f);
        invincibilityTimer = Random.Range(30.0f, 60.0f);
        heartTimer = Random.Range(30.0f, 60.0f);
}

    // Update is called once per frame
    void Update () { 
        if(Game_Init.gameStarted == true)
        {
            proteinTimer -= Time.deltaTime;
            junkTimer -= Time.deltaTime;
            gapFoodTimer -= Time.deltaTime;
            invincibilityTimer -= Time.deltaTime;
            heartTimer -= Time.deltaTime;

            if (proteinTimer < 0)
            {
                SpawnProtein();
            } 
            if (junkTimer < 0)
            {
                SpawnJunk();
            }
            if (gapFoodTimer < 0)
            {
                StartCoroutine(SpawnGapFood());
            }
            if (invincibilityTimer < 0)
            {
                SpawnInvincibilityToken();
            }
            if (heartTimer < 0)
            {
                SpawnHeartToken();
            }
        }
        
    }

    void SpawnProtein ()
    {
        GameObject prot = Instantiate(protein, new Vector2(Random.Range(-8, 8), 6), Quaternion.identity) as GameObject;
        proteinTimer = Random.Range(0.5f, 2.5f);
    }

    void SpawnJunk()
    {
        GameObject jun = Instantiate(junks[(Random.Range(0, junks.Length))], new Vector2(Random.Range(-9, 9), 8), Quaternion.identity) as GameObject;
        if(P_collide.proteins < 30)
        {
            junkTimer = Random.Range(0.5f, 2.5f);
        }
        if (P_collide.proteins > 29 && P_collide.proteins < 60)
        {
            junkTimer = Random.Range(0.3f, 2.0f);
        }
        if (P_collide.proteins > 59 && P_collide.proteins < 90)
        {
            junkTimer = Random.Range(0.2f, 1.5f);
        }
        if (P_collide.proteins > 89 && P_collide.proteins < 120)
        {
            junkTimer = Random.Range(0.2f, 1.0f);
        }
        if (P_collide.proteins > 120)
        {
            junkTimer = Random.Range(0.1f, 0.8f);
        }
        jun.GetComponent<Rigidbody2D>().gravityScale = Random.Range(1, 3);
    }

    void SpawnInvincibilityToken ()
    {
        GameObject prot = Instantiate(invincibilityToken, new Vector2(Random.Range(-6, 6), 6), Quaternion.identity) as GameObject;
        invincibilityTimer = Random.Range(30.0f, 60.0f);
    }

    void SpawnHeartToken ()
    {
        Instantiate(heartToken, new Vector2(Random.Range(-6, 6), 6), Quaternion.identity);
        heartTimer = Random.Range(30.0f, 60.0f);
    }

    IEnumerator SpawnGapFood()
    {
        proteinTimer = 1000;
        junkTimer = 1000;
        gapFoodTimer = 1000;
        int random = Random.Range(1, 5);
        for (int i = 0; i < random; i++)
        {
            yield return new WaitForSeconds(1);
            int xPos = -8;
            int gapXPos = Random.Range(-7, 7);
            GameObject gapJunkGroup = new GameObject();
            gapJunkGroup.name = "Gap Junk Group";
            //put in audio
            for (int j = 0; j < 14; j++)
            {
                GameObject jun = Instantiate(junks[(Random.Range(0, junks.Length))], new Vector2(xPos, 5), Quaternion.identity) as GameObject;
                //add more audio
                Destroy(jun.GetComponent<Rigidbody2D>());
                if (xPos != gapXPos )
                {
                    xPos++;
                } else
                {
                    GameObject e = Instantiate(gapEmpty, new Vector2(xPos + 2, 5), Quaternion.identity) as GameObject;
                    xPos = xPos + 4;
                    e.gameObject.transform.parent = gapJunkGroup.gameObject.transform;
                }
                jun.gameObject.transform.parent = gapJunkGroup.gameObject.transform;
                yield return new WaitForSeconds(0.03f);
            }
            float randomTime = Random.Range(0.5f, 3.0f);
            iTween.ShakePosition(gapJunkGroup, new Vector2(0.2f, 0.2f), randomTime);
            yield return new WaitForSeconds(randomTime);
            gapJunkGroup.AddComponent<Rigidbody2D>();
            gapJunkGroup.GetComponent<Rigidbody2D>().gravityScale = 10;
            gapJunkGroup.gameObject.layer = 9;
            gapJunkGroup.gameObject.tag = "junk";
            gapJunkGroup.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            gapJunkGroup.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            gapJunkGroup.AddComponent<AudioSource>();
        }
        junkTimer = Random.Range(2.0f, 5.0f);
        proteinTimer = Random.Range(0.5f, 1.0f);
        gapFoodTimer = Random.Range(20.0f, 50.0f);
    }
}
