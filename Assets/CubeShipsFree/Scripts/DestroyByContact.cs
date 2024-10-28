using UnityEngine;

namespace CubeSpaceFree
{
    public class DestroyByContact : MonoBehaviour
    {
        public GameObject explosion;
        public GameObject playerExplosion;
        public int scoreValue;
        public int enemyHealth = 1;
        public int playerHealth = 1; // Defina como 1 para teste inicial
        private static GameController gameController;
        private bool isVisible = false;

        void Start()
        {
            if (!gameController)
            {
                gameController = GameObject.FindObjectOfType<GameController>();
                if (gameController != null)
                    Debug.Log("GameController encontrado!");
                else
                    Debug.LogError("GameController não encontrado!");
            }
        }

        void OnBecameInvisible()
        {
            isVisible = false;
        }

        void OnBecameVisible()
        {
            isVisible = true;
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("Colisão detectada com: " + other.name);

            // Ignora colisão entre balas
            if (this.GetComponent<Bullet>() && other.GetComponent<Bullet>())
                return;

            // Ignora colisão com Boundary
            if (other.name == "Boundary")
                return;

            // Ignora colisão entre inimigos e balas inimigas
            if ((other.GetComponent<Enemy>() && this.GetComponent<EnemyBullet>()) ||
                (other.GetComponent<EnemyBullet>() && this.GetComponent<Enemy>()))
                return;

            // Instancia a explosão para o objeto atual
            if (explosion)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Debug.Log("Explosão instanciada.");
            }

            // Verifica colisão com o Player
            if (other.CompareTag("Player"))
            {
                Debug.Log("Colisão com Player detectada.");
                HandlePlayerCollision(other);
            }

            // Verifica colisão com o Inimigo e reduz a vida se for atingido por um disparo
            if (this.GetComponent<Enemy>() && other.GetComponent<Bullet>())
            {
                enemyHealth--;
                Debug.Log("Vida do inimigo: " + enemyHealth);
                
                if (enemyHealth <= 0)
                {
                    Debug.Log("Inimigo destruído.");
                    gameController.AddScore(scoreValue);
                    Destroy(gameObject, 0.1f);
                }
            }

            // Destrói o objeto de disparo ao contato
            if (other.GetComponent<Bullet>())
            {
                Destroy(other.gameObject);
                Debug.Log("Disparo destruído.");
            }
        }

        private void HandlePlayerCollision(Collider player)
        {
            playerHealth--;
            Debug.Log("Player foi atingido! Vida restante: " + playerHealth);

            // Instancia a explosão do jogador no local correto
            if (playerExplosion)
            {
                Instantiate(playerExplosion, player.transform.position, player.transform.rotation);
                Debug.Log("Explosão do jogador instanciada.");
            }

            // Verifica se a vida do jogador chegou a zero
            if (playerHealth <= 0)
            {
                Debug.Log("Vida do Player chegou a 0, destruindo...");

                // Notifica o GameController e destrói o jogador
                if (gameController != null)
                {
                    gameController.GameOver();
                    Debug.Log("GameOver chamado.");
                }
                else
                {
                    Debug.LogError("GameController não encontrado!");
                }

                Destroy(player.gameObject); // Destrói o jogador diretamente
            }
        } 
    }
}
