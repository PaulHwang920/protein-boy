using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protein : MonoBehaviour {

    private bool canRotate = false;

    void Update() {
        if (canRotate == true)
        {
            transform.Rotate(Vector2.up * 100 * Time.deltaTime);
        }
    }
		
    void OnCollisionEnter2D(Collision2D col)
    {
        canRotate = true;
    }
}
