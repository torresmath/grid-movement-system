using System.Collections;
using System.Collections.Generic;
using TheLiquidFire.Notifications;
using UnityEngine;
using System.Linq;
using DG.Tweening;

namespace chocobo
{
    public class Tile : MonoBehaviour
    {
        private const string PlayerMoveToNotification = "PlayerMovement.PlayerMoveTo";
        private const string FinishedMoveNotification = "PlayerMovement.FinishedMove";
        private const string NormalizePositionsNotification = "NormalizePositions";

        public Point pos;
        public List<GameObject> content;
        public int x;
        public int y;

        protected SpriteRenderer highlightSprite;
        [SerializeField] protected Ease easeType;

        public Tile(int x, int y)
        {
            pos = new Point(x, y);
        }

        private void Awake()
        {
            highlightSprite = GetComponentInChildren<SpriteRenderer>();
            highlightSprite.enabled = false;
            highlightSprite.transform.DOScale(new Vector2(0, 0), .2f).SetEase(Ease.InBack);
                
        }

        public void OnEnable()
        {
            this.AddObserver(OnPlayerMoveTo, PlayerMoveToNotification);
            this.AddObserver(OnNormalizePositions, NormalizePositionsNotification);
        }

        private void OnDisable()
        {
            this.RemoveObserver(OnPlayerMoveTo, PlayerMoveToNotification);
            this.RemoveObserver(OnNormalizePositions, NormalizePositionsNotification);
        }

        void OnPlayerMoveTo(object sender, object args)
        {
            if (args == null)
                return;
            var directions = args as DirectionArgs;

            if (RoundedPositionEquals(directions.direction.x, directions.direction.y))
            {
                if (!TileHaveContent())
                {
                    this.PostNotification("Tile.CanMoveTo", directions);
                }
                    
            }
                
        }

        void OnNormalizePositions(object sender, object args)
        {
            NormalizeContent();
        }

        public void Match(int x, int y)
        {
            gameObject.transform.localPosition = new Vector2(x, y);
            pos = new Point(x, y);
            this.x = x;
            this.y = y;
        }

        bool RoundedPositionEquals(float x, float y)
        {
            return Mathf.RoundToInt(x) == this.x && Mathf.RoundToInt(y) == this.y;
        }

        bool TileHaveContent()
        {
            bool tileHaveCharacters = content.All(c => c.GetComponent<Character>() != null);
            return content.Count > 0 && !tileHaveCharacters;
        }

        void MatchContent()
        {
            if (content == null)
                return;
            foreach (GameObject gameObj in content)
                gameObj.transform.localPosition = new Vector2(x, y);
        }

        void NormalizeContent()
        {
            GameObject actorHolder = GameObject.FindGameObjectWithTag("Actor Holder");
            content = new List<GameObject>();

            content = actorHolder.GetComponentsInChildren<Transform>()
                .Where(transform => transform.parent == actorHolder.transform)
                .Where(transform => transform.transform.localPosition.x == x && transform.transform.localPosition.y == y)
                .Select(transform => transform.gameObject)
                .ToList();

            HighlightContent(content);
        }

        void HighlightContent(List<GameObject> content)
        {
            if (content.Count > 0)
            {
                highlightSprite.enabled = true;
                highlightSprite.transform.DOScale(new Vector2(1f, 1f), .2f).SetEase(easeType);
            } else
            {
                highlightSprite.transform.DOScale(new Vector2(0, 0), .2f).SetEase(easeType).OnComplete(() => highlightSprite.enabled = false);
            }
        }
    }
}

