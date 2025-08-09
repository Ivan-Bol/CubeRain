using UnityEngine;

public class Painter : MonoBehaviour
{
    public Color GenerateRandomColor()
    {
        return Random.ColorHSV();
    }
}
