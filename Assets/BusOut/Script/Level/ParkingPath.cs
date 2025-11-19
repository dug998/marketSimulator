using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParkingPath : MonoBehaviour
{
    [SerializeField] GameObject objTop;
    [SerializeField] GameObject objBottom;
    [SerializeField] GameObject objLeft;
    [SerializeField] GameObject objRight;

    Vector3 pointLT, pointRT, pointLB, pointRB;
    private void Start()
    {
        var boxColliderTop = objTop.GetComponent<BoxCollider>();
        var center = boxColliderTop.center;
        var size = boxColliderTop.size;
        pointLT = ConvertToWorldSpace(objTop, new Vector3(center.x - size.x / 2, 0, center.z));
        pointRT = ConvertToWorldSpace(objTop, new Vector3(center.x + size.x / 2, 0, center.z));

        var boxColliderBottom = objBottom.GetComponent<BoxCollider>();
        center = boxColliderBottom.center;
        size = boxColliderBottom.size;
        pointLB = ConvertToWorldSpace(objBottom, new Vector3(center.x - size.x / 2, 0, center.z));
        pointRB = ConvertToWorldSpace(objBottom, new Vector3(center.x + size.x / 2, 0, center.z));

    }
    public List<Vector3> GenerateParkingExitPath(Vector3 direction, GameObject collision, Vector3 collisionPosition, Vector3 targetPosition)
    {
        var res = new List<Vector3>();
        if (collision == objBottom)
        {
            var zBottom = pointLB.z;
            res.Add(new Vector3(collisionPosition.x, 0, zBottom));
            if (direction.x >= 0)
            {
                res.Add(pointRB);
                res.Add(pointRT);
            }
            else
            {
                res.Add(pointLB);
                res.Add(pointLT);
            }
        }
        else if (collision == objRight)
        {
            var xRight = pointRB.x;
            res.Add(new Vector3(xRight,0, collisionPosition.z));
            res.Add(pointRT);
        }
        else if (collision == objLeft)
        {
            var xLeft = pointLB.x;
            res.Add(new Vector3(xLeft, 0, collisionPosition.z));
            res.Add(pointLT);
        }
        else if (collision == objTop)
        {
            res.Add(new Vector3(collisionPosition.x, 0, GetZTop()));
        }
        res.Add(new Vector3(targetPosition.x, 0, GetZTop()));

        res.Add(targetPosition);
        return res;
    }
    private Vector3 ConvertToWorldSpace(GameObject obj, Vector3 point)
    {
        var pos = obj.transform.TransformPoint(point);
        return pos;
    }

    public float GetZTop()
    {
        return pointLT.z;
    }
}
