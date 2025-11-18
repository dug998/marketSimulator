using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayableArea : MonoBehaviour
{
    public PlacablePoint[] PlacablePoints => _placablePoints;

    [SerializeField] private PlacablePoint[] _placablePoints;

}
