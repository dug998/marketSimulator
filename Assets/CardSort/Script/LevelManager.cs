using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    public List<LevelCtrl> LevelCtrlList;

    public static LevelCtrl Current => Instance._currentLevel;
    public static LevelSetup LevelSetup => Instance._currentLevel._levelSetup;

    private LevelCtrl _currentLevel;
    public void Init()
    {
        DestroyCurLevel();
        _currentLevel = Instantiate(LevelCtrlList[Data.CurLevel], transform);
        _currentLevel.Init();

    }
    public void DestroyCurLevel()
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel.gameObject);
        }
    }
    public void CheckIncreaseLevel()
    {
        if (Data.CurLevel < LevelCtrlList.Count - 1)
        {
            Data.CurLevel++;
        }
        else
        {
            Data.CurLevel = 0;
        }
    }
}
