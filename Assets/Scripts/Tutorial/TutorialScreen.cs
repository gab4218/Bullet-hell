using System.Collections;
using UnityEngine;

public class TutorialScreen : MonoBehaviour, IScreen
{
    public bool skippable = true;
    public void Activate()
    {
        return;
    }

    public void Deactivate()
    {
        return;
    }

    public void Free()
    {
        Destroy(gameObject);
    }

    public void Next()
    {
        StartCoroutine(WaitNext());
    }

    private IEnumerator WaitNext()
    {
        yield return new WaitForEndOfFrame();
        GoNext();
    }

    private void GoNext()
    {
        ScreenManager.instance.Pop();
        TutorialManager.instance.NextScreen();
    }

    public void End()
    {
        AsyncLoadManager.instance.LoadScene("Menu");
    }

    private void Update()
    {
        if (!skippable) return;
        if ((Input.anyKeyDown || (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began))) ScreenManager.instance.Pop();
    }
}
