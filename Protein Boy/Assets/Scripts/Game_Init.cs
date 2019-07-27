using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Init : MonoBehaviour {

    public static bool gameStarted;
    public bool canStartGame = false;
    public GameObject cam;

    public GameObject hearts;
    public GameObject mainLogo;
    public GameObject startMessage;

    void Awake() {
        Application.targetFrameRate = 60;
        P_collide.health = 3;
        P_collide.proteins = 0;
        gameStarted = false;
    }

    private void Start()
    {
        startMessage.SetActive(false);
        mainLogo.SetActive(true);
        hearts.SetActive(false);
    }

    void Update() {
        if (Input.GetButton ("Horizontal"))
        {
            gameStarted = true;
        }
        if (gameStarted == true)
        {
            startMessage.SetActive(false);
            cam.gameObject.transform.position = Vector3.Lerp(cam.gameObject.transform.position, new Vector3(0, 0, -10), 2 * Time.deltaTime);
            cam.GetComponent<Camera>().orthographicSize = Mathf.Lerp(cam.GetComponent<Camera>().orthographicSize, 5, 5 * Time.deltaTime);
            StartCoroutine(StartGame());
        }

        //IOS
        //if (Input.touchCount > 0)
        //{
        //    Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        //    Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
        //    if (gameStarted == false && mainLogo.GetComponent<Collider2D>() == Physics2D.OverlapPoint (touchPos))
        //    {
        //        StartCoroutine(StartGame());
        //    }
        //}

        //PC
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    Vector2 mousePos = new Vector2(worldPos.x, worldPos.y);
        //    if (gameStarted == false && mainLogo.GetComponent<Collider2D>() == Physics2D.OverlapPoint(mousePos))
        //    {
        //        StartCoroutine(StartGame());
        //    }
        //}
    }

    IEnumerator StartGame()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            cam.gameObject.transform.position = Vector3.Lerp(cam.gameObject.transform.position, new Vector3 (2f, -2f, -10), 0.1f);
            cam.GetComponent<Camera>().orthographicSize = Mathf.Lerp (cam.GetComponent<Camera>().orthographicSize, 2, 0.2f);
            // startMessage.SetActive(true);
            // mainLogo.transform.position = Vector2.Lerp(mainLogo.transform.position, new Vector2(0, 15), 0.1f);
            yield return new WaitForSeconds(0.02f);
        }
        // Destroy(mainLogo);
        hearts.SetActive(true);
        canStartGame = true;
    }
}
