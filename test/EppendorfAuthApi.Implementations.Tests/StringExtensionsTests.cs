using EppendorfAuthApi.Implementations;

public class StringExtensionsTests
{
    [Fact]
    public void IsBase64ShouldReturnFalseWhenStringIsNotBase64()
    {
        "!@#".IsBase64().Should().BeFalse();
    }

    [Fact]
    public void IsBase64ShouldReturnTrueWhenStringIsBase64()
    {
        "cXdlcnR5".IsBase64().Should().BeTrue();
    }

    [Fact]
    public void ToSha256ShouldReturnCorrectHash()
    {
        const string text = "qwerty";
        const string textSha256 = "65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5";

        text.ToSha256().Should().Be(textSha256);
    }
}