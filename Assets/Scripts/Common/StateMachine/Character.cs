using UnityEngine;

namespace FunProject.Characters.Common
{
    public interface ICharacter
    {
        float speed { get; set; }
        Animator _animator { get; set; }
        void UpdateSpeed();
        void LookInMovementDirection();
    }
}