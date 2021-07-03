using System.Reflection.Metadata.Ecma335;

namespace DiscountManagement.Application.Contract.CollegueDiscount
{
    public class ColleagueDiscountViewModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string Product { get; set; } 
        public int DiscountRate { get; set; }
        public bool IsRemove { get; set; }  
        public string CreationDate { get; set; }
    }
}