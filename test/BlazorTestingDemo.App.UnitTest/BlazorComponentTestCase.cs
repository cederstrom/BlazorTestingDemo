using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using TestContext = Bunit.TestContext;

namespace BlazorTestingDemo.App.UnitTest;

public class BlazorComponentTestCase<T> : TestContextWrapper where T : IComponent
{
	private IRenderedComponent<T>? _component;
	private TestContext? _testContext;

	public IRenderedComponent<T> Component => _component ??= RenderComponent();

	private IRenderedComponent<T> RenderComponent() => RenderComponent<T>();

	protected sealed override TestContext? TestContext
	{
		get
		{
			if (_testContext != null) return _testContext;
			_testContext = new TestContext();
			ConfigureTestContext(_testContext);
			return _testContext;
		}
		set => _testContext = value;
	}

	public IElement FindByTestAttr(string value) => Component.Find($"[data-testid='{value}']");

	protected virtual void ConfigureTestContext(TestContext testContext)
	{
		testContext.Services.AddOptions();
		testContext.Services.AddLogging();
		testContext.JSInterop.Mode = JSRuntimeMode.Loose;
	}
}