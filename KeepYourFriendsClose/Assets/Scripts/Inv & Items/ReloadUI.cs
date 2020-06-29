using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadUI : MonoBehaviour
{
    public GameObject reloadBar;
    public Transform innerBar;
    private bool showBar;
    private float timer = 0f;
    private float reloadTime = 0f;
    private float progress = 0f;

    private void Update()
    {
        if (showBar)
        {
            timer += Time.deltaTime;

            if (timer <= reloadTime)
            {
                progress = Mathf.Clamp01(timer / reloadTime - 0.05f);
                innerBar.localScale = new Vector3(progress, 0.7f, 1);
            }
            else
            {
                timer = 0f;
                progress = 0f;
                innerBar.localScale = new Vector3(0, 0.7f, 1);
                showBar = false;
            }
        }
    }

    public void ShowReloadBar(float _reloadTime)
    {
        reloadBar.SetActive(true);
        reloadTime = _reloadTime;
        showBar = true;
    }

    public void HideReloadBar()
    {
        reloadBar.SetActive(false);
        showBar = false;
        timer = 0f;
        progress = 0f;
        innerBar.localScale = new Vector3(0, 0.7f, 1);
    }
}
