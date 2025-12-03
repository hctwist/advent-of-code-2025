using AdventOfCode.Framework.Solutions;

namespace AdventOfCode;

[Solution(Day = 3)]
internal class Day3Solution : ISolution
{
    /// <inheritdoc />
    public string SolveProblem1(ProblemInput input, ISolutionLogger logger)
    {
        var banks = ParseBanks(input.Lines);
        return CalculateMaximumOutputJoltage(banks, 2).ToString();
    }

    /// <inheritdoc />
    public string SolveProblem2(ProblemInput input, ISolutionLogger logger)
    {
        var banks = ParseBanks(input.Lines);
        return CalculateMaximumOutputJoltage(banks, 12).ToString();
    }

    private static long CalculateMaximumOutputJoltage(IEnumerable<IReadOnlyList<int>> banks, int batteryCount)
    {
        var outputJoltage = 0L;

        foreach (var bank in banks)
        {
            var joltage = 0L;

            var remainingBatteryCount = batteryCount;
            var cursor = 0;

            while (remainingBatteryCount > 0)
            {
                var maxIndex = cursor;

                for (var i = cursor + 1; i <= bank.Count - remainingBatteryCount; i++)
                {
                    if (bank[i] > bank[maxIndex])
                    {
                        maxIndex = i;
                    }
                }

                joltage = joltage * 10 + bank[maxIndex];
                remainingBatteryCount--;
                cursor = maxIndex + 1;
            }

            outputJoltage += joltage;
        }

        return outputJoltage;
    }

    private static IEnumerable<IReadOnlyList<int>> ParseBanks(IEnumerable<string> input)
    {
        return input.Select(l => l.Select(c => c - '0').ToList());
    }
}