using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenGO : IScreen
{
    Dictionary<Behaviour, bool> _priorState = new();

    public Transform _root;

    public ScreenGO(Transform root)
    {
        _root = root;
        _priorState = new();
    }

    public void Pop() => ScreenManager.instance.Pop();

    public void Activate()
    {
        Debug.Log("asda");
        if (_priorState == default) return;
        foreach (var entry in _priorState)
        {
            entry.Key.enabled = entry.Value;
            if (entry.Key.TryGetComponent(out Rigidbody2D rb)) rb.isKinematic = false;
        }
        //SoundSingleton.instance?.musicSource.UnPause();
        _priorState.Clear();
        
    }

    public void Deactivate()
    {
        _priorState = new();
        foreach (var b in _root.GetComponentsInChildren<Behaviour>())
        {
            Debug.Log("asas");
            if (b is Canvas || b is CanvasScaler || b is Image || b is Camera || b is TMP_Text || b is AudioSource || b is AudioListener) continue;
            _priorState.Add(b, b.enabled);
            b.enabled = false;
            if (b.TryGetComponent(out Rigidbody2D rb))
            {
                rb.velocity = Vector3.zero;
                rb.isKinematic = false;
            }


        }
        //SoundSingleton.instance?.musicSource.Pause();
    }

    public void Free()
    {
        GameObject.Destroy(_root.gameObject);
    }
}
