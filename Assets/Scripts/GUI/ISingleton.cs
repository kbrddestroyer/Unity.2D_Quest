using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommonInterfaces
{
    public interface ISingleton
    {
        public static ISingleton Instance { get; protected set; }
    }
}