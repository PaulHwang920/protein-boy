﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour {

    public float timeToDestroy;

	void Start () {
        if(timeToDestroy == 0)
        {
            timeToDestroy = 10;
        }
        StartCoroutine(DestroyThis());
	}

    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);

    }

}
