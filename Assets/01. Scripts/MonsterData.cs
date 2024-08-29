using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "ScriptableObject/MonsterData")]
public class MonsterData : ScriptableObject
{
    [SerializeField] private float damage = default;
    [SerializeField] private float attackCoolTime = default;
    [SerializeField] private float attackDistance = default;
    [SerializeField] private float stopDistnace = default;
    [SerializeField] private float maxHp = default;
    [SerializeField] private float checkDistance = default;

    public float Damage => damage;
    public float AttackCooolTime => attackCoolTime;
    public float AttackDistance => attackDistance;
    public float StopDistnace => stopDistnace;
    public float MaxHp => maxHp;
    public float CheckDistance => checkDistance;
}
