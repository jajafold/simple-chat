using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Uri;

public class UriBuilder
{
    public string? Host { get; internal set; }
    public List<string> Fragments { get; private set; } = new();
    public Dictionary<string, string> Queries { get; private set; } = new ();

    public UriBuilder(string? host, List<string> fragments, Dictionary<string, string?> queries)
    {
        Host = host;
        Fragments = fragments;
        Queries = queries;
    }
    
    public UriBuilder() {}

    public override string ToString()
    {
        var fragmentsString = string.Join("/", Fragments);
        var queriesString = string.Join("&", Queries.Select(x => $"{x.Key}={x.Value}"));

        return $"{Host}/{fragmentsString}?{queriesString}";
    }
}

public static class UriBuilderExtensions
{
    public static UriBuilder AddHost(this UriBuilder builder, string? host)
    {
        builder.Host = host;
        return builder;
    }

    public static UriBuilder AddFragment(this UriBuilder builder, string fragment)
    {
        builder.Fragments.Add(fragment);
        return builder;
    }

    public static UriBuilder AddQuery(this UriBuilder builder, string queryName, object queryValue)
    {
        builder.Queries.Add(queryName, queryValue.ToString() ?? throw new ArgumentException("Invalid query value!"));
        return builder;
    }
}