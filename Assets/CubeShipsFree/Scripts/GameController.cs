using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CubeSpaceFree
{
    public class GameController : MonoBehaviour
    {
        public GameObject[] hazards;        // list of hazards (enemies) to spawn
        public Vector3 spawnValues;         // holds position of spawn points.  Use the x position to specify left and rightmost position
        public int itemPerHazardCount = 5;  // how many hazard to spawn per hazard type
        public float spawnWait;             // how long to wait per spawn
        public float startWait;             // how long to wait at the beginning of the game before spawning enemies
        public float waitWave;              // how long to wait before spawning the next wave

        public Text scoreText;
        public Button restartButton;
        public Text gameOverText;
        public bool isGameOver = false;
        public int score;

        // Use this for initialization
        void Start()
        {
            score = 0;

            if (restartButton != null)
                restartButton.gameObject.SetActive(false);
            else
                Debug.LogError("restartButton não está atribuído!");

            if (gameOverText != null)
                gameOverText.gameObject.SetActive(false);
            else
                Debug.LogError("gameOverText não está atribuído!");

            StartCoroutine(SpawnWaves());
            UpdateScore();
        }

        public void Update()
        {
            // pause key
            if (Input.GetKeyDown(KeyCode.F1))
            {
                Time.timeScale = 0;
            }
            else if (Input.GetKeyDown(KeyCode.F2))
            {
                Time.timeScale = 1;
            }
        }

        public void Restart()
        {
            Application.LoadLevel(Application.loadedLevelName);
        }

        IEnumerator SpawnWaves()
        {
            yield return new WaitForSeconds(startWait);
            while (true)
            {
                for (int i = 0; i < hazards.Length; i++)
                {
                    GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                    for (int j = 0; j < itemPerHazardCount; j++)
                    {
                        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(hazard, spawnPosition, spawnRotation);
                        yield return new WaitForSeconds(spawnWait);
                    }
                }

                yield return new WaitForSeconds(waitWave);

                if (isGameOver)
                {
                    break;
                }
            }
        }

        // Add score
        public void AddScore(int newScoreValue)
        {
            score += newScoreValue;
            UpdateScore();
        }

        // Update score display
        void UpdateScore()
        {
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score;
            }
            else
            {
                Debug.LogError("scoreText não está atribuído!");
            }
        }

        public void GameOver()
        {
            if (restartButton != null)
                restartButton.gameObject.SetActive(true);
            else
                Debug.LogError("restartButton não está atribuído!");

            if (gameOverText != null)
                gameOverText.gameObject.SetActive(true);
            else
                Debug.LogError("gameOverText não está atribuído!");
        }
    }
}
