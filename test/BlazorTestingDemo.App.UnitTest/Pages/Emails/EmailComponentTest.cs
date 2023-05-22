using AngleSharp.Dom;
using BlazorTestingDemo.App.Pages.Emails;
using BlazorTestingDemo.Core.Models;
using BlazorTestingDemo.Core.Services;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ClearExtensions;
using TestContext = Bunit.TestContext;

namespace BlazorTestingDemo.App.UnitTest.Pages.Emails;

[TestFixture]
public class EmailComponentTest
{
	private EmailComponentTestCase _tc = null!;

	[SetUp]
	public void SetUp()
	{
		_tc = new EmailComponentTestCase();
		_tc.MockEmailService.ClearSubstitute();
		_tc.MockEmailService.SendEmail(Arg.Is<Email>(e => e.ToAddress == "fail@foobar")).Returns(false);
		_tc.MockEmailService.SendEmail(Arg.Is<Email>(e => e.ToAddress == "success@foobar.com")).Returns(true);
	}

	[Test]
	public void Shows_error_message_when_unable_to_send_email()
	{
		_tc.ToAddress.Type("fail@foobar");
		_tc.SubjectInput.Type("Hello");
		_tc.BodyInput.Type("How are you?");
		_tc.SubmitButton.Click();

		Assert.That(_tc.StatusMessage.TextContent, Is.EqualTo("Sorry, could not send the email"));
	}

	[Test]
	public void Shows_success_message_when_email_sent()
	{
		_tc.ToAddress.Type("success@foobar.com");
		_tc.SubjectInput.Type("Hello");
		_tc.BodyInput.Type("How are you?");
		_tc.SubmitButton.Click();

		Assert.That(_tc.StatusMessage.TextContent, Is.EqualTo("Success!"));
	}
}

public class EmailComponentTestCase : BlazorComponentTestCase<SendEmail>
{
	public IEmailService MockEmailService { get; }
	public IElement ToAddress => FindByTestAttr("to-address-input");
	public IElement SubjectInput => FindByTestAttr("from-address-input");
	public IElement BodyInput => FindByTestAttr("body-input");
	public IElement SubmitButton => FindByTestAttr("submit-button");
	public IElement StatusMessage =>  FindByTestAttr("status-message");

	public EmailComponentTestCase()
	{
		MockEmailService = Substitute.For<IEmailService>();
	}

	protected override void ConfigureTestContext(TestContext testContext)
	{
		base.ConfigureTestContext(testContext);
		testContext.Services.AddSingleton(MockEmailService);
	}
}
