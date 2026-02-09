namespace UserService.Application.Options
{
	internal class AuthOptions
	{
		public string Audience { get; set; } = "";
		public string Issuer { get; set; } = "";
		public string Key { get; set; } = "";
		public int ExpirationTimeMin { get; set; } = 60 * 24 * 30; // 30 days
	}
}
