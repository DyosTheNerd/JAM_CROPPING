/*
* Copyright (c) Marvin Woelke
* ExtensionMethods 2021
*/

//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    /* DAS HINZUFÜGEN IN EINER LISTE:
     * Liste Erstellen. Diese wird gefüllt. Der erste Wert, ist die Erste Insel.
     * Wenn Rechts hinzugefügt wird, dann einfach ADD
     * Wenn Links hinzugefügt wird, dann einfach INSERT[0].ADD
     * 
     * DIES ÜBERPRÜFEN OB/WIE MÖGLICH!
     * 
     */

    //public static List<T> AddFirst<T>(this List<T> list, T obj)
    //{
    //    //if (list != null && list.Count > 0)

    //    List<T> result = new List<T>();
    //    //list.CopyTo()
    //    list.Insert(0, obj)
    //    result.Add(obj);
    //    result.AddRange(list);

    //    return result;
    //}
	
	public static Vector2 GetDirection(this Transform from, Transform to)
    {
        return GetDirection(from.position, to.position);
    }

    public static Vector2 GetDirection(this Vector3 from, Vector3 to)
    {
        return (to - from).normalized;
    }

    /// <summary>
    /// Gets a random value from the list. Can still run error when list null or empty.
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="list">List</param>
    /// <returns>Random object from list.</returns>
    public static T GetRandom<T>(this List<T> list)
    {
        //if (list != null && list.Count > 0)
        return list[Random.Range(0, list.Count)];
    }

    public static List<T> GetAllWithoutNull<T>(this List<T> list)
    {
        List<T> result = new List<T>();

        foreach (T t in list)
        {
            if (t != null)
                result.Add(t);
        }

        return result;
    }

    /// <summary>
    /// Destroyes all child objects in the given transform.
    /// </summary>
    /// <param name="transform"></param>
    public static void DestroyChildren(this Transform transform)
    {
        foreach (Transform child in transform)
            Object.Destroy(child.gameObject);
    }

    public static void ResetLocal(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    public static void Reset(this Transform transform)
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    public static bool IsObjectVisible(this Camera @this, Renderer renderer)
    {
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(@this), renderer.bounds);

        #region OTHER METHODS:
        //var planes = GeometryUtility.CalculateFrustumPlanes(@this);

        //foreach (var plane in planes)
        //{
        //    if (plane.GetDistanceToPoint(obj.position) < 0) // obj. GameObject
        //    {
        //        return false;
        //    }
        //}

        //return true;

        ////////////////////////////////////////////////////////////////
        //Vector3 screenPoint = MAIN.Camera.WorldToViewportPoint(obj.position);


        //return screenPoint.z > 0 &&
        //         screenPoint.x > 0 &&
        //        screenPoint.x < 1 &&
        //        screenPoint.y > 0 &&
        //       screenPoint.y < 1;
        #endregion
    }
}
