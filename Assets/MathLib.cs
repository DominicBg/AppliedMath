using System.Collections;
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
        //Vector2 cartesianCoordinate = new Vector2();
        float r = polarCoordinate.x;
        float theta = polarCoordinate.y;

        float x = r * Mathf.Cos(theta);
        float y = r * Mathf.Sin(theta);

        Vector2 cartesianCoordinate = new Vector2(x, y);
        return cartesianCoordinate;
    }

    /// <summary>
    /// Transform a cartesian coordinate (x, y) into a  polar  (radius, angle)  point
    /// </summary>
    public static Vector2 CartesianToPolar(Vector2 cartesianCoordinate)
    {
        //Vector2 polarCoordinate = new Vector2();

        float x = cartesianCoordinate.x;
        float y = cartesianCoordinate.y;

        float r = Mathf.Sqrt(x * x + y * y);
        float theta = Mathf.Atan2(y, x);
        Vector2 polarCoordinate = new Vector2(r, theta);

        return polarCoordinate;
    }
}
