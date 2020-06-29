using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunItemUI : MonoBehaviour
{
    public GameObject ammoSprite;
    public ReloadUI reloadUI;
    private int clipSize;
    private int currentClipSize;

    [System.Serializable]
    public class BulletSprites
    {
        public Image bulletSprite;
    }

    public BulletSprites[] bullets;

    public void SetClipSize(int _clipSize)
    {
        clipSize = _clipSize;
        currentClipSize = clipSize;
    }

    public void EnableAmmoSprites() { ammoSprite.SetActive(true); }
    public void DisableAmmoSprites() { ammoSprite.SetActive(false); }

    public void DarkenOneBullet()
    {
        bullets[clipSize - currentClipSize].bulletSprite.color += new Color(-0.5f, -0.5f, -0.5f, 0);
        currentClipSize--;
    }

    public void ResetAllBullets()
    {
        currentClipSize = clipSize;

        for (int i = 0; i < clipSize; i++)
        {
            bullets[i].bulletSprite.color = new Color(1f, 1f, 1f, 0.883f);
        }
    }
}
