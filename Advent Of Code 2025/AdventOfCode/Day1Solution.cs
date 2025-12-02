using AdventOfCode.Framework.Solutions;

namespace AdventOfCode;

[Solution(Day = 1)]
internal class Day1Solution : ISolution
{
    private static readonly int StartingDialValue = 50;
    private static readonly int MaxDialValue = 100;

    /// <inheritdoc />
    public string SolveProblem1(ProblemInput input, ISolutionLogger logger)
    {
        var rotations = ParseRotations(input.Lines);

        var dialValue = StartingDialValue;
        var zeroes = 0;

        foreach (var rotation in rotations)
        {
            dialValue += rotation;

            if (dialValue % MaxDialValue == 0)
            {
                zeroes++;
            }
        }

        return zeroes.ToString();
    }

    /// <inheritdoc />
    public string SolveProblem2(ProblemInput input, ISolutionLogger logger)
    {
        var rotations = ParseRotations(input.Lines);

        var dialValue = StartingDialValue;
        var zeroes = 0;

        foreach (var rotation in rotations)
        {
            var fullRotations = int.Abs(rotation / MaxDialValue);
            zeroes += fullRotations;

            var previouslyZero = dialValue == 0;

            var remainingRotation = rotation % MaxDialValue;
            dialValue += remainingRotation;

            var touchedZero = false;

            if (dialValue == 0)
            {
                touchedZero = true;
            }
            else if (dialValue < 0)
            {
                dialValue += MaxDialValue;
                touchedZero = true;
            }
            else if (dialValue >= MaxDialValue)
            {
                dialValue -= MaxDialValue;
                touchedZero = true;
            }

            if (!previouslyZero && touchedZero)
            {
                zeroes++;
            }
        }

        return zeroes.ToString();
    }

    private static IEnumerable<int> ParseRotations(IEnumerable<string> rotations)
    {
        foreach (var rotation in rotations)
        {
            var directionIndicator = rotation[0];
            var direction = directionIndicator switch
            {
                'L' => -1,
                'R' => 1,
                _ => throw new ArgumentException()
            };

            var amount = int.Parse(rotation[1..]);

            yield return direction * amount;
        }
    }
}