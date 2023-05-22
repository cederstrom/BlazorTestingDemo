namespace BlazorTestingDemo.Core.Models;

public class Email
{
	public string? ToAddress { get; set; }
	public string? FromAddress { get; set; }
	public string? Subject { get; set; }
	public string? Body { get; set; }
	public DateTime? ReceivedAt { get; init; }
}