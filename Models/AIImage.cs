using System.ComponentModel.DataAnnotations;

namespace AITutorWebsite.Models
{
    public class AIImage
    {
        public AIImage()
        {
            Prompt = "";
            ImageGenerator = "";
            Filename = "";
            UserId = "";
            UserName = "";
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Prompt { get; set; }

        [Required]
        [Display(Name = "Image Generator")]
        public string ImageGenerator { get; set; }

        [Display(Name = "Upload Date")]
        public DateTime UploadDate { get; set; }

        [Required]
        [Display(Name = "Image URL")]
        [RegularExpression(@"^.*\.(jpg|jpeg|png)$", ErrorMessage = "Only JPG or PNG images are allowed.")]
        [MaxLength(2048)]
        public string Filename { get; set; }

        public int Like { get; set; }

        public bool canIncreaseLike { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
