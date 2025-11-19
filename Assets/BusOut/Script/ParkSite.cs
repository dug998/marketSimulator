using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParkSite : MonoBehaviour
{
    public TextMeshPro textNumberPassenger;

    private void OnEnable()
    {
        EventGame.Game.OnUpdateNumberPassenger += UpdateNumberPassenger;
    }
    public void UpdateNumberPassenger(int number)
    {
        textNumberPassenger.text = $"{number}";
    }
}
