namespace QPCore.Model.WebElement
{
    public class ElementTreeItem
    {
        public int ElementId { get; set; }
        public int PageId { get; set; }
        public int GroupId { get; set; }
        public string ElementName { get; set; }
        public bool IsComposite { get; set; }
    }
}