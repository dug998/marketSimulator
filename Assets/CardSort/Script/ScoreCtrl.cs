using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCtrl : MonoBehaviour
{
    [SerializeField] Text _textScore;
    private void Awake()
    {
        EventGame.Game.OnUpdateScoreLevel += OnUpdateScoreLevel;
        EventGame.Game.OnStartLevel += OnStartLevel;
    }
    private void OnStartLevel()
    {
        OnUpdateScoreLevel();
    }
    private void OnUpdateScoreLevel()
    {
        _textScore.text = $"{LevelManager.Current.CurrentScore}/{LevelManager.LevelSetup.ScoreCardLimit}";
    }
}
