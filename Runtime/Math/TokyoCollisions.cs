using UnityEngine;

namespace Tokyo.Math.Collisions {

    public class TokyoCollisions {

        public static bool CircleRectangle(Vector2 circlePos, float circleRadius, Rect rect) {
            return CircleRectangle(circlePos, circleRadius, rect.min, rect.max);
        }
        
        public static bool CircleRectangle(Vector2 circlePos, float circleRadius, Bounds bounds) {
            return CircleRectangle(circlePos, circleRadius, bounds.min, bounds.max);
        }

        private static bool CircleRectangle(Vector2 circlePos, float circleRadius, Vector2 min, Vector2 max) {
            Vector2 closestPoint = circlePos;

            closestPoint.x = (closestPoint.x < min.x) ? min.x : closestPoint.x;
            closestPoint.x = (closestPoint.x > max.x) ? max.x : closestPoint.x;

            closestPoint.y = (closestPoint.y < min.y) ? min.y : closestPoint.y;
            closestPoint.y = (closestPoint.y > max.y) ? max.y : closestPoint.y;

            Vector2 offset = circlePos - closestPoint;
            return offset.sqrMagnitude < circleRadius * circleRadius;
        }

    }

}