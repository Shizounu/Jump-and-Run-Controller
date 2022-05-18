using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [Header("Health")]
    public bool Alive;

    public float MaxHealth;
    private float _Health;
    public float Health{
        get{return _Health;}
        set{
            _Health = value;
            if(_Health >= MaxHealth)
                _Health = MaxHealth;
            if(_Health <= 0)
                Alive = false;
        }
    }
}