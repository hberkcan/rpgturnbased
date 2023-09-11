namespace MyMessagingSystem
{
    public struct BattleStateChangedEvent
    {
        public BattleStateType BattleState;

        public BattleStateChangedEvent(BattleStateType battleState)
        {
            BattleState = battleState;
        }
    }
}