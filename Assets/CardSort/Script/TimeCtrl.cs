using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TimeCtrl : MonoBehaviour
{
    [SerializeField] Text _textTime;
    private Coroutine countDownCoroutine;
    private void Awake()
    {
        EventGame.Game.OnStartLevel += OnStartLevel;
    }
    private void OnStartLevel()
    {
        StopAllCoroutines();
        countDownCoroutine = StartCoroutine(ICountDown());
    }
    IEnumerator ICountDown()
    {
        float time = LevelManager.LevelSetup.timePlayLevel;
        UpdateTextTime(time);
        var wait = new WaitForSeconds(1);
        while (time > 0)
        {
            time -= 1;
            UpdateTextTime(time);
            yield return wait;
        }
        time = 0;
        UpdateTextTime(time);
        GameManager.Instance.LoseGame();
    }

    void UpdateTextTime(float t)
    {
        int minutes = Mathf.FloorToInt(t / 60);
        int seconds = Mathf.FloorToInt(t % 60);
        _textTime.text = $"{minutes:00}:{seconds:00}";
    }

}
