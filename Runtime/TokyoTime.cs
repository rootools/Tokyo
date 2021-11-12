using System;

namespace Tokyo {

    public static class TokyoTime {

        public static long CurrentTimestamp => new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
    }

}