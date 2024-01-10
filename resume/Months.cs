namespace BGSulz;

public enum Months
{
    Invalid = 0,
    January = 1,
    February = 2,
    March = 3,
    April = 4,
    May = 5,
    June = 6,
    July = 7,
    August = 8,
    September = 9,
    October = 10,
    November = 11,
    December = 12
}

public static class MonthsHelper
{
    public static Months ToMonth(this string self) => Enum.TryParse<Months>(self, out var res) ? res : Months.Invalid;
}