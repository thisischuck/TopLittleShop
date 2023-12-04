using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeUI : MonoBehaviour
{
    public CraftRecipe recipe;
    public GameObject parentPrefab;

    void Start()
    {
        LoadRecipeItem(recipe);
    }

    void LoadRecipeItem(CraftRecipe recipe)
    {
        foreach (var item in recipe.recipe)
        {
            string id = item.Key;
            int value = item.Value;

            GameObject o = Instantiate(parentPrefab, this.transform);

            //find the sprite
            List<GameItem> items = new List<GameItem>(Resources.LoadAll<GameItem>("Data/GameItems/"));
            GameItem i = items.Find(x =>
            {
                return id == x.id;
            });
            o.GetComponentInChildren<Image>().sprite = i.itemSprite;
            o.GetComponentInChildren<TextMeshProUGUI>().text = value.ToString();
        }
    }
}
