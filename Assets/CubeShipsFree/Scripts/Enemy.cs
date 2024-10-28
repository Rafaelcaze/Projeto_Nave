using UnityEngine;
using System.Collections;


namespace CubeSpaceFree
{
        // Stub class to make it easier to check object type during collision (can be otpimized by using Tag instead).
        public class Enemy : MonoBehaviour
    {
       
        void Morrer()
        {
            // Adicione a lógica para destruir o inimigo
            Destroy(gameObject);
        }
    }
}
