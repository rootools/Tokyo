﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace CoreToolkit {
    public static class CoreToolkitTime {

        public static long CurrentTimestamp {
            get {
                return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            }
        }
    }
}