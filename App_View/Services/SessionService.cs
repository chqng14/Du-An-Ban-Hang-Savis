using App_Data.Models;
using Newtonsoft.Json;

namespace App_View.Services
{
    public static class SessionService
    {
        //Lưu vào session
        public static void SetObjectToSession(ISession session, string key, object value)
        {
            var jsonData = JsonConvert.SerializeObject(value);
            session.SetString(key, jsonData);
        }
        //Lấy đối tượng 
        public static User GetUserFromSession(ISession session, string key)
        {
            string jsonData = session.GetString(key);
            if (jsonData != null)
            {
                var user = JsonConvert.DeserializeObject<User>(jsonData);
                return user;
            }
            return null;
        }

        
        public static bool CheckObjectInList(Guid id, List<User> lst)
        {
            return lst.Any(c => c.Id == id);
        }
    }
}
