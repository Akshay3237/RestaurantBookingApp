namespace Restorent_app.Models
{
    public interface IReposetoryUser
    {
        public UserModel createUser(UserModel user);

        public bool findUser(string username, string password);

        public UserModel updateUser(int id, UserModel user);

        public int getUserId(string username);

        public void deleteUser(int id);

        public bool userExist(string username, string email);

        public UserModel getUserByUserId(int UserId);
    }
}
