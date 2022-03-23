using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;
using System.Text.Json;

namespace System.Text.Json.Flatten.Tests;

public class FlattenerTests
{
    [TestCase(@"true", "", "true")]
    [TestCase(@"{""a"":1}", ".a", "1")]
    [TestCase(@"{""a"":1.0}", ".a", "1.0")]
    public void TestBasicValues(string json, string property, string expectedJson)
    {
        Helpers.ParseJson(json).Flatten().Should().ContainKey(property)
            .WhoseValue.GetRawText().Should().Be(expectedJson);
    }

    [TestCase(@"{""a"":{""b"":""c""}}", ".a.b", @"""c""")]
    public void TestObjects(string json, string property, string expectedJson)
    {
        Helpers.ParseJson(json).Flatten().Should().ContainKey(property)
            .WhoseValue.GetRawText().Should().Be(expectedJson);
    }

    [TestCase(@"{""a"":[1, 2, 3, 4, 5]}", ".a[0]", @"1")]
    [TestCase(@"{""a"":[1, 2, 3, 4, 5]}", ".a[4]", @"5")]
    public void TestArrays(string json, string property, string expectedJson)
    {
        Helpers.ParseJson(json).Flatten().Should().ContainKey(property)
            .WhoseValue.GetRawText().Should().Be(expectedJson);
    }

    [TestCase(@"{""a"":[{""b"": ""c""}]}", ".a[0].b", @"""c""")]
    public void TestCompound(string json, string property, string expectedJson)
    {
        Helpers.ParseJson(json).Flatten().Should().ContainKey(property)
            .WhoseValue.GetRawText().Should().Be(expectedJson);
    }
}