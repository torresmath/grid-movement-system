using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace chocobo
{
    public class TargetLine : MonoBehaviour
    {
        public Character character;
        LineRenderer lineRenderer;

        Vector2 clickPosition;
        Vector2 distance;
        Vector2 origin;
        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.enabled = false;
        }


        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                GetMousePosition();
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, origin);
                lineRenderer.SetPosition(1, clickPosition);
            }
        }

        void GetMousePosition()
        {
            clickPosition = -Vector2.one;
            distance = -Vector2.one;
            
            origin = new Vector2(character.transform.localPosition.x, character.transform.localPosition.y);
            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 0));

        }
    }

}
