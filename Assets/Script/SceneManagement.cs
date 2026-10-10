using UnityEngine;

namespace Korakit
{

    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class SceneManagement : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private GameObject optionPanel;
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject lossPanel;

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

            if (lossPanel != null)
                lossPanel.SetActive(false);
        }

        public void ShowLossPanel()
        {
            if (lossPanel != null)
                lossPanel.SetActive(true);

            if (winPanel != null)
                winPanel.SetActive(false);
        }

        // Shared
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
