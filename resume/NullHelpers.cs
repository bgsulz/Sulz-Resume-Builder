namespace BGSulz;

public static class NullHelpers
{
    public static TResult DoOrFallback<T, TResult>(this T? self, Func<T, TResult> transformer, TResult fallback) 
        where T : struct
    {
        return self.HasValue ? transformer(self.Value) : fallback;
    }
}