using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts.SupportItems
{
    public interface IInteractive
    {
        void Interact(GameObject target);
    }
}
