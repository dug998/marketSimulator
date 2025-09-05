using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


public static class GameExtension
{
    public static T Cast<T>(this object o) where T : class
    {
        if (o is T v)
            return v;
        return null;
    }

    public static void Hide(this GameObject go)
    {
        go.SetActive(false);
    }

    public static void Show(this GameObject go)
    {
        go.SetActive(true);
    }

    public static void Hide(this MonoBehaviour go)
    {
        go.gameObject.SetActive(false);
    }

    public static void Show(this MonoBehaviour go)
    {
        go.gameObject.SetActive(true);
    }

    public static void Hide(this Component go)
    {
        go.gameObject.SetActive(false);
    }

    public static void Show(this Component go)
    {
        go.gameObject.SetActive(true);
    }
    public static void SetActive(this Component go, bool isActive)
    {
        go.gameObject.SetActive(isActive);
    }
    public static void Reset(this Transform t, Transform parent = null)
    {
        t.SetParent(parent);
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
    }

    public static void SetAnchor(this Image img, Vector2 anchor)
    {
        img.GetComponent<RectTransform>().anchoredPosition = anchor;
    }

    public static void ChangeAlpha(this Graphic graphic, float a)
    {
        var color = graphic.color;
        color.a = a;
        graphic.color = color;
    }

    public static void ChangeAlpha(this Material material, float a)
    {
        var color = material.color;
        color.a = a;
        material.color = color;
    }

    public static void ChangeAnchorX(this RectTransform rect, float x)
    {
        var pos = rect.anchoredPosition;
        pos.x = x;
        rect.anchoredPosition = pos;
    }

    public static void ChangeAnchorY(this RectTransform rect, float y)
    {
        var pos = rect.anchoredPosition;
        pos.y = y;
        rect.anchoredPosition = pos;
    }

    public static void ChangeSizeX(this RectTransform rect, float x)
    {
        var size = rect.sizeDelta;
        size.x = x;
        rect.sizeDelta = size;
    }

    public static void ChangeSizeY(this RectTransform rect, float y)
    {
        var size = rect.sizeDelta;
        size.y = y;
        rect.sizeDelta = size;
    }
    public static RectTransform Rect<T>(this T self) where T : Component
    {
        return self.GetComponent<RectTransform>();
    }
    public static float Clamp0360(float eulerAngles)
    {
        float result = eulerAngles - Mathf.CeilToInt(eulerAngles / 360f) * 360f;
        if (result < 0)
        {
            result += 360f;
        }

        return result;
    }

    public static void CopyString(this string str)
    {
        GUIUtility.systemCopyBuffer = str;
    }

    public static string ConvertSecondToTime(long seconds, string format)
    {
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(seconds);
        return dateTime.ToString(format);
    }

    public static DateTime ConvertSecondToDateTime(this long seconds)
    {
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(seconds);
        return dateTime;
    }

