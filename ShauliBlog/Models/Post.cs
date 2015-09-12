using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShauliBlog.Models
{
    public class Post : IComparable<Post>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Post Headline")]
        public string Headline { get; set; }

        [Required]
        [Display(Name = "Author")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Author's website")]
        public string Website { get; set; }

        [Required]
        [Display(Name = "Publicity date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Timestamp { get; set; }

        [Required]
        [Display(Name = "Content")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Display(Name = "Post comments")]
        public virtual List<Comment> Comments{ get; set; }

        public int CompareTo(Post other)
        {
            return (DateTime.Compare(other.Timestamp, this.Timestamp));
        }
    }
}