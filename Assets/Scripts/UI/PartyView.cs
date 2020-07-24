using System.Collections;
using System.Collections.Generic;
using TheLiquidFire.Notifications;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace chocobo
{
    public class PartyView : MonoBehaviour
    {
        private const string UpdatePartyOrderNotification = "UpdatePartyOrderNotification";
        [SerializeField] protected List<Image> partySlots;

        private void OnEnable()
        {
            this.AddObserver(OnUpdatePartyOrder, UpdatePartyOrderNotification);
            partySlots = GetComponentsInChildren<Image>()
                .Where(image => image.transform.parent == this.transform)
                .ToList();
        }

        private void OnDisable()
        {
            this.RemoveObserver(OnUpdatePartyOrder, UpdatePartyOrderNotification);
        }

        void OnUpdatePartyOrder(object sender, object args)
        {
            var party = args as List<Character>;
            if (party == null)
                return;

            
            for (int i = 0; i < partySlots.Count; i++)
            {
                partySlots[i].sprite = party[i].Data.UISprite;
            }

        }
    }
}

