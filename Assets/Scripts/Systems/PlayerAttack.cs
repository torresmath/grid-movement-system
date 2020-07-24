using System.Collections;
using System.Collections.Generic;
using TheLiquidFire.Notifications;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private const string AttackNotification = "InputNotification.Attack";

    void OnEnable()
    {
        this.AddObserver(OnAttack, AttackNotification);
    }

    void OnDestroy()
    {
        this.RemoveObserver(OnAttack, AttackNotification);
    }

    void OnAttack(object sender, object args)
    {

    }
}
