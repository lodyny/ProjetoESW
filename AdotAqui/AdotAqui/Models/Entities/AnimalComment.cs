using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Models.Entities
{
    /// <summary>
    /// Represents a animal commentary
    /// </summary>
    public partial class AnimalComment
    {
        [Key]
        public int CommentId { get; set; }

        public int AnimalId { get; set; }
        public int UserId { get; set; }
        public string Commentary { get; set; }
        public DateTime InsertDate { get; set; }

        private string UserEmail { get; set; }
        private string UserName { get; set; }
        private string UserImage { get; set; }

        public void SetEmail(string email)
        {
            UserEmail = email;
        }

        public string GetEmail()
        {
            return UserEmail;
        }

        public void SetUserName(string username)
        {
            this.UserName = username;
        }

        public string GetUserName()
        {
            return UserName;
        }

        public void SetUserImage(string image)
        {
            this.UserImage = image;
        }

        public string GetUserImage()
        {
            return UserImage;
        }

    }
}
