using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseManager : MonoBehaviour
{
    public static bool pause = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (pause == false))
        {
            modePause();
            Debug.Log("Pausa");
        }

        if (Input.GetKeyDown(KeyCode.Escape) && (pause == true))
        {
            modePlay();
            Debug.Log("Continue");
        }
    }
    public void modePause()
    {
        pause = true;
        Debug.Log($"{gameObject.transform.GetChild(0).gameObject}");
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);

        Time.timeScale = 0;
    }
    public void modePlay()
    {
        pause = false;
        Debug.Log($"{gameObject.transform.GetChild(0).gameObject}");
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);

        Time.timeScale = 1;
    }
}
