﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathLib {
    
    //Il semble y avoir des erreurs dans ce code...
    //https://en.wikipedia.org/wiki/Polar_coordinate_system#Converting_between_polar_and_Cartesian_coordinates

    public static Vector2 RotateVector2D(float angle, Vector2 vector)
    {
        Vector2 polarCoordinate = CartesianToPolar(vector);
        //Deg2Rad car l'input est en degré, mais qu'on doit convertir en radiant pour la formule
        //le y = theta
        polarCoordinate.y += angle * Mathf.Deg2Rad;

        return PolarToCartesian(polarCoordinate);
    }

    /// <summary>
    /// Transform a polar coordinate (radius, angle) into a cartesian (x,y) point
    /// </summary>
    public static Vector2 PolarToCartesian(Vector2 polarCoordinate)
    {
        //insert formula
        Vector2 cartesianCoordinate = new Vector2();
        return cartesianCoordinate;
    }

    /// <summary>
    /// Transform a cartesian coordinate (x, y) into a  polar  (radius, angle)  point
    /// </summary>
    public static Vector2 CartesianToPolar(Vector2 cartesianCoordinate)
    {
        //insert formula
        Vector2 polarCoordinate = new Vector2();
        return polarCoordinate;
    }
}
