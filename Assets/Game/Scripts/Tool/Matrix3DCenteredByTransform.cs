using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
public class Matrix3DCenteredByTransform : MonoBehaviour
{
    public GameObject prefab;          // Prefab cần sinh
    public Transform centerTransform;  // Vị trí trung tâm
    public int sizeX = 3;
    public int sizeY = 3;
    public int sizeZ = 3;
    [Header("Spacing (khoảng cách giữa các đối tượng)")]
    public float spacingX = 2f;
    public float spacingY = 2f;
    public float spacingZ = 2f;


    [Button]
    void GenerateMatrix3D()
    {
        if (centerTransform == null || prefab == null)
        {
            Debug.LogWarning("Chưa gán centerTransform hoặc prefab!");
            return;
        }

        // Tính offset để khối nằm giữa
        Vector3 centerOffset = new Vector3(
            (sizeX - 1) * spacingX / 2f,
            (sizeY - 1) * spacingY / 2f,
            (sizeZ - 1) * spacingZ / 2f
        );
        for (int i = centerTransform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(centerTransform.GetChild(i).gameObject);
        }

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                for (int z = 0; z < sizeZ; z++)
                {
                    // Vị trí từng ô
                    Vector3 localPos = new Vector3(x * spacingX, y * spacingY, z * spacingZ) - centerOffset;
                    Vector3 worldPos = centerTransform.position + localPos;

                    var obj = Instantiate(prefab, worldPos, Quaternion.identity, transform);
                    obj.transform.SetParent(centerTransform);
                    obj.name = $"Item_{x}_{y}_{z}";
                }
            }
        }
    }
}
#endif