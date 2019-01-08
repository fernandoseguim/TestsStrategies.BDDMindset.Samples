using Flunt.Notifications;

namespace TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Request
{
	public abstract class BaseCommand : Notifiable
	{
		public abstract bool IsValid();
	}
}
