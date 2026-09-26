using CodingGuide.Core.Search;

namespace CodingGuide.Tests;

public class TokenizerTests
{
    [Fact]
    public void 한글은_두_글자씩_겹쳐_자른다()
    {
        Assert.Equal(["리스", "스트", "트에", "에서"], Tokenizer.Tokenize("리스트에서"));
    }

    [Fact]
    public void 영문은_소문자_단어와_카멜케이스_조각을_낸다()
    {
        var tokens = Tokenizer.Tokenize("SelectMany");
        Assert.Contains("selectmany", tokens);
        Assert.Contains("select", tokens);
        Assert.Contains("many", tokens);
    }

    [Fact]
    public void 오류_코드는_통째로_남는다()
    {
        Assert.Contains("cs0103", Tokenizer.Tokenize("CS0103 오류"));
    }

    [Fact]
    public void 한_글자_영문은_버린다()
    {
        Assert.Empty(Tokenizer.Tokenize("a b c"));
    }

    [Fact]
    public void 한글과_영문이_섞여도_나뉜다()
    {
        var tokens = Tokenizer.Tokenize("린큐_메소드_Where");
        Assert.Contains("린큐", tokens);
        Assert.Contains("where", tokens);
    }

    [Theory]
    [InlineData("dictonary", "dictionary", 1)]
    [InlineData("selct", "select", 1)]
    [InlineData("abc", "abc", 0)]
    [InlineData("ab", "ba", 1)]
    public void 편집_거리(string a, string b, int expected)
    {
        Assert.Equal(expected, SearchEngine.EditDistance(a, b));
    }

    [Fact]
    public void 동의어는_조사가_붙어도_찾는다()
    {
        var dict = SynonymDictionary.Parse("걸러, 필터, where");
        var expanded = dict.Expand("조건으로 걸러내고 싶어").ToList();
        Assert.Contains("where", expanded);
    }

    [Fact]
    public void 영문_동의어는_단어가_정확히_같아야_한다()
    {
        var dict = SynonymDictionary.Parse("where, 필터");
        Assert.Empty(dict.Expand("nowhere"));
        Assert.Contains("필터", dict.Expand("Where 사용법"));
    }
}
