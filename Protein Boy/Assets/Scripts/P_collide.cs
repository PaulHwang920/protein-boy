using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_collide : MonoBehaviour {

    public GameObject cam;
    public GameObject proteinUI;
    public GameObject protein;
    public static int proteins;
    public static int health = 3;
    private bool canTakeDamage = true;
    public AudioClip collectProtein;
    public AudioClip pain;

    // Update is called once per frame
    void Update () {
		if(health <= 0)
        {
            StartCoroutine(GameOver());
        }
	}

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "protein")
        {
            GetComponent<AudioSource>().PlayOneShot(collectProtein, volumeScale: 0.25f);
            proteins++;
            proteinUI.GetComponent<Text>().text = (proteins.ToString());

            Destroy(trig.gameObject);
        }
        if (trig.gameObject.tag == "food")
        {
            TakeDamage();
        }
        if (trig.gameObject.tag == "heart")
        {
            GainHeart();
            Destroy(trig.gameObject);
        }
        if (trig.gameObject.tag == "gapempty")
        {
            StartCoroutine(SpawnLotsFood());
        }
        if (trig.gameObject.tag == "invincibility")
        {
            StartCoroutine(InvincibilityToken());
            Destroy(trig.gameObject);
        }
    }

    void TakeDamage()
    {
        if (canTakeDamage == true)
        {
            iTween.ShakePosition(cam, new Vector3(0.2f, 0.2f, 0.2f), 1);
            GetComponent<AudioSource>().PlayOneShot(pain, volumeScale: 0.25f);
            health--;
            StartCoroutine(Invincibility());
        }  
    }

    void GainHeart()
    {
        if (health < 3)
        {
            health++;
        }
    }

    IEnumerator SpawnLotsFood()
    {
        yield return new WaitForSeconds(0.3f);
        //play some audio
        int explodeProteins = Random.Range(3, 6);
        for (int i = 0; i < explodeProteins; i++)
        {
            GameObject p = Instantiate (protein, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 3), Quaternion.identity) as GameObject;
            p.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Random.Range(5.0f, 10.0f), ForceMode2D.Impulse);
            int r = Random.Range(0, 2);
            if (r > 0)
            {
                p.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Random.Range(3.0f, 10.0f), ForceMode2D.Impulse);
            }
            else
            {
                p.GetComponent<Rigidbody2D>().AddForce(Vector2.left * Random.Range(3.0f, 10.0f), ForceMode2D.Impulse);
            }
        }
        yield return new WaitForSeconds(1);
    }

    IEnumerator GameOver ()
    {
        yield return new WaitForSeconds(0);
        Application.LoadLevel("main");
        yield return null;
    }

    IEnumerator InvincibilityToken()
    {
        canTakeDamage = false;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        gameObject.GetComponent<AudioSource>().pitch = 1.2f;
        yield return new WaitForSeconds(12);

        for (int i = 0; i < 10; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            yield return new WaitForSeconds(0.25f);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            yield return new WaitForSeconds(0.25f);
        }
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        gameObject.GetComponent<AudioSource>().pitch = 1.0f;
        canTakeDamage = true;
    }

    IEnumerator Invincibility()
    {
        canTakeDamage = false;
        for (int i = 0; i < 7; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            yield return new WaitForSeconds(0.10f);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            yield return new WaitForSeconds(0.10f);
        }
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        canTakeDamage = true;
    }

}
