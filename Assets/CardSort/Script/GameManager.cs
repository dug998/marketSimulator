using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private void OnEnable()
    {
        EventGame.UI.OnNextLevel += OnNextLevel;
        EventGame.UI.OnReplayLevel += OnReplayLevel;
    }
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        Init();
    }
    public void Init()
    {
        OnPlayGame();
    }
    public void OnPlayGame()
    {
        LevelManager.Instance.Init();
        EventGame.Game.OnStartLevel?.Invoke();
    }
    public void WinGame()
    {
        Debug.Log("win game! ");

        OnNextLevel();
    }
    public void LoseGame()
    {
        Debug.Log("Lose Game !");
        OnReplayLevel();
    }
    public void OnNextLevel()
    {
        LevelManager.Instance.CheckIncreaseLevel();
        OnPlayGame();
    }
    public void OnReplayLevel()
    {
        OnPlayGame();
    }

}
