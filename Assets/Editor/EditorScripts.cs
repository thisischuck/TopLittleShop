using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.ServiceModel.Discovery.Version11;

public class EditorScripts : MonoBehaviour
{
    [MenuItem("Helpers/Give id to GameItems")]
    static void IdScriptableObjectsByName()
    {
        foreach (Object o in Selection.objects)
        {
            if (o is ScriptableObject)
            {
                GameItem sc = AssetDatabase.LoadAssetAtPath<GameItem>(AssetDatabase.GetAssetPath(o));
                sc.id = sc.name;
            }
            else
            {
                Debug.LogError("This is not a scritable object");
                Debug.Log(o.GetType());
            }
        }
    }

    [MenuItem("Helpers/GiveSpriteToSc")]
    static void GiveSpriteToSc()
    {
        foreach (Object o in Selection.objects)
        {
            if (o is ScriptableObject)
            {
                GameItem sc = AssetDatabase.LoadAssetAtPath<GameItem>(AssetDatabase.GetAssetPath(o));
                var path = "Assets/Mighty Heroes (Rogue) 2D Fantasy Characters Pack/Rogue/" + sc.itemSprite.name.Replace("01", "03") + ".png";
                Sprite s = AssetDatabase.LoadAssetAtPath<Sprite>(path);
                if (s)
                    sc.itemSprite = s;
                else
                {
                    print(path);
                    print(s);
                }
            }
            else
            {
                Debug.LogError("This is not a scritable object");
                Debug.Log(o.GetType());
            }
        }
    }
}
