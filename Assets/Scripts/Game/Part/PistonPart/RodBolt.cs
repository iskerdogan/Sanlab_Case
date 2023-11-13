using DG.Tweening;
using UnityEngine;

namespace Game.Part.PistonPart
{
    public class RodBolt : Mountable
    {
        [SerializeField] private PartSlot targetSlot2;
        [SerializeField] private Transform mountParent;
        [SerializeField] private Transform demountParent;
        
        protected override void Mount()
        {
            base.Mount();
            transform.SetParent(mountParent);
        }

        protected override void Demount()
        {
            transform.SetParent(demountParent);
            base.Demount();
        }

        protected override bool CheckPartMatch(PartSlot partSlot)
        {
            return targetSlot == partSlot || targetSlot2 == partSlot;
        }
        
    }
}