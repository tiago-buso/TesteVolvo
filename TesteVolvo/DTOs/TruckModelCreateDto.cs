using Flunt.Notifications;
using System.ComponentModel.DataAnnotations;

namespace TesteVolvo.DTOs
{
    public class TruckModelCreateDto : Notifiable<Notification>
    {              

        [Display(Name = "Descrição")]
        public string Description { get; private set; }

        [Display(Name = "Ano do Modelo")]
        public int YearOfModel { get; private set; }

        public TruckModelCreateDto(string description, int yearOfModel)
        {
            if (string.IsNullOrEmpty(description))
            {
                AddNotification("description", "Please inform the description of the truck model.");
            }

            if (description.Length > 2)
            {
                AddNotification("descriptionLength", "Please inform only 2 chars in the description field.");
            }

            if (yearOfModel < DateTime.Now.Year)
            {
                AddNotification("yearOfModel", "The year of the model cannot be lower than the current year. Please inform a valid year of the model.");
            }

            if (this.IsValid)
            {
                Description = description;
                YearOfModel = yearOfModel;
            }
        }

    }
}
