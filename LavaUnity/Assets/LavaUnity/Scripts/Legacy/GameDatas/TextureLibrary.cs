using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TextureLibrary : ScriptableObject 
{
	public List<TextureItem> TextureItem;
	public Sprite DummySpr;
	public Texture2D DummyIcon;
	Dictionary<string, Sprite> TextureDic;

	Dictionary<string, Texture2D> Icon;

	public Sprite GetSprite(string key)
	{
		if(TextureDic == null)
		{
			TextureDic = new Dictionary<string, Sprite>();
			int len = TextureItem.Count;
			for(int i = 0; i < len; ++i)
			{
				TextureDic.Add(TextureItem[i].Key, TextureItem[i].Spr);
			}
		}

		Sprite rs;
		if(!TextureDic.TryGetValue(key, out rs))
		{
			rs = DummySpr;
		}

		return rs;
	}

	public Texture2D GetIcon(string key)
	{
		if(Icon == null)
		{
			return DummyIcon;
		}

		Texture2D rs;
		if(Icon.TryGetValue(key, out rs))
		{
			return rs;
		}
		else
		{
			return DummyIcon;
		}
	}

	public void AddIcon(string key, Texture2D texture)
	{
		if(Icon == null)
		{
			Icon = new Dictionary<string, Texture2D>();
		}

		if(!Icon.ContainsKey(key))
		{
			Icon.Add(key, texture);
		}
		else
		{
			Icon[key] = texture;
		}
	}

#if UNITY_EDITOR
    [MenuItem("Lava/Scriptable/Data/TextureLibrary")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<TextureLibrary>();
    }
#endif
}

[System.Serializable]
public class TextureItem
{
	public string Key;
	public Sprite Spr;

	public TextureItem(string key, Sprite spr)
	{
		Key = key;
		Spr = spr;
	}
}
