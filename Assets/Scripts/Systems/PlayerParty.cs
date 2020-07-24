using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TheLiquidFire.Notifications;
using UnityEngine;

namespace chocobo
{
    public class PlayerParty : MonoBehaviour
    {
        private const string ReorderPartyNotification = "InputNotification.Reorder";

        protected Party party;

        private void Start()
        {
            party = GetComponent<Party>();
            this.AddObserver(OnReorderParty, ReorderPartyNotification);
            this.PostNotification("UpdatePartyOrderNotification", party.Characters);
        }
        private void OnEnable()
        {
            
        }

        private void OnDestroy()
        {
            this.RemoveObserver(OnReorderParty, ReorderPartyNotification);
        }

        void OnReorderParty(object sender, object args)
        {
            List<Character> characters = party.Characters;
            List<Character> charactersReordered = new List<Character>();

            for (int i = 0; i < characters.Count; i++)
            {
                var n = i + 1;
                Character character = characters[i];
                Character nextCharacter = n < characters.Count ? characters[n] : characters[0];

                charactersReordered.Add(nextCharacter);
            }

            party.Characters = charactersReordered;
            this.PostNotification("UpdatePartyOrderNotification", charactersReordered);
            this.PostNotification("NormalizePositions");
        }
    }
}
