using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogPod.MVC.Entites
{
    public class CityEntity
    {
        public required int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? City { get; set; }
        public byte[]? Image { get; set; }

        public string? Country { get; set; }
        public List<SelectListItem> GetCurrentCountries()
        {
            return new List<SelectListItem>()
        {
            new SelectListItem { Value = Country, Text = Country }
        };
        }




        public string? getImg()
        {
            if (Image != null && Image.Length > 0)
            {
                string imageBase64 = Convert.ToBase64String(Image);
                string imageUrl = $"data:image;base64,{imageBase64}";
                return imageUrl;
            }
            else
            {
                return "";
            }
        }
    }
}
