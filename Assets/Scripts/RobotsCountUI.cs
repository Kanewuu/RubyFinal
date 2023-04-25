using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RobotsCountUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fixedRobotsText;
    public static RobotsCountUI instance { get; private set; }

    int fixedRobotsCount = 0;

    void Awake()
    {
        instance = this;
    }

    public void SetValue(int robots)
    {
        fixedRobotsCount+=robots;
        fixedRobotsText.text = "Robots Fixed: "+ fixedRobotsCount.ToString() + "/5";
    }
}
