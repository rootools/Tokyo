namespace CoreToolkit.Math {
    public static class CoreToolkitMath {

        public static float Remap(float input, float inMin, float inMax, float outMin, float outMax) {
            return outMin + (input - inMin) * (outMax - outMin) / (inMax - inMin);
        }
        
    }
}