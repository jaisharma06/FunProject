using UnityEngine;

namespace FunProject.Characters.Liam
{
    [CreateAssetMenu(fileName = "LiamData", menuName = "Data/Liam Data/Base Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Movement")]
        public float walkSpeed = 7f;
        public float runSpeed = 14f;

        [Header("Ground")]
        public float groundCheckRadius = 0.3f;
        public LayerMask groundLayer;

        [Header("Jump")]
        public float jumpForce = 10f;
    }
}
