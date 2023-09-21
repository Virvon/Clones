using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class NumberFormatter
{
    public static string FormatNumberWithCommas(int number)
    {
        var culture = new CultureInfo("ru-RU");
        return number.ToString("#,#", culture);
    }

    public static string FormatNumberWithCommas(float value)
    {
        Console.WriteLine(value.ToString("0.00", CultureInfo.InvariantCulture));
        return String.Format(CultureInfo.InvariantCulture, "{0:0.00}", value);
    }
}
