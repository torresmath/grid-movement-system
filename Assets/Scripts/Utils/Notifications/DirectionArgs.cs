using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionArgs
{
    public InputArgs inputs;
    public Vector2 direction;

    public DirectionArgs(InputArgs inputs, Vector2 direction)
    {
        this.inputs = inputs;
        this.direction = direction;
    }
}
