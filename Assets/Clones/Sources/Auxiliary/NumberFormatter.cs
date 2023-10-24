using System;
using System.Globalization;
using UnityEngine;

public static class NumberFormatter
{
    public static string DivideIntegerOnDigits(int value)
    {
        if(value == 0)
            return "0";

        var culture = new CultureInfo("ru-RU");
        return value.ToString("#,#", culture);
    }

    public static string DivideFloatOnDigits(float value)
    {
        if (value == 0)
            return "0";

        Console.WriteLine(value.ToString("0.00", CultureInfo.InvariantCulture));
        return String.Format(CultureInfo.InvariantCulture, "{0:0.00}", value);
    }

    public static string ConvertSecondsToTimeString(float totalSecondsFloat)
    {
        int totalSeconds = Mathf.RoundToInt(totalSecondsFloat);

        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        string minutesString = minutes.ToString().PadLeft(2, '0');
        string secondsString = seconds.ToString().PadLeft(2, '0');

        return minutesString + ":" + secondsString;
    }
}
