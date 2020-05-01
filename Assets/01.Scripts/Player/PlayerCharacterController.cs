using System.Collections;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField]
    private Sprite idleSprite;
    [SerializeField]
    private Sprite[] sprites;

    private new SpriteRenderer renderer;
    private IEnumerator recoverSprite;
    private WaitForSeconds wait = new WaitForSeconds(0.5F);

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite(int key)
    {
        renderer.sprite = sprites[key];

        if (recoverSprite != null)
        {
            StopCoroutine(recoverSprite);
        }

        recoverSprite = RecoverSprite();
        StartCoroutine(recoverSprite);
    }

    private IEnumerator RecoverSprite()
    {
        yield return wait;
        renderer.sprite = idleSprite;
    }
}
