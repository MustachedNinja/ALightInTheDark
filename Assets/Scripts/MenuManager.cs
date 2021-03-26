using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel = null;
    [SerializeField] private GameObject levelSelectPanel = null;

    void Start() {
        mainMenuPanel.SetActive(true);
        levelSelectPanel.SetActive(false);
    }

    public void PlayLevel(int level) {
        SceneManager.LoadScene(level);
    }

    public void QuitGame() {
        // save any game data here
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
