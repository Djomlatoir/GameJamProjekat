using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button leaderBoardButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    // Start is called before the first frame update
    private void Awake()
    {
        playButton.onClick.AddListener(() =>{
            SceneManager.LoadScene(1);

        });
        leaderBoardButton.onClick.AddListener(() => {
            SceneManager.LoadScene(3);
        });
        settingsButton.onClick.AddListener(() => {
            SceneManager.LoadScene(4);
        });
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
    // Update is called once per frame
   
}
