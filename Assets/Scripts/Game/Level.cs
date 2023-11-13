using Game.Part;
using UnityEngine;

namespace Game
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private PartSlot[] slots;

        public bool CheckSlots()
        {
            foreach (var slot in slots)
            {
                if (!slot.isFull) return false;
            }
            return true;
        }
    }
}