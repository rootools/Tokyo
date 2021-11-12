using UnityEngine;

namespace Tokyo.Math.Collisions {

    public class TokyoCollisions {

        public static bool CircleRectangle(Vector2 circlePos, float circleRadius, Rect rect) {
            Vector2 closestPoint = circlePos;

            closestPoint.x = (closestPoint.x < rect.min.x) ? rect.min.x : closestPoint.x;
            closestPoint.x = (closestPoint.x > rect.max.x) ? rect.max.x : closestPoint.x;

            closestPoint.y = (closestPoint.y < rect.min.y) ? rect.min.y : closestPoint.y;
            closestPoint.y = (closestPoint.y > rect.max.y) ? rect.max.y : closestPoint.y;

            Vector2 offset = circlePos - closestPoint;
            return offset.sqrMagnitude < circleRadius * circleRadius;
        }

    }

}