using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RetroJam.CaptainBlood.CursorLib
{
    [System.Serializable]
    public class ButtonBound
    {
        public Vector2 position;

        public static bool operator <(ButtonBound a, Vector3 b) { return a.position.x < b.x && a.position.y < b.y; }
        public static bool operator >(ButtonBound a, Vector3 b) { return a.position.x > b.x && a.position.y > b.y; }
        public static bool operator <(ButtonBound a, Vector2Int b) { return a.position.x < b.x && a.position.y < b.y; }
        public static bool operator >(ButtonBound a, Vector2Int b) { return a.position.x > b.x && a.position.y > b.y; }
        public static bool operator <(Vector3 a, ButtonBound b) { return a.x < b.position.x && a.y < b.position.y; }
        public static bool operator >(Vector3 a, ButtonBound b) { return a.x > b.position.x && a.y > b.position.y; }
        public static bool operator <(Vector2Int a, ButtonBound b) { return a.x < b.position.x && a.y < b.position.y; }
        public static bool operator >(Vector2Int a, ButtonBound b) { return a.x > b.position.x && a.y > b.position.y; }
    }

    [System.Serializable]
    public class Button
    {
        public ButtonBound bottomLeft;
        public ButtonBound upRight;

        public Button(Vector2 _bl, Vector2 _ur)
        {
            bottomLeft = new ButtonBound();
            upRight = new ButtonBound();

            bottomLeft.position = _bl;
            upRight.position = _ur;
        }

        public bool IsCursorOver(Transform _cursor)
        {
            return _cursor.position > bottomLeft && _cursor.position < upRight;
        }
    }
}