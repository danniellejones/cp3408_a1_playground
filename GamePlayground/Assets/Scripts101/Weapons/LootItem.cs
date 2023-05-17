using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItem : MonoBehaviour
{
    private bool isClaimed = false;

    private Action<GameObject> claimedCallback;

    public bool IsClaimed()
    {
        return isClaimed;
    }

    public void SetClaimed()
    {
        isClaimed = true;
        claimedCallback?.Invoke(gameObject);
    }

    public void setClaimedCallback(Action<GameObject> callback)
    {
        claimedCallback = callback;
    }
}
