namespace Tokyo.Math {

    public static class TokyoMath {

        public static float Remap(float input, float inMin, float inMax, float outMin, float outMax) {
            return outMin + (input - inMin) * (outMax - outMin) / (inMax - inMin);
        }

    }

}