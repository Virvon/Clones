using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class NumberFormatter
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

        int formattedLength = numLength + numCommas;

        System.Text.StringBuilder formattedNumber = new System.Text.StringBuilder(formattedLength);

        int commaCount = 0;

        for (int i = numLength - 1; i >= 0; i--)
        {
            if (commaCount == 3)
            {
                formattedNumber.Insert(0, ',');
                commaCount = 0;
            }

            formattedNumber.Insert(0, numberAsString[i]);
            commaCount++;
        }

        return formattedNumber.ToString();
    }
}
