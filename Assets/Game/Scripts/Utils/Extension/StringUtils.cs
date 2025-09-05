using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public static class StringUtils
{
    public static string FormatMoney(this long v, long max = 1000000000)
    {
        if (v < max)
        {
            return v.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("de"));
        }
        else
        {
            return FormatMoneyK(v);
        }
    }

    public static string FormatTimeOffline(this long second)
    {
        if (second < 60)
        {
            return $"{second} giây trước";
        }

        if (second < 3600)
        {
            return $"{second / 60} phút trước";
        }

        if (second < 86400)
        {
            return $"{second / 3600} giờ trước";
        }

        return $"{second / 86400} ngày trước";
    }

    public static string FormatMoneyK(double value, int digit = 2)
    {
        if (value >= 1000000000)
        {
            value /= 1000000000;
            return Math.Round(value, digit) + "B";
        }

        if (value >= 1000000)
        {
            value /= 1000000;
            return Math.Round(value, digit) + "M";
        }

        if (!(value >= 1000)) return $"{Math.Round(value, digit)}";
        value /= 1000;
        return Math.Round(value, digit) + "K";
    }

    public static string CheckAvailableUserName(string userName)
    {
        var msg = "";
        var disallowName = new List<string>()
        {
            "admin",
            "moderator",
            "hồ chí minh",
            "ho_chi_minh",
            "mod",
            "lồn",
            "địt",
            "cứt",
            "ditme"
        };
        Regex regex =
            new Regex(
                @"[a-z0-9_ A-ZàÀảẢãÃáÁạẠăĂằẰẳẲẵẴắẮặẶâÂầẦẩẨẫẪấẤậẬđĐèÈẻẺẽẼéÉẹẸêÊềỀểỂễỄếẾệỆìÌỉỈĩĨíÍịỊòÒỏỎõÕóÓọỌôÔồỒổỔỗỖốỐộỘơƠờỜởỞỡỠớỚợỢùÙủỦũŨúÚụỤưƯừỪửỬữỮứỨựỰỳỲỷỶỹỸýÝỵỴ]+");
        Regex reCheckSpaceUserName = new Regex(@"^\s|\s$");
        if (userName.Length < 5)
        {
            msg = "Tên đăng ký phải có ít nhất 5 ký tự";
        }
        else if (userName.Length > 20)
        {
            msg = "Tên đăng ký không nhiều hơn 20 ký tự";
        }
        else if (regex.Matches(userName).Count > 1)
        {
            msg = "Tên đăng ký không được chứa ký tự đặc biệt";
        }
        else if (reCheckSpaceUserName.Matches(userName).Count > 0)
        {
            msg = "Tên đăng ký không được chưa ký tự khoảng trống ở đầu hoặc cuối";
        }
        else
        {
            for (int i = 0; i < disallowName.Count; i++)
            {
                string name = userName.ToLower();
                if (name.Contains(disallowName[i]))
                {
                    msg = "Tên đăng ký sử dụng từ ngữ không hợp lệ";
                }
            }
        }

        return msg;
    }
    public static string ConvertSecondsToTimeFormat(this long seconds)
    {
        var hours = seconds / 3600;
        var minutes = (seconds % 3600) / 60;
        var remainingSeconds = seconds % 60;

        return $"{hours:D2}:{minutes:D2}:{remainingSeconds:D2}";
    }
    public static string ConvertSecondsToTimeFormatDay(this long seconds)
    {
        var day = seconds / 86400;
        var hours = (seconds % 86400) / 3600;
        var minutes = (seconds % 3600) / 60;
        if (day <= 0)
        {
            var remainingSeconds = seconds % 60;
            return $"{hours:D2}h:{minutes:D2}m:{remainingSeconds:D2}s";
        }    
        return $"{day:D1}d:{hours:D2}h:{minutes:D2}m";
    }

    public static string FormatMinute(this int second)
    {
        if (second < 60)
        {
            return $"00:{second:D2}";
        }

        return $"{(second / 60):D2}:{(second % 60):D2}";
    }
    public static string ProcessStringLine(this string msg, int maxLengthInLine)
    {
        var ps = "";
        var words = msg.Split(' ');
        var currentSize = 0;
        foreach (var w in words)
        {
            if (currentSize + w.Length <= maxLengthInLine)
            {
                if (currentSize + w.Length + 1 <= maxLengthInLine)
                {
                    ps += $"{w} ";
                    currentSize += w.Length + 1;
                }
                else
                {
                    ps += w;
                    currentSize += w.Length;
                }
            }
            else
            {
                ps += "<br>" + w;
                currentSize = w.Length;
            }
        }

        return ps;
    }

    public static string ProcessMessage(this string msg, out int sender)
    {
        var arr = msg.Split(':');
        sender = int.Parse(arr[0]);
        return msg.Replace($"{sender}:", "");
    }

    public static string ToBold(this string msg)
    {
        return $"<b>{msg}</b>";
    }

    public static string ToColor(this string msg, string color)
    {
        return $"<color={color}>{msg}</color>";
    }

    public static string ToColor(this string msg, Color color)
    {
        return $"<color=#{color.ToHexString()}>{msg}</color>";
    }

    public static string CutNumberFromString(this string msg, string color)
    {
        var isOnDigitStr = false;
        var outPut = "";
        var currentDigit = "";
        for (int i = 0; i < msg.Length; i++)
        {
            if (char.IsDigit(msg[i]))
            {
                if (isOnDigitStr)
                {
                    // outPut += msg[i];
                    currentDigit += msg[i];
                    continue;
                }
                else
                {
                    isOnDigitStr = true;
                    outPut += $"<color={color}>";
                    currentDigit = "";
                    currentDigit += msg[i];
                    // outPut += msg[i];
                }
            }
            else
            {
                if (isOnDigitStr)
                {
                    outPut += FormatMoney(long.Parse(currentDigit), 10000);
                    outPut += "</color>";
                    outPut += msg[i];
                    isOnDigitStr = false;
                    currentDigit = "";
                }
                else
                {
                    outPut += msg[i];
                }
            }
        }

        if (isOnDigitStr)
        {
            outPut += FormatMoney(long.Parse(currentDigit), 10000);
            outPut += $"</color>";
        }

        return outPut;
    }

    public static string ValidateUserName(this string userName)
    {
        var error = "";

        if (userName.Length < 6)
        {
            error = "Use 6 characters or more for username";
            return error;
        }

        if (userName.Length > 18)
        {
            error = "Use 18 characters or less for username";
            return error;
        }

        var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
        if (!regexItem.IsMatch(userName))
        {
            error = "Only letters, numbers (0-9) are allowed";
            return error;
        }
        return error;
    }

    public static string ValidatePassword(this string password)
    {
        var error = "";

        if (password.Length < 6)
        {
            error = "Use 6 characters or more for password";
            return error;
        }

        if (password.Length > 18)
        {
            error = "Password maxNormal 18 character";
            return error;
        }
        return error;
    }
}