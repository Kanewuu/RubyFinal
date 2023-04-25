using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CogsUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cogsAmountText;
    public static CogsUI instance { get; private set; }

    void Awake()
    {
        instance = this;
    }

    public void SetValue(int cogs)
    {
        cogsAmountText.text = "x"+cogs.ToString();
    }
}
