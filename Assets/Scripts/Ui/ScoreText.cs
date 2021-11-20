using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI score;

    public TextMeshProUGUI PlayerName => playerName;
    public TextMeshProUGUI Score => score;
}
