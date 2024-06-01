using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button mainMenuBtn;
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button lastCheckpointBtn;
    public CheckPoints checkPoint;
    [SerializeField] private Button quitButton;
    // Start is called before the first frame update
    private void Awake()
    {
        mainMenuBtn.onClick.AddListener(() => {
            SceneManager.LoadScene(0);

        });
        restartBtn.onClick.AddListener(() => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        });
        lastCheckpointBtn.onClick.AddListener(() => {
            checkPoint.TeleportPlayer();

        });

        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
