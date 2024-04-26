using Microsoft.EntityFrameworkCore.Storage;

namespace API.Extensions
{
	public static class DateTimeExtensions
	{
		public static int CalculateAge(this DateOnly dateOfBirth)
		{
			DateOnly currentDate = DateOnly.FromDateTime(DateTime.Today);
			int age = currentDate.Year - dateOfBirth.Year;
			//Decrement if the the birthday has not come yet
			if (currentDate.Month < dateOfBirth.Month ||
				(currentDate.Month == dateOfBirth.Month && currentDate.Day < dateOfBirth.Day))
			{
				age--;
			}

			return age;

		}
	}
}
