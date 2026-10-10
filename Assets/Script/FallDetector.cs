using UnityEngine;

namespace Korakit
{
    public class FallDetector : MonoBehaviour
    {
        [SerializeField] private float fallLimitY = -10f;

        private bool hasFallen = false;

        private void Update()
        {
            if (hasFallen) return;

            if (transform.position.y < fallLimitY)
            {
                hasFallen = true;

                SceneManagement manager = FindFirstObjectByType<SceneManagement>();

                if (manager != null)
                    manager.LoseGame();
            }
        }
    }
}
