using BlazorTestingDemo.Core.Models;

namespace BlazorTestingDemo.Core.Services;

public interface IEmailService
{
	public bool SendEmail(Email email);
	public IEnumerable<Email> GetMailsFrom(string fromAddress);
}

public class StubEmailService : IEmailService
{
	public bool SendEmail(Email email)
	{
		return true;
	}

	public IEnumerable<Email> GetMailsFrom(string fromAddress)
	{
		return new []
		{
			new Email { ToAddress = "to@stub.se", FromAddress = "from@stub.se", Subject = "Hello", Body = "World" }
		};
	}
}

public class FakeEmailService : IEmailService
{
	private IList<Email> History { get; } = new List<Email>();

	public bool SendEmail(Email email)
	{
		History.Add(email);
		return true;
	}

	public IEnumerable<Email> GetMailsFrom(string fromAddress)
	{
		return History;
	}
}
