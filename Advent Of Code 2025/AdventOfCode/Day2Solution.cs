using AdventOfCode.Framework.Solutions;

namespace AdventOfCode;

[Solution(Day = 2)]
internal class Day2Solution : ISolution
{
    /// <inheritdoc />
    public string SolveProblem1(ProblemInput input, ISolutionLogger logger)
    {
        var ranges = ParseRanges(input.Raw);

        var invalidIdSum = 0L;

        foreach (var range in ranges)
        {
            for (var id = range.Start; id <= range.End; id++)
            {
                var idDigitCount = GetDigitCount(id);
                var repeatingDigitCount = idDigitCount / 2;

                if (idDigitCount % repeatingDigitCount != 0)
                {
                    continue;
                }

                if (IsRepeating(id, repeatingDigitCount))
                {
                    invalidIdSum += id;
                }
            }
        }

        return invalidIdSum.ToString();
    }

    /// <inheritdoc />
    public string SolveProblem2(ProblemInput input, ISolutionLogger logger)
    {
        var ranges = ParseRanges(input.Raw);

        var invalidIdSum = 0L;

        foreach (var range in ranges)
        {
            for (var id = range.Start; id <= range.End; id++)
            {
                var digitCount = GetDigitCount(id);

                for (var repeatingDigitCount = 1; repeatingDigitCount < digitCount; repeatingDigitCount++)
                {
                    if (digitCount % repeatingDigitCount != 0)
                    {
                        continue;
                    }

                    if (IsRepeating(id, repeatingDigitCount))
                    {
                        invalidIdSum += id;
                        break;
                    }
                }
            }
        }

        return invalidIdSum.ToString();
    }

    private static int GetDigitCount(long l)
    {
        return (int)double.Floor(double.Log10(l) + 1);
    }

    private static bool IsRepeating(long id, int digits)
    {
        var previousTrimmed = TrimEnd(id, digits, out var remaining);

        while (remaining != 0)
        {
            var trimmed = TrimEnd(remaining, digits, out remaining);
            if (trimmed != previousTrimmed)
            {
                return false;
            }
        }

        return true;
    }

    private static long TrimEnd(long l, int digits, out long remaining)
    {
        var factor = checked((long)double.Pow(10, digits));

        remaining = l / factor;
        return l - remaining * factor;
    }

    private static IEnumerable<ProductIdRange> ParseRanges(string ranges)
    {
        var rangesSplit = ranges.Split(",");

        foreach (var r in rangesSplit)
        {
            var rangeSplit = r.Split('-');

            var start = long.Parse(rangeSplit[0]);
            var end = long.Parse(rangeSplit[1]);

            yield return new ProductIdRange(start, end);
        }
    }

    private readonly record struct ProductIdRange(long Start, long End);
}