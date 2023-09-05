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
        
        public static Vector2? IntersectLines(Vector2 l1pa, Vector2 l1pb, Vector2 l2pa, Vector2 l2pb) {
            float denominator = ((l1pb.x - l1pa.x) * (l2pb.y - l2pa.y)) - ((l1pb.y - l1pa.y) * (l2pb.x - l2pa.x));

            if (denominator == 0) // lines are parallel and will never intersect
                return null;

            float numerator1 = ((l1pa.y - l2pa.y) * (l2pb.x - l2pa.x)) - ((l1pa.x - l2pa.x) * (l2pb.y - l2pa.y));
            float numerator2 = ((l1pa.y - l2pa.y) * (l1pb.x - l1pa.x)) - ((l1pa.x - l2pa.x) * (l1pb.y - l1pa.y));
    
            if (numerator1 == 0 || numerator2 == 0) // lines are coincident and intersect everywhere
                return null;

            float r = numerator1 / denominator;
            float s = numerator2 / denominator;

            // lines intersect each other
            if ((r >= 0 && r <= 1) && (s >= 0 && s <= 1)) {
                float x = l1pa.x + (r * (l1pb.x - l1pa.x));
                float y = l1pa.y + (r * (l1pb.y - l1pa.y));
        
                return new Vector2(x, y);
            } else {
                return null;
            }
        }

    }

}