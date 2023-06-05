using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackItAPI.Entities
{
	[Table("ChatLog")]
	public class ChatLog
	{
		[Key]
		public int LogID { get; set; }
		public Guid GUID { get; set; }
		public int MemberID { get; set; }
		public string? Prompt { get; set; }
		public string? Answer { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
