namespace Day5;

public static class PageRulesArrayUtils
{
    public static int IndexForPage(this int[] pages, int targetPage) => pages
        .Select((page, index) => new { Page = page, Index = index })
        .First(p => p.Page == targetPage)
        .Index;
}