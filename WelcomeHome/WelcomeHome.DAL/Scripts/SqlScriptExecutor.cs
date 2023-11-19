using Microsoft.Data.SqlClient;

namespace WelcomeHome.DAL.Scripts;

internal static class SqlScriptExecutor
{
	public static void Execute(string scriptPath, string connectionString)
	{
		var script = File.ReadAllText(scriptPath);

		using SqlConnection connection = new(connectionString);
		SqlCommand command = new(script, connection);
		connection.Open();
		command.ExecuteNonQuery();
	}
}
