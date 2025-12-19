using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private IScreen[] _tutorialScreens;
    public int index = 0;

    void Start()
    {
        ScreenManager.instance.Push(_tutorialScreens[index]);
    }

    private void Update()
    {
        
    }

    public void NextScreen()
    {
        index++;
        ScreenManager.instance.Push(_tutorialScreens[index]);
    }

}
