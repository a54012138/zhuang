using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace WinFrom_WebApi_Demo;

public class WebPost
{
	private static readonly string DefaultUserAgent;

	private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x00002048
	}

	public static string ApiPost(string url, IDictionary<string, string> parameters)
	{
	//Invalid MethodBodyBlock: Invalid relative virtual address (RVA): 0x0000205C
	}
}
