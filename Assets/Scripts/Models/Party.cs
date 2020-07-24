using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace chocobo
{
    public class Party : MonoBehaviour
    {
        [SerializeField] protected List<Character> characters;
        public List<Character> Characters { get { return characters; } set { characters = value; } }
    }
}

