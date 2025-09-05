using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Object = UnityEngine.Object;

public class GameUtils
{
    private static readonly List<int> ALPHA_CHAR_CODES = new List<int>()
        { 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 65, 66, 67, 68, 69, 70 };

    public static bool IsAndroid()
    {
        return Application.platform == RuntimePlatform.Android;
    }

    public static bool IsIOS()
    {
        return Application.platform == RuntimePlatform.IPhonePlayer;
    }

    public static bool IsWeb()
    {
        return Application.platform == RuntimePlatform.WebGLPlayer;
    }

    public static bool IsEditor()
    {
        return Application.platform == RuntimePlatform.OSXEditor ||
               Application.platform == RuntimePlatform.WindowsEditor;
    }

    public static string GetPlatform()
    {
#if UNITY_IOS
        return "iOS";
#elif UNITY_ANDROID
        return "Android";
#endif
        return "other";
    }

    public static string SureGetPlatform()
    {
#if UNITY_IOS
        return "iOS";
#elif UNITY_ANDROID
        return "Android";
#endif
        return "Android";
    }

    public static string GetDeviceId()
    {
        if (!IsWeb()) return SystemInfo.deviceUniqueIdentifier;
        //var s = LocalStorageUtils.GetDeviceID();
        //if (string.IsNullOrEmpty(s))
        //{
        //    s = IsWeb() ? CreateID() : SystemInfo.deviceUniqueIdentifier;
        //    LocalStorageUtils.SetDeviceID(s);
        //}
        var s = "";
        SDLogger.Log("device idNode: " + s);
        return s;
    }

    public static string GetVersion()
    {
        return Application.version;
    }

    public static string GetBundleId()
    {
        return Application.identifier;
    }

    public static string CreateID()
    {
        var uid = new int[36];
        var index = 0;
        var rand = new System.Random();
        for (var i = 0; i < 8; i++)
        {
            uid[index++] = ALPHA_CHAR_CODES[rand.Next(0, 15)];
        }

        for (var i = 0; i < 3; i++)
        {
            uid[index++] = 45; // charCode for "-"
            for (var j = 0; j < 4; j++)
            {
                uid[index++] = ALPHA_CHAR_CODES[rand.Next(0, 15)];
            }
        }

        uid[index++] = 45; // charCode for "-"
        //        var time:Number = new Date().getTime();
        var time = DateTime.Now.Millisecond;
        // Note: time is the number of milliseconds since 1970,
        // which is currently more than one trillion.
        // We use the low 8 hex digits of this number in the UID.
        // Just in case the system clock has been reset to
        // Jan 1-4, 1970 (in which case this number could have only
        // 1-7 hex digits), we pad on the left with 7 zeros
        // before taking the low digits.
        var timeString = ("0000000" + time.ToString("X16")).ToUpper().Substring(0, 8);
        for (var i = 0; i < 8; i++)
        {
            uid[index++] = timeString[i];
        }

        for (var i = 0; i < 4; i++)
        {
            uid[index++] = ALPHA_CHAR_CODES[rand.Next(0, 15)];
        }

        var res = uid.Aggregate("", (current, c) => current + Convert.ToChar(c));
        return res;
    }

    public static int[] ParseRoomName(string roomName)
    {
        var arr = roomName.Split('_');
        var parse = new int[arr.Length];
        for (var i = 0; i < arr.Length; i++)
        {
            parse[i] = int.Parse(arr[i]);
        }

        return parse;
    }

    public static Texture2D TakeScreenShot()
    {
        var ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();
        return ss;
    }

    public static List<T> GetAssetList<T>(string path) where T : class
    {
#if UNITY_EDITOR
        List<FileInfo> fileInfos = new List<FileInfo>();
        EnumerateFiles(Application.dataPath + "/" + path, fileInfos);
        string[] fileEntries = fileInfos.Select(s => s.FullName).ToArray();

        return fileEntries.Select(fileName =>
            {
                string temp = fileName.Replace("\\", "/");
                int index = temp.IndexOf("Assets", StringComparison.Ordinal);
                string localPath = temp.Substring(index);

                //SDLogger.Log(localPath);
                return AssetDatabase.LoadAssetAtPath(localPath, typeof(T));
            })
            //Filtering null values, the Where statement does not work for all types T
            .OfType<T>() //.Where(asset => asset != null)
            .ToList();
#endif
        return new List<T>();
    }

    internal static void EnumerateFiles(string sFullPath, List<FileInfo> fileInfoList)
    {
        try
        {
            DirectoryInfo di = new DirectoryInfo(sFullPath);
            FileInfo[] files = di.GetFiles();

            foreach (FileInfo file in files)
                fileInfoList.Add(file);

            //Scan recursively
            DirectoryInfo[] dirs = di.GetDirectories();
            if (dirs.Length < 1)
                return;
            foreach (DirectoryInfo dir in dirs)
                EnumerateFiles(dir.FullName, fileInfoList);
        }
        catch (Exception ex)
        {
            SDLogger.LogError("Exception in Helper.EnumerateFiles" + ex.Message);
        }
    }

    public static List<FileInfo> GetAllFiles(DirectoryInfo info)
    {
        var temps = info.GetDirectories();
        var ls = new List<FileInfo>();
        ls.AddRange(info.GetFiles());
        foreach (var temp in temps)
        {
            ls.AddRange(GetAllFiles(temp));
        }

        return ls;
    }


