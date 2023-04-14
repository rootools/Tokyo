using UnityEngine;

namespace Tokyo.Math.Collisions {

    public class TokyoCollisions {

        public static bool CircleRectangle(Vector2 circlePos, float circleRadius, Rect rect) {
            return CircleRectangle(circlePos, circleRadius, rect.min, rect.max);
        }
        
        public static bool CircleRectangle(Vector2 circlePos, float circleRadius, Bounds bounds) {
            return CircleRectangle(circlePos, circleRadius, bounds.min, bounds.max);
        }

        private static Vector2 _circleRectangleClosestPoint;

        private static bool CircleRectangle(Vector2 circlePos, float circleRadius, Vector2 min, Vector2 max) {
            _circleRectangleClosestPoint.x = circlePos.x;
            _circleRectangleClosestPoint.y = circlePos.y;

            _circleRectangleClosestPoint.x = (_circleRectangleClosestPoint.x < min.x) ? min.x : _circleRectangleClosestPoint.x;
            _circleRectangleClosestPoint.x = (_circleRectangleClosestPoint.x > max.x) ? max.x : _circleRectangleClosestPoint.x;

            _circleRectangleClosestPoint.y = (_circleRectangleClosestPoint.y < min.y) ? min.y : _circleRectangleClosestPoint.y;
            _circleRectangleClosestPoint.y = (_circleRectangleClosestPoint.y > max.y) ? max.y : _circleRectangleClosestPoint.y;

            _circleRectangleClosestPoint.x = circlePos.x - _circleRectangleClosestPoint.x;
            _circleRectangleClosestPoint.y = circlePos.y - _circleRectangleClosestPoint.y;
            
            return _circleRectangleClosestPoint.sqrMagnitude < circleRadius * circleRadius;
        }

    }

}