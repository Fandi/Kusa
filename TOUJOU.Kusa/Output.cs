namespace TOUJOU.Kusa
{
	public class Output
	{
		public ErrorInfo ErrorInfo { get; protected set; }

		public Output()
		{
			ErrorInfo = new ErrorInfo();
		}

		public Output(string message, string stackTrace)
		{
			ErrorInfo = new ErrorInfo(message, stackTrace);
		}
	}
}
