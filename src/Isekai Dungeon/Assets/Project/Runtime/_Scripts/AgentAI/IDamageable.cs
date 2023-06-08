using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    float CalculateDamage();
    void ReceiveDamage(float dmg);
}
