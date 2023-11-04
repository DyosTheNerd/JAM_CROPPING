using System.Reflection.Emit;
using UnityEngine;

public class GoalComponent
{
    public GoalComponent(Color c)
    {
        this._color = c;
    }

    public Color _color;
    public bool solved;
}