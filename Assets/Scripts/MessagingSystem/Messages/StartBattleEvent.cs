using System.Collections.Generic;

namespace MyMessagingSystem
{
    public struct StartBattleEvent
    {
        public List<CharacterData> selectedCharacters;
        public StartBattleEvent(List<CharacterData> selectedCharacters)
        {
            this.selectedCharacters = selectedCharacters;
        }
    }
}