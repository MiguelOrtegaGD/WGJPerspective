using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    public void Fade(bool activate)
    {
        if (GameObject.Find("Fade"))
            GameObject.Find("Fade").GetComponent<Animator>().Play(activate ? "In" : "Out");
        else
        {
            GameObject fade = (GameObject)Instantiate(Resources.Load("Fade"), GameObject.FindGameObjectWithTag("MainCanvas").transform);
            fade.GetComponent<Animator>().Play(activate ? "In" : "Out");
        }
    }
}
