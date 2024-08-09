using System;
using System.Net.Http;
using System.Threading.Tasks;
using Json;
partial class Program
{
	[CmdArg(Ordinal = 0, Required = true, Description = "The question to ask (use quotes)", ItemName = "question")]
	static string Question;
	[CmdArg(Name = "key", Description = "The API Key", ItemName = "key",Required =true)]
	static string Key = "";
	const string _url_fmt = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent?key={0}";
	const string _json_prologue = "{\"contents\":[{\"parts\":[{\"text\":\""; const string _json_epilogue = "\"}]}]}";
	static async Task MainAsync()
	{
		var client = new HttpClient();
		//client.BaseAddress = ;
		var content = new StringContent(_json_prologue + JsonUtility.EscapeString(Question) + _json_epilogue);
		var result = await client.PostAsync(new Uri(string.Format(_url_fmt, Key)), content);
		string resultContent = await result.Content.ReadAsStringAsync();
		dynamic obj = JsonObject.Parse(resultContent);
		Console.WriteLine(obj.candidates[0].content.parts[0].text);
	}
	static void Run()
	{
		MainAsync().Wait();
	}
}

