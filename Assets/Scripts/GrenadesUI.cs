using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrenadesUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI grenadeAmountText;
    public static GrenadesUI instance { get; private set; }

    void Awake()
    {
        instance = this;
    }

    public void SetValue(int grenades)
    {
        grenadeAmountText.text = "x" + grenades.ToString();
    }
}
