using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponAcсess
{
    public Weapon GetCurrentWeapon();
    public void ChangeCurrentWeapon();
}
