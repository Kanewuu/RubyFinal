using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooseUI : MonoBehaviour
{
    [SerializeField] GameObject looseUI;
    public static LooseUI instance { get; private set; }

    void Awake()
    {
        instance = this;
        looseUI.SetActive(false);
    }

    public void ShowLoose()
    {
        looseUI.SetActive(true);
    }
}
