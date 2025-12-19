using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoadManager : MonoBehaviour
{
    public static AsyncLoadManager instance;

    private void Awake()
    {
        if (instance == null && instance != this)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    [SerializeField] private Image loadingScreenImage, loadingBarImage;
    private bool loadingScene;


    private void Start()
    {
        loadingBarImage.gameObject.SetActive(false);
        loadingScreenImage.gameObject.SetActive(false);
    }

    public void LoadScene(string scene)
    {
        if (!loadingScene)
        {
            loadingScreenImage.gameObject.SetActive(true);
            loadingBarImage.gameObject.SetActive(true);
            loadingScene = true;
            ScreenManager.instance.Push("Loader");
            StartCoroutine(AsyncLoad(scene));
        }
    }


    private IEnumerator AsyncLoad(string scene)
    {

        AsyncOperation aOp = SceneManager.LoadSceneAsync(scene);
        aOp.allowSceneActivation = false;
        while (aOp.progress < 0.9f)
        {
            loadingBarImage.fillAmount = aOp.progress / 0.9f;
            Debug.Log("As");
            yield return null;
        }
        loadingBarImage.fillAmount = 1;
        loadingBarImage.gameObject.SetActive(false);
        loadingScreenImage.gameObject.SetActive(false);
        loadingScene = false;
        aOp.allowSceneActivation = true;
    }
}
