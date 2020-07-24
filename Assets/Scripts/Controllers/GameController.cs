using System.Collections;
using System.Collections.Generic;
using TheLiquidFire.Notifications;
using UnityEngine;


namespace chocobo
{
    public class GameController : MonoBehaviour
    {
        public int minX = 0;
        public int maxX = 10;
        public int minY = 0;
        public int maxY = 10;
        public GameObject tilePrefab;
        public GameObject tileHolder;
        public GameObject actorHolder;
        void Start()
        {
            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    GameObject instance = Instantiate(tilePrefab, tileHolder.transform) as GameObject;
                    instance.name = "Tile " + x + " " + y;
                    Tile t = instance.GetComponent<Tile>();
                    t.Match(x, y);
                }
            }

            this.PostNotification("NormalizePositions");

        }
    }
}

