using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Korakit
{

    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class SceneManagement : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private GameObject optionPanel;
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;

        [SerializeField] private TMP_Text winScoreText;
        [SerializeField] private TMP_Text loseScoreText;

        private bool gameEnded = false;

        // Main Menu
        public void StartGame()
        {
            LoadScene("StageSelection");
        }

        public void OpenOption()
        {
            if (optionPanel != null)
                optionPanel.SetActive(true);
        }

        public void CloseOption()
        {
            if (optionPanel != null)
                optionPanel.SetActive(false);
        }

        public void ExitGame()
        {
            Application.Quit();

            Debug.Log("Exit Game");
        }

        // Stage Selection
        public void SelectStage1()
        {
            LoadScene("Capsule Player");
        }
        public void SelectStage2()
        {
            LoadScene("Humanoid Player");
        }

        public void BackToMainMenu()
        {
            LoadScene("mainmenu");
        }

        // Gameplay
        public void BackToStageSelection()
        {
            LoadScene("StageSelection");
        }

        public void ShowWinPanel()
        {
            if (winPanel != null)
                winPanel.SetActive(true);

            if (losePanel != null)
                losePanel.SetActive(false);
        }

        public void ShowLosePanel()
        {
            if (losePanel != null)
                losePanel.SetActive(true);

            if (winPanel != null)
                winPanel.SetActive(false);
        }

        public void WinGame()
        {
            if (gameEnded) return;

            gameEnded = true;
            UpdateResultScore(winScoreText);
            ShowWinPanel();
        }
        public void LoseGame()
        {
            if (gameEnded) return;

            gameEnded = true;
            UpdateResultScore(loseScoreText);
            ShowLosePanel();
        }
        private void UpdateResultScore(TMP_Text scoreText)
        {
            if (scoreText == null)
                return;

            int finalScore = ScoreManager.Instance != null
                ? ScoreManager.Instance.score
                : 0;

            scoreText.text = "Score: " + finalScore;
        }

        // Shared
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
