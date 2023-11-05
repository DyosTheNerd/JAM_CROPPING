using UnityEngine;

public class Colors
{
  public static Color[] colorList = new Color[] { Color.red, Color.blue, Color.yellow };

  public static Color grey = Color.grey;

  public static Color getResultingColor(Color[] colors)
  {
    Color result ;

    switch (colors.Length)
    {
      case 0:
        result = Color.white;
        break;
      case 1:
        result = colors[0];
        break;
      default:
        result = Color.black;
        break;
    }
    
    return result;
    
  }
  
}