using System.Collections;
using UnityEngine;

public class AOE : MonoBehaviour
{
    [SerializeField] private float _lifetime = 1f;
    [SerializeField] private int _damage = 1;
    private Pool<AOE> _parent;

    public static void SetState(AOE a, bool state)
    {
        a.gameObject.SetActive(state);
        if (state)
        {
            a.StartCoroutine(a.AOEtimer());
        }
    }

    public void SetCreator(Pool<AOE> p)
    {
        _parent = p;
    }

    public IEnumerator AOEtimer()
    {
        float t = 0;
        while (t < _lifetime)
        {
            t += Time.deltaTime;
            yield return null;
        }
        _parent.Return(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IHittable hit)) hit.OnHit(_damage);
    }
}
