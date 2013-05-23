using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace TestApplication.Models
{
    public class User : IEntity<int>
    {
        private int id;

        private string firstName;

        private string lastName;

        private DateTime birthday;

        private Department department;

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
        [DataType(DataType.Date)]
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

        [HiddenInput(DisplayValue = false)]
        [DisplayName("Department")]
        public virtual Department Department
        {
            get
            {
                return department;
            }
            set
            {
                department = value;
            }
        }

        [DisplayName("Department")]
        public virtual int DepartmentId { get; set; }
    }
}