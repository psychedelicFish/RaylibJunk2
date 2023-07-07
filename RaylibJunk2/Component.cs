using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaylibJunk2
{
    internal class Component
    {
        public GameObject parent { get; protected set; }
        public Component(GameObject parent)
        {
            this.parent = parent;
        }

        public virtual void Update() { }
    }
}
