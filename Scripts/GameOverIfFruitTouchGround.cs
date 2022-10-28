using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverIfFruitTouchGround : MonoBehaviour
{
    public int maxNumberOfError = 3;

    private int currentNumberOfError = 0;

    private void OnCollisionEnter(Collision collision)
    {
        // if layer == Sliceable then
        if (collision.gameObject.layer == 6)
        {
            currentNumberOfError++;
            if (currentNumberOfError == maxNumberOfError)
            {
                SceneManager.LoadScene(0);
            }
            Destroy(collision.gameObject);
        }
    }
}
