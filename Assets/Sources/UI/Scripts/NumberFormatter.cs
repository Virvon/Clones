using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting.FullSerializer;
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
}
