    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class P_move : MonoBehaviour {

    private int xPos;
    private float time = 0.1f;
    private bool isTouch = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("clicked");
        }
            //IOS
            if (isTouch == true)
        {
            if (Input.touchCount > 0 && Game_Init.gameStarted == true)
            {
                GetComponent<Animator>().SetBool("isMoving", true);
                if (Input.GetTouch(0).position.x > Screen.width / 2 && time > 0.075f && xPos < 8)
                {
                    xPos++;
                    time = 0.0f;
                }
                if (Input.GetTouch(0).position.x < Screen.width / 2 && time > 0.075f && xPos > -8)
                {
                    xPos--;
                    time = 0.0f;
                }
                time += Time.deltaTime;
            }
        } else
        {
            //KEYBOARD
            if (Input.GetButton ("Horizontal") && Game_Init.gameStarted == true)
            {
                GetComponent<Animator>().SetBool("isMoving", true);
                if (Input.GetAxis("Horizontal") > 0 && time > 0.075f && xPos < 8)
                {
                    xPos++;
                    time = 0.0f;
                }
                if (Input.GetAxis("Horizontal") < 0 && time > 0.075f && xPos > -8)
                {
                    xPos--;
                    time = 0.0f;
                }
                time += Time.deltaTime;
            }
        }
        MovePlayer();
    }

    void MovePlayer ()
    {
        if (Input.GetButton("Horizontal") == false && isTouch == false)
        {
            GetComponent<Animator>().SetBool("isMoving", false);
        }
        if (Input.touchCount < 1 && isTouch == true)
        {
            GetComponent<Animator>().SetBool("isMoving", false);
        }
        Vector2 playerPos = gameObject.transform.position;
        playerPos.x = xPos;
        gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, playerPos, 10 * Time.deltaTime);
    }
    }
