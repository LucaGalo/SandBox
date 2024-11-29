using System.Collections;
using UnityEngine;

public class MaterialAnimation : MonoBehaviour
{
    [SerializeField] Texture[] textures;
    [SerializeField] Renderer myRenderer;
    [SerializeField] float fpm;

    Coroutine _coroutine;

    private void Start()
    {
        StopAllCoroutines();
        StartAnimation();
    }

    public void StartAnimation()
    {
        _coroutine = StartCoroutine(Animate());
    }

    public void StopAnimation()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    IEnumerator Animate()
    {
        int textureIndex = 0;
        while(true)
        {
            if(myRenderer != null && textures.Length > 0)
                myRenderer.sharedMaterial.mainTexture = textures[textureIndex];
            yield return new WaitForSeconds(60f/fpm);
            textureIndex = (textureIndex + 1) % textures.Length;
        }
    }
}
