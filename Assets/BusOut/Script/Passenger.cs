using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger : MonoBehaviour
{
    public int idColor;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    public void Init(int numberColor)
    {
        idColor = numberColor;
        skinnedMeshRenderer.material = LevelSetup.Instance.GetMaterialByIdColor(idColor);
    }
    public void MoveToCar(CarController car)
    {
        Destroy(gameObject);
    }
    public void Run()
    {

    }
}
