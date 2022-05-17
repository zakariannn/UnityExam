using UnityEngine;

namespace Misc {

    public static class Helper {
        public static void SetAxisTowards(Directions axis, Transform t, Vector2 direction) {
            switch (axis) {
                case Directions.Up:
                    t.up = direction;
                    break;
                case Directions.Down:
                    t.up = -direction;
                    break;
                case Directions.Right:
                    t.right = direction;
                    break;
                case Directions.Left:
                    t.right = -direction;
                    break;
            }
        }
    }

    public enum Directions {
        Up,
        Right,
        Down,
        Left,
    }

}