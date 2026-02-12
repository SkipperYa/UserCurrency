namespace UserService.Application.Dto
{
	/// <summary>
	/// Модель регитсрации пользователя
	/// </summary>
	public class RegistrationUserDto
	{
		public string Name { get; set; } = "";
		public string Password { get; set; } = "";
	}
}
