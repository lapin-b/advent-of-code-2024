namespace Day5;

internal class UpdatePageRuleComparator: IComparer<int>
{
    private readonly PageSortRule[] _pageSortRules;

    public UpdatePageRuleComparator(PageSortRule[] pageSortRules)
    {
        _pageSortRules = pageSortRules;
    }
    
    public int Compare(int page1, int page2)
    {
        // Console.WriteLine($"{page1} / {page2}");
         var applicableRule = _pageSortRules.FirstOrDefault(
             r => (r.OrderingBoundary == page1 || r.OrderingBoundary == page2) 
                  && (r.PrintedBeforeBoundary == page1 || r.PrintedBeforeBoundary == page2)
         );

         if (applicableRule is null)
         {
             // Order can be kept
             return 0;
         } 
         else if (applicableRule.PrintedBeforeBoundary == page1)
         {
            // Page 1 should be printed before (placed before)
            return -1;
         }
         else
         {
             return 1;
         }
    }
}