using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace chocobo
{
    public class Tweening : MonoBehaviour
    {
        [Range(-1, 1)]
        [SerializeField] protected float jumpLeft;
        [Range(-1, 1)]
        [SerializeField] protected float jumpRight;
        [Range(0, 1)]
        [SerializeField] protected float jumpUp;

        static Tweening _instance;
        public static Tweening Instance
        {
            get { return _instance ?? (_instance = new Tweening()); }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }

            _instance = this;

        }
        public Tweener MoveTo(Tweener tweener, Transform transform, DirectionArgs directions)
        {
            if (tweener != null && tweener.active)
                return tweener;

            if (directions.inputs.x != 0 && directions.inputs.y == 0)
            {
                tweener = transform.DOMoveX(directions.direction.x, 0.2f);
                return tweener;
            }

            tweener = transform.DOMoveY(directions.direction.y, 0.2f);
            return tweener;
        }

        public Tweener JumpTo(Tweener tweener, Transform transform, InputArgs inputs, bool facing)
        {
            int horizontal = inputs.x;
            int vertical = inputs.y;
            

            if (horizontal != 0)
            {
                tweener = transform.DOMoveY(transform.localPosition.y + jumpUp, 0.1f)
                                .OnComplete(() =>
                                transform.DOMoveY(transform.localPosition.y - jumpUp, 0.1f));
                return tweener;
            }

            var jumpDirection = !facing ? jumpLeft : jumpRight;
            tweener = transform.DOMoveX(transform.localPosition.x + jumpDirection, 0.1f)
                            .OnComplete(() =>
                            transform.DOMoveX(transform.localPosition.x - jumpDirection, 0.1f));
            return tweener;
        }

        public Tweener ScaleTo(Tweener tweener, Transform transform)
        {
            tweener = transform.DOScale(new Vector3(0.8f, 1.2f), 0.1f).OnComplete(() =>
                transform.DOScale(new Vector3(1.0f, 1.0f), 0.1f));

            return tweener;
        }

        public Tweener FaceTo(Tweener tweener, Transform transform, bool facing)
        {
            float rotation = transform.localRotation.eulerAngles.y;

            if (!facing && rotation == 180)
                tweener = transform.DOLocalRotate(new Vector3(0, 0), 0.1f);
            else if (facing && rotation == 0)
                tweener = transform.DOLocalRotate(new Vector3(0, 180), 0.1f);

            return tweener;
        }
    }
}

