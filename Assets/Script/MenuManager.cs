using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Interactive360
{
    public class MenuManager : MonoBehaviour
    {
        //0 is bookstore
        //1 is Kam;

        public GameObject m_menu;
        public Button[] m_buttonsInScene;
        public GameObject m_playButton;
        public GameObject m_pauseButton;
        private AudioSource m_menuToggleAudio;
       // [SerializeField] string m_oculusMenuToggle = "Button2";

        void Start()
        {
            
            BindButtonsToScenes();
            m_menuToggleAudio = GetComponent<AudioSource>();
        }
        

     
        /*
    void Update ()
    {
        checkForInput();
    }
    */
    

            
        public void toggleMenu()
        {
            m_menu.SetActive(!m_menu.activeInHierarchy);
        }
        
        /*
        private void checkForInput()
        {
            if(Input.GetButtonDown(m_oculusMenuToggle))
            {
                toggleMenu();
                if(m_menuToggleAudio)
                {
                    m_menuToggleAudio.Play();
                }
            }
        }
        */

        public void toggleButton()
        {
            m_pauseButton.SetActive(!m_pauseButton.activeInHierarchy);
            m_playButton.SetActive(!m_playButton.activeInHierarchy);
        }

        /*
         Quit the application.
         */
        public void quit()
        {
            Application.Quit();
        }

        
        public void BindButtonsToScenes()
        {
            if(m_buttonsInScene.Length != GameManager.instance.scenesToLoad.Length)
            {
                Debug.Log("Amount of buttons and scenes do not match!");
                return;
            } else
            {
                for (int i = 0; i < m_buttonsInScene.Length; i++)
                {
                    string sceneName = GameManager.instance.scenesToLoad[i];
                    m_buttonsInScene[i].onClick.AddListener(() => GameManager.instance.SelectScene(sceneName));
                }
            }
        }
        

    }
}

