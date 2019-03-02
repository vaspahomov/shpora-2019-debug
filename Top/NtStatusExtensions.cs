namespace Top
{
    internal static class NtStatusExtensions
    {
        public static bool IsSuccessful(this NtStatus status) => (uint) status <= int.MaxValue;
    }
}