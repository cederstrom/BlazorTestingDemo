using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace BlazorTestingDemo.App.UnitTest;

public static class ElementExtensions
{
	public static void Type(this IElement element, string value)
	{
		element.Change(new ChangeEventArgs { Value = value });
	}
}
