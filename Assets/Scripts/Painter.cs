using UnityEngine;

public class Painter : MonoBehaviour
{
    public Color Paint()
    {
        return Random.ColorHSV();
    }
}