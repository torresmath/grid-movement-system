using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imovable : MonoBehaviour
{
    public Vector2[] parts;

    void Start()
    {
        Vector3 localPos = transform.localPosition;

        for (int i = 0; i < parts.Length; i++)
        {
            GameObject part = Instantiate(new GameObject(gameObject.name + " Part " + i), this.gameObject.transform);
            part.transform.localPosition = new Vector3(
                (localPos.x + parts[i].x),
                (localPos.y + parts[i].y),
                0f);
        }
    }
}
