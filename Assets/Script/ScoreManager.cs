using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set;}
    public TMP_Text _scoreText;

    private int _currentScore;

    private void Awake()
    {
        Instance = this;
    }

    public void TargetHit()
    {
        _currentScore += 1;
        _scoreText.text = _currentScore.ToString();
    }
}
