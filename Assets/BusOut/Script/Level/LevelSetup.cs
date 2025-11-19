using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bus
{
    public class LevelSetup : MonoSingleton<LevelSetup>
    {
        public List<Material> materials;
        public Material GetMaterialByIdColor(int id)
        {
            return materials[id % materials.Count];
        }

    }
}
