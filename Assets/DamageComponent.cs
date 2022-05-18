using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    [Header("Resistances")]
    [Range(0,.9f)] public float kineticResistance; 
    [Range(0,.9f)] public float fireResistance;
    [Range(0,.9f)] public float iceResistance;  

    public void dealDamage(damageStruct[] damage, ref float Health){
        for (int i = 0; i < damage.Length; i++)
            dealDamage(damage[i], ref Health);
    }
    public void dealDamage(damageStruct damage, ref float Health){
        switch (damage.damageType)
        {
            case DamageType.Kinetic :
                Health -= damage.amount * (1 - kineticResistance);
                break;
            case DamageType.Fire :
                Health -= damage.amount * (1 - fireResistance);
                break;
            case DamageType.Ice :
                Health -= damage.amount * (1 - iceResistance);
                break;
        }
    }
}

public enum DamageType{
    Kinetic,
    Fire,
    Ice
}

public struct damageStruct{
    public damageStruct(float a, DamageType dt = DamageType.Kinetic){
        damageType = dt;
        amount = a;
    }

    public DamageType damageType;
    public float amount;
}