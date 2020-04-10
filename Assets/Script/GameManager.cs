using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;


namespace Interactive360 {

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;

        Scene scene;
        VideoPlayer video;
        Animator anim;
        Image fadeImage;

        AsyncOperation operation;

        [Header("Scene Management")]
        public string[] scenesToLoad;
        public string activeScene;

        [Space]
        [Header("UI Settings")]

        public bool useFade;
        public GameObject fadeOverlay;
        public GameObject ControlUI;
        public GameObject LoadingUI;


        void Awake()
        {
            if (instance == null)
            {
                //DontDestroyOnLoad(gameObject);
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            scene = SceneManager.GetActiveScene();
            activeScene = scene.buildIndex + " - " + scene.name;
        }

        public void SelectScene(string sceneToLoad)
        {
            if(useFade)
            {
                StartCoroutine(FadeOutAndIn(sceneToLoad));
            }
            else
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            //set the active scene to the next scene to load;
            activeScene = sceneToLoad;
        }

        IEnumerator FadeOutAndIn(string sceneToload)
        {
            //get references to animatr and image component
            anim = fadeOverlay.GetComponent<Animator>();
            fadeImage = fadeOverlay.GetComponent<Image>();

            //turn control UI off and loading UI on;
            ControlUI.SetActive(false);
            LoadingUI.SetActive(true);

            //set FadeOut to true on the animator so our image will fade out
            anim.SetBool("FadeOut", true);

            yield return new WaitUntil(() => fadeImage.color.a == 1);
            SceneManager.LoadScene(sceneToload);
            Scene scene = SceneManager.GetSceneByName(sceneToload);
            Debug.Log("loading scene: " + scene.name);
            yield return new WaitUntil(() => scene.isLoaded);

            video = FindObjectOfType<VideoPlayer>();
            yield return new WaitUntil(() => video.isPrepared);

            anim.SetBool("FadeOut", false);

            yield return new WaitUntil(() => fadeImage.color.a == 0);
            LoadingUI.SetActive(false);

            if (ControlUI)
            ControlUI.SetActive(true);
     
        }

        public void Pausevideo()
        { 
            if(!video)
            {
                video = FindObjectOfType<VideoPlayer>();
            }
            video.Play();
        }

    }
}











