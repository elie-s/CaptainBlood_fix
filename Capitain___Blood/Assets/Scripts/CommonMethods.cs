using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class CommonMethods
{
    public static List<T> ToList<T>(this T[] _array)
    {
        List<T> result = new List<T>();

        foreach (T item in _array)
        {
            result.Add(item);
        }

        return result;
    }

    public static List<T>[] NewListArray<T>(int _index)
    {
        List<T>[] result = new List<T>[_index];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = new List<T>();
        }

        return result;
    }

    public static T Last<T>(this T[] _array)
    {
        return _array[_array.Length - 1];
    }

    public static T Last<T>(this List<T> _list)
    {
        return _list[_list.Count - 1];
    }

    public static bool Contains<T>(this T[] _array, T _element) where T : System.IEquatable<T>
    {
        foreach (T item in _array)
        {
            if (item.Equals(_element)) return true;
        }

        return false;
    }

    public static T RandomValue<T> (this List<T> _list)
    {
        T result = _list[Random.Range(0, _list.Count)];
        _list.Remove(result);
        return result;
    }

    public static void DebugElements<T>(this List<T> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            Debug.Log(_list.ToString() + "'s item " + i + ": " + _list[i]);
        }
    }

    public static void RemoveMultiple<T>(this List<T> _list, params T[] _itemsToRemove)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            if (_list.Count == 0) return;

        }
    }

    public static void Fade(this SpriteRenderer _sprite, MonoBehaviour _monoBehaviour, bool _fadeAway, float _duration)
    {
        _monoBehaviour.StartCoroutine(FadeImage(_fadeAway));

        IEnumerator FadeImage(bool fadeAway)
        {
            // fade from opaque to transparent
            if (fadeAway)
            {
                // loop over 1 second backwards
                for (float i = 1; i >= 0; i -= (Time.deltaTime / _duration))
                {
                    // set color with i as alpha
                    _sprite.color = new Color(1, 1, 1, i);
                    yield return null;
                }
            }
            // fade from transparent to opaque
            else
            {
                // loop over 1 second
                for (float i = 0; i <= 1; i += (Time.deltaTime / _duration))
                {
                    // set color with i as alpha
                    _sprite.color = new Color(1, 1, 1, i);
                    yield return null;
                }
            }
        }
    }

    public static void Fade(this Image _sprite, MonoBehaviour _monoBehaviour, bool _fadeAway, float _duration)
    {
        
        float red = _sprite.color.r;
        float green = _sprite.color.g;
        float blue = _sprite.color.b;
        _monoBehaviour.StartCoroutine(FadeImage(_fadeAway));

        IEnumerator FadeImage(bool fadeAway)
        {
            // fade from opaque to transparent
            if (fadeAway)
            {
                // loop over 1 second backwards
                for (float i = 1; i >= 0; i -= (Time.deltaTime / _duration))
                {
                    // set color with i as alpha
                    _sprite.color = new Color(red, green, blue, i);
                    yield return null;
                }

                _sprite.color = new Color(red, green, blue, 0);
            }
            // fade from transparent to opaque
            else
            {
                // loop over 1 second
                for (float i = 0; i <= 1; i += (Time.deltaTime / _duration))
                {
                    // set color with i as alpha
                    _sprite.color = new Color(red, green, blue, i);
                    yield return null;
                }

                _sprite.color = new Color(red, green, blue, 1);
            }
        }
    }
}
