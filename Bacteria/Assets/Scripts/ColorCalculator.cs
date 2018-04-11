using UnityEngine;
using System.Collections;

public static class ColorCalculator {

	public static Color MergeColors(Color firstColor, float firstColorWeighting, Color secondColor, float secondColorWeighting){
        Color mergedColor;

        float red = 0.0f;
        float green = 0.0f;
        float blue = 0.0f;
        float alpha = 0.0f;

        red = firstColor.r * firstColorWeighting + secondColor.r * secondColorWeighting;
        green = firstColor.g * firstColorWeighting + secondColor.g * secondColorWeighting;
        blue = firstColor.b * firstColorWeighting + secondColor.b * secondColorWeighting;
        alpha = firstColor.a * firstColorWeighting + secondColor.a * secondColorWeighting;

        float totalWeighting = (firstColorWeighting + secondColorWeighting);

        red = red / totalWeighting;
        green = green / totalWeighting;
        blue = blue / totalWeighting;
        alpha = alpha / totalWeighting;

        mergedColor = new Color(red, green, blue, alpha);

        return mergedColor;
    }

    public static ImmunityColour GetDominantColour(Color checkColor)
    {
        float highest;
        ImmunityColour imColor;

        highest = checkColor.r;
        imColor = ImmunityColour.RED;

        if (checkColor.g > highest)
        {
            highest = checkColor.g;
            imColor = ImmunityColour.GREEN;
        }

        if (checkColor.b > highest)
        {
            highest = checkColor.b;
            imColor = ImmunityColour.BLUE;
        }

        if (checkColor.b == checkColor.r && checkColor.r == checkColor.g)
        {
            imColor = ImmunityColour.MIX;
        }

        return imColor;
    }

}
