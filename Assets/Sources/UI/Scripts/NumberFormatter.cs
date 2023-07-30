using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class NumberFormatter //Возможно проблема в том, что класс static
{
    public static string FormatNumberWithCommas(int number)
    {
        string numberAsString = number.ToString();

        if (numberAsString.Length <= 3)
        {
            return numberAsString;
        }

        int numLength = numberAsString.Length;
        int numCommas = (numLength - 1) / 3;

        char[] formattedNumber = new char[numLength + numCommas];

        int commaCount = 0;

        for (int i = numLength - 1; i >= 0; i--)
        {
            if (commaCount == 3)
            {
                formattedNumber[i + numCommas] = ',';
                commaCount = 0;
                numCommas--;
            }
            else
            {
                formattedNumber[i + numCommas] = numberAsString[i];
                commaCount++;
            }
        }

        Debug.Log(new string(formattedNumber));

        return new string(formattedNumber);
    }
}
