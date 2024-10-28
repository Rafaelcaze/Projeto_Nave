using UnityEngine;
using System.Collections;


namespace CubeSpaceFree
{
  public class SpawnEnemy : MonoBehaviour
  {
    [SerializeField]
    private GameObject Enemy;

    [SerializeField]
    private float IntervaloMin;
    [SerializeField]
    private float IntervaloMax;

    private float cont;

    void Start()
    {
        cont = 0;
    }

    void Update()
    {
        cont += Time.deltaTime;

        if (cont > IntervaloMin)
        {
            float varX;
            int aux = Random.Range(0, 2);

            if (aux == 0)
            {
                varX = -6.5f;
            }
            else
            {
                varX = 6.5f;
            }

            Vector3 PosicaoCriacao = new Vector3(varX, transform.position.y, transform.position.z);
            Instantiate(Enemy, PosicaoCriacao, transform.rotation);
            cont = 0;
        }
    }
  }  
}
