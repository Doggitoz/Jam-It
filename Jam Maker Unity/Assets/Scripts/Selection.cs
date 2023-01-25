using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public int index;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        GameManager.GM.UpdateIngredientSelection.AddListener(UpdateVisuals);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void UpdateVisuals()
    {
        if (GameManager.GM.GetIngredientAtIndex(index) == null)
        {
            spriteRenderer.sprite = null;
            return;
        }
        spriteRenderer.sprite = GameManager.GM.GetIngredientAtIndex(index).Sprite;
    }

    private void OnMouseDown()
    {
        if (spriteRenderer != null)
        {
            GameManager.GM.RemoveIngredient(index);
        }
    }

}
