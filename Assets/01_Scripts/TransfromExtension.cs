using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheGreatGGM
{
    public static class TransfromExtension
    {
        public static float GGM(this Transform target)
        {
            Vector3 pos = target.position;
            return pos.x + pos.y + pos.z;
        }
    }
}
