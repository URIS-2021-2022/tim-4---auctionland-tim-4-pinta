//using Licnost.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Threading.Tasks;

//namespace Licnost.Data
//{
//    public class UserMockRepository : IUserRepository
//    {
//        public static List<User> Users { get; set; } = new List<User>();
//        private readonly static int iterations = 1000;

//        public UserMockRepository()
//        {
//            FillData();
//        }

//        private void FillData()
//        {
//            var user1 = HashPassword("testpassword");

//            Users.AddRange(new List<User>
//            {
//                new User
//                {
//                    UserID = Guid.Parse("B5175C32-A852-4496-A75A-C7E6F383FE36"),
//                    FirstName = "Petar",
//                    LastName = "Petrovic",
//                    UserName = "petar.petrovic",
//                    Email = "petar.petrovic@testmail.com",
//                    Password = user1.Item1,
//                    Salt = user1.Item2
//                }
//            });
//        }

//        public bool UserWithCredentialsExists(string username, string password)
//        {
//            User user = Users.FirstOrDefault(u => u.UserName == username);

//            if (user == null)
//            {
//                return false;
//            }

//            if (VerifyPassword(password, user.Password, user.Salt))
//            {
//                return true;
//            }
//            return false;
//        }

//        private Tuple<string, string> HashPassword(string password)
//        {
//            var sBytes = new byte[password.Length];
//            new RNGCryptoServiceProvider().GetNonZeroBytes(sBytes);
//            var salt = Convert.ToBase64String(sBytes);

//            var derivedBytes = new Rfc2898DeriveBytes(password, sBytes);

//            return new Tuple<string, string>(Convert.ToBase64String(derivedBytes.GetBytes(256)), salt);
//        }


//        public bool VerifyPassword(string password, string savedHash, string savedSalt)
//        {
//            var saltBytes = Convert.FromBase64String(savedSalt);
//            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, iterations);
//            if (Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == savedHash)
//            {
//                return true;
//            }
//            return false;
//        }
//    }
//}
