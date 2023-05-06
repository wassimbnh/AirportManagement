using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Domain
{
    public class Passenger
    {
        [Key]
        public string PassportNumber { get; set; }
        [StringLength(100)]
        public string Photo { get; set; }
        public FullName FullName { get; set; }
        [Display(Name ="Date of birth")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [DataType(DataType.EmailAddress,ErrorMessage ="email invalide")]
        public string EmailAddress { get; set; }

        [RegularExpression("@[0-9]{8}")]
        public string TelNumber { get; set; }
        // public ICollection<Flight> Flights { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }

        //public Boolean CheckProfile(string prenom, string nom)
        //{
        //    return (FirstName.Equals(prenom) && LastName.Equals(nom));

        //}
        public Boolean CheckProfile(string prenom, string nom, string email=null)
        {
            if(email!=null)
            return (FullName.FirstName.Equals(prenom) && FullName.LastName.Equals(nom) 
                    && EmailAddress.Equals(email));
            else
            return (FullName.FirstName.Equals(prenom) && FullName.LastName.Equals(nom));

        }
        public virtual void PassengerType()
        { Console.WriteLine("I am a passenger"); }

    }
}
