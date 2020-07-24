using System.Collections;
using System.Collections.Generic;
using TheLiquidFire.Notifications;
using UnityEngine;

namespace chocobo
{
    public class InputController : MonoBehaviour
    {

		Repeater _hor = new Repeater("Horizontal");
		Repeater _ver = new Repeater("Vertical");

		public List<string> buttons;

        void Update()
        {
            for (int i = 0; i < buttons.Count; ++i)
                if (Input.GetButtonUp(buttons[i]))
                {
                    this.PostNotification("InputNotification." + buttons[i]);
                }

			int x = _hor.Update();
			int y = _ver.Update();
			if (x != 0 || y != 0)
			{
				this.PostNotification("InputNotification.Move", new InputArgs(x, y));
			}
		}
    }

	class Repeater
	{
		const float threshold = 0.5f;
		const float rate = 0.25f;
		float _next;
		bool _hold;
		string _axis;

		public Repeater(string axisName)
		{
			_axis = axisName;
		}

		public int Update()
		{
			int retValue = 0;
			int value = Mathf.RoundToInt(Input.GetAxisRaw(_axis));

			if (value != 0)
			{
				if (Time.time > _next)
				{
					retValue = value;
					_next = Time.time + (_hold ? rate : threshold);
					_hold = true;
				}
			}
			else
			{
				_hold = false;
				_next = 0;
			}

			return retValue;
		}
	}
}

