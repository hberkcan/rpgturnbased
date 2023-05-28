using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    public IEnumerator AttackBehaviour(CharacterAttack attacker, CharacterHealth target);
}
