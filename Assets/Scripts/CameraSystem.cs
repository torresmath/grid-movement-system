using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace chocobo
{
    public class CameraSystem : MonoBehaviour
    {
        public PlayerMovement playerMovement;
        public GameObject objectToFollow;

        public float speed = 2.0f;
        [Range(-5, 5)]
        public float distance = -4f;

        void Update()
        {
            objectToFollow = playerMovement.Party.Characters[0].gameObject;
            float interpolation = speed * Time.deltaTime;

            Vector3 position = this.transform.position;
            position.y = Mathf.Lerp(this.transform.position.y, objectToFollow.transform.position.y, interpolation);
            position.x = Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x + distance, interpolation);

            this.transform.position = position;
        }
    }
}

