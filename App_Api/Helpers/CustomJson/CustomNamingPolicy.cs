using System.Text.Json;

namespace App_Api.Helpers.CustomJson
{
    public class CustomNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            // Thực hiện chuyển đổi tên thuộc tính theo quy ước của bạn
            return name; // Ví dụ: không thay đổi tên thuộc tính
        }
    }
}
