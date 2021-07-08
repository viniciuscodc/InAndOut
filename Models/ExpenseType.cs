using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace InAndOut.Models
{
    public class ExpenseType
    {
        [Key]
        public int Id { get; set;} 

        [Required]
        [DisplayName ("Expense Type Name")]
        public string ExpenseTypeName { get; set;}
    }
}