using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField]
    private Sprite idleSprite;
    [SerializeField]
    private Sprite[] sprites;

    private new SpriteRenderer renderer;
    private Tween tween;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite(int key)
    {
        renderer.sprite = sprites[key];
        tween?.Kill();
        tween = DOVirtual.DelayedCall(0.5F, () => renderer.sprite = idleSprite);
    }
}
