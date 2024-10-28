using UnityEngine;
using System.Collections;


namespace CubeSpaceFree
{
    public class Bullet : MonoBehaviour
    {
 
        public void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}