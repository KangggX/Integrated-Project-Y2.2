using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramGlitch : MonoBehaviour
{
    public float _minGlitch;
    public float _maxGlitch;
    public float _glitchLength;
    public float _timeBetweenGlitches;

    Renderer _renderer;
    Material _mainMaterial;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _mainMaterial = _renderer.material;
    }

    private void Start()
    {
        StartCoroutine(Glitch());
    }

    IEnumerator Glitch()
    {
        

        yield return new WaitForSeconds(_timeBetweenGlitches);

        _mainMaterial.SetFloat("_GlitchStrength", Random.Range(_minGlitch, _maxGlitch));

        yield return new WaitForSeconds(_glitchLength);

        _mainMaterial.SetFloat("_GlitchStrength", 0f);

        StartCoroutine(Glitch());
    }
}