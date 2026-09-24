using System.Collections;
using UnityEngine;

// Interface for any game object that attacks.
public interface IAttacker
{
    GameObject attackPrefab { get; set; }
    float attackCooldown { get; set; }
    
    void Attack();
    IEnumerator Cooldown();
}
