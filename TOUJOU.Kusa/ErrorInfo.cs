namespace TOUJOU.Kusa
{
	public class ErrorInfo
	{
		public string Message { get; protected set; }
		public string StackTrace { get; protected set; }

		public ErrorInfo()
		{
		}

		public ErrorInfo(string message, string stackTrace)
		{
			Message = message;
			StackTrace = stackTrace;
		}
	}
}
