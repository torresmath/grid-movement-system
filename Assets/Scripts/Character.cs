using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TheLiquidFire.Notifications;
using UnityEngine;

namespace chocobo
{
    public class Character : MonoBehaviour
    {

        private const string NormalizePositionsNotification = "NormalizePositions";

        [SerializeField] protected Vector2 currentPosition;
        public Vector2 CurrentPosition
        {
            get { return currentPosition; }
        }

        [SerializeField] protected CharacterData data;
        public CharacterData Data { get { return data; } private set { } }

        Tweener mover;
        Tweener jumper;
        Tweener scaler;
        Tweener facer;
        bool facing;
        bool tweenersExist;
        bool tweenersActive;

        public bool TweenersExist { get { return tweenersExist; } }
        public bool TweenersActive { get { return tweenersActive; } }

        private void OnEnable()
        {
            this.AddObserver(OnNormalizePositions, NormalizePositionsNotification);
        }

        void OnNormalizePositions(object sender, object args)
        {
            NormalizePosition();
        }

        public void CharacterMove(DirectionArgs directions)
        {
            tweenersExist = mover != null && jumper != null && scaler != null && facer != null;
            if (tweenersExist)
            {
                tweenersActive = mover.active || jumper.active || scaler.active || facer.active;
                if (tweenersActive) return;
            }

            ScaleTo();
            MoveTo(directions);
            JumpTo(directions.inputs);
            FaceTo(directions.inputs.x);
        }

        void MoveTo(DirectionArgs directions)
        {
            if (mover != null && mover.active)
                return;
            mover = Tweening.Instance.MoveTo(mover, transform, directions).OnComplete(() => this.PostNotification(NormalizePositionsNotification));
        }

        void JumpTo(InputArgs inputs)
        {
            if (jumper != null && jumper.active)
                return;
            jumper = Tweening.Instance.JumpTo(jumper, transform, inputs, facing);

        }

        void ScaleTo()
        {
            if (scaler != null && scaler.active)
                return;
            scaler = Tweening.Instance.ScaleTo(scaler, transform);
        }

        void FaceTo(int horizontal)
        {
            if (horizontal == 0)
                return;

            if (facer != null && facer.active)
                return;
            facing = horizontal > 0;
            facer = Tweening.Instance.FaceTo(facer, transform, facing);
        }

        void NormalizePosition()
        {
            transform.localPosition = new Vector2(Mathf.RoundToInt(transform.localPosition.x), Mathf.RoundToInt(transform.localPosition.y));
            currentPosition = transform.localPosition;
        }
    }
}

