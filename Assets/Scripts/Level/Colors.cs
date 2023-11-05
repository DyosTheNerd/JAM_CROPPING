using UnityEngine;

public class Colors
{
  public static Color[] colorList = new Color[] { Color.red, Color.blue, Color.yellow };

  public static Color grey = Color.grey;

  public static Color getResultingColor(Color[] colorsToMix)
  {
    Color result ;

    switch (colorsToMix.Length)
    {
      case 0:
        result = Color.white;
        break;
      case 1:
        result = colorsToMix[0];
        break;
      case 2:
        if (colorsToMix[0] == colorsToMix[1])
        {
          result = colorsToMix[0];
          break;
        }

        result = colorsToMix[1];
        break;
      default:
        
        result = Color.black;
        break;
    }
    
    return result;
    
  }
  
}