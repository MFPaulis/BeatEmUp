using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<CharacterHealth>(out CharacterHealth health);
        if(health != null)
        {
            Debug.Log(other.gameObject.name);
            health.Damage(10);
        }
    }
}
