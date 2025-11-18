[System.Serializable]
public class LevelData
{
    public int Level;
    public ParkingPlotData ParkingPlotData;
    public QueuePassengerData QueuePassengerData;
    public float ScaleFactor;
}

[System.Serializable]
public class ParkingPlotData
{
    public CarData[] Cars;
}

[System.Serializable]
public class CarData
{
    public int Color;
    public int Size;
    public int Direction;
    public PositionData Position;
}

[System.Serializable]
public class PositionData
{
    public float x;
    public float y;
    public NormalizedData normalized;
    public float magnitude;
    public float sqrMagnitude;
}

[System.Serializable]
public class NormalizedData
{
    public float x;
    public float y;
    public float magnitude;
    public float sqrMagnitude;
}

[System.Serializable]
public class QueuePassengerData
{
    public QueuePassenger QueuePassenger;
}

[System.Serializable]
public class QueuePassenger
{
    public PassengerItem[] List;
    public bool IsEmpty;
    public int Count;
}

[System.Serializable]
public class PassengerItem
{
    public int color;
    public int num;
}
