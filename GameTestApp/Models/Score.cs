using SQLite;

namespace GameTestApp.Models
{
    [Table("Scores")]
    public class Score
    {
        [Column("id"), PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Column("player")]
        public string Player { get; set; }
        [Column("score")]
        public int FScore { get; set; }
    }
}
