using UnityEngine;

public class Waypoints : MonoBehaviour
{
    /*public static Transform[] points;

    void Awake()
    {
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }*/

    public static Transform[] path1;
    public static Transform[] path2;

    void Awake()
    {
        path1 = new Transform[5];
        path2 = new Transform[5];

        for (int i = 0; i < 5; i++)
        {
            path1[i] = transform.GetChild(i);
            path2[i] = transform.GetChild(i);
        }

        path2[1] = transform.GetChild(5);
        path2[2] = transform.GetChild(6);
    }
}
