using System;

namespace Domain
{
    public class Audit
    {
	    public long Id { get; set; }

	    public string EntityName { get; set; }
	    public string EntityId { get; set; }

	    public string Login { get; set; }
	    public DateTime Time { get; set; }

	    public string ModType { get; set; }
	    public string RowAfter { get; set; }
    }
}