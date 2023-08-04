using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleManager
{
    public void SetState(BattleStateType state);

    public GameObject GetRandomPlayerCharacter();
    public GameObject EnemyGO { get; }
    public GameObject ClickedHero { get; }
}
