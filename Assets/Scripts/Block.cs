using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private TextMeshPro text;

    private int hitsRemaining = 30;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<TextMeshPro>();
        UpdateVisualState();
    }

    private void UpdateVisualState()
    {
        text.SetText(hitsRemaining.ToString());
        spriteRenderer.color = Color.Lerp(Color.white, Color.red, hitsRemaining / 30f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (--hitsRemaining > 0)
            UpdateVisualState();
        else
            Destroy(gameObject);
    }
}
