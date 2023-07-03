using System.Collections.Generic;

namespace MyMessagingSystem
{
    public struct StartBattleEvent
    {
        public List<CharacterDataSO> selectedCharacters;
        public StartBattleEvent(List<CharacterDataSO> selectedCharacters)
        {
            this.selectedCharacters = selectedCharacters;
        }
    }
}