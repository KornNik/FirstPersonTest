using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public struct SymbolsTextureData
{
    //Ссылка на атлас шрифта
    public Texture texture;
    //Массив набора символов по порядку, начиная с левого-верхнего угла
    public char[] chars;

    //Dictionary с координатами каждого символа - номер строки и столбца
    private Dictionary<char, Vector2> charsDict;

    public void Initialize()
    {
        charsDict = new Dictionary<char, Vector2>();
        for (int i = 0; i < chars.Length; i++)
        {
            var c = char.ToLowerInvariant(chars[i]);
            if (charsDict.ContainsKey(c)) continue;
            //Расчет координат символа, преобразуем порядковый номер символа
            //в номер строки и столбца, зная, что длина строки равна 10.
            var uv = new Vector2(i % 10, 9 - i / 10);
            charsDict.Add(c, uv);
        }
    }

    public Vector2 GetTextureCoordinates(char c)
    {
        c = char.ToLowerInvariant(c);
        if (charsDict == null) Initialize();

        if (charsDict.TryGetValue(c, out Vector2 texCoord))
            return texCoord;
        return Vector2.zero;
    }
}
