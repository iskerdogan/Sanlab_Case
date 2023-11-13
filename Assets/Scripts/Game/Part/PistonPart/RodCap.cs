using UnityEngine;

namespace Game.Part.PistonPart
{
    public class RodCap : Mountable
    {
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
    }
}