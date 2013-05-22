using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TestApplication.Models
{
    public class User : IEntity<int>
    {
        private int id;

        private string firstName;

        private string lastName;

        private DateTime birthday;

        public User()
        {
            birthday = DateTime.Today;
        }

        [DataMember]
        public virtual int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        [DataMember]
        [Required(ErrorMessage = "Please enter first name")]
        [DisplayName("First name")]
        public virtual string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                firstName = value;
            }
        }

        [DataMember]
        [Required(ErrorMessage = "Please enter last name")]
        [DisplayName("Last name")]
        public virtual string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }

        [DataMember]
        [DisplayName("Birthday")]
        [Required]
        [DataType(DataType.DateTime)]
        public virtual DateTime Birthday
        {
            get
            {
                return birthday;
            }
            set
            {
                birthday = value;
            }
        }
    }
}