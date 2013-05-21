#region [Imports]

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion

namespace TestApplication.Models
{
    /// <summary>
    /// The user tool.
    /// </summary>
    public class UserTool : IEntity<int>
    {
        /// <summary>
        /// The id.
        /// </summary>
        private int id;

        /// <summary>
        /// The name.
        /// </summary>
        private string name;

        /// <summary>
        /// The description.
        /// </summary>
        private string description;

        /// <summary>
        /// The buy date.
        /// </summary>
        private DateTime buyDate;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserTool"/> class.
        /// </summary>
        public UserTool()
            : this(-1, DateTime.Now)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserTool"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="buyDate">
        /// The buy date.
        /// </param>
        public UserTool(int id, DateTime buyDate)
        {
            this.id = id;
            this.buyDate = buyDate;
            description = string.Empty;
            name = string.Empty;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DisplayName("Name")]
        public virtual string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        [DisplayName("Tool description")]
        public virtual string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        [DisplayName("Buy date")]
        [DataType(DataType.DateTime)]
        public virtual DateTime BuyDate
        {
            get
            {
                return buyDate;
            }
            set
            {
                buyDate = value;
            }
        }
    }
}