using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoSingleton<LevelLoader>
{
    public List<TextAsset> jsonFiles;

    public LevelData currentLevelData;
    public LevelData LoadLevel(int lv)
    {
       return currentLevelData = JsonUtility.FromJson<LevelData>(jsonFiles[lv % jsonFiles.Count].text);
    }
    //void Start()
    //{
    //    LevelData data = JsonUtility.FromJson<LevelData>(jsonFile.text);

    //    Debug.Log("Load Level: " + data.Level);
    //    Debug.Log("Car count: " + data.ParkingPlotData.Cars.Length);
    //    Debug.Log("Queue count: " + data.QueuePassengerData.QueuePassenger.Count);
    //}
}
