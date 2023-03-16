#region var
string input = args[0];

const int LENGTH_FIVE_BANDS = 19;
const int LENGTH_FOUR_BANDS = 15;
const int LENGTH = 3;
const int FIRST = 0;
const int SECOND = 4;
const int THIRD = 8;
const int FOURTH = 12;
const int FIFTH = 16;

const string INVALID_COLOR = "There was an invalid color Code. Please use the first three digits of the color.";
#endregion

#region program
Console.OutputEncoding = System.Text.Encoding.Default;
if (!CheckHyphens(input))
{
    Console.WriteLine("You didn't place the hyphens right.");
}
else if (LENGTH_FIVE_BANDS == input.Length)
{
    if (TryConvertColorToDigit(input.Substring(FIRST, LENGTH), out int colorValue1)
        && TryConvertColorToDigit(input.Substring(SECOND, LENGTH), out int colorValue2)
        && TryConvertColorToDigit(input.Substring(THIRD, LENGTH), out int colorValue3)
        && TryGetMultiplierFromColor(input.Substring(FOURTH, LENGTH), out double multiplier)
        && TryGetToleranceFromColor(input.Substring(FIFTH, LENGTH), out double toleranceValue))
    {
        Decode5ColorBands(colorValue1, colorValue2, colorValue3, multiplier, toleranceValue, out double colorCodeValue, out string tolerance);
        Console.WriteLine($"Resistance: {colorCodeValue}Ω\n\rTolerance: {tolerance}");
    }
    else
    {
        Console.WriteLine(INVALID_COLOR);
    }
}
else if (LENGTH_FOUR_BANDS == input.Length)
{
    if (TryConvertColorToDigit(input.Substring(FIRST, LENGTH), out int colorValue1) && TryConvertColorToDigit(input.Substring(SECOND, LENGTH), out int colorValue2) && TryGetMultiplierFromColor(input.Substring(THIRD, LENGTH), out double multiplier) && TryGetToleranceFromColor(input.Substring(FOURTH, LENGTH), out double toleranceValue))
    {
        Decode4ColorBands(colorValue1, colorValue2, multiplier, toleranceValue, out double colorCodeValue, out string tolerance);
        Console.WriteLine($"Resistance: {colorCodeValue}Ω\n\rTolerance: {tolerance}");
    }
    else
    {
        Console.WriteLine(INVALID_COLOR);
    }
}
else
{
    Console.WriteLine("No valid length. Please enter a code with 4 or 5 valid Colors. Each color consisting of it's first three letters.");
}
#endregion

#region methods
bool TryConvertColorToDigit(string color, out int colorValue) =>
    (colorValue = color switch
    {
        "Bla" => 0,
        "Bro" => 1,
        "Red" => 2,
        "Ora" => 3,
        "Yel" => 4,
        "Gre" => 5,
        "Blu" => 6,
        "Vio" => 7,
        "Gra" => 8,
        "Whi" => 9,
        _ => -1
    }) != -1;

bool TryGetMultiplierFromColor(string color, out double multiplier) =>
    (multiplier = color switch
    {
        "Bla" => 1,
        "Bro" => 10,
        "Red" => 100,
        "Ora" => 1_000,
        "Yel" => 10_000,
        "Gre" => 100_000,
        "Blu" => 1_000_000,
        "Vio" => 10_000_000,
        "Gra" => 100_000_000,
        "Whi" => 1_000_000,
        "Gol" => 0.1,
        "Sil" => 0.01,
        _ => -1
    }) != -1;

bool TryGetToleranceFromColor(string color, out double tolerance) =>
    (tolerance = color switch
    {
        "Bro" => 1,
        "Red" => 2,
        "Gre" => 0.5,
        "Blu" => 0.25,
        "Vio" => 0.1,
        "Gra" => 0.05,
        "Gol" => 5,
        "Sil" => 10,
        _ => -1
    }) != -1;
    
void Decode4ColorBands(int colorCodeValue1, int colorCodeValue2, double multiplier, double toleranceValue, out double colorCodeValue, out string tolerance)
{
    colorCodeValue = (colorCodeValue1 * 10 + colorCodeValue2) * multiplier;
    tolerance = $"±{toleranceValue}%";
}
void Decode5ColorBands(int colorCodeValue1, int colorCodeValue2, int colorCodeValue3, double multiplier, double toleranceValue, out double colorCodeValue, out string tolerance)
{
    colorCodeValue = (colorCodeValue1 * 100 + colorCodeValue2 * 10 + colorCodeValue3) * multiplier;
    tolerance = $"±{toleranceValue}%";
}
bool CheckHyphens(string input)
{
    for (int i = 1; i <= input.Length - 2; i++)
    {
        if ((i + 1) % 4 == 0 && input[i] != '-') { return false; }
    }
    return true;
}   
#endregion