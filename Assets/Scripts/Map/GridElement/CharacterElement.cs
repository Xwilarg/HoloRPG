using HoloRPG.Character;

namespace HoloRPG.Map.GridElement
{
    public class CharacterElement : IGridElement
    {
        public CharacterElement(CharacterImpl charac)
            => _me = charac;

        private CharacterImpl _me;
    }
}
