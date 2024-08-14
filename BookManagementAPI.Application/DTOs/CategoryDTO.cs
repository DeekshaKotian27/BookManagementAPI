namespace BookManagementAPI.Application.DTOs
{
    public class CategoryDTO
    {
        public string CategoryName { get; set; }
    }

    public class ResponseMultipleCategoryDTO
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public List<ResponseCategoryBookDTO> Books { get; set; }
    }
}
