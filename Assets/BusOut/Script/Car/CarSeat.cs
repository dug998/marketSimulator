using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bus
{
    public class CarSeat : MonoBehaviour
    {
        public List<Transform> _seats = new List<Transform>();
        public Transform GetSeat(int index)
        {
            return _seats[index % _seats.Count];
        }
    }
}
