using System.Collections;
using UnityEngine;

public class MaterialAnimation : MonoBehaviour
{
    const float FRAME_RATE = 3;
    [SerializeField] Texture[] textures;
    [SerializeField] Renderer myRenderer;

    Coroutine _coroutine;

    private void Start()
    {
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
            myRenderer.material.mainTexture = textures[textureIndex];
            yield return new WaitForSeconds(1f/FRAME_RATE);
            textureIndex = (textureIndex + 1) % textures.Length;
        }
    }
}
