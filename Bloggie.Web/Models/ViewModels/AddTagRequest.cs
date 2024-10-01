namespace Bloggie.Web.Models.ViewModels
{
    public class AddTagRequest
    {
        [System.ComponentModel.DataAnnotations.Required]
     
        public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string DisplayName { get; set; }
    }
}
