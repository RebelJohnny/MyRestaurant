namespace MyRestaurant.Framework.Helpers
{
    public sealed class TimestampIdGenerator : ITimestampIdGenerator
    {
        private readonly Lock @lock = new();

        private long _lastTimestamp;
        private int _sequence;

        public long NextId()
        {
            lock (@lock)
            {
                long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                if (timestamp == _lastTimestamp)
                {
                    _sequence++;
                }
                else
                {
                    _sequence = 0;
                    _lastTimestamp = timestamp;
                }

                // Reserve the last 10 bits for the sequence
                return (timestamp << 10) | (uint)_sequence;
            }
        }
    }
}
