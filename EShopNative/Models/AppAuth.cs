using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace EShopNative.Models
{
    [Table("appAuth")] // must match your Supabase table name
    public class AppAuth : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("auth_key")]
        public string AuthKey { get; set; }   // varchar(20)

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