    private static System.Random random = new System.Random();

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static int GetMaxIndexHasValue(int[] arr, int targetValue)
    {
        for (int i = arr.Length - 1; i >= 0; i--)
        {
            if (arr[i] >= targetValue)
            {
                return i;
            }
        }

        return -1;
    }

    public static string ConvertTimeSpanStr(TimeSpan timeSpan)
    {
        return $"{timeSpan.Days}D {timeSpan.Hours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
    }


    public static string GetSecondStr(int seconds)
    {
        var hours = seconds / 3600;
        var minutes = seconds / 60;
        var sec = seconds % 60;
        if (hours > 0)
        {
            return hours + " giờ " + (minutes % 60) + " phút";
        }

        if (minutes > 0 && sec > 0)
        {
            return minutes + " phút " + sec + " giây";
        }

        if (minutes > 0)
        {
            return minutes + " phút";
        }

        return sec + " giây";
    }

    public static int[] SecondsToArray(int seconds)
    {
        int[] tmp = new int[3]
        {
            seconds / 3600,
            seconds / 60 - (seconds / 3600) * 60,
            seconds % 60
        };
        return tmp;
    }

    public static string GetMin_Second(int seconds)
    {
        var hours = seconds / 3600;
        var minutes = (seconds % 3600) / 60;
        var sec = seconds % 60;

        if (hours > 0)
        {
            return $"{hours}:{minutes:D2}:{sec:D2}";
        }

        return minutes > 0 ? $"{minutes}:{sec:D2}" : $"{sec:D2}s";
    }
    public static string GetMin_Second_HH_MM(int seconds)
    {
        var hours = seconds / 3600;
        var minutes = (seconds % 3600) / 60;
        var sec = seconds % 60;

        if (hours > 0)
        {
            return $"{hours}h:{minutes:D2}m";
        }

        return $"{minutes}m:{sec:D2}s";
    }
    public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp);
        return dateTime;
    }

    public static long DateTimeToTimeStamp(DateTime dateTime)
    {
        var span = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        return (long)span.TotalSeconds;
    }

    public static string SecondsToDay(long amount)
    {
        var days = amount / 86400;
        var hours = amount / 3600;
        var minutes = amount / 60;
        var sec = amount % 60;

        return $"{days}d:{hours % 24}h:{minutes % 60}m:{sec}s";
    }

    private static DateTime Next(DateTime from, DayOfWeek dayOfWeek)
    {
        int start = (int)from.DayOfWeek;
        int target = (int)dayOfWeek;
        if (target <= start) target += 7;
        return from.AddDays(target - start);
    }

    public static TimeSpan TimeToTheDayInWeek(DateTime from, DayOfWeek dayOfWeek)
    {
        TimeSpan span = (Next(from, dayOfWeek).Date + new TimeSpan(23, 59, 00)).Subtract(DateTime.Now);
        return span;
    }

    /// <summary>
    /// Thời gian cuối cùng của tuần: 23h59p59s
    /// </summary>
    /// <returns></returns>
    public static DateTime TimeEndWeek()
    {
        return DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 8).AddMinutes(-1).AddSeconds(59);
    }

    public static int GetWeekOfYear(DateTime dateTime)
    {
        var d = dateTime;
        var cul = CultureInfo.CurrentCulture;

        int weekNum = cul.Calendar.GetWeekOfYear(
            d,
            CalendarWeekRule.FirstDay,
            DayOfWeek.Monday);

        return weekNum - 1;
    }

    public static Quaternion ToRotationY(Vector3 p1, Vector3 p2)
    {
        var dir = p1 - p2;
        var ro = Quaternion.LookRotation(dir);
        ro.x = 0;
        ro.z = 0;
        return ro;
    }

    public static Quaternion ToRotationZ(Vector3 p1, Vector3 p2)
    {
        var dir = p1 - p2;
        var ro = Quaternion.LookRotation(dir);
        ro.x = 0;
        ro.y = 0;
        return ro;
    }

    public static void SaveAssets(Object target)
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(target);
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }

    public static float GetStartVo(Vector3 startPos, Vector3 endPos, float g, float maxH, float time)
    {
        var d = Vector3.Distance(startPos, endPos);
        var vx = d / time;
        var t2 = time / 2;
        var vy = (maxH + 1 / 2f * g * t2 * t2) / t2;
        return Mathf.Sqrt(vx * vx + vy * vy);
    }

    public static string GetAssetPath(Object o)
    {
#if UNITY_EDITOR
        return AssetDatabase.GetAssetPath(o);
#endif
        return "";
    }


    public static string GetGUID(Object o)
    {
#if UNITY_EDITOR
        return AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(o));
#endif
        return "";
    }

    public static Vector2 GetIntersectionPointCoordinates(Vector2 p1, Vector2 p2, Vector2 v1, Vector2 v2, out bool found)
    {
        float tmp = (v2.x - v1.x) * (p2.y - p1.y) - (v2.y - v1.y) * (p2.x - p1.x);

        if (tmp == 0)
        {
            // No solution!
            found = false;
            return Vector2.zero;
        }

        float mu = ((p1.x - v1.x) * (p2.y - p1.y) - (p1.y - v1.y) * (p2.x - p1.x)) / tmp;

        found = true;

        return new Vector2(
            v1.x + (v2.x - v1.x) * mu,
            v1.y + (v2.y - v1.y) * mu
        );
    }

}