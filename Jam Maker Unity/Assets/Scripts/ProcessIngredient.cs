using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessIngredient : MonoBehaviour
{
    [SerializeField] GameObject[] buttonGroups;
    [SerializeField] AudioClip click;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.GM.UpdateIngredientSelection.AddListener(ResetIngredient);
    }

    public void ResetIngredient()
    {
        for (int i = 0; i < 3; i++)
        {
            if (GameManager.GM.GetIngredientAtIndex(i) != null)
            {
                buttonGroups[i].SetActive(true);
            }
        }
    }

    public void Smash(int index)
    {
        ClickSound();
        buttonGroups[index].gameObject.SetActive(false);
        GameManager.GM.ProcessIngredient(IngredientType.Smashed, index);
    }

    public void Juice(int index)
    {
        ClickSound();
        buttonGroups[index].gameObject.SetActive(false);
        GameManager.GM.ProcessIngredient(IngredientType.Juiced, index);
    }

    public void ClickSound()
    {
        AudioManager.AM.PlayEffect(click);
    }

}

[System.Serializable]
public enum ButtonProcessType
{
    Smash, Juice
}
