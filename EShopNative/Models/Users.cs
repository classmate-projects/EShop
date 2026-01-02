namespace EShopNative.Models
{
    public class Users
    {
        public int Id { get; set; }                  // maps to id
        public string Username { get; set; }         // maps to username
        public string Email { get; set; }            // maps to email
        public string PasswordHash { get; set; }     // maps to password_hash
        public string FullName { get; set; }         // maps to full_name
        public string PhoneNumber { get; set; }      // maps to phone_number
        public bool IsActive { get; set; }           // maps to is_active
        public DateTime CreatedAt { get; set; }      // maps to created_at (timestamptz)
        public DateTime UpdatedAt { get; set; }      // maps to updated_at (timestamptz)
    }

}
