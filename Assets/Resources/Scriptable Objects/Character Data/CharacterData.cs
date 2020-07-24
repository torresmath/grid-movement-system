using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace chocobo
{
    [CreateAssetMenu(fileName = "new Character", menuName = "Character")]
    public class CharacterData : ScriptableObject
    {
        public string characterName;
        public Sprite sprite;
        public Sprite UISprite;
    }
}

