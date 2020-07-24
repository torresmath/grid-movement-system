using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TheLiquidFire.Notifications;
using UnityEngine;

namespace chocobo
{
    public class PlayerMovement : MonoBehaviour
    {
        private const string MoveNotification = "InputNotification.Move";
        private const string TileCanMoveToNotification = "Tile.CanMoveTo";
        private const string UpdatePartyOrderNotification = "UpdatePartyOrderNotification";


        [Range(0, 10)]
        public float speed = 1f;
        [Range(0, 2)]
        public float delay = 1f;

        [SerializeField] protected Party party;
        [SerializeField] protected List<Character> characters;
        public Party Party { get { return party; } }
        void OnEnable()
        {
            party = GetComponentInChildren<Party>();
            characters = party.Characters;
            DOTween.Init();
            this.AddObserver(OnMove, MoveNotification);
            this.AddObserver(OnCanMoveTo, TileCanMoveToNotification);
            this.AddObserver(OnUpdatePartyOrder, UpdatePartyOrderNotification);
        }

        void OnDestroy()
        {
            this.RemoveObserver(OnMove, MoveNotification);
            this.RemoveObserver(OnCanMoveTo, TileCanMoveToNotification);
        }

        void OnMove(object sender, object args)
        {
            if (args == null)
                return;
            var direction = args as InputArgs;

            var newPos = new Vector2(party.Characters[0].gameObject.transform.localPosition.x + (direction.x), party.Characters[0].gameObject.transform.localPosition.y + (direction.y));
            this.PostNotification("PlayerMovement.PlayerMoveTo", new DirectionArgs(new InputArgs(direction.x, direction.y), newPos));
        }

        void OnUpdatePartyOrder(object sender, object args)
        {
            party = GetComponentInChildren<Party>();
            characters = party.Characters;
        }

        void OnCanMoveTo(object sender, object args)
        {
            if (args == null)
                return;
            var directions = args as DirectionArgs;
            MoveParty(directions);

        }

        void MoveParty(DirectionArgs directions)
        {
            party.Characters[0].CharacterMove(directions);

            for (int i = 1; i < party.Characters.Count; i++)
            {
                Vector2 characterPos = party.Characters[i].CurrentPosition;
                Vector2 previousCharacterPos = party.Characters[i - 1].CurrentPosition;
                int inputX = previousCharacterPos.x.CompareTo(characterPos.x);
                int inputY = previousCharacterPos.y.CompareTo(characterPos.y);
                InputArgs inputArgs = new InputArgs(inputX, inputY);
                DirectionArgs directionArgs = new DirectionArgs(inputArgs, previousCharacterPos);
                party.Characters[i].CharacterMove(directionArgs);    
            }

            //this.PostNotification("NormalizePositions");

        }

    }
}

