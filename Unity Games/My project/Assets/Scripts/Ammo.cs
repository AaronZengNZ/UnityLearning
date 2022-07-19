using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;

    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public float ammoAmount;
    }

    public float GetCurrentAmmo(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoAmount;
    }

    public void ReduceCurrentAmmo(AmmoType ammoType)
    {
        ReduceCurrentAmmo(1, ammoType);
    }

    public void IncreaseCurrentAmmo(float amount, AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoAmount += amount;
    }

    public void ReduceCurrentAmmo(float amount, AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoAmount -= amount;
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach(AmmoSlot slot in ammoSlots)
        {
            if(slot.ammoType == ammoType)
            {
                return slot;
            }
        }
        return null;
    }
}