    public static long TotalSeconds(this DateTime date)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan diff = date.ToUniversalTime() - origin;
        return (long)(diff.TotalSeconds);
    }

    public static string Base64Encode(this string str)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(str);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(this string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public static List<T> Split<T>(this List<T> ls, int start, int length)
    {
        if (ls.Count < start + length)
        {
            return null;
        }

        var l = new List<T>();
        for (int i = start; i < start + length; i++)
        {
            l.Add(ls[i]);
        }

        return l;
    }

    public static List<T> Splice<T>(this List<T> list, int offset, int count)
    {
        var startIdx = offset < 0 ? list.Count + offset : offset;
        var result = list.Skip(startIdx).Take(count).ToList();
        list.RemoveRange(startIdx, count);
        return result;
    }

    public static void WaitNewFrame(this MonoBehaviour obj, Action callback)
    {
        Executors.RunCoroutineWithoutReturn(IEWaitNewFrame(callback));
    }

    public static void WaitNewFrame(this MonoBehaviour obj, Action callback, out Coroutine ct)
    {
        ct = Executors.RunCoroutineWithReturn(IEWaitNewFrame(callback));
    }

    public static void TimeOut(this MonoBehaviour obj, Action callback, float time)
    {
        Executors.RunCoroutineWithoutReturn(IETimeOut(callback, time));
    }

    public static void Wait2NewFrame(this MonoBehaviour obj, Action callback)
    {
        Executors.RunCoroutineWithoutReturn(IEWait2NewFrame(callback));
    }

    public static IEnumerator IEWaitNewFrame(Action callBack)
    {
        yield return null;
        callBack?.Invoke();
    }

    private static IEnumerator IETimeOut(Action callBack, float t)
    {
        yield return new WaitForSeconds(t);
        callBack?.Invoke();
    }

    private static IEnumerator IEWait2NewFrame(Action callBack)
    {
        yield return null;
        yield return null;
        callBack?.Invoke();
    }

    public static void WaitEndOfFrame(this MonoBehaviour obj, Action callback)
    {
        Executors.RunCoroutineWithoutReturn(IEWaitEndOfFrame(callback));
    }

    private static IEnumerator IEWaitEndOfFrame(Action callBack)
    {
        yield return null;
        callBack?.Invoke();
    }

    public static void WaitUntil(Func<bool> predicate, Action callback)
    {
        Executors.RunCoroutineWithoutReturn(IEWaitUntil(predicate, callback));
    }

    public static void WaitUntil(this MonoBehaviour obj, Func<bool> predicate, Action callback)
    {
        Executors.RunCoroutineWithoutReturn(IEWaitUntil(predicate, callback));
    }

    private static IEnumerator IEWaitUntil(Func<bool> predicate, Action callBack)
    {
        yield return new WaitUntil(predicate);
        callBack?.Invoke();
    }

    public static List<T> SpliceGetLast<T>([NotNull] this List<T> ls, int count)
    {
        var temp = new List<T>();
        for (int i = 0; i < count; i++)
        {
            var x = ls.Count - (count - i);
            if (x >= 0)
            {
                temp.Add(ls[x]);
            }
        }

        return temp;
    }

    public static int Nearest(this float[] arr, float value)
    {
        var index = 0;
        var v = Mathf.Abs(arr[0] - value);
        for (int i = 1; i < arr.Length; i++)
        {
            if (Mathf.Abs(arr[i] - value) < v)
            {
                index = i;
                v = Mathf.Abs(arr[i] - value);
            }
        }

        return index;
    }

    public static async void SetCount<T>(this List<T> list, int count, T defaulValue)
    {
        for (var i = 0; i < count; i++)
        {
            list.Add(defaulValue);
        }
    }

    public static List<T> Clone<T>(this List<T> list) where T : struct
    {
        var temp = new List<T>();
        for (int i = 0; i < list.Count; i++)
        {
            temp.Add(list[i]);
        }

        return temp;
    }

    public static int SumRange(this List<int> ls, int index)
    {
        if (index > ls.Count)
        {
            SDLogger.LogError("index out list");

            return 0;
        }

        var t = 0;
        for (int i = 0; i < index; i++)
        {
            t += ls[i];
        }

        return t;
    }

    public static T[] Clone<T>(this T[] list) where T : struct
    {
        var temp = new T[list.Length];
        for (int i = 0; i < list.Length; i++)
        {
            temp[i] = list[i];
        }

        return temp;
    }

    public static void DeSpawnAllChild(this RectTransform t)
    {
        for (int i = t.childCount - 1; i >= 0; i--)
        {
            // SDPool.DeSpawn(t.GetChild(i));
        }
    }
    public static void DeSpawnAllChild(this Transform t)
    {
        for (int i = t.childCount - 1; i >= 0; i--)
        {
            // SDPool.DeSpawn(t.GetChild(i));
        }
    }
    public static void SetScaleByViewSize(this Image im, Sprite sp, float viewSize)
    {
        im.Show();
        im.sprite = sp;
        im.SetNativeSize();
        var tex = sp.texture;
        var scaleSize = viewSize / Mathf.Max(tex.width, tex.height);
        im.transform.localScale = scaleSize * Vector3.one;
    }

    public static void UpdateMeshRenderer(this SkinnedMeshRenderer current, SkinnedMeshRenderer newMeshRenderer)
    {
        // update mesh
        current.sharedMesh = newMeshRenderer.sharedMesh;

        Transform[] childrens = current.GetComponentsInChildren<Transform>(true);
        // sort bones.
        Transform[] bones = new Transform[newMeshRenderer.bones.Length];
        for (int boneOrder = 0; boneOrder < newMeshRenderer.bones.Length; boneOrder++)
        {
            bones[boneOrder] = Array.Find<Transform>(childrens, c => c.name == newMeshRenderer.bones[boneOrder].name);
        }

        current.bones = bones;
    }

    public static void SetTransformLayer(this Transform t, LayerMask mask, bool isAllChild)
    {
        t.gameObject.layer = mask;
        if (isAllChild)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                t.GetChild(i).SetTransformLayer(mask, true);
            }
        }
    }

    public static string ConvertCoin(double value)
    {
        string sConvert = "";
        if (value is < 1000000 or > 999999999999999)
        {
            if (value < 1000000)
            {
                return value.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                string[] s = value.ToString(CultureInfo.InvariantCulture).Split('.', 'E');
                sConvert = s[0] + "." + s[1][0] + "E" + s[2];
                return sConvert;
            }
        }
        else
        {
            string strValue = value.ToString(CultureInfo.InvariantCulture);
            var index = strValue.Length - 1;
            var soMuDu = index % 3;
            string strTail = SetTail(index);
            switch (soMuDu)
            {
                case 0:
                    sConvert += strValue[0] + "." + strValue[1] + strTail;
                    break;
                case 1:
                    sConvert += strValue[0];
                    sConvert += strValue[1] + "." + strValue[2] + strTail;
                    break;
                case 2:
                    sConvert += strValue[0];
                    sConvert += strValue[1];
                    sConvert += strValue[2] + "." + strValue[3] + strTail;
                    break;
            }
        }

        return sConvert;
    }

    private static string SetTail(int value)
    {
        if (value is >= 0 and < 3)
        {
            return "";
        }
        else if (value is >= 3 and < 6)
        {
            return "K";
        }
        else if (value is >= 6 and < 9)
        {
            return "M";
        }
        else if (value is >= 9 and < 12)
        {
            return "B";
        }
        else if (value is >= 12 and < 15)
        {
            return "T";
        }

        return "";
    }

    public static string FormatMoneyK(long value)
    {
        double valueTemp = value;
        if (valueTemp >= 1000000000)
        {
            valueTemp /= 1000000000;
            return Math.Floor(valueTemp) + "B";
        }

        if (valueTemp >= 1000000)
        {
            valueTemp /= 1000000;
            return Math.Round(valueTemp, 2) + "M";
        }

        if (!(valueTemp >= 1000)) return valueTemp.ToString(CultureInfo.InvariantCulture);
        valueTemp /= 1000;
        return Math.Round(valueTemp, 2) + "K";
    }

    public static string UnPascalCase(this string text, char insert = ' ')
    {
        if (string.IsNullOrWhiteSpace(text))
            return "";
        var newText = new StringBuilder(text.Length * 2);
        newText.Append(text[0]);
        for (int i = 1; i < text.Length; i++)
        {
            var currentUpper = char.IsUpper(text[i]);
            var prevUpper = char.IsUpper(text[i - 1]);
            var nextUpper = (text.Length > i + 1) ? char.IsUpper(text[i + 1]) || char.IsWhiteSpace(text[i + 1]) : prevUpper;
            var spaceExists = char.IsWhiteSpace(text[i - 1]);
            if (currentUpper && !spaceExists && (!nextUpper || !prevUpper))
                newText.Append(insert);
            newText.Append(text[i]);
        }
        return newText.ToString();
    }

    public static void RotateTarget(this Transform a, Transform target, float speed)
    {
        var rotation = Quaternion.LookRotation(target.position - a.position);
        rotation.x = 0;
        rotation.z = 0;
        a.rotation = Quaternion.Slerp(a.rotation, rotation, Time.deltaTime * speed);
    }

    public static void RotateTarget(this RectTransform a, Transform target, float speed)
    {
        var rotation = Quaternion.LookRotation(target.position - a.position);
        rotation.x = 0;
        rotation.z = 0;
        a.rotation = Quaternion.Slerp(a.rotation, rotation, Time.deltaTime * speed);
    }

    public static T GetRandom<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static List<T> GetClone<T>(this List<T> list)
    {
        var newList = new List<T>();
        foreach (var o in list)
        {
            newList.Add(o);
        }

        return newList;
    }

    public static List<T> GetRandomListWithoutDuplicate<T>(this List<T> list, int count)
    {
        if (list.Count <= 0) return null;
        if (list.Count < count)
        {
            SDLogger.Log($"WARNING: list{typeof(T)} co so phan tu:{list.Count} nho hon {count}");
        }

        var index = new List<int>();
        for (int i = 0; i < list.Count; i++)
        {
            index.Add(i);
        }

        var n = Mathf.Min(list.Count, count);
        var newList = new List<T>();
        for (int i = 0; i < n; i++)
        {
            var ranIndex = Random.Range(0, index.Count);
            newList.Add(list[index[ranIndex]]);
            index.Remove(index[ranIndex]);
        }

        return newList;
    }
    public static void DetachAndReSpawnChild(this RectTransform t, MonoBehaviour childPrefab, int count)
    {
        foreach (Transform child in t)
        {
            child.Hide();
        }

        for (int i = 0; i < count; i++)
        {
            if (t.childCount <= i)
            {
                Object.Instantiate(childPrefab, t);
            }
            else
            {
                t.GetChild(i).Show();
            }
        }

    }

    private static System.Random _RNG = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = _RNG.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }

    public static void SetLeft(this RectTransform rt, float left)
    {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }

    public static void SetRight(this RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }
    public static void Swap<T>(this List<T> list, int index1, int index2)
    {
        if (index1 < 0 || index1 >= list.Count || index2 < 0 || index2 >= list.Count)
        {
            throw new ArgumentOutOfRangeException("Index out of range");
        }

        (list[index1], list[index2]) = (list[index2], list[index1]);
    }
}