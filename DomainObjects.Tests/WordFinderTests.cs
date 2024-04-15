using AutoFixture;
using DomainObjects.Exceptions;

namespace DomainObjects.Tests;

public class WordFinderTests
{
    private readonly Fixture _fixture = new();

    private static readonly string[] ExpectedHorizontalWords = ["AIRPLANE", "ARRIVAL", "WHERE"];
    private static readonly string[] ExpectedTop10 = ["EIGHT", "ELEVEN", "FIVE", "NINE", "ONE", "TEN", "THREE", "TWELVE", "SEVEN", "SIX"];
    private static readonly string[] ExpectedVerticalWords = ["HOUSE", "STREET"];

    [Fact]
    public void ReturnsTop10Words()
    {
        var matrix = new string[]
        {
            "OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO",
            "NNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN",
            "EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE",
            "OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO",
            "NNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN",
            "EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE",
            "ONEONEONEONEONEONEONEONEONEONEONEONEONEONEONEONEONEONEONEONE",
            "ONEONEONEONEONEONEONEONEONEONEONEONEONEONEONEONEONEONEONEONE",
            "TWOTWOTWOTWOTWOTWOTWOTWOTWOTWOTWOTWOTWOTWOTWOTWOTWOTWOTWOTWO",            
            "TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT",
            "HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH",
            "RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR",
            "EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE",
            "EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE",
            "THREETHREETHREETHREETHREETHREETHREETHREETHREETHREETHREETHREE",
            "THREETHREETHREETHREETHREETHREETHREETHREETHREETHREETHREETHREE",
            "FOURFOURFOURFOURFOURFOURFOURFOURFOURFOURFOURFOURFOURFOURFOUR",
            "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF",
            "IIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII",
            "VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV",
            "EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE",
            "SIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIX",
            "SIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIX",
            "SIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIX",
            "SIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIXSIX",
            "SEVENSEVENSEVENSEVENSEVENSEVENSEVENSEVENSEVENSEVENSEVENSEVEN",
            "SEVENSEVENSEVENSEVENSEVENSEVENSEVENSEVENSEVENSEVENSEVENSEVEN",
            "SEVENSEVENSEVENSEVENSEVENSEVENSEVENSEVENSEVENSEVENSEVENSEVEN",
            "SEVENSEVENSEVENSEVENSEVENSEVENSEVENSEVENSEVENSEVENSEVENSEVEN",
            "EIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHT",
            "EIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHT",
            "EIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHT",
            "EIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHTEIGHT",
            "NINENINENINENINENINENINENINENINENINENINENINENINENINENINENINE",
            "NINENINENINENINENINENINENINENINENINENINENINENINENINENINENINE",
            "NINENINENINENINENINENINENINENINENINENINENINENINENINENINENINE",
            "NINENINENINENINENINENINENINENINENINENINENINENINENINENINENINE",
            "TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT",
            "EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE",
            "NNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN",
            "TENTENTENTENTENTENTENTENTENTENTENTENTENTENTENTENTENTENTENTEN",
            "EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE",
            "LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL",
            "EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE",
            "VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV",
            "EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE",
            "NNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN",
            "TWELVETWELVETWELVETWELVETWELVETWELVETWELVETWELVETWELVETWELVE",
            "TWELVETWELVETWELVETWELVETWELVETWELVETWELVETWELVETWELVETWELVE",
            "TWELVETWELVETWELVETWELVETWELVETWELVETWELVETWELVETWELVETWELVE",
            "TWELVETWELVETWELVETWELVETWELVETWELVETWELVETWELVETWELVETWELVE",
            "TWELVETWELVETWELVETWELVETWELVETWELVETWELVETWELVETWELVETWELVE",
            "TWELVETWELVETWELVETWELVETWELVETWELVETWELVETWELVETWELVETWELVE",
            "THIRTEENTHIRTEENTHIRTEENTHIRTEENTHIRTEENTHIRTEENTHIRTEENTHIR",
            "FOURTEENFOURTEENFOURTEENFOURTEENFOURTEENFOURTEENFOURTEENFOUR",
            "FIFTEENFIFTEENFIFTEENFIFTEENFIFTEENFIFTEENFIFTEENFIFTEENFIFT",
            "SIXTEENSIXTEENSIXTEENSIXTEENSIXTEENSIXTEENSIXTEENSIXTEENSIXT",
            "SEVENTEENSEVENTEENSEVENTEENSEVENTEENSEVENTEENSEVENTEENSEVENT",
            "NINETEENNINETEENNINETEENNINETEENNINETEENNINETEENNINETEENNINE",
            "TWENTYTWENTYTWENTYTWENTYTWENTYTWENTYTWENTYTWENTYTWENTYTWENTY"
        };

        var words = new[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "nineteen" };

        var wordFinder = new WordFinder(matrix);

        var result = wordFinder.Find(words);

        Assert.NotNull(result);
        Assert.Equivalent(ExpectedTop10, result);
    }

