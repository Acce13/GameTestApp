using SQLite;

namespace GameTestApp.Models
{
    [Table("GameTests")]
    public class GameTest
    {
        [Column("id"), PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Column("type")]
        public string Type { get; set; }
        [Column("question")]
        public string Question { get; set; }
        [Column("answer")]
        public string Answer { get; set; }
        [Column("options")]
        public string Options { get; set; }
    }
}
