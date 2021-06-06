using System.Collections.Generic;
using UnityEngine;

namespace HoloRPG.Character
{
    public class CharacterManager : MonoBehaviour
    {
        private readonly List<CharacterImpl> _characters = new();

        private void Start()
        {
            _characters.Add(new CharacterImpl(2, 2, 1));
            _characters.Add(new CharacterImpl(2, 3, 1));
            _characters.Add(new CharacterImpl(10, 10, 2));
        }
    }
}