    [Fact]
    public void SearchAppliesShortCircuit()
    {
        var matrix = new string[]
        {
            "FIVE",
            "IDEA",
            "VERR",
            "EAYL"
        };

        var words = new[] { "street", "strength", "philosophy", "house" };

        var wordFinder = new WordFinder(matrix);

        var result = wordFinder.Find(words);

        Assert.NotNull(result);
        Assert.Equivalent(Array.Empty<string>(), result);
    }

    [Fact]
    public void SearchHorizontalWordsSuccessfully()
    {
        var matrix = new string[]
        {
            "APAQWEDSAFWERASF",
            "WHEREAUIHOIWEFQD",
            "MOUYTSASDFWEFCDS",
            "IUWAIRPLANEREFDA",
            "PSLMECASFDRWFASD",
            "AECMEARRIVALADFA"
        };

        var words = new[] { "where", "arrival", "airplane" };

        var wordFinder = new WordFinder(matrix);

        var verticalWords = wordFinder.Find(words);

        Assert.NotNull(verticalWords);
        Assert.Equivalent(ExpectedHorizontalWords, verticalWords);
    }

    [Fact]
    public void SearchVerticalWordsSuccessfully()
    {
        var matrix = new string[]
        {
            "APAQWE",
            "WHRESA",
            "MOUYTS",
            "IUWERK",
            "PSLMEC",
            "AECMED",
            "MCNFTU",
            "KJOLKJ",
            "AMGIEK"
        };

        var words = new[] { "street", "car", "love", "house" };

        var wordFinder = new WordFinder(matrix);

        var verticalWords = wordFinder.Find(words);

        Assert.NotNull(verticalWords);
        Assert.Equivalent(ExpectedVerticalWords, verticalWords);
    }

    [Theory]
    [InlineData(65,10)]
    [InlineData(10, 65)]
    [InlineData(65, 65)]
    public void ThrowsAnException_When_MaxSizeIsExceeded(int rows, int columns)
    {
        var overSizedMatrix = GetMatrix(rows, columns);

        _ = Assert.Throws<MaxSizeExceededException>(() =>
        {
            _ = new WordFinder(overSizedMatrix);
        });
    }

    [Fact]
    public void ThrowsAnException_When_StringsAreDifferentSize()
    {
        var irregularMatrix = new List<string>
        {
            GetString(10),
            GetString(20),
            GetString(15),
            GetString(10)
        };

        _ = Assert.Throws<NotSameLengthRowException>(() =>
        {
            _ = new WordFinder(irregularMatrix);
        });
    }

    [Fact]
    public void ThrowsAnException_When_MatrixIsNull()
    {
        _ = Assert.Throws<NullOrEmptyMatrixException>(() =>
        {
            _ = new WordFinder(null);
        });
    }

    [Fact]
    public void ThrowsAnException_When_MatrixIsEmpty()
    {
        _ = Assert.Throws<NullOrEmptyMatrixException>(() =>
        {
            _ = new WordFinder(Array.Empty<string>());
        });
    }

    private IEnumerable<string> GetMatrix(int rows, int columns)
    {
        var matrix = new List<string>();

        for(var i = 0; i < rows; i++)
        {
            matrix.Add(GetString(columns));
        }

        return matrix;
    }

    private string GetString(int length)
    {
        return string.Join(string.Empty, _fixture.CreateMany<char>(length));
    }
}