using System;

namespace WebAppProject.Logic
{
    [Serializable]
    public class  User
    {
        
        public User(string id, string userName, string email)
        {
            if (id == null || userName == null || email == null)
            {
                throw new ArgumentNullException("Arguments must be defined!");
            }
            this.Id = id;
            this.UserName = userName;
            this.Email = email;
        }
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }


        public override bool Equals(Object other)
        {
            if (other != null && other is User)
            {
                User obj2 = (User) other;
                return obj2.Id == this.Id
                       && obj2.Email == this.Email
                       && obj2.UserName == this.UserName;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() + UserName.GetHashCode()
                   + Email.GetHashCode();
        }

        public User()
        {
            
        }
    }
}


