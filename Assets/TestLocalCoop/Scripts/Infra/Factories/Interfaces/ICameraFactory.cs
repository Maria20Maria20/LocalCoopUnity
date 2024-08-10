using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace TestLocalCoop.Scripts.Infra.Factories.Interfaces
{
    public interface ICameraFactory
    {
        public GameObject Camera { get; }
        public GameObject MainCamera { get; }
        public Task WarmUp();
        public Task<GameObject> Create(string layerMask);
        public Task<GameObject> CreateMainCamera(float rectViewportX, string nameMainMenu, string cullingMask);
    }
}
